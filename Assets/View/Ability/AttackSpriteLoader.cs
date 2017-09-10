using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Model.Ability;
using Assets.Model.Action;
using Assets.Template.CB;
using Assets.Template.Other;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.View.Script.FX;
using UnityEngine;

namespace Assets.View.Ability
{

    // TODO: Clean this up
    public class AttackSpriteLoader : ASingleton<AttackSpriteLoader>
    {
        private const string ATTACK_PATH = "Sprites/Attacks/";
        private const string BULLET_PATH = "Sprites/Bullet/";
        private const string EMBED = "_Embed";
        private const string EXTENSION = "_Spritesheet";

        public AttackSpriteLoader() { }

        public Sprite GetAttackSprite(MAbility a)
        {
            var path = StringUtil.PathBuilder(ATTACK_PATH, a.Type.ToString());
            return GetSprite(path);
        }

        public GameObject GetBullet(MAction a, Callback callback, float speed)
        {
            var sprite = this.GetBulletSprite(a);
            var bullet = new GameObject();
            bullet.transform.position = a.Data.Source.Handle.transform.position;
            var renderer = bullet.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.sortingLayerName = Layers.PARTICLES;
            var angle = Vector3.Angle(a.Data.Target.Handle.transform.position, bullet.transform.position);
            if (a.Data.Target.Handle.transform.position.y < bullet.transform.position.y)
                angle = -angle;
            if (a.Data.Source.Proxy.LParty)
                bullet.transform.Rotate(0, 0, angle);
            else
                bullet.transform.localRotation = Quaternion.Euler(0, 180, angle);

            var embed = this.GetEmbedBullet(a, callback, bullet, speed);
            if (embed != null)
                return embed;
            else
                return this.GetDefaultBullet(a, callback, bullet, speed);
        }

        private GameObject GetDefaultBullet(MAction a, Callback callback, GameObject bullet, float speed)
        {
            var data = new SRaycastMoveData();
            data.Epsilon = CombatGUIParams.DEFAULT_EPSILON;
            data.Handle = bullet;
            data.Speed = speed;
            data.Target = a.Data.Target.Handle.transform.position;
            var raycast = bullet.AddComponent<SBulletThenDelete>();
            raycast.Action = a;
            raycast.Init(data);
            raycast.AddCallback(callback);
            return bullet;
        }

        private GameObject GetEmbedBullet(MAction a, Callback callback, GameObject bullet, float speed)
        {
            var data = new SRaycastMoveThenEmbedData();
            data.EmbedSprite = this.GetBulletEmbedSprite(a);
            data.Epsilon = CombatGUIParams.DEFAULT_EPSILON;
            data.Handle = bullet;
            data.Speed = speed;
            var pos = a.Data.Target.Handle.transform.position;
            if (a.Data.Source.Handle.transform.position.x > pos.x)
                pos.x += 0.14f;
            else
                pos.x -= 0.14f;
            data.Target = pos;
            data.Offset = 0.125f;
            data.Rotation = 25f;
            
            if (a.Data.Target.Current.GetType().Equals(typeof(CharController)))
            {
                var tgt = a.Data.Target.Current as CharController;
                data.TargetObject = tgt.Handle;
            }
            else
                data.TargetObject = a.Data.Target.Handle;

            if (a.Data.LWeapon &&
                a.Data.Source.Proxy.GetLWeapon() != null &&
                a.Data.Source.Proxy.GetLWeapon().Embed)
            {
                var raycast = bullet.AddComponent<SBulletThenEmbed>();
                raycast.Action = a;
                raycast.Init(data);
                raycast.AddCallback(callback);
                return bullet;
            }
            else if (a.Data.Source.Proxy.GetRWeapon() != null &&
                a.Data.Source.Proxy.GetRWeapon().Embed)
            {
                var raycast = bullet.AddComponent<SBulletThenEmbed>();
                raycast.Action = a;
                raycast.Init(data);
                raycast.AddCallback(callback);
                return bullet;
            }
            return null;
        }

        private Sprite GetBulletSprite(MAction a)
        {
            if (a.Data.LWeapon && 
                a.Data.Source.Proxy.GetLWeapon() != null &&
                a.Data.Source.Proxy.GetLWeapon().CustomBullet)
            {
                var path = StringUtil.PathBuilder(
                    BULLET_PATH,
                    a.Data.Source.Proxy.GetLWeapon().SpriteFXPath);
                return this.GetSprite(path);
            }
            else if (a.Data.Source.Proxy.GetRWeapon() != null &&
                a.Data.Source.Proxy.GetRWeapon().CustomBullet)
            {
                var path = StringUtil.PathBuilder(
                    BULLET_PATH,
                    a.Data.Source.Proxy.GetRWeapon().SpriteFXPath);
                return this.GetSprite(path);
            }
            else
            {
                return this.GetAttackSprite(a.ActiveAbility);
            }
        }

        private Sprite GetBulletEmbedSprite(MAction a)
        {
            if (a.Data.LWeapon &&
                a.Data.Source.Proxy.GetLWeapon() != null &&
                a.Data.Source.Proxy.GetLWeapon().CustomBullet)
            {
                var path = StringUtil.PathBuilder(
                    BULLET_PATH,
                    a.Data.Source.Proxy.GetLWeapon().SpriteFXPath,
                    EMBED);
                return this.GetSprite(path);
            }
            else if (a.Data.Source.Proxy.GetRWeapon() != null &&
                a.Data.Source.Proxy.GetRWeapon().CustomBullet)
            {
                var path = StringUtil.PathBuilder(
                    BULLET_PATH,
                    a.Data.Source.Proxy.GetRWeapon().SpriteFXPath,
                    EMBED);
                return this.GetSprite(path);
            }
            else
            {
                return this.GetAttackSprite(a.ActiveAbility);
            }
        }

        public Sprite GetSprite(string path)
        {
            var stuff = Resources.LoadAll(path);
            if (stuff.Length == 2)
                return stuff[1] as Sprite;
            else
                return null;
        }
    }
}
