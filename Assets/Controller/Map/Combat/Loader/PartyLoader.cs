using Assets.Data.Party.Table;
using Assets.Model.Party.Builder;
using Assets.Template.Other;

namespace Assets.Controller.Map.Combat.Loader
{
    public class PartyLoader
    {
        public void Init(MMapController map, MapInitInfo info)
        {
            var builder = new PartyBuilder();
            foreach(var armyParam in info.LArmies)
            {
                var army = ArmyTable.Instance.Table[armyParam.X][armyParam.Y];
                foreach(var metaParams in army.Metaparties)
                {
                    var kvp = new Pair<string, int>(metaParams.X, metaParams.Y);
                    var meta = MetapartyTable.Instance.Table[armyParam.X][metaParams.X];
                    foreach(var party in meta.Parties)
                    {
                        var partyParams = new Pair<string, int>(party.X, (int)(party.Y * metaParams.Y));
                        map.GetLParties().Add(builder.Build(partyParams));
                    }
                }
            }

            foreach (var party in map.GetLParties())
                foreach (var character in party.GetChars())
                    character.Proxy.SetLParty(true);

            foreach (var armyParam in info.RArmies)
            {
                var army = ArmyTable.Instance.Table[armyParam.X][armyParam.Y];
                foreach (var metaParams in army.Metaparties)
                {
                    var kvp = new Pair<string, int>(metaParams.X, metaParams.Y);
                    var meta = MetapartyTable.Instance.Table[armyParam.X][metaParams.X];
                    foreach (var party in meta.Parties)
                    {
                        var partyParams = new Pair<string, int>(party.X, (int)(party.Y * metaParams.Y));
                        map.GetRParties().Add(builder.Build(partyParams));
                    }
                }
            }
        }
    }
}
