using Assets.Model.Party.Builder;

namespace Assets.Controller.Map.Combat.Loader
{
    public class PartyLoader
    {
        public void Init(MapController map, MapInitInfo info)
        {
            var builder = new PartyBuilder();
            foreach(var kvp in info.LParties)
            {
                map.GetLParties().Add(builder.Build(kvp));
                foreach (var party in map.GetLParties())
                {
                    foreach (var character in party.GetChars())
                    {
                        character.Model.SetLParty(true);
                    }
                }
            }
            foreach (var kvp in info.RParties)
            {
                map.GetRParties().Add(builder.Build(kvp));
                foreach (var party in map.GetRParties())
                {
                    foreach (var character in party.GetChars())
                    {
                        character.Model.SetLParty(false);
                    }
                }
            }
        }
    }
}
