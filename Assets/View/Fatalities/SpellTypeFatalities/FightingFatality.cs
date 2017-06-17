using Controller.Managers.Map;
using Controller.Map;
using Generics.Scripts;
using Generics.Utilities;
using Model.Abilities;
using Model.Characters;
using Model.Events.Combat;
using UnityEngine;
using View.Biomes;
using View.Characters;
using View.Scripts;

namespace View.Fatalities
{
    public class FightingFatality : GenericFatality
    {
        public FightingFatality(CMapGUIControllerHit parent, DisplayHitStatsEvent e) 
            : base(FatalityEnum.Fighting, parent, e)
        {
            
        }

        public override void Init()
        {
            base.Init();
            if (this._event.Hit.Ability.Type.GetType() == (typeof(ActiveAbilitiesEnum)))
            {
                var active = this._event.Hit.Ability as GenericActiveAbility;
                if (active.CastType == AbilityCastTypeEnum.Bullet)
                    this.InitBulletFatality();
            }
            else
                this.InitMeleeFatality();
        }

        protected override void InitBulletFatality()
        {
            base.InitBulletFatality();
            var zoom = this._event.Hit.Source.Handle.AddComponent<DramaticZoom>();
            var position = this._event.Hit.Source.Handle.transform.position;
            position.y -= 0.35f;
            zoom.Init(position, FatalityParams.ZOOM_SPEED, FatalityParams.ZOOM_FOV, FatalityParams.ZOOM_BULLET_HANG, this.InitAttackSpriteWithBullet);
        }

        private void InitAttackSpriteWithBullet()
        {
            var attackerScript = this._event.Hit.Source.Handle.AddComponent<AttackerJoltScript>();
            var position = Vector3.Lerp(this._event.Hit.Target.CurrentTile.Model.Center, this._event.Hit.Source.CurrentTile.Model.Center, 0.85f);
            attackerScript.Init(this._event.Hit.Source, position, 0.8f, base.Done);
            this.HandleBulletGraphics();
        }

        private void HandleBulletGraphics()
        {
            var sprite = AttackSpriteLoader.Instance.GetAttackSprite(this._event.Hit.Ability as GenericActiveAbility);
            var bullet = new GameObject();
            var script = bullet.AddComponent<RaycastWithDeleteScript>();
            bullet.transform.position = this._event.Hit.Source.transform.position;
            var renderer = bullet.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.sortingLayerName = CMapGUIControllerParams.PARTICLES_LAYER;
            script.Init(bullet, this._event.Hit.Target.transform.position, 2f, this.ProcessFatality);
        }

        private void ProcessBlood()
        {
            var c = this._event.Hit.Target.Model;
            var sprite = MapBridge.Instance.GetSplatterSprites(5);
            foreach (var neighbor in this._event.Hit.Target.CurrentTile.Adjacent)
            {
                foreach (var outerNeighbor in neighbor.Adjacent)
                {
                    this._parent.ProcessSplatter(1, outerNeighbor);
                }
                this._parent.ProcessSplatter(2, neighbor);
            }
            this._parent.ProcessSplatter(5, this._event.Hit.Target.CurrentTile);
        }

        private void ProcessExplosion()
        {
            var path = StringUtil.PathBuilder(
                CMapGUIControllerParams.EFFECTS_PATH,
                CMapGUIControllerParams.FIGHTING_FATALITY,
                CMapGUIControllerParams.PARTICLES_EXTENSION);
            var position = this._event.Hit.Target.transform.position;
            var boom = Resources.Load(path);
            var particles = GameObject.Instantiate(boom) as GameObject;
            particles.transform.position = position;
            particles.transform.SetParent(this._event.Hit.Target.Handle.transform);
            particles.name = CMapGUIControllerParams.FIGHTING_FATALITY + " Particles";
            var explosionPath = StringUtil.PathBuilder(
                CMapGUIControllerParams.EFFECTS_PATH,
                "FightingFatalityExplosion",
                CMapGUIControllerParams.PARTICLES_EXTENSION);
            var explosion = GameObject.Instantiate(Resources.Load(explosionPath)) as GameObject;
            explosion.transform.position = position;
            explosion.name = "BOOM";
            var scriptOne = particles.AddComponent<DestroyByLifetime>();
            scriptOne.lifetime = 5f;
            var scriptTwo = explosion.AddComponent<DestroyByLifetime>();
            scriptTwo.lifetime = 8f;
            this._event.Hit.Target.Particles.Add(particles);
            this._event.Hit.Target.Particles.Add(explosion);
        }

        protected override void ProcessFatality()
        {
            base.ProcessFatality();
            this.ProcessBlood();
            this.ProcessExplosion();
            this.ProcessGear();
            this.ProcessFatalityBanner();
        }

        private void ProcessGear()
        {
            var c = this._event.Hit.Target.Model;
            if (c.Type == CharacterTypeEnum.Humanoid)
            {
                var renderer = c.ParentController.SpriteHandlerDict["CharTorso"].GetComponent<SpriteRenderer>();
                renderer.sprite = null;
                renderer = c.ParentController.SpriteHandlerDict["CharHead"].GetComponent<SpriteRenderer>();
                renderer.sprite = null;
                renderer = c.ParentController.SpriteHandlerDict["CharDeco1"].GetComponent<SpriteRenderer>();
                renderer.sprite = null;
                renderer = c.ParentController.SpriteHandlerDict["CharDeco2"].GetComponent<SpriteRenderer>();
                renderer.sprite = null;
                renderer = c.ParentController.SpriteHandlerDict["CharDeco3"].GetComponent<SpriteRenderer>();
                renderer.sprite = null;
                renderer = c.ParentController.SpriteHandlerDict["CharFace"].GetComponent<SpriteRenderer>();
                renderer.sprite = null;

                if (c.Armor != null)
                {
                    var script = c.ParentController.SpriteHandlerDict["CharArmor"].AddComponent<GearExplosionScript>();
                    script.Init(c.ParentController.SpriteHandlerDict["CharArmor"], c.ParentController);
                }
                if (c.Helm != null)
                {
                    var script = c.ParentController.SpriteHandlerDict["CharHelm"].AddComponent<GearExplosionScript>();
                    script.Init(c.ParentController.SpriteHandlerDict["CharHelm"], c.ParentController);
                }
                if (c.LWeapon != null)
                {
                    var script = c.ParentController.SpriteHandlerDict["CharLWeapon"].AddComponent<GearExplosionScript>();
                    script.Init(c.ParentController.SpriteHandlerDict["CharLWeapon"], c.ParentController);
                }
                if (c.RWeapon != null)
                {
                    var script = c.ParentController.SpriteHandlerDict["CharRWeapon"].AddComponent<GearExplosionScript>();
                    script.Init(c.ParentController.SpriteHandlerDict["CharRWeapon"], c.ParentController);
                }
            }
        }
    }
}