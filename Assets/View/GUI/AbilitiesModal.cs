﻿using Controller.Managers;
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

        public void ResetModal()
        {
            foreach (var btn in this._btns)
                GameObject.Destroy(btn);
            if (this._modal.activeSelf)
                this.SetModalValues();
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
            var protoImg = this._proto.GetComponent<Image>();
            var protoTxtContainer = GameObject.FindGameObjectWithTag("ProtoAbilityBtnTextTag");
            var protoTxt = protoTxtContainer.GetComponent<Text>();
            var protoRect = protoTxtContainer.GetComponent<RectTransform>();
            var content = GameObject.FindGameObjectWithTag("ActiveModalContent");
            for (int i = 0; i < character.Model.ActiveAbilities.Count; i++)
            {
                var clone = new GameObject();
                clone.transform.SetParent(content.transform);
                clone.transform.localScale = protoBtn.transform.localScale;

                var rect = clone.AddComponent<RectTransform>();
                rect.sizeDelta = protoImg.rectTransform.sizeDelta;

                var img = clone.AddComponent<Image>();
                img.sprite = protoImg.sprite;
                img.color = protoImg.color;

                var cloneBtn = clone.AddComponent<Button>();
                cloneBtn.colors = protoBtn.colors;

                var cloneTxtContainer = new GameObject();
                cloneTxtContainer.transform.SetParent(clone.transform);

                var txt = cloneTxtContainer.AddComponent<Text>();
                txt.text = character.Model.ActiveAbilities[i].TypeStr;
                txt.font = protoTxt.font;
                txt.fontSize = protoTxt.fontSize;
                txt.color = protoTxt.color;
                txt.transform.localScale = protoTxt.transform.localScale;
                txt.alignment = protoTxt.alignment;
                txt.transform.SetParent(cloneTxtContainer.transform);

                var txtRect = cloneTxtContainer.GetComponent<RectTransform>();
                txtRect.localPosition = new Vector2(0, 0);

                this._btns.Add(clone);
                var btnScript = clone.AddComponent<AbilityBtnClick>();
                btnScript.Init(clone, (ActiveAbilitiesEnum)character.Model.ActiveAbilities[i].Type);
            }
            this._proto.SetActive(false);
        }
    }
}