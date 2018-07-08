using Assets.Controller.Character;
using Assets.Controller.Event.Combat;
using Assets.Controller.Map.Tile;
using Assets.Model.AI.Agent.Combat;
using Assets.Model.Event.Combat;
using System.Collections.Generic;

namespace Assets.Controller.AI.Agent
{
    public class CAgent
    {
        private const int CALLBACK_PRIORITY = 10;
        private const double WEIGHT_TO_ABILITY_SCALAR = 10000;

        private CChar _agent;
        private int _loops;
        private List<CTile> _prevTiles;

        public void ProcessTurn(CChar agent)
        {
            this._agent = agent;
            this._prevTiles = new List<CTile> { agent.Tile };
            this.DetermineNextAction(null);
        }

        private void DetermineNextAction(object o)
        {
            this._loops++;
            if (this._loops >= 5)
                this.EndTurn(null);
            else
            {
                var abilityCalc = new AgentAbilityCalc();
                var moveCalc = new AgentMoveParticleCalc();

                var tileAndWeight = moveCalc.GetMoveTile(this._agent);
                var abilityAndWeight = abilityCalc.GetAbilityToPerform(this._agent);
                if (abilityAndWeight != null)
                    abilityAndWeight.Weight *= WEIGHT_TO_ABILITY_SCALAR;

                if (abilityAndWeight == null && tileAndWeight.X == null)
                    this.EndTurn(null);
                else if (abilityAndWeight == null)
                    this.DoMove(tileAndWeight.X);
                else if (tileAndWeight.X == null)
                    this.DoAbility(abilityAndWeight);
                else
                {
                    if (abilityAndWeight.Weight > tileAndWeight.Y)
                        this.DoAbility(abilityAndWeight);
                    else
                        this.DoMove(tileAndWeight.X);
                }
            }
        }

        private void DoAbility(AgentAbilityData abilityData)
        {
            if (abilityData.Weight > 0)
            {
                var data = new EvPerformAbilityData();
                data.Ability = abilityData.Ability.Type;
                data.LWeapon = abilityData.LWeapon;
                data.ParentWeapon = abilityData.ParentWeapon;
                data.Source = this._agent;
                data.Target = abilityData.Target.Tile;
                data.WpnAbility = abilityData.WpnAbiltiy;
                var e = new EvPerformAbility(data);
                e.AddCallback(this.DetermineNextAction, CALLBACK_PRIORITY);
                e.TryProcess();
            }
            else
                this.EndTurn(null);
        }

        private void DoMove(CTile tile)
        {
            if (this._prevTiles.Contains(tile))
                this.EndTurn(null);
            else
            {
                this._prevTiles.Add(tile);
                var data = new EvPathMoveData();
                data.Char = this._agent;
                data.Target = tile;
                var path = new EvPathMoveUtil().GetPathMove(data);
                path.AddCallback(this.DetermineNextAction, CALLBACK_PRIORITY);
                path.TryProcess();
            }
        }

        private void EndTurn(object o)
        {
            var e = new EvEndTurn();
            e.TryProcess();
        }
    }
}
