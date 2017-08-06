using Characters.Params;
using Controller.Characters;
using Controller.Managers;

namespace Model.Events.Combat
{
    public class BuffEvent : CombatEvent
    {
        public string BuffStr { get; set; }
        public CharController ToBuff { get; set; }

        public BuffEvent(CombatEventManager parent, FlatSecondaryStatModifier buff, CharController toBuff) :
            base(ECombatEv.Buff, parent)
        {
            this.BuffStr = buff.Type.ToString().Replace("_", " ");
            this.ToBuff = toBuff;
            toBuff.Model.TryAddMod(buff);
            this.RegisterEvent();
        }

        public BuffEvent(CombatEventManager parent, SecondaryStatMod buff, CharController toBuff) :
            base(ECombatEv.Buff, parent)
        {
            this.BuffStr = buff.Type.ToString().Replace("_", " ");
            this.ToBuff = toBuff;
            toBuff.Model.TryAddMod(buff);
            this.RegisterEvent();
        }
    }
}
