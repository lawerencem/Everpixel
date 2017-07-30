﻿using Characters.Params;
using Controller.Characters;
using Controller.Managers;

namespace Model.Events.Combat
{
    public class BuffEvent : CombatEvent
    {
        public string BuffStr { get; set; }
        public GenericCharacterController ToBuff { get; set; }

        public BuffEvent(CombatEventManager parent, FlatSecondaryStatModifier buff, GenericCharacterController toBuff) :
            base(CombatEventEnum.Buff, parent)
        {
            this.BuffStr = buff.Type.ToString().Replace("_", " ");
            this.ToBuff = toBuff;
            toBuff.Model.TryAddMod(buff);
            this.RegisterEvent();
        }

        public BuffEvent(CombatEventManager parent, SecondaryStatModifier buff, GenericCharacterController toBuff) :
            base(CombatEventEnum.Buff, parent)
        {
            this.BuffStr = buff.Type.ToString().Replace("_", " ");
            this.ToBuff = toBuff;
            toBuff.Model.TryAddMod(buff);
            this.RegisterEvent();
        }
    }
}
