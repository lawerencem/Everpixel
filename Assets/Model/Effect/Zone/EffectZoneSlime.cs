using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Model.Effect.Fortitude;
using Assets.Model.Zone.Duration;

namespace Assets.Model.Effect.Zone
{
    namespace Assets.Model.Effect.Zone
    {
        public class EffectZoneSlime : MEffect
        {
            public EffectZoneSlime() : base(EEffect.Slime_Zone) { }

            public override void TryProcessHit(MHit hit, bool prediction)
            {
                base.TryProcessHit(hit, prediction);
                if (base.CheckConditions(hit))
                {
                    var zoneData = base.GetDurationZoneData(hit);
                    zoneData.Dur = (int)this.Data.X;
                    zoneData.Effect = this.GetSlimeEffect();
                    zoneData.Resist = this.Data.Resist;
                    zoneData.ResistBase = hit.Data.Source.Proxy.GetStat(ESecondaryStat.Spell_Penetration);
                    var zone = new SlimeZone();
                    zone.SetData(zoneData);
                    if (!prediction)
                    {
                        base.ProcessZoneFX(zoneData, hit);
                        hit.Data.Target.AddZone(zone);
                    }
                }
            }

            private EffectSlime GetSlimeEffect()
            {
                var data = new MEffectData();
                data.Duration = (int)this.Data.Z;
                data.ParticlePath = "Slime";
                data.X = this.Data.X;
                data.Y = this.Data.Y;
                var slime = new EffectSlime();
                slime.SetData(data);
                return slime;
            }
        }
    }
}
