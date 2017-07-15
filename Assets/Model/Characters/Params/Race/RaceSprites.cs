using System.Collections.Generic;

namespace Assets.Model.Characters.Params
{
    public class RaceSprites
    {
        public List<int> Dead { get; set; }
        public List<int> Face { get; set; }
        public List<int> Flinch { get; set; }
        public List<int> Head { get; set; }
        public List<int> HeadDeco1 { get; set; }
        public List<int> HeadDeco2 { get; set; }
        public List<int> TorsoDeco1 { get; set; }
        public List<int> TorsoDeco2 { get; set; }
        public List<int> Torso { get; set; }

        public RaceSprites()
        {
            this.Face = new List<int>();
            this.Flinch = new List<int>();
            this.Dead = new List<int>();
            this.Head = new List<int>();
            this.HeadDeco1 = new List<int>();
            this.HeadDeco2 = new List<int>();
            this.TorsoDeco1 = new List<int>();
            this.TorsoDeco2 = new List<int>();
            this.Torso = new List<int>();
        }
    }
}
