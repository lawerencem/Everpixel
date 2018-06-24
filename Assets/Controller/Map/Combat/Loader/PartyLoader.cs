using Assets.Model.Party.Builder;

namespace Assets.Controller.Map.Combat.Loader
{
    public class PartyLoader
    {
        public void Init(CMap map, MapInitInfo info)
        {
            var builder = new PartyBuilder();

            foreach (var partyInfo in info.LParties)
            {
                var party = builder.Build(partyInfo);
                party.SetAIControlled(partyInfo.AIControlled);
                map.GetLParties().Add(party);
            }

            foreach (var partyInfo in info.RParties)
            {
                var party = builder.Build(partyInfo);
                party.SetAIControlled(partyInfo.AIControlled);
                map.GetRParties().Add(party);
            }

            foreach (var party in map.GetLParties())
                foreach (var character in party.GetChars())
                    character.Proxy.SetLParty(true);
        }
    }
}
