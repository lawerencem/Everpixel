using Controller.Managers;
using Generics;
using Model.Abilities;
using Model.Characters;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View.Scripts;

namespace View.GUI
{
    public class AbilitiesModal : MonoBehaviour
    {
        private const string MODAL = "ActiveModalHeaderTag";
        private const string PROTO = "ProtoAbilityBtnTag";

        private List<GameObject> _btns = new List<GameObject>();
        private GameObject _modal;
        private GameObject _proto;

        public void Init()
        {
            this._modal = GameObject.FindGameObjectWithTag(MODAL);
            this._proto = GameObject.FindGameObjectWithTag(PROTO);
            this.SetModalInactive();
        }

        public void SetModalActive()
        {
            if (!this._modal.activeSelf)
                this._modal.SetActive(true);
            this.SetModalValues();
        }

        public void SetModalInactive()
        {
            this._modal.SetActive(false);
            foreach (var controller in this._btns) { GameObject.Destroy(controller); }
        }

        public void SetModalValues()
        {
            var character = CombatEventManager.Instance.GetCurrentCharacter();
            this._proto.SetActive(true);
            var protoBtn = this._proto.GetComponent<Button>();
            var protoCollider = this._proto.GetComponent<Collider2D>();
            var protoImg = this._proto.GetComponent<Image>();
            var protoTxtContainer = GameObject.FindGameObjectWithTag("ProtoAbilityBtnTextTag");
            var protoTxt = protoTxtContainer.GetComponent<Text>();
            for (int i = 0; i < character.Model.ActiveAbilities.Count; i++)
            {
                var clone = new GameObject();
                clone.transform.SetParent(this._modal.transform);
                var img = clone.AddComponent<Image>();
                img.sprite = protoImg.sprite;
                img.transform.localScale = protoImg.transform.localScale;
                img.color = protoImg.color;
                img.type = Image.Type.Sliced;
                img.rectTransform.sizeDelta = new Vector2(45, 20);
                var btn = clone.AddComponent<Button>();
                btn.colors = protoBtn.colors;
                var txtContainer = new GameObject();
                txtContainer.transform.SetParent(this._modal.transform);
                var txt = txtContainer.AddComponent<Text>();
                clone.transform.localScale = this._proto.transform.localScale;
                txt.transform.SetParent(txtContainer.transform);
                var rect = txt.rectTransform;
                rect.localScale = new Vector3(0.5f, 0.5f);
                txt.font = protoTxt.font;
                txt.fontSize = protoTxt.fontSize;
                txt.alignment = protoTxt.alignment;
                txt.horizontalOverflow = protoTxt.horizontalOverflow;
                txt.verticalOverflow = protoTxt.verticalOverflow;
                txt.transform.SetParent(clone.transform);
                txt.text = character.Model.ActiveAbilities[i].TypeStr;
                clone.transform.localPosition = new Vector3(-40, -45);
                this._btns.Add(clone);
                var btnScript = clone.AddComponent<AbilityBtnClick>();
                btnScript.Init(clone, (ActiveAbilitiesEnum)character.Model.ActiveAbilities[i].Type);
            }
            this._proto.SetActive(false);
        }
    }
}