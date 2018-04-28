using Assets.Controller.Character;
using Assets.Model.Action;
using Assets.Model.Character.Enum;
using Assets.Model.Zone;
using Assets.View.Equipment;

namespace Assets.Model.Event.Combat
{
    public class EvUndoSpearwallData
    {
        public MAction Action { get; set; }
        public CChar Char { get; set; }
    }

    public class EvUndoSpearwall : MEvCombat
    {
        private EvUndoSpearwallData _data;

        public EvUndoSpearwall() : base(ECombatEv.UndoSpearWall) { }
        public EvUndoSpearwall(EvUndoSpearwallData d) : base(ECombatEv.UndoSpearWall) { this._data = d; }

        public void SetData(EvUndoSpearwallData data) { this._data = data; }

        public override void TryProcess()
        {
            base.TryProcess();
            if (this.VerifyAndPopulateData())
            {
                if (FActionStatus.HasFlag(this._data.Char.Proxy.GetActionFlags().CurFlags, FActionStatus.Flags.Spearwalling))
                {
                    var util = new VWeaponUtil();
                    if (this._data.Action != null)
                        util.UndoSpearWallFX(this._data.Action);
                    if (this._data.Char.Proxy.GetLWeapon() != null)
                    {
                        var wpn = this._data.Char.Proxy.GetLWeapon();
                        if (wpn.View.SpearWalling)
                            util.UndoSpearWallFX(this._data.Char, wpn, true);
                    }
                    if (this._data.Char.Proxy.GetRWeapon() != null)
                    {
                        var wpn = this._data.Char.Proxy.GetRWeapon();
                        if (wpn.View.SpearWalling)
                            util.UndoSpearWallFX(this._data.Char, wpn, false);
                    }
                    var zones = this._data.Char.Proxy.GetZones().FindAll(x => x.Type == EZone.Spear_Wall_Zone);
                    foreach (var zone in zones)
                        zone.RemoveFromParentAndSource();
                    FActionStatus.SetSpearwallingFalse(this._data.Char.Proxy.GetActionFlags());
                }
            }
        }

        private bool VerifyAndPopulateData()
        {
            if (this._data == null)
                return false;
            if (this._data.Char == null)
                return false;
            return true;
        }
    }
}
