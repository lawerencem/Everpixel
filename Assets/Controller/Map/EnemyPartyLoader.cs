using Controller.Characters;
using Generics;
using Model.Biomes;
using Model.Characters;
using Model.Parties;
using System.Collections.Generic;
using View.Builders;

namespace Controller.Managers.Map
{
    public class EnemyPartyLoader : AbstractSingleton<EnemyPartyLoader>
    {
        private PartyBuilder _partyBuilder = new PartyBuilder();

        public List<CharacterParams> GetParty(string party)
        {
            return this._partyBuilder.Build(party);
        }
    }
}
