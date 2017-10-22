using Assets.Model.Ability.Enum;
using Assets.Template.Other;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Model.Ability
{
    public class AbilityFactory : ASingleton<AbilityFactory>
    {
        public AbilityFactory() { }

        public List<MAbility> CreateNewObject(List<EAbility> abilities)
        {
            var abs = new List<MAbility>();
            foreach(var ability in abilities)
            {
                var proto = AbilityTable.Instance.Table[ability];
                abs.Add(proto.Copy());
            }
            return abs;
        }

        public MAbility CreateNewObject(EAbility ability)
        {
            try
            {
                var proto = AbilityTable.Instance.Table[ability];
                return proto.Copy();
            }
            catch(Exception e)
            {
                Debug.LogError(e);
                return null;
            }
        }
    }
}
