using Assets.Controller.Character;
using Assets.Model.Ability.Enum;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Model.Event.Combat;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Model.Ability.Music
{
    public class WeenlightSonata : MSong
    {
        private const string PARTICLES = "Dark";
        private const string GHOST_WEEN = "Ghost_Ween";

        public WeenlightSonata() : base(EAbility.Weenlight_Sonata)
        {
            this._songType = ESong.Black_Music;
        }

        public override void Predict(MHit hit)
        {
            
        }

        public override void Process(MHit hit)
        {
            base.Process(hit);
            var tiles = this.GetSelfCenteredTiles(hit);
            var deadWeens = new List<CChar>();
            foreach (var tile in tiles)
            {
                var tileWeens = new List<CChar>();
                foreach (var nonCurrent in tile.GetNonCurrent())
                {
                    if (nonCurrent.Proxy.GetParams().Name.Equals("Ween"))
                    {
                        tileWeens.Add(nonCurrent);
                    }
                }
                foreach (var ween in tileWeens)
                {
                    tile.GetNonCurrent().Remove(ween);
                    deadWeens.Add(ween);
                }
            }
            this.ProcessDeadWeens(deadWeens, hit);
        }

        public override bool IsValidActionEvent(AbilityArgs arg)
        {
            return true;
        }

        private void ProcessDeadWeens(List<CChar> deadweens, MHit hit)
        {
            foreach(var ween in deadweens)
            {
                foreach(var sub in ween.SubComponents)
                {
                    GameObject.Destroy(sub.Value);
                }
                ween.SubComponents.Clear();

                var data = new EvSummonData();
                data.Duration = (int)this.Data.Duration;
                data.LParty = hit.Data.Source.Proxy.LParty;
                data.ParticlePath = PARTICLES;
                data.Party = hit.Data.Source.Proxy.GetParentParty();
                data.TargetTile = ween.Tile.GetNearestEmptyTile();
                data.ToSummon = GHOST_WEEN;
                var e = new EvSummon(data);
                e.TryProcess();
            }

        }
    }
}
