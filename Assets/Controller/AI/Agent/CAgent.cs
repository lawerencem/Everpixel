using Assets.Controller.Character;
using Assets.Controller.Event.Combat;
using Assets.Model.AI.Agent.Combat;
using Assets.Model.Event.Combat;

namespace Assets.Controller.AI.Agent
{
    public class CAgent
    {
        public void ProcessTurn(CChar agent)
        {
            var moveCalc = new AgentMoveParticleCalc();
            var tileAndWeight = moveCalc.GetMoveTile(agent);
            if (tileAndWeight.X != null)
            {
                var data = new EvPathMoveData();
                data.Char = agent;
                data.Target = tileAndWeight.X;
                var path = new EvPathMoveUtil().GetPathMove(data);
                path.AddCallback(this.EndTurn);
                path.TryProcess();
            }
            else
                this.EndTurn(null);
        }

        private void EndTurn(object o)
        {
            var e = new EvEndTurn();
            e.TryProcess();
        }
    }
}
