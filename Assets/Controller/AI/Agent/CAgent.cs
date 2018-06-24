using Assets.Controller.Character;
using Assets.Model.Event.Combat;

namespace Assets.Controller.AI.Agent
{
    public class CAgent
    {
        public void ProcessTurn(CChar agent)
        {
            this.EndTurn(agent);
        }

        private void EndTurn(CChar agent)
        {
            var e = new EvEndTurn();
            e.TryProcess();
        }
    }
}
