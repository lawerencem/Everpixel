using Assets.Model.Culture;

namespace Assets.Model.Party.Param
{
    public class PartyBuildParams
    {
        public bool AIControlled { get; set; }
        public ECulture Culture { get; set; }
        public string Name { get; set; }
        public double Difficulty { get; set; }
    }
}
