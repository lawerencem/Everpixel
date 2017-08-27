using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;

namespace Assets.Model.Event.Combat
{
    public class EvModHPData
    {
        public int Dmg { get; set; }
        public bool IsHeal { get; set; }
        public CharController Target { get; set; }
    }

    public class EvModHP : MEvCombat
    {
        private EvModHPData _data;

        public EvModHP() : base(ECombatEv.ModifyHP) { }
        public EvModHP(EvModHPData data) : base(ECombatEv.ModifyHP) { this._data = data; }

        public override void TryProcess()
        {
            base.TryProcess();
            if (this.IsInitialized())
            {
                if (this._data.Target != null)
                    this._data.Target.Model.ModifyHP(this._data.Dmg, this._data.IsHeal);
                if (!this._data.IsHeal)
                {
                    VCombatController.Instance.DisplayText(
                        this._data.Dmg.ToString(), this._data.Target.Handle,
                        CombatGUIParams.RED,
                        CombatGUIParams.DMG_TEXT_OFFSET);
                }
                else
                {
                    VCombatController.Instance.DisplayText(
                        this._data.Dmg.ToString(),
                        this._data.Target.Handle,
                        CombatGUIParams.GREEN,
                        CombatGUIParams.DMG_TEXT_OFFSET);
                }
            }
        }

        private bool IsInitialized()
        {
            if (this._data.Target == null)
                return false;
            return true;
        }
    }
}
