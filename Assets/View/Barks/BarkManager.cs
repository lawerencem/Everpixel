using Controller.Managers;
using Controller.Managers.Map;
using Controller.Map;
using Generics.Utilities;
using Model.Events.Combat;

namespace View.Barks
{
    public class BarkManager
    {
        private BarkManager() {}

        private static BarkManager _instance;
        public static BarkManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BarkManager();
                return _instance;
            }
        }

        public void ProcessFatalityBark(DisplayActionEvent e)
        {
            bool ownTeam = false;
            foreach(var hit in e.FatalityHits)
            {
                if (hit.Target != null && e.EventController.Source.LParty == hit.Target.LParty)
                {
                    ownTeam = true;
                    break;
                }
            }
            this.RandomFatalityBark(e, ownTeam);
        }

        private void EnemyFatalityBark(DisplayActionEvent e)
        {
            var barks = BarkTable.Instance.Table[BarkCategoryEnum.EnemyFatality];
            var bark = ListUtil<string>.GetRandomListElement(barks);
            CMapGUIController.Instance.DisplayText(bark, e.EventController.Source.Handle, CMapGUIControllerParams.WHITE);
        }

        private void ProcessEnemyTeamFatalityBark(DisplayActionEvent e)
        {
            var roll = RNG.Instance.Next(2);
            if (roll == 0)
                this.NeutralFataliyBark(e);
            else
                this.EnemyFatalityBark(e);
        }

        private void NeutralFataliyBark(DisplayActionEvent e)
        {
            var barks = BarkTable.Instance.Table[BarkCategoryEnum.NeutralFatality];
            var bark = ListUtil<string>.GetRandomListElement(barks);
            var character = CombatEventManager.Instance.GetRandomCharacter();
            CMapGUIController.Instance.DisplayText(bark, character.Handle, CMapGUIControllerParams.WHITE);
        }

        private void OwnTeamFatalityBark(DisplayActionEvent e)
        {
            var barks = BarkTable.Instance.Table[BarkCategoryEnum.FriendlyFatality];
            var bark = ListUtil<string>.GetRandomListElement(barks);
            CMapGUIController.Instance.DisplayText(bark, e.EventController.Source.Handle, CMapGUIControllerParams.WHITE);
        }

        private void RandomFatalityBark(DisplayActionEvent e, bool ownTeam)
        {
            if (ownTeam)
                this.OwnTeamFatalityBark(e);
            else
                this.ProcessEnemyTeamFatalityBark(e);
        }        
    }
}
