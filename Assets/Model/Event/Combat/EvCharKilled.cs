//using Controller.Characters;
//using Controller.Managers;
//using Model.Characters;

//namespace Assets.Model.Event.Combat
//{
//    public class EvCharKilled : MCombatEv
//    {
//        public CharController Killed { get; set; }

//        public EvCharKilled(CombatEventManager parent,CharController killed) :
//            base(ECombatEv.CharacterKilled, parent)
//        {
//            this.Killed = killed;
//            this.RegisterEvent();
//            FCharacterStatus.SetDeadTrue(this.Killed.Model.StatusFlags);
//            var tgtTile = this.Killed.CurrentTile;
//            tgtTile.DeadCharacters.Add(this.Killed);
//            tgtTile.Model.Current = null;
//        }
//    }
//}
