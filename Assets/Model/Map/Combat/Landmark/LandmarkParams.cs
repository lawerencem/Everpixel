namespace Assets.Model.Map.Combat.Landmark
{
    public class LandmarkParams
    {
        private ELandmark _type;

        public int Radius { get; set; }
        public int Length { get; set; }

        public LandmarkParams(ELandmark type)
        {
            this._type = type;
        }
    }
}
