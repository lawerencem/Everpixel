﻿using Characters.Params;
using Controller.Characters;
using Controller.Managers;
using Model.Combat;

namespace Model.Events.Combat
{
    public class DebuffEvent : CombatEvent
    {
        private FlatSecondaryStatModifier _flatDebuff;
        private SecondaryStatModifier _debuff;

        public CharController ToDebuff { get; set; }
        public bool Resisted { get; set; }

        public DebuffEvent(CombatEventManager parent, FlatSecondaryStatModifier debuff, CharController toDebuff) :
            base(CombatEventEnum.Debuff, parent)
        {
            this.ToDebuff = toDebuff;
            this.Process();
        }

        public DebuffEvent(CombatEventManager parent, SecondaryStatModifier debuff, CharController toDebuff) :
            base(CombatEventEnum.Debuff, parent)
        {
            this.ToDebuff = toDebuff;
            this.Process();
        }

        private void Process()
        {
            // TODO:
            this.RegisterEvent();
        }
    }
}
