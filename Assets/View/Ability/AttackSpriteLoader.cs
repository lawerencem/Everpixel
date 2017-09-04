using Assets.Controller.GUI.Combat;
using Assets.Model.Ability;
using Assets.Model.Action;
using Assets.Template.CB;
using Assets.Template.Other;
using Assets.Template.Util;
using Assets.View.Script.FX;
using UnityEngine;

namespace Assets.View.Ability
{
    public class AttackSpriteLoader : ASingleton<AttackSpriteLoader>
    {
        private const string ATTACK_PATH = "Sprites/Attacks/";
        private const string EXTENSION = "_Spritesheet";

        public AttackSpriteLoader() { }

        public Sprite GetAttackSprite(MAbility a)
        {
            var path = StringUtil.PathBuilder(ATTACK_PATH, a.Type.ToString());
            return GetSprite(path);
        }

        public GameObject GetBullet(MAction a, Callback callback, float speed)
        {
            var bullet = new GameObject();
            var raycast = bullet.AddComponent<SBullet>();
            raycast.Action = a;
            bullet.transform.position = a.Data.Source.Handle.transform.position;
            var renderer = bullet.AddComponent<SpriteRenderer>();
            var sprite = AttackSpriteLoader.Instance.GetAttackSprite(a.ActiveAbility);
            renderer.sprite = sprite;
            renderer.sortingLayerName = Layers.PARTICLES;
            if (!a.Data.Source.Proxy.LParty)
                bullet.transform.localRotation = Quaternion.Euler(0, 180, 0);
            raycast.Init(bullet, a.Data.Target.Handle.transform.position, speed);
            raycast.AddCallback(callback);
            return bullet;
        }

        private Sprite GetSprite(string path)
        {
            var stuff = Resources.LoadAll(path);
            if (stuff.Length == 2)
                return stuff[1] as Sprite;
            else
                return null;
        }
    }
}
