using Assets.Controller.Character;
using Assets.Controller.Equipment.Weapon;
using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Action;
using Assets.Model.Map.Tile;
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

        public AgentAbilityData GetAbilityToPerform(CChar agent)
        {
            this._agent = agent;
            this._abilityWeights = this.GetInitialAbilityWeights();
            this.PredictAbilities();
            foreach (var prediction in this._predictions)
            {
                if (prediction.Ability.Data.ProcessDamage)
                {
                    if (this._abilityWeights.ContainsKey(prediction.Ability.Type))
                        prediction.Weight *= this._abilityWeights[prediction.Ability.Type];
                }
                else
                {
                    if (this._abilityWeights.ContainsKey(prediction.Ability.Type))
                        prediction.Weight = this._abilityWeights[prediction.Ability.Type];
                }
            }
            this._predictions.Sort((x, y) => y.Weight.CompareTo(x.Weight));
            if (this._predictions.Count > 0)
                return this._predictions[0];
            else
                return null;
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
                        this.PredictAbilitiesHelper(tile, ability, false, null, false);
                }
            }
            var leftWpn = this._agent.Proxy.GetLWeapon();
            var rightWpn = this._agent.Proxy.GetRWeapon();
            if (leftWpn != null)
            {
                foreach (var ability in leftWpn.Model.Data.Abilities)
                {
                    var tiles = this._agent.Tile.Model.GetAoETiles(ability.Data.Range);
                    foreach (var tile in tiles)
                    {
                        if (tile.GetCurrentOccupant() != null && tile.GetCurrentOccupant().GetType().Equals(typeof(CChar)))
                            this.PredictAbilitiesHelper(tile, ability, true, leftWpn, true);
                    }
                }
            }
            if (rightWpn != null)
            {
                foreach (var ability in rightWpn.Model.Data.Abilities)
                {
                    var tiles = this._agent.Tile.Model.GetAoETiles(ability.Data.Range);
                    foreach (var tile in tiles)
                    {
                        if (tile.GetCurrentOccupant() != null && tile.GetCurrentOccupant().GetType().Equals(typeof(CChar)))
                            this.PredictAbilitiesHelper(tile, ability, false, rightWpn, true);
                    }
                }
            }
        }

        private void PredictAbilitiesHelper(MTile tile, MAbility ability, bool lWeapon, CWeapon parentWeapon, bool wpnAbility)
        {
            var target = tile.GetCurrentOccupant() as CChar;
            var prediction = new AgentAbilityData();
            prediction.Ability = ability;
            prediction.LWeapon = lWeapon;
            prediction.ParentWeapon = parentWeapon;
            prediction.Target = target;
            prediction.Weight = 0;
            prediction.WpnAbiltiy = wpnAbility;
            if (ability.Data.Hostile)
            {
                if (target.Proxy.LParty != this._agent.Proxy.LParty)
                {
                    this.HandleAbility(prediction);
                    this._predictions.Add(prediction);
                }
            }
            else
            {
                if (target.Proxy.LParty == this._agent.Proxy.LParty)
                {
                    this.HandleAbility(prediction);
                    this._predictions.Add(prediction);
                }
            }
        }

        private void HandleAbility(AgentAbilityData prediction)
        {
            var data = new ActionData();
            data.Ability = prediction.Ability.Type;
            data.WpnAbility = prediction.WpnAbiltiy;
            data.LWeapon = prediction.LWeapon;
            data.ParentWeapon = null;
            data.Source = this._agent;
            data.Target = prediction.Target.Tile;
            var action = new MAction(data);
            action.TryPredict();
            foreach (var hit in action.Data.Hits)
            {
                if (prediction.Ability.Data.Hostile)
                {
                    if (prediction.Target.Proxy.LParty != this._agent.Proxy.LParty)
                        prediction.Weight += hit.Data.Dmg;
                    else
                        prediction.Weight -= hit.Data.Dmg;
                }
                else
                {
                    if (prediction.Target.Proxy.LParty != this._agent.Proxy.LParty)
                        prediction.Weight -= hit.Data.Dmg;
                    else
                        prediction.Weight += hit.Data.Dmg;
                }
            }       
        }
    }
}
