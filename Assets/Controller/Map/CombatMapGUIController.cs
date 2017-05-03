using Controller.Characters;
using Controller.Map;
using Generics.Hex;
using Generics.Scripts;
using Model.Map;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View.Biomes;

namespace Controller.Managers.Map
{
    public class CombatMapGuiController
    {
        private const string MAP_GUI_LAYER = "BackgroundTileGUI";
        private const string IMG = "ActingBoxImgTag";
        private const string NAME = "ActingBoxNameTag";
        private const string UI_LAYER = "UI";

        private const string ARMOR = "ArmorTextTag";
        private const string AP = "APTextTag";
        private const string HELM = "HelmTextTag";
        private const string HP = "HPTextTag";
        private const string L_WEAP = "LWeaponTextTag";
        private const string MORALE = "MoraleTextTag";
        private const string R_WEAP = "RWeaponTextTag";
        private const string STAM = "StaminaTextTag";

        private List<GameObject> _boxImages = new List<GameObject>();
        private List<GameObject> _tilePath = new List<GameObject>();

        public void ClearPotentialPathView()
        {
            foreach (var old in this._tilePath)
                GameObject.Destroy(old);
            this._tilePath.Clear();
        }

        public void SetActingBoxToController(GenericCharacterController c)
        {
            this.SetTagText(NAME, c.View.Name);
            this.SetTagText(AP, c.Model.CurrentAP.ToString() + " / " + c.Model.SecondaryStats.MaxAP);
            this.SetTagText(HP, c.Model.CurrentHP.ToString() + " / " + c.Model.SecondaryStats.MaxHP);
            this.SetTagText(STAM, c.Model.CurrentStamina.ToString() + " / " + c.Model.SecondaryStats.Stamina);
            this.SetTagText(MORALE, c.Model.CurrentMorale.ToString() + " / " + c.Model.SecondaryStats.Morale);
            this.SetTagText(STAM, c.Model.CurrentStamina.ToString() + " / " + c.Model.SecondaryStats.Stamina);

            if (c.Model.LWeapon != null)
                this.SetTagText(L_WEAP, c.Model.LWeapon.Name);
            else
                this.SetTagText(L_WEAP, "");    
            
            if (c.Model.RWeapon != null)
                this.SetTagText(R_WEAP, c.Model.RWeapon.Name);
            else
                this.SetTagText(R_WEAP, "");

            this.SetBoxImg(IMG, c);
        }

        public void DecoratePath(List<TileController> p)
        {
            foreach (var old in this._tilePath) { GameObject.Destroy(old); }

            if (p != null)
            {
                foreach (var tile in p)
                {
                    var tView = new GameObject();
                    var renderer = tView.AddComponent<SpriteRenderer>();
                    renderer.sprite = MapBridge.Instance.GetMovePathSprite();
                    renderer.transform.position = tile.Model.Center;
                    renderer.sortingLayerName = MAP_GUI_LAYER;
                    tView.name = "Path Tile";
                    var color = renderer.color;
                    color.a = 0.50f;
                    renderer.color = color;
                    this._tilePath.Add(tView);
                }
            }
        }

        private void SetBoxImg(string boxTag, GenericCharacterController c)
        {
            var box = GameObject.FindGameObjectWithTag(IMG);
            if (box != null)
            {
                foreach (var img in this._boxImages) { GameObject.Destroy(img); }
                this._boxImages.Clear();
                foreach(var h in c.SpriteHandlers)
                {
                    var parentPosition = h.transform.parent.position;
                    var xOffset = (h.transform.position.x - parentPosition.x) * 2.5f;
                    var yOffset = (h.transform.position.y - parentPosition.y) * 2.5f;
                    var renderer = h.GetComponent<SpriteRenderer>();
                    var tempImage = new GameObject();
                    var r = tempImage.AddComponent<SpriteRenderer>();
                    r.sprite = renderer.sprite;                    
                    r.sortingLayerName = (UI_LAYER + renderer.sortingLayerName);
                    var position = box.transform.position;
                    position.x += xOffset;
                    position.y += yOffset;
                    if (r.sortingLayerName != "UICharTorso")
                        r.transform.position = position;
                    else
                        r.transform.position = box.transform.position;
                    r.transform.localScale = new Vector3(2.5f, 2.5f);
                    r.name = "ActingImg";
                    r.transform.SetParent(box.transform);
                    this._boxImages.Add(tempImage);
                }
            }
        }
        
        private void SetTagText(string tag, string toSet)
        {
            var tagged = GameObject.FindGameObjectWithTag(tag);
            if (tagged != null)
            {
                var text = tagged.GetComponent<Text>();
                text.text = toSet;
            }
        }
    }
}
