using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Model.Character.Enum;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.View.Ability;
using Assets.View.Event;
using Assets.View.Script.FX;
using UnityEngine;

namespace Assets.View.Fatality.Magic
{
    public class FightingFatality : MFatality
    {
        public FightingFatality(FatalityData data) : base(EFatality.Fighting, data)
        {
            base.Init();
            base.Start(this.ProcessJolt);
        }

        private void ProcessBlood(CharController target)
        {
            foreach (var neighbor in target.Tile.GetAdjacent())
            {
                foreach (var outerNeighbor in neighbor.GetAdjacent())
                {
                    this.ProcessBloodHelper(target, 0.2);
                }
                this.ProcessBloodHelper(target, 0.5);
            }
            this.ProcessBloodHelper(target, 1.0);
        }

        private void ProcessBloodHelper(CharController target, double percent)
        {
            var data = new EvSplatterData();
            data.DmgPercent = percent;
            data.Target = target.Handle;
            var e = new EvSplatter(data);
            e.TryProcess();
        }

        private void ProcessJolt(object o)
        {
            var pos = Vector3.Lerp(
                this._data.Source.Handle.transform.position,
                this._data.Target.Handle.transform.position,
                FatalityParams.FATALITY_MELEE_LERP);
            var attack = this._data.Source.Handle.AddComponent<SAttackerJolt>();
            attack.Action = this._data.Action;
            var bullet = AttackSpriteLoader.Instance.GetBullet(this._data.Action, this.ProcessExplosion, FatalityParams.FIGHTING_BULLET_SPEED);
            attack.Init(this._data.Source, pos, FatalityParams.FATALITY_ATTACK_SPEED);
        }

        private void ProcessExplosion(object o)
        {
            foreach(var hit in this._data.FatalHits)
            {
                if (hit.Data.Target.Current != null &&
                    hit.Data.Target.Current.GetType().Equals(typeof(CharController)))
                {
                    var tgt = hit.Data.Target.Current as CharController;

                    var path = StringUtil.PathBuilder(
                        CombatGUIParams.EFFECTS_PATH,
                        CombatGUIParams.FIGHTING_FATALITY,
                        CombatGUIParams.PARTICLES_EXTENSION);
                    var position = tgt.Handle.transform.position;
                    var boom = Resources.Load(path);
                    var particles = GameObject.Instantiate(boom) as GameObject;
                    particles.transform.position = position;
                    particles.name = CombatGUIParams.FIGHTING_FATALITY + " Particles";
                    var explosionPath = StringUtil.PathBuilder(
                        CombatGUIParams.EFFECTS_PATH,
                        "FightingFatalityExplosion",
                        CombatGUIParams.PARTICLES_EXTENSION);
                    var explosion = GameObject.Instantiate(Resources.Load(explosionPath)) as GameObject;
                    explosion.transform.position = position;
                    explosion.name = "BOOM";
                    var scriptOne = particles.AddComponent<SDestroyByLifetime>();
                    var scriptTwo = explosion.AddComponent<SDestroyByLifetime>();
                    scriptOne.Init(particles, 5f);
                    scriptOne.AddCallback(this.CallbackHandler);
                    scriptOne.AddCallback(hit.CallbackHandler);
                    scriptOne.AddCallback(this.AddBob);
                    scriptTwo.Init(explosion, 8f);
                    this.ProcessBlood(tgt);
                    this.ProcessGear(tgt);
                }
                else
                {
                    VHitController.Instance.ProcessDefenderHit(hit);
                }
            }
        }

        private void ProcessGear(CharController c)
        {
            if (c.Proxy.Type == ECharType.Humanoid)
            {
                if (c.SubComponents.ContainsKey(Layers.CHAR_TORSO))
                {
                    var renderer = c.SubComponents[Layers.CHAR_TORSO].GetComponent<SpriteRenderer>();
                    renderer.sprite = null;
                }
                if (c.SubComponents.ContainsKey(Layers.CHAR_HEAD))
                {
                    var renderer = c.SubComponents[Layers.CHAR_HEAD].GetComponent<SpriteRenderer>();
                    renderer.sprite = null;
                }
                if (c.SubComponents.ContainsKey(Layers.CHAR_HEAD_DECO_1))
                {
                    var renderer = c.SubComponents[Layers.CHAR_HEAD_DECO_1].GetComponent<SpriteRenderer>();
                    renderer.sprite = null;
                }
                if (c.SubComponents.ContainsKey(Layers.CHAR_HEAD_DECO_2))
                {
                    var renderer = c.SubComponents[Layers.CHAR_HEAD_DECO_2].GetComponent<SpriteRenderer>();
                    renderer.sprite = null;
                }
                if (c.SubComponents.ContainsKey(Layers.CHAR_FACE))
                {
                    var renderer = c.SubComponents[Layers.CHAR_FACE].GetComponent<SpriteRenderer>();
                    renderer.sprite = null;
                }
                if (c.Proxy.GetArmor() != null)
                {
                    var script = c.SubComponents[Layers.CHAR_ARMOR].AddComponent<SFightFatalityExplosionMove>();
                    script.Init(c.SubComponents[Layers.CHAR_ARMOR], c);
                }
                if (c.Proxy.GetHelm() != null)
                {
                    var script = c.SubComponents[Layers.CHAR_HELM].AddComponent<SFightFatalityExplosionMove>();
                    script.Init(c.SubComponents[Layers.CHAR_HELM], c);
                }
                if (c.Proxy.GetLWeapon() != null)
                {
                    var script = c.SubComponents[Layers.CHAR_L_WEAPON].AddComponent<SFightFatalityExplosionMove>();
                    script.Init(c.SubComponents[Layers.CHAR_L_WEAPON], c);
                }
                if (c.Proxy.GetRWeapon() != null)
                {
                    var script = c.SubComponents[Layers.CHAR_R_WEAPON].AddComponent<SFightFatalityExplosionMove>();
                    script.Init(c.SubComponents[Layers.CHAR_R_WEAPON], c);
                }
            }
            else
            {
                var renderer = c.SubComponents[Layers.CHAR_TORSO].GetComponent<SpriteRenderer>();
                renderer.sprite = null;
            }
        }
    }
}
