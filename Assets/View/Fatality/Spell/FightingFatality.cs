//using Generics.Scripts;
//using Generics.Utilities;
//using UnityEngine;

//namespace Assets.View.Fatality.Spell
//{
//    public class FightingFatality : MFatality
//    {
//        public FightingFatality(CMapGUIControllerHit parent, DisplayActionEvent e)
//            : base(FatalityEnum.Fighting, parent, e)
//        {

//        }

//        public override void Init()
//        {
//            base.Init();
//            if (this._event.EventController.Action.CastType == CastTypeEnum.Bullet ||
//                this._event.EventController.Action.CastType == CastTypeEnum.Raycast)
//                this.InitBulletFatality();
//            else
//                this.InitMeleeFatality();
//            foreach(var hit in this._event.FatalityHits)
//            {
//                if (hit.Target != null)
//                    hit.Target.KillFXProcessed = true;
//            }
//        }

//        protected override void InitBulletFatality()
//        {
//            base.InitBulletFatality();
//            var zoom = this._event.EventController.Source.Handle.AddComponent<DramaticHangCallbackZoomOut>();
//            var position = this._event.EventController.Source.Handle.transform.position;
//            position.y -= 0.35f;
//            zoom.Init(position, FatalityParams.ZOOM_SPEED, FatalityParams.ZOOM_FOV, FatalityParams.ZOOM_BULLET_HANG, this.InitAttackSpriteWithBullet);
//        }

//        private void InitAttackSpriteWithBullet()
//        {
//            var attackerScript = this._event.EventController.Source.Handle.AddComponent<AttackerJoltScript>();
//            var position = Vector3.Lerp(this._event.EventController.Target.Model.Center, this._event.EventController.Source.CurrentTile.Model.Center, 0.85f);
//            attackerScript.Init(this._event.EventController.Source, position, 0.8f, base.Done);
//            this.HandleBulletGraphics();
//        }

//        private void HandleBulletGraphics()
//        {
//            var sprite = AttackSpriteLoader.Instance.GetAttackSprite(this._event.EventController.Action);
//            var bullet = new GameObject();
//            var script = bullet.AddComponent<RaycastWithDeleteScript>();
//            bullet.transform.position = this._event.EventController.Source.transform.position;
//            var renderer = bullet.AddComponent<SpriteRenderer>();
//            renderer.sprite = sprite;
//            renderer.sortingLayerName = CMapGUIControllerParams.PARTICLES_LAYER;
//            if (!this._event.EventController.Source.LParty)
//                bullet.transform.localRotation = Quaternion.Euler(0, 180, 0);
//            script.Init(bullet, this._event.EventController.Target.transform.position, 2f, this.ProcessFatality);
//        }

//        private void ProcessBlood(Hit hit)
//        {
//            var c = hit.Target;
//            foreach(var fatality in this._event.FatalityHits)
//            {
//                if (fatality.Target != null)
//                {
//                    foreach (var neighbor in fatality.Target.CurrentTile.Adjacent)
//                    {
//                        foreach (var outerNeighbor in neighbor.Adjacent)
//                        {
//                            this._parent.ProcessSplatter(1, outerNeighbor);
//                        }
//                        this._parent.ProcessSplatter(2, neighbor);
//                    }
//                    this._parent.ProcessSplatter(5, fatality.Target.CurrentTile);
//                }
//            }
//        }

//        private void ProcessExplosion(Hit hit)
//        {
//            var path = StringUtil.PathBuilder(
//                CMapGUIControllerParams.EFFECTS_PATH,
//                CMapGUIControllerParams.FIGHTING_FATALITY,
//                CMapGUIControllerParams.PARTICLES_EXTENSION);
//            var position = hit.Target.transform.position;
//            var boom = Resources.Load(path);
//            var particles = GameObject.Instantiate(boom) as GameObject;
//            particles.transform.position = position;
//            particles.name = CMapGUIControllerParams.FIGHTING_FATALITY + " Particles";
//            var explosionPath = StringUtil.PathBuilder(
//                CMapGUIControllerParams.EFFECTS_PATH,
//                "FightingFatalityExplosion",
//                CMapGUIControllerParams.PARTICLES_EXTENSION);
//            var explosion = GameObject.Instantiate(Resources.Load(explosionPath)) as GameObject;
//            explosion.transform.position = position;
//            explosion.name = "BOOM";
//            var scriptOne = particles.AddComponent<DestroyByLifetime>();
//            scriptOne.lifetime = 5f;
//            var scriptTwo = explosion.AddComponent<DestroyByLifetime>();
//            scriptTwo.lifetime = 8f;
//        }

//        protected override void ProcessFatality()
//        {
//            foreach(var hit in this._event.FatalityHits)
//            {
//                this.ProcessBlood(hit);
//                this.ProcessExplosion(hit);
//                this.ProcessGear(hit);
//                hit.Done();
//            }
//            this._event.AttackFXDone();
//            this.ProcessFatalityView();
//        }

//        private void ProcessGear(Hit hit)
//        {
//            var c = hit.Target.Model;
//            if (c.Type == ECharacterType.Humanoid)
//            {
//                var renderer = c.ParentController.SpriteHandlerDict[ViewParams.CHAR_TORSO].GetComponent<SpriteRenderer>();
//                renderer.sprite = null;
//                renderer = c.ParentController.SpriteHandlerDict[ViewParams.CHAR_HEAD].GetComponent<SpriteRenderer>();
//                renderer.sprite = null;
//                renderer = c.ParentController.SpriteHandlerDict[ViewParams.CHAR_HEAD_DECO_1].GetComponent<SpriteRenderer>();
//                renderer.sprite = null;
//                renderer = c.ParentController.SpriteHandlerDict[ViewParams.CHAR_HEAD_DECO_2].GetComponent<SpriteRenderer>();
//                renderer.sprite = null;
//                renderer = c.ParentController.SpriteHandlerDict[ViewParams.CHAR_TORSO_DECO_1].GetComponent<SpriteRenderer>();
//                renderer.sprite = null;
//                renderer = c.ParentController.SpriteHandlerDict[ViewParams.CHAR_TORSO_DECO_2].GetComponent<SpriteRenderer>();
//                renderer.sprite = null;
//                renderer = c.ParentController.SpriteHandlerDict[ViewParams.CHAR_FACE].GetComponent<SpriteRenderer>();
//                renderer.sprite = null;

//                if (c.Armor != null)
//                {
//                    var script = c.ParentController.SpriteHandlerDict[ViewParams.CHAR_ARMOR].AddComponent<GearExplosionScript>();
//                    script.Init(c.ParentController.SpriteHandlerDict[ViewParams.CHAR_ARMOR], c.ParentController);
//                }
//                if (c.Helm != null)
//                {
//                    var script = c.ParentController.SpriteHandlerDict[ViewParams.CHAR_HELM].AddComponent<GearExplosionScript>();
//                    script.Init(c.ParentController.SpriteHandlerDict[ViewParams.CHAR_HELM], c.ParentController);
//                }
//                if (c.LWeapon != null)
//                {
//                    var script = c.ParentController.SpriteHandlerDict[ViewParams.CHAR_L_WEAPON].AddComponent<GearExplosionScript>();
//                    script.Init(c.ParentController.SpriteHandlerDict[ViewParams.CHAR_L_WEAPON], c.ParentController);
//                }
//                if (c.RWeapon != null)
//                {
//                    var script = c.ParentController.SpriteHandlerDict[ViewParams.CHAR_R_WEAPON].AddComponent<GearExplosionScript>();
//                    script.Init(c.ParentController.SpriteHandlerDict[ViewParams.CHAR_R_WEAPON], c.ParentController);
//                }
//            }
//            else
//            {
//                var renderer = c.ParentController.SpriteHandlerDict[ViewParams.CHAR_TORSO].GetComponent<SpriteRenderer>();
//                renderer.sprite = null;
//            }
//        }
//    }
//}