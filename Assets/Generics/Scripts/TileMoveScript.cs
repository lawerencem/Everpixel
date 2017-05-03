using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Model.Events.Combat;
using Model.Map;
using System;
using UnityEngine;

namespace Generics.Scripts
{
    public class TileMoveScript : MonoBehaviour
    {
        private const float SPEED = 5f;

        public GenericCharacterController Character { get; set; }
        public Path Path { get; set; }
        public TileController Source { get; set; }
        public TileController Target { get; set; }

        public void Update()
        {
            float move = SPEED * Time.deltaTime;
            var newPosition = Vector3.Lerp(this.Character.transform.position, Target.transform.position, move);
            this.Character.transform.position = newPosition;
            if (Vector3.Distance(this.Character.transform.position, Target.transform.position) <= 0.02)
            {
                this.Character.CurrentTile.Model.Current = null;
                this.Character.CurrentTile = Target;
                this.Character.CurrentTile.Model.Current = this.Character;
                this.Character.transform.position = Target.transform.position;
                this.Character.Model.CurrentAP -= this.Target.Model.Cost;
                var next = this.Path.GetNextTile(this.Target);
                if (next != null)
                {
                    if (this.Character.Model.CurrentAP >= next.Model.Cost)
                    {
                        var nextTilEvent = new TraverseTileEvent(CombatEventManager.Instance, this.Path, this.Character.CurrentTile, next);
                    }
                    else
                    {
                        var traversed = new PathTraversedEvent(CombatEventManager.Instance, this.Character);
                    }
                }
                else
                {
                    var traversed = new PathTraversedEvent(CombatEventManager.Instance, this.Character);
                }
                Destroy(this);
            }
        }

        public void Init(GenericCharacterController c, Path p, TileController s, TileController t)
        {
            if (c.Model.CurrentAP >= t.Model.Cost)
            {
                this.Character = c;
                this.Path = p;
                this.Source = s;
                this.Target = t;
            }
            else
            {
                var traversed = new PathTraversedEvent(CombatEventManager.Instance, this.Character);
                Destroy(this);
            }
        }
    }
}
