﻿using Assets.Controller.Character;
using Assets.Model.Character.Enum;
using Assets.Template.Other;
using Assets.Template.Utility;
using Assets.View.Event;
using UnityEngine;

namespace Assets.View.Character
{
    public class VCharUtil : ASingleton<VCharUtil>
    {
        public void AssignDeadEyes(CharController c)
        {
            if (c.Proxy.Type == ECharType.Humanoid)
            {
                var sprite = CharSpriteLoader.Instance.GetHumanoidDeadEyes(c.Proxy.Race);
                var eyes = c.SubComponents[Layers.CHAR_FACE];
                var renderer = eyes.GetComponent<SpriteRenderer>();
                renderer.sprite = sprite;
            }
        }

        public void AssignDeadLayer(CharController c)
        {
            foreach(var subcomponent in c.SubComponents)
            {
                var sub = subcomponent.Value;
                var renderer = sub.GetComponent<SpriteRenderer>();
                var layer = renderer.sortingLayerName;
                renderer.sortingLayerName =  layer.Replace(Layers.CHAR, Layers.DEAD);
            }
        }

        public void AssignDeadWeapons(CharController c)
        {
            if (c.Proxy.GetLWeapon() != null)
            {
                RotateTranslateUtil.Instance.RandomRotateAndTranslate(
                    c.SubComponents[Layers.CHAR_L_WEAPON],
                    ViewParams.SPLATTER_VARIANCE);
            }
            if (c.Proxy.GetRWeapon() != null)
            {
                RotateTranslateUtil.Instance.RandomRotateAndTranslate(
                    c.SubComponents[Layers.CHAR_R_WEAPON],
                    ViewParams.SPLATTER_VARIANCE);
            }
        }

        public void AssignDeathSplatter(CharController c)
        {
            var data = new EvSplatterData();
            data.DmgPercent = 0.50;
            data.Target = c.Handle;
            var e = new EvSplatter(data);
            e.TryProcess();
        }

        public void RandomTranslateRotateOnDeath(CharController c)
        {
            RotateTranslateUtil.Instance.RandomRotateAndTranslate(
                c.Handle,
                ViewParams.SPLATTER_VARIANCE);
        }

        public void ProcessDeadChar(CharController c)
        {
            this.AssignDeadWeapons(c);
            this.AssignDeadEyes(c);
            this.AssignDeadLayer(c);
            this.RandomTranslateRotateOnDeath(c);
            this.AssignDeathSplatter(c);
        }
    }
}
