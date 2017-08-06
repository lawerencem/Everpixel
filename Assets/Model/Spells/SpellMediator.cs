using Assets.Generics;
using Generics;
using Model.Abilities;
using Model.Characters;
using System.Collections.Generic;

namespace Model.Spells
{
    public class SpellMediator : AbstractSingleton<SpellMediator>
    {
        public void SetCharacterSpells(MChar c, CharacterParams cParams)
        {
            foreach(var kvp in cParams.Spells)
            {
                var spell = SpellFactory.Instance.CreateNewObject(kvp.Y);
                int lvl = spell.SpellLevel;
                if (!c.ActiveSpells.Spells.ContainsKey(lvl))
                    c.ActiveSpells.Spells.Add(lvl, new Dictionary<EAbility, Pair<int, Ability>>());
                if (!c.ActiveSpells.Spells[lvl].ContainsKey(kvp.Y))
                    c.ActiveSpells.Spells[lvl].Add(kvp.Y, new Pair<int, Ability>(kvp.X, spell));
            }
        }
    }
}