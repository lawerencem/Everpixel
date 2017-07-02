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
        }        
    }
}
