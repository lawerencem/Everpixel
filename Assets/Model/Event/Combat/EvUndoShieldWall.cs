using Assets.Controller.Character;
using Assets.Model.Action;
using Assets.Model.Character.Enum;
using Assets.View.Equipment;

namespace Assets.Model.Event.Combat
{
    public class EvUndoShieldWallData
    {
        public MAction Action { get; set; }
        public CChar Char { get; set; }
    }

    public class EvUndoShieldWall : MEvCombat
    {
        private EvUndoShieldWallData _data;

        public EvUndoShieldWall() : base(ECombatEv.UndoShieldWall) { }
        public EvUndoShieldWall(EvUndoShieldWallData d) : base(ECombatEv.UndoShieldWall) { this._data = d; }

        public void SetData(EvUndoShieldWallData data) { this._data = data; }

        public override void TryProcess()
        {
            base.TryProcess();
            if (this.VerifyAndPopulateData())
            {
                if (FActionStatus.HasFlag(this._data.Char.Proxy.GetActionFlags().CurFlags, FActionStatus.Flags.ShieldWalling))
                {
                    var util = new VWeaponUtil();
                    if (this._data.Action != null)
                    {
                        var action = this._data.Action;
                        util.UndoShieldWallFX(action.Data.Source, action.Data.ParentWeapon, action.Data.LWeapon);
                    }
                    if (this._data.Char.Proxy.GetLWeapon() != null)
                    {
                        var wpn = this._data.Char.Proxy.GetLWeapon();
                        if (wpn.View.ShieldWalling)
                            util.UndoShieldWallFX(this._data.Char, wpn, true);
                    }
                    if (this._data.Char.Proxy.GetRWeapon() != null)
                    {
                        var wpn = this._data.Char.Proxy.GetRWeapon();
                        if (wpn.View.ShieldWalling)
                            util.UndoSpearWallFX(this._data.Char, wpn, false);
                    }
                    FActionStatus.SetShieldWallingFalse(this._data.Char.Proxy.GetActionFlags());
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
