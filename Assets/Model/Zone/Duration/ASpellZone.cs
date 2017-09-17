using Assets.Controller.Character;

namespace Assets.Model.Zone.Duration
{
    public abstract class ASpellZone : ADurationZone
    {
        protected CChar _caster;

        public ASpellZone(ZoneArgsCont arg) : base(arg) { this._caster = arg.Caster; }
    }
}
