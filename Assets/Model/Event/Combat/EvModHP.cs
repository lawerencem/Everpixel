using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Model.Combat.Hit;
using Assets.View.Event;

namespace Assets.Model.Event.Combat
{
    public class EvModHPData
    {
        public int Dmg { get; set; }
        public FHit Flags { get; set; }
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
                    this.DisplayDmg();
                    this.DisplaySplatter();
                }
                else
                    this.DisplayHeal();
            }
        }

        private void DisplayDmg()
        {
            if (!FHit.HasFlag(this._data.Flags.CurFlags, FHit.Flags.Dodge))
            {
                VCombatController.Instance.DisplayText(
                    this._data.Dmg.ToString(), this._data.Target.Handle,
                    CombatGUIParams.RED,
                    CombatGUIParams.DMG_TEXT_OFFSET);
            }
        }

        private void DisplayHeal()
        {
            VCombatController.Instance.DisplayText(
                this._data.Dmg.ToString(),
                this._data.Target.Handle,
                CombatGUIParams.GREEN,
                CombatGUIParams.DMG_TEXT_OFFSET);
        }

        private void DisplaySplatter()
        {
            if (this._data.Dmg > 0)
            {
                var data = new EvSplatterData();
                data.DmgPercent =
                    (this._data.Dmg /
                    this._data.Target.Model.GetCurrentStats().GetSecondaryStats().MaxHP);
                data.Target = this._data.Target.Tile.Handle;
                var e = new EvSplatter(data);
                e.TryProcess();
            }
        }

        private bool IsInitialized()
        {
            if (this._data.Target == null || this._data.Flags == null)
                return false;
            return true;
        }
    }
}
