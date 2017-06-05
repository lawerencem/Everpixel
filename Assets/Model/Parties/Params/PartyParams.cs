using Assets.Generics;
using Generics.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Parties
{
    public class PartyParams
    {
        public string Name { get; set; }
        public List<Pair<string, int>> SubParties = new List<Pair<string, int>>();

        public List<string> GetRandomSubPartyNames(int difficulty)
        {
            var names = new List<string>();
            int remaining = difficulty;
            while (remaining > 0)
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
