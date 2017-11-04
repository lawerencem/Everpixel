using Assets.Template.Other;
using Assets.Template.Util;
using System.Collections.Generic;

namespace Assets.Model.Party.Param
{
    public class PartyParams
    {
        public string Name { get; set; }
        public List<Pair<string, int>> SubParties = new List<Pair<string, int>>();

        public List<string> GetRandomSubPartyNames(int difficulty)
        {
            var names = new List<string>();
            int remaining = difficulty;
            for(int i = 0; i < 10; i++)
            {
                int index = RNG.Instance.Next(this.SubParties.Count);
                if (remaining >= this.SubParties[index].Y)
                {
                    names.Add(this.SubParties[index].X + "_" + this.SubParties[index].Y);
                    remaining -= this.SubParties[index].Y;
                }
            }
            return names;
        }
    }
}
