using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability;
using Assets.Model.Action;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Template.CB;
using Assets.Template.Other;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.Template.Utility;
using Assets.View.Script.FX;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.View.Ability
{
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
            return this.GetSprite(path);
        }

        public List<Sprite> GetSingleFXSprites(MAbility a)
        {
            var path = StringUtil.PathBuilder(ATTACK_PATH, a.Type.ToString());
            var stuff = Resources.LoadAll(path);
            var sprites = new List<Sprite>();
            for(int i = 1; i < stuff.Length; i++)
                sprites.Add(stuff[i] as Sprite);
            return sprites;
        }

        public GameObject GetBullet(MHit hit, Callback callback, float speed)
        {
            var sprite = this.GetBulletSprite(hit.Data.Ability.Data.ParentAction);
            var bullet = new GameObject();
            bullet.transform.position = hit.Data.Source.GameHandle.transform.position;
            var renderer = bullet.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.sortingLayerName = SortingLayers.PARTICLES;
            var angle = Vector3.Angle(hit.Data.Target.Handle.transform.position, bullet.transform.position);
            if (hit.Data.Target.Handle.transform.position.y < bullet.transform.position.y)
                angle = -angle;
            if (hit.Data.Source.Proxy.LParty)
                bullet.transform.Rotate(0, 0, angle);
            else
                bullet.transform.localRotation = Quaternion.Euler(0, 180, angle);

            var embed = this.GetEmbedBullet(hit, callback, bullet, speed);
            if (embed != null)
                return embed;
            else
                return this.GetDeleteBullet(hit, callback, bullet, speed);
        }

        public GameObject GetRaycast(MAction action, CTile tgt, Callback callback, float speed)
        {
            var sprite = this.GetBulletSprite(action);
            var bullet = new GameObject();
            bullet.transform.position = action.Data.Source.GameHandle.transform.position;
            var renderer = bullet.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.sortingLayerName = SortingLayers.PARTICLES;
            var tgtPosition = tgt.Handle.transform.position;
            var angle = Vector3.Angle(tgtPosition, bullet.transform.position);
            if (tgtPosition.y < bullet.transform.position.y)
                angle = -angle;
            if (action.Data.Source.Proxy.LParty)
                bullet.transform.Rotate(0, 0, angle);
            else
                bullet.transform.localRotation = Quaternion.Euler(0, 180, angle);
            return this.GetDeleteRay(action, tgtPosition, callback, bullet, speed);
        }

        public void DoSingleFX(MHit hit)
        {
            var ability = hit.Data.Ability.Data.ParentAction.ActiveAbility;
            var sprites = this.GetSingleFXSprites(ability);
            var roll = RNG.Instance.Next(ability.Data.MinSprites, ability.Data.MaxSprites);
            for(int i = 0; i < roll; i++)
            {
                var fx = new GameObject();
                var renderer = fx.AddComponent<SpriteRenderer>();
                var index = ListUtil<int>.GetRandomElement(ability.Data.Sprites);
                renderer.sprite = sprites[index];
                renderer.sortingLayerName = SortingLayers.PARTICLES;
                fx.transform.position = hit.Data.Target.Model.Center;
                RotateTranslateUtil.Instance.RandomTranslate(fx, CombatGUIParams.SINGLE_FX_OFFSET);
                fx.transform.SetParent(hit.Data.Target.Handle.transform);
                var delete = fx.AddComponent<SDestroyByLifetime>();
                delete.Init(fx, CombatGUIParams.SINGLE_FX_DUR);
            }
            hit.CallbackHandler(null);
        }

        private GameObject GetDeleteBullet(MHit hit, Callback callback, GameObject bullet, float speed)
        {
            var data = new SRaycastMoveData();
            data.Epsilon = CombatGUIParams.DEFAULT_EPSILON;
            data.Handle = bullet;
            data.Speed = speed;
            data.Target = hit.Data.Target.Handle.transform.position;
            var script = bullet.AddComponent<SBulletThenDelete>();
            script.Action = hit.Data.Action;
            script.Init(data);
            script.AddCallback(callback);
            return bullet;
        }

        private GameObject GetDeleteRay(MAction a, Vector3 tgt, Callback callback, GameObject bullet, float speed)
        {
            var data = new SRaycastMoveData();
            data.Epsilon = CombatGUIParams.DEFAULT_EPSILON;
            data.Handle = bullet;
            data.Speed = speed;
            data.Target = tgt;
            var script = bullet.AddComponent<SBulletThenDelete>();
            script.Action = a;
            script.Init(data);
            script.AddCallback(callback);
            return bullet;
        }

        private GameObject GetEmbedBullet(MHit hit, Callback callback, GameObject bullet, float speed)
        {
            var renderer = bullet.GetComponent<SpriteRenderer>();
            renderer.sortingOrder = renderer.sortingOrder + 1;
            var a = hit.Data.Ability.Data.ParentAction;
            var proxy = a.Data.Source.Proxy;
            var data = new SBulletThenEmbedData();
            data.EmbedSprite = this.GetBulletEmbedSprite(a);
            data.Epsilon = CombatGUIParams.DEFAULT_EPSILON;
            data.Handle = bullet;
            data.Speed = speed;
            var pos = hit.Data.Target.Handle.transform.position;
            if (hit.Data.Source.GameHandle.transform.position.x > pos.x)
                pos.x += CombatGUIParams.X_CORRECTION;
            else
                pos.x -= CombatGUIParams.X_CORRECTION;
            data.Target = pos;
            data.Offset = CombatGUIParams.BULLET_OFFSET;
            data.Rotation = CombatGUIParams.MAX_ROTATION;
            if (hit.Data.Target.Current != null && 
                hit.Data.Target.Current.GetType().Equals(typeof(CChar)))
            {
                data.TargetChar = hit.Data.Target.Current as CChar;
            }
            data.TargetableObjects = this.GetEmbedBulletTargets(hit);
            if (a.Data.LWeapon && proxy.GetLWeapon() != null && proxy.GetLWeapon().Embed)
                return this.GetEmbedBulletHelper(a, bullet, callback, data);
            else if (proxy.GetRWeapon() != null && proxy.GetRWeapon().Embed)
                return this.GetEmbedBulletHelper(a, bullet, callback, data);
            return null;
        }

        private GameObject GetEmbedBulletHelper(
            MAction a,
            GameObject bullet,
            Callback callback,
            SBulletThenEmbedData data)
        {
            var raycast = bullet.AddComponent<SBulletThenEmbed>();
            raycast.Action = a;
            raycast.Init(data);
            raycast.AddCallback(callback);
            return bullet;
        }

        private List<GameObject> GetEmbedBulletTargets(MHit hit)
        {
            var tgts = new List<GameObject>();

            if (hit.Data.Target.Current != null &&
                hit.Data.Target.Current.GetType().Equals(typeof(CChar)))
            {
                var tgt = hit.Data.Target.Current as CChar;
                if (FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Dodge))
                    tgts.Add(tgt.Tile.Handle);
                else if (FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Block))
                {
                    if (tgt.Proxy.GetLWeapon() != null &&
                        tgt.Proxy.GetLWeapon().IsTypeOfShield())
                    {
                        tgts.Add(tgt.SubComponents[SortingLayers.CHAR_L_WEAPON]);
                    }
                    if (tgt.Proxy.GetRWeapon() != null &&
                        tgt.Proxy.GetRWeapon().IsTypeOfShield())
                    {
                        tgts.Add(tgt.SubComponents[SortingLayers.CHAR_R_WEAPON]);
                    }
                }
                else
                {
                    if (tgt.Proxy.Type != ECharType.Critter)
                    {
                        if (tgt.SubComponents.ContainsKey(SortingLayers.CHAR_ARMOR))
                            tgts.Add(tgt.SubComponents[SortingLayers.CHAR_ARMOR]);
                        else
                            tgts.Add(tgt.SubComponents[SortingLayers.CHAR_TORSO]);
                        if (tgt.SubComponents.ContainsKey(SortingLayers.CHAR_HELM))
                            tgts.Add(tgt.SubComponents[SortingLayers.CHAR_HELM]);
                        else
                            tgts.Add(tgt.SubComponents[SortingLayers.CHAR_HEAD]);
                        if (tgt.SubComponents.ContainsKey(SortingLayers.CHAR_MOUNT))
                            tgts.Add(tgt.SubComponents[SortingLayers.CHAR_MOUNT]);
                    }
                    else
                        tgts.Add(tgt.SubComponents[SortingLayers.CHAR_MAIN]);
                }
            }
            else
                tgts.Add(hit.Data.Target.Handle);
            return tgts;
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
