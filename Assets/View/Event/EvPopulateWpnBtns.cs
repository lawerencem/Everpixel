using Assets.Controller.Manager.Combat;
using Assets.Controller.Manager.GUI;
using Assets.Model.Ability;
using Assets.Model.Character.Enum;
using Assets.Template.Other;
using Assets.View.Script.GUI;
using System.Collections.Generic;

namespace Assets.View.Event
{
    public class EvPopulateWpnBtns : MGuiEv
    {
        public EvPopulateWpnBtns() : base(EGuiEv.PopulateWpnBtns) {  }

        public override void TryProcess()
        {
            base.TryProcess();
            var curr = CombatManager.Instance.GetCurrentlyActing();
            if (curr.Model.Type == ECharType.Humanoid)
            {
                var abilities = new List<Pair<MAbility, bool>>();
                var left = curr.Model.GetEquipment().GetLWeapon();
                var right = curr.Model.GetEquipment().GetRWeapon();

                if (left != null)
                    foreach (var ability in left.Abilities)
                        abilities.Add(new Pair<MAbility, bool>(ability, false));
                if (right != null)
                    foreach (var ability in right.Abilities)
                        abilities.Add(new Pair<MAbility, bool>(ability, true));

                this.ProcessAbilities(abilities);
            }
        }

        private void ProcessAbilities(List<Pair<MAbility, bool>> abilities)
        {
            for(int i = 0; i < 7; i++)
            {
                var tag = "WpnBtnTag" + i;
                if (i >= abilities.Count)
                    GUIManager.Instance.SetComponentActive(tag, false);
                else
                {
                    GUIManager.Instance.SetComponentActive(tag, true);
                    var btn = GUIManager.Instance.GetComponent(tag);
                    if (btn != null)
                    {
                        var script = btn.GetComponent<SWpnBtn>();
                        script.SetAbility(abilities[i].X.Type, !abilities[i].Y);
                    }
                }
            }
        }
    }
}

