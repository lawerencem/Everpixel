namespace Model.Abilities.Music
{
    public class GenericSong : GenericActiveAbility
    {
        public enum SongTypeEnum
        {
            None,
            BlackMusic,
            BlueMusic,
            RedMusic,
        }

        protected SongTypeEnum _songType;
        public SongTypeEnum SongType { get { return this._songType; } }

        public GenericSong(ActiveAbilitiesEnum type) : base(type)
        {
            
        }
    }
}
