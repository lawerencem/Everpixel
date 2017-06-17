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
    public class SlashFatality : GenericFatality
    {
        public SlashFatality(CMapGUIControllerHit parent, DisplayHitStatsEvent e)
            : base(FatalityEnum.Slash, parent, e)
        {

        }

        public override void Init()
        {
            base.Init();
            base.InitMeleeFatality();
            var zoom = this._event.Hit.Source.Handle.AddComponent<DramaticZoom>();
            var position = this._event.Hit.Source.Handle.transform.position;
            position.y -= 0.35f;
            zoom.Init(position, FatalityParams.ZOOM_SPEED, FatalityParams.ZOOM_FOV, FatalityParams.ZOOM_MELEE_HANG, this.ProcessFatality);

        }

        protected override void ProcessFatality()
        {
            var fatalityScript = this._event.Hit.Source.Handle.AddComponent<BoomerangFatalityScript>();
            var position = Vector3.Lerp(this._event.Hit.Source.Handle.transform.position, this._event.Hit.Target.transform.position, 0.55f);
            fatalityScript.Init(this._event.Hit.Source.Handle, position, 0.75f, ProcessHeadFatality);
        }

        private void ProcessHeadFatality()
        {
            var head = this._event.Hit.Target.SpriteHandlerDict["CharHead"];
            var tgtTile = ListUtil<TileController>.GetRandomListElement(this._event.Hit.Target.CurrentTile.Adjacent);
            head.transform.SetParent(tgtTile.transform);

            var spin = head.AddComponent<HeadRotationScript>();
            bool spinRight = true;
            var roll = RNG.Instance.Next(1);
            if (roll == 1)
                spinRight = false;
            var percent = RNG.Instance.NextDouble();
            spin.Init(head, (float)(5f * percent), spinRight, base.Done);
            spin.InitHeadRotation(tgtTile, this._parent);
            var translate = head.AddComponent<RaycastMove>();
            translate.Init(head, tgtTile.Model.Center, 1f, spin.Done);
            this.HandleParticles();
            this._parent.ProcessCharacterKilled(this._event.Hit.Target);
            base.ProcessFatalityBanner();
        }

        private void HandleParticles()
        {
            var path = StringUtil.PathBuilder(
                CMapGUIControllerParams.EFFECTS_PATH,
                CMapGUIControllerParams.SLASH_FATALITY,
                CMapGUIControllerParams.PARTICLES_EXTENSION);
            var position = this._event.Hit.Target.transform.position;
            var prefab = Resources.Load(path);
            var headBlood = GameObject.Instantiate(prefab) as GameObject;
            var bodyBlood = GameObject.Instantiate(prefab) as GameObject;

            bodyBlood.transform.position = position;
            bodyBlood.transform.SetParent(this._event.Hit.Target.Handle.transform);

            bodyBlood.name = CMapGUIControllerParams.SLASH_FATALITY + " Particles";
            headBlood.name = CMapGUIControllerParams.SLASH_FATALITY + " Particles";

            var bodyScript = bodyBlood.AddComponent<DestroyByLifetime>();
            bodyScript.lifetime = 5f;
            var headScript = headBlood.AddComponent<DestroyByLifetime>();
            headScript.lifetime = 5f;

            var empty = new GameObject();
            headBlood.transform.SetParent(empty.transform);

            headBlood.transform.SetParent(this._event.Hit.Target.SpriteHandlerDict["CharHead"].transform);
            var emptyScript = empty.AddComponent<DestroyByLifetime>();
            emptyScript.lifetime = 5f;
            headBlood.transform.position = this._event.Hit.Target.SpriteHandlerDict["CharHead"].transform.position;
        }
    }
}