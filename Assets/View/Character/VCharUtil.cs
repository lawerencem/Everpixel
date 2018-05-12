using Assets.Controller.Character;
using Assets.Model.Character.Enum;
using Assets.Template.Other;
using Assets.Template.Utility;
using Assets.View.Event;
using Assets.View.Script.FX;
using UnityEngine;

namespace Assets.View.Character
{
    public class VCharUtil : ASingleton<VCharUtil>
    {
        public void AssignDeadEyes(CChar c)
        {
            if (c.Proxy.Type == ECharType.Humanoid)
            {
                var sprite = CharSpriteLoader.Instance.GetHumanoidDeadEyes(c.Proxy.Race);
                var eyes = c.SubComponents[SortingLayers.CHAR_FACE];
                var renderer = eyes.GetComponent<SpriteRenderer>();
                renderer.sprite = sprite;
            }
            else if (c.Proxy.Type == ECharType.Critter)
            {
                var sprite = CharSpriteLoader.Instance.GetCritterDeadSprite(c);
                var renderer = c.GameHandle.GetComponent<SpriteRenderer>();
                renderer.sprite = sprite;
            }
        }

        public void AssignDeadLayer(CChar c)
        {
            foreach(var subcomponent in c.SubComponents)
            {
                var sub = subcomponent.Value;
                var renderer = sub.GetComponent<SpriteRenderer>();
                var layer = renderer.sortingLayerName;
                renderer.sortingLayerName = layer.Replace(SortingLayers.CHAR, SortingLayers.DEAD);
            }
            foreach(var embed in c.Embedded)
            {
                var renderer = embed.GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    var layer = renderer.sortingLayerName;
                    renderer.sortingLayerName = layer.Replace(SortingLayers.CHAR, SortingLayers.DEAD);
                }
            }
        }

        public void AssignPlusLayer(CChar c)
        {
            foreach (var subcomponent in c.SubComponents)
            {
                var sub = subcomponent.Value;
                var renderer = sub.GetComponent<SpriteRenderer>();
                var layer = renderer.sortingLayerName;
                renderer.sortingLayerName = layer.Replace(SortingLayers.CHAR, SortingLayers.PLUS);
            }
            foreach (var embed in c.Embedded)
            {
                var renderer = embed.GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    var layer = renderer.sortingLayerName;
                    renderer.sortingLayerName = layer.Replace(SortingLayers.CHAR, SortingLayers.PLUS);
                }
            }
        }

        public void AssignDeadWeapons(CChar c)
        {
            if (c.Proxy.GetLWeapon() != null)
            {
                RotateTranslateUtil.Instance.RandomRotateAndTranslate(
                    c.SubComponents[SortingLayers.CHAR_L_WEAPON],
                    ViewParams.SPLATTER_VARIANCE);
            }
            if (c.Proxy.GetRWeapon() != null)
            {
                RotateTranslateUtil.Instance.RandomRotateAndTranslate(
                    c.SubComponents[SortingLayers.CHAR_R_WEAPON],
                    ViewParams.SPLATTER_VARIANCE);
            }
        }

        public void AssignDeathSplatter(CChar c)
        {
            var data = new EvSplatterData();
            data.DmgPercent = 0.50;
            data.Target = c.GameHandle;
            var e = new EvSplatter(data);
            e.TryProcess();
        }

        public void ProcessGearExplosion(CChar c)
        {
            if (c.Proxy.Type == ECharType.Humanoid)
            {
                if (c.SubComponents.ContainsKey(SortingLayers.CHAR_TORSO))
                {
                    var renderer = c.SubComponents[SortingLayers.CHAR_TORSO].GetComponent<SpriteRenderer>();
                    renderer.sprite = null;
                }
                if (c.SubComponents.ContainsKey(SortingLayers.CHAR_HEAD))
                {
                    var renderer = c.SubComponents[SortingLayers.CHAR_HEAD].GetComponent<SpriteRenderer>();
                    renderer.sprite = null;
                }
                if (c.SubComponents.ContainsKey(SortingLayers.CHAR_HEAD_DECO_1))
                {
                    var renderer = c.SubComponents[SortingLayers.CHAR_HEAD_DECO_1].GetComponent<SpriteRenderer>();
                    renderer.sprite = null;
                }
                if (c.SubComponents.ContainsKey(SortingLayers.CHAR_HEAD_DECO_2))
                {
                    var renderer = c.SubComponents[SortingLayers.CHAR_HEAD_DECO_2].GetComponent<SpriteRenderer>();
                    renderer.sprite = null;
                }
                if (c.SubComponents.ContainsKey(SortingLayers.CHAR_FACE))
                {
                    var renderer = c.SubComponents[SortingLayers.CHAR_FACE].GetComponent<SpriteRenderer>();
                    renderer.sprite = null;
                }
                if (c.Proxy.GetArmor() != null)
                {
                    var script = c.SubComponents[SortingLayers.CHAR_ARMOR].AddComponent<SFightFatalityExplosionMove>();
                    script.Init(c.SubComponents[SortingLayers.CHAR_ARMOR], c);
                }
                if (c.Proxy.GetHelm() != null)
                {
                    var script = c.SubComponents[SortingLayers.CHAR_HELM].AddComponent<SFightFatalityExplosionMove>();
                    script.Init(c.SubComponents[SortingLayers.CHAR_HELM], c);
                }
                if (c.Proxy.GetLWeapon() != null)
                {
                    var script = c.SubComponents[SortingLayers.CHAR_L_WEAPON].AddComponent<SFightFatalityExplosionMove>();
                    script.Init(c.SubComponents[SortingLayers.CHAR_L_WEAPON], c);
                }
                if (c.Proxy.GetRWeapon() != null)
                {
                    var script = c.SubComponents[SortingLayers.CHAR_R_WEAPON].AddComponent<SFightFatalityExplosionMove>();
                    script.Init(c.SubComponents[SortingLayers.CHAR_R_WEAPON], c);
                }
                if (c.SubComponents.ContainsKey(SortingLayers.CHAR_TORSO_DECO_1))
                {
                    var renderer = c.SubComponents[SortingLayers.CHAR_TORSO_DECO_1].GetComponent<SpriteRenderer>();
                    renderer.sprite = null;
                }
                if (c.SubComponents.ContainsKey(SortingLayers.CHAR_TORSO_DECO_2))
                {
                    var renderer = c.SubComponents[SortingLayers.CHAR_TORSO_DECO_2].GetComponent<SpriteRenderer>();
                    renderer.sprite = null;
                }
            }
            else
            {
                var renderer = c.SubComponents[SortingLayers.CHAR_TORSO].GetComponent<SpriteRenderer>();
                renderer.sprite = null;
            }
        }

        public void RandomTranslateRotateOnDeath(CChar c)
        {
            RotateTranslateUtil.Instance.RandomRotateAndTranslate(
                c.GameHandle,
                ViewParams.SPLATTER_VARIANCE);
        }

        public void ProcessDeadChar(CChar c)
        {
            this.AssignDeadWeapons(c);
            this.AssignDeadEyes(c);
            this.AssignDeadLayer(c);
            this.RandomTranslateRotateOnDeath(c);
            this.AssignDeathSplatter(c);
            foreach (var kvp in c.View.EffectParticlesDict)
                GameObject.Destroy(kvp.Value);
        }

        public void SetBodyViewComponentsNull(CChar c)
        {
            foreach (var sub in c.SubComponents)
            {
                if (!sub.Key.ToLowerInvariant().Contains("Weapon") &&
                    !sub.Key.ToLowerInvariant().Contains("Armor"))
                {
                    var renderer = sub.Value.GetComponent<SpriteRenderer>();
                    if (renderer != null)
                    {
                        renderer.sprite = null;
                    }
                }
            }
        }

        public void UnassignPlusLayer(CChar c)
        {
            foreach (var subcomponent in c.SubComponents)
            {
                var sub = subcomponent.Value;
                var renderer = sub.GetComponent<SpriteRenderer>();
                var layer = renderer.sortingLayerName;
                renderer.sortingLayerName = layer.Replace(SortingLayers.PLUS, SortingLayers.CHAR);
            }
            foreach (var embed in c.Embedded)
            {
                var renderer = embed.GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    var layer = renderer.sortingLayerName;
                    renderer.sortingLayerName = layer.Replace(SortingLayers.PLUS, SortingLayers.CHAR);
                }
            }
        }
    }
}
