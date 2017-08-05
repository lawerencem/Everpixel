using Controller.Characters;
using Controller.Managers;
using Model.Characters;

namespace Model.Events.Combat
{
    public class CharacterKilledEvent : CombatEvent
    {
        public CharController Killed { get; set; }

        public CharacterKilledEvent(CombatEventManager parent,CharController killed) :
            base(CombatEventEnum.CharacterKilled, parent)
        {
            this.Killed = killed;
            this.RegisterEvent();
            CharacterStatusFlags.SetDeadTrue(this.Killed.Model.StatusFlags);
            var tgtTile = this.Killed.CurrentTile;
            tgtTile.DeadCharacters.Add(this.Killed);
            tgtTile.Model.Current = null;
        }
    }
}
