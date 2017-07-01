namespace Model.Abilities.Music
{
    public class GenericSong : GenericAbility
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

        public GenericSong(AbilitiesEnum type) : base(type)
        {
            
        }
    }
}
