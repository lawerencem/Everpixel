using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Model.Character.Enum;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.View.Character;
using UnityEngine;

namespace Assets.View.Fatality.Weapon
{
    public class CrushFatality : MFatality
    {
        public CrushFatality(FatalityData data) : base(EFatality.Crush, data) {}

        public override void Init()
        {
            base.Init();
            base.Start(this.ProcessJolt);
        }

        private void ProcessCrush(object o)
        {
            foreach (var hit in this._data.FatalHits)
            {
                if (hit.Data.Target.Current != null &&
                    hit.Data.Target.Current.GetType().Equals(typeof(CharController)))
                {
                    var tgt = this._data.Target.Current as CharController;
                    var path = StringUtil.PathBuilder(
                        CombatGUIParams.EFFECTS_PATH,
                        CombatGUIParams.CRUSH_FATALITY,
                        CombatGUIParams.PARTICLES_EXTENSION);
                    var position = tgt.Handle.transform.position;
                    var prefab = Resources.Load(path);
                    var particles = GameObject.Instantiate(prefab) as GameObject;
                    particles.transform.position = position;
                    particles.name = CombatGUIParams.CRUSH_FATALITY + " Particles";
                    var lifetime = particles.AddComponent<SDestroyByLifetime>();
                    lifetime.lifetime = 5f;
                    VCharUtil.Instance.ProcessDeadChar(tgt);
                    this.CallbackHandler(this);
                    hit.CallbackHandler(this);
                }
            }
        }

        private void ProcessJolt(object o)
        {
            var pos = Vector3.Lerp(
                this._data.Source.Handle.transform.position,
                this._data.Target.Handle.transform.position,
                FatalityParams.FATALITY_LERP);
            var boomerang = this._data.Source.Handle.AddComponent<SBoomerang>();
            boomerang.AddCallback(this.ProcessCrush);
            boomerang.AddDoneCallback(this.AddBob);
            boomerang.Init(this._data.Source.Handle, pos, FatalityParams.FATALITY_ATTACK_SPEED);
        }
    }
}
