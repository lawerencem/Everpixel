namespace Assets.Model.Map.Landmark
{
    public class LandmarkParams
    {
        private ELandmark _type;

        public int Height { get; set; }
        public int RadiusMax { get; set; }
        public int RadiusMin { get; set; }
        public int LengthMax { get; set; }
        public int LengthMin { get; set; }

        public LandmarkParams(ELandmark type)
        {
            this._type = type;
        }
    }
}
