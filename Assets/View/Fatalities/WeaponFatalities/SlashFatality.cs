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
            var head = this._event.Hit.Target.SpriteHandlerDict["CharHead"];
            var tgtTile = ListUtil<TileController>.GetRandomListElement(this._event.Hit.Target.CurrentTile.Adjacent);
            head.transform.SetParent(tgtTile.transform);
            
            var spin = head.AddComponent<HeadRotationScript>();
            bool spinRight = true;
            var roll = RNG.Instance.Next(1);
            if (roll == 1)
                spinRight = false;
            spin.Init(head, 2.5f, spinRight);
            spin.InitHeadRotation(tgtTile, this._parent);
            var translate = head.AddComponent<RaycastMove>();
            translate.Init(head, tgtTile.Model.Center, 3f, spin.Done);
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
            bodyScript.lifetime = 3f;
            var headScript = headBlood.AddComponent<DestroyByLifetime>();
            headScript.lifetime = 3f;

            var empty = new GameObject();
            headBlood.transform.SetParent(empty.transform);
            empty.transform.Rotate(0, 0, 180f);
            headBlood.transform.SetParent(this._event.Hit.Target.SpriteHandlerDict["CharHead"].transform);
            var emptyScript = empty.AddComponent<DestroyByLifetime>();
            emptyScript.lifetime = 3f;
            headBlood.transform.position = this._event.Hit.Target.SpriteHandlerDict["CharHead"].transform.position;
        }
    }
}