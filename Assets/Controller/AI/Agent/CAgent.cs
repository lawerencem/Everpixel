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
            var tile = moveCalc.GetMoveTile(agent);
            if (tile != null)
            {
                var data = new EvPathMoveData();
                data.Char = agent;
                data.Target = tile;
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
