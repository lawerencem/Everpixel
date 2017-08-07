﻿//using Controller.Characters;
//using Controller.Managers;
//using Controller.Map;
//using Model.Events.Combat;
//using Model.Map;
//using System;
//using UnityEngine;

//namespace Generics.Scripts
//{
//    public class TileMoveScript : MonoBehaviour
//    {
//        private const float SPEED = 8f;

//        public CharController Character { get; set; }
//        public Path Path { get; set; }
//        public TileController Source { get; set; }
//        public TileController Target { get; set; }

//        public void Update()
//        {
//            float move = SPEED * Time.deltaTime;
//            var newPosition = Vector3.Lerp(this.Character.transform.position, Target.transform.position, move);
//            this.Character.transform.position = newPosition;
//            if (Vector3.Distance(this.Character.transform.position, Target.transform.position) <= 0.02)
//            {
//                foreach(var zone in Target.Zones)
//                {
//                    var enter = new EvZoneEnter(CombatEventManager.Instance, Character, zone);
//                }
//                this.Character.CurrentTile.Model.Current = null;
//                this.Character.CurrentTile = Target;
//                this.Character.CurrentTile.Model.Current = this.Character;
//                this.Character.transform.position = Target.transform.position;
//                // TODO: Get cost via interface 
//                var next = this.Path.GetNextTile(this.Target);
//                if (next != null)
//                {
//                    if (this.Character.Model.GetCurrentAP() >= this.Character.Model.GetTileTraversalAPCost(next.Model))
//                    {
//                        var nextTilEvent = new EvTraverseTile(CombatEventManager.Instance, this.Path, this.Character.CurrentTile, next);
//                    }
//                    else
//                    {
//                        var traversed = new EvPathTraversed(CombatEventManager.Instance, this.Character);
//                        Destroy(this);
//                    }
//                }
//                else
//                {
//                    var traversed = new EvPathTraversed(CombatEventManager.Instance, this.Character);
//                }
//                Destroy(this);
//            }
//        }

//        public void Init(CharController c, Path p, TileController s, TileController t)
//        {
//            this.Character = c;
//            this.Path = p;
//            this.Source = s;
//            this.Target = t;

//            if (c.Model.GetCurrentAP() < c.Model.GetTileTraversalAPCost(t.Model))
//            {
//                var traversed = new EvPathTraversed(CombatEventManager.Instance, this.Character);
//                Destroy(this);
//            }
//            else
//            {
//                var curAP = this.Character.Model.GetCurrentAP();
//                curAP -= this.Character.Model.GetTileTraversalAPCost(this.Target.Model);
//                this.Character.Model.SetCurrentAP(curAP);
//            }
//        }
//    }
//}
