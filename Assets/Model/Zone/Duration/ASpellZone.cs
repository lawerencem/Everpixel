using Controller.Characters;
using Controller.Map;
using UnityEngine;

namespace Assets.Model.Zone.Duration
{
    public abstract class ASpellZone : ADurationZone
    {
        protected GenericCharacterController _caster;

        public ASpellZone(int dur, GenericCharacterController caster, GameObject handle, TileController tile) 
            : base(dur, handle, tile)
        {
            this._caster = caster;
        }
    }
}
