using Assets.Model.Party.Builder;

namespace Assets.Controller.Map.Combat.Loader
{
    public class PartyLoader
    {
        public void Init(CMap map, MapInitInfo info)
        {
            var builder = new PartyBuilder();
            
            foreach(var party in info.LParties)
                map.GetLParties().Add(builder.Build(party));

            foreach (var party in info.RParties)
                map.GetRParties().Add(builder.Build(party));

            foreach (var party in map.GetLParties())
                foreach (var character in party.GetChars())
                    character.Proxy.SetLParty(true);
        }
    }
}
