using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.View.Character;
using Assets.View.Script.FX;
using Template.Script;
using UnityEngine;

namespace Assets.View.Fatality.Weapon.Ability
{
    public class WeenBurster : MFatality
    {
        private const string WEEN_BURSTER = "Ween_Burster";
        

        public WeenBurster(FatalityData data) : base(EFatality.Weenburster, data)
        {

        }

        public override void Init()
        {
            base.Init();
            base.Start(this.ProcessJolt);
        }

        private void ProcessJolt(object o)
        {
            var pos = Vector3.Lerp(
                this._data.Source.Handle.transform.position,
                this._data.Target.Handle.transform.position,
                FatalityParams.FATALITY_MELEE_LERP);
            var attack = this._data.Source.Handle.AddComponent<SAttackerJolt>();
            attack.Action = this._data.Action;
            attack.AddCallback(this.ProcessShake);
            attack.Init(this._data.Source, pos, FatalityParams.FATALITY_ATTACK_SPEED);
        }

        private void ProcessShake(object o)
        {
            foreach (var hit in this.Data.FatalHits)
            {
                if (hit.Data.Target.Current != null &&
                    hit.Data.Target.Current.GetType().Equals(typeof(CChar)))
                {
                    var tgt = hit.Data.Target.Current as CChar;
                    var data = new SIntervalJoltScriptData();
                    data.Dur = 4f;
                    data.Speed = 22f;
                    data.TimeInterval = 0.18f;
                    data.ToJolt = tgt.Handle;
                    data.X = 0.1f;
                    data.Y = 0.025f;
                    var jolt = tgt.Handle.AddComponent<SIntervalJoltScript>();
                    jolt.AddCallback(this.HandleWeen);
                    jolt.AddObjectToList(tgt);
                    this._fatalityMap.Add(jolt.ID, jolt);
                    jolt.Init(data);
                }
            }
        }

        private void HandleWeen(object o)
        {
            var bloodPath = StringUtil.PathBuilder(
                CombatGUIParams.EFFECTS_PATH,
                "WeenBursterFatality",
                CombatGUIParams.PARTICLES_EXTENSION);

            if (o.GetType().Equals(typeof(SIntervalJoltScript)))
            {
                var script = o as SIntervalJoltScript;
                var tgt = script.GetObjectList()[0] as CChar;
                var sprite = CharSpriteLoader.Instance.GetFatalitySprite(WEEN_BURSTER);
                this.LayFatalityDeco(sprite, tgt, Layers.PARTICLES);

                var bloodPrefab = Resources.Load(bloodPath);
                var particles = GameObject.Instantiate(bloodPrefab) as GameObject;
                particles.transform.position = tgt.Handle.transform.position;
                particles.name = " Ween Burster Blood Particles";
                if (tgt.Proxy.LParty)
                    particles.transform.Rotate(0, 90, 0);
                else
                    particles.transform.Rotate(0, 270, 0);
                var bloodLifetime = particles.AddComponent<SDestroyByLifetime>();
                bloodLifetime.Init(particles, 8f);
            }
        }
    }
}
