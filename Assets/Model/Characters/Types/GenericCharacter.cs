using Model.Classes;
using Model.Equipment;
using System.Collections.Generic;

namespace Model.Characters
{
    public class GenericCharacter : AbstractCharacter<CharacterTypeEnum>
    {
        public Dictionary<ClassEnum, GenericClass> BaseClasses { get; set; }
        public int CurrentAP { get; set; }
        public int CurrentHP { get; set; }
        public int CurrentMorale { get; set; }
        public int CurrentStamina { get; set; }
        public GenericWeapon LWeapon { get; set; }
        public GenericWeapon RWeapon { get; set; }

        public GenericCharacter()
        {
            this.BaseClasses = new Dictionary<ClassEnum, GenericClass>();
        }
    }
}
