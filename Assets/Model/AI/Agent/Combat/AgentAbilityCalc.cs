using Assets.Controller.Character;
using Assets.Controller.Equipment.Weapon;
using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Action;
using Assets.Model.Combat.Hit;
using Assets.Model.Map.Tile;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.AI.Agent.Combat
{
    public class AgentAbilityData
    {
        public MAbility Ability { get; set; }
        public bool LWeapon { get; set; }
        public CWeapon ParentWeapon { get; set; }
        public double Weight { get; set; }
        public bool WpnAbiltiy { get; set; }
        public CChar Target { get; set; }
    }

    public class AgentAbilityCalc
    {
        private Dictionary<EAbility, double> _abilityWeights;
        private CChar _agent;
        private List<AgentAbilityData> _predictions;

        public AgentAbilityCalc()
        {
            this._predictions = new List<AgentAbilityData>();
        }

        public Pair<AgentAbilityData, double> GetAbilityToPerform(CChar agent)
        {
            this._agent = agent;
            this._abilityWeights = this.GetInitialAbilityWeights();
            AgentAbilityData data = null;
            var weight = 0;
            this.PredictAbilities();
            this._predictions.Sort((x, y) => y.Weight.CompareTo(x.Weight));
            if (this._predictions.Count > 0)
            {
                data = new AgentAbilityData();
                data.Ability = this._predictions[0].Ability;
                data.LWeapon = this._predictions[0].LWeapon;
                data.ParentWeapon = this._predictions[0].ParentWeapon;
                data.Weight = this._predictions[0].Weight;
                data.WpnAbiltiy = this._predictions[0].WpnAbiltiy;
            }

            return new Pair<AgentAbilityData, double>(data, weight);
        }

        private Dictionary<EAbility, double> GetInitialAbilityWeights()
        {
            var role = new AgentRoleFactory().GetAgentRole(this._agent.Proxy.GetAIRole());
            return role.GetAbilityWeights(this._agent);
        }

        private void PredictAbilities()
        {
            foreach (var ability in this._agent.Proxy.GetActiveAbilities())
            {
                var tiles = this._agent.Tile.Model.GetAoETiles(ability.Data.Range);
                foreach (var tile in tiles)
                {
                    if (tile.GetCurrentOccupant() != null && tile.GetCurrentOccupant().GetType().Equals(typeof(CChar)))
                    {
                        var target = tile.GetCurrentOccupant() as CChar;
                        this.HandleAbility(ability, tile, target, false, false);
                    }
                }
            }
        }

        private void HandleAbility(MAbility ability, MTile tile, CChar target, bool wpnAbility, bool lWeapon)
        {
            double weight = 0;
            if (ability.Data.Hostile)
            {
                if (target.Proxy.LParty != this._agent.Proxy.LParty)
                {
                    var data = new ActionData();
                    data.Ability = ability.Type;
                    data.WpnAbility = wpnAbility;
                    data.LWeapon = lWeapon;
                    data.ParentWeapon = null;
                    data.Source = this._agent;
                    data.Target = target.Tile;
                    var action = new MAction(data);
                    action.TryPredict();
                    foreach (var hit in action.Data.Hits)
                        weight += this.CalculateHitWeight(hit);
                }
            }
            else
            {
                
            }
        }

        private double CalculateHitWeight(MHit hit)
        {
            return hit.Data.Dmg;
        }
    }
}
