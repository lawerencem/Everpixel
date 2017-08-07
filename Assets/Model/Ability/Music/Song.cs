﻿using Assets.Model.Ability.Enum;

namespace Assets.Model.Ability.Music
{
    public class Song : MAbility
    {
        public enum EnumSong
        {
            None,
            BlackMusic,
            BlueMusic,
            RedMusic,
        }

        protected EnumSong _songType;
        public EnumSong SongType { get { return this._songType; } }

        public Song(EAbility type) : base(type) { }
    }
}