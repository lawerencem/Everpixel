﻿using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Model.Ability;
using Assets.Model.Action;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Template.CB;
using Assets.Template.Other;
using Assets.Template.Script;
using Assets.Template.Util;
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
            return GetSprite(path);
        }

        public GameObject GetBullet(MHit hit, Callback callback, float speed)
        {
            var sprite = this.GetBulletSprite(hit.Data.Ability.Data.ParentAction);
            var bullet = new GameObject();
            bullet.transform.position = hit.Data.Source.Handle.transform.position;
            var renderer = bullet.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.sortingLayerName = Layers.PARTICLES;
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

        private GameObject GetEmbedBullet(MHit hit, Callback callback, GameObject bullet, float speed)
        {
            var a = hit.Data.Ability.Data.ParentAction;
            var proxy = a.Data.Source.Proxy;
            var data = new SBulletThenEmbedData();
            data.EmbedSprite = this.GetBulletEmbedSprite(a);
            data.Epsilon = CombatGUIParams.DEFAULT_EPSILON;
            data.Handle = bullet;
            data.Speed = speed;
            var pos = hit.Data.Target.Handle.transform.position;
            if (hit.Data.Source.Handle.transform.position.x > pos.x)
                pos.x += CombatGUIParams.X_CORRECTION;
            else
                pos.x -= CombatGUIParams.X_CORRECTION;
            data.Target = pos;
            data.Offset = CombatGUIParams.BULLET_OFFSET;
            data.Rotation = CombatGUIParams.MAX_ROTATION;
            if (hit.Data.Target.Current != null && 
                hit.Data.Target.Current.GetType().Equals(typeof(CharController)))
            {
                data.TargetChar = hit.Data.Target.Current as CharController;
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

            if (hit.Data.Target.Current.GetType().Equals(typeof(CharController)))
            {
                var tgt = hit.Data.Target.Current as CharController;
                if (FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Dodge))
                    tgts.Add(tgt.Tile.Handle);
                else if (FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Block))
                {
                    if (tgt.Proxy.GetLWeapon() != null &&
                        tgt.Proxy.GetLWeapon().IsTypeOfShield())
                    {
                        tgts.Add(tgt.SubComponents[Layers.CHAR_L_WEAPON]);
                    }
                    if (tgt.Proxy.GetRWeapon() != null &&
                        tgt.Proxy.GetRWeapon().IsTypeOfShield())
                    {
                        tgts.Add(tgt.SubComponents[Layers.CHAR_R_WEAPON]);
                    }
                }
                else
                {
                    tgts.Add(tgt.SubComponents[Layers.CHAR_MAIN]);
                    if (tgt.Proxy.Type != ECharType.Critter)
                    {
                        if (tgt.SubComponents.ContainsKey(Layers.CHAR_ARMOR))
                            tgts.Add(tgt.SubComponents[Layers.CHAR_ARMOR]);
                        if (tgt.SubComponents.ContainsKey(Layers.CHAR_HELM))
                            tgts.Add(tgt.SubComponents[Layers.CHAR_HELM]);
                        if (tgt.SubComponents.ContainsKey(Layers.CHAR_MOUNT))
                            tgts.Add(tgt.SubComponents[Layers.CHAR_MOUNT]);
                    }
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
