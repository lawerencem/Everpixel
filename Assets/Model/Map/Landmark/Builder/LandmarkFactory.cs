using Assets.Controller.Map.Tile;

namespace Assets.Model.Map.Landmark.Builder
{
    public class LandmarkFactory
    {
        public void BuildLandmark(ELandmark lm, CTile tile)
        {
            switch(lm)
            {
                case (ELandmark.Flat_Hill): { new FlatHillBuilder().BuildLandmark(tile); } break;
                case (ELandmark.Pond): { new PondBuilder().BuildLandmark(tile); } break;
                case (ELandmark.Ridge): { new RidgeBuilder().BuildLandmark(tile); } break;
            }
        }
    }
}
