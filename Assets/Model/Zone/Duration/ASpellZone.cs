using Controller.Characters;
using Controller.Map;
using UnityEngine;

namespace Assets.Model.Zone.Duration
{
    public abstract class ASpellZone : ADurationZone
    {
        protected CharController _caster;

        public ASpellZone(ZoneArgsContainer arg) : base(arg) { this._caster = arg.Caster; }
    }
}
