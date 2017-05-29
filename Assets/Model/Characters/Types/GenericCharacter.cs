using Assets.Generics;
using Characters.Params;
using Model.Classes;
using Model.Equipment;
using System.Collections.Generic;

namespace Model.Characters
{
    public class GenericCharacter : AbstractCharacter<CharacterTypeEnum>
    {
        public GenericCharacter()
        {
            this.BaseClasses = new Dictionary<ClassEnum, GenericClass>();
            this.PStatMods = new List<PrimaryStatModifier>();
            this.SStatMods = new List<SecondaryStatModifier>();
            this.IndefSStatMods = new List<Pair<object, List<IndefSecondaryStatModifier>>>();
        }
    }
}
