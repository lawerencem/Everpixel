using Assets.Controller.Character;
using Assets.Model.Action;
using Assets.Model.Character.Enum;
using Assets.View.Equipment;

namespace Assets.Model.Event.Combat
{
    public class EvUndoRiposteData
    {
        public MAction Action { get; set; }
        public CChar Char { get; set; }
    }

    public class EvUndoRiposte : MEvCombat
    {
        private EvUndoRiposteData _data;

        public EvUndoRiposte() : base(ECombatEv.UndoRiposte) { }
        public EvUndoRiposte(EvUndoRiposteData d) : base(ECombatEv.UndoRiposte) { this._data = d; }

        public void SetData(EvUndoRiposteData data) { this._data = data; }

        public override void TryProcess()
        {
            base.TryProcess();
            if (this.VerifyAndPopulateData())
            {
                if (FActionStatus.HasFlag(this._data.Char.Proxy.GetActionFlags().CurFlags, FActionStatus.Flags.Riposting))
                {
                    var util = new VWeaponUtil();
                    if (this._data.Char.Proxy.GetLWeapon() != null)
                    {
                        var wpn = this._data.Char.Proxy.GetLWeapon();
                        if (wpn.View.Riposting)
                            util.UndoRiposte(this._data.Char, wpn, true);
                    }
                    if (this._data.Char.Proxy.GetRWeapon() != null)
                    {
                        var wpn = this._data.Char.Proxy.GetRWeapon();
                        if (wpn.View.Riposting)
                            util.UndoRiposte(this._data.Char, wpn, false);
                    }
                    FActionStatus.SetRipostingFalse(this._data.Char.Proxy.GetActionFlags());
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
