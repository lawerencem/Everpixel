using Assets.Controller.Character;
using Assets.Controller.Manager.Combat;
using Assets.Model.Ability;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Script.GUI
{
    public class AbilityModalManager : SGui
    {
        private List<GameObject> _btns = new List<GameObject>();
        private GameObject _modal;
        private GameObject _proto;

        public static AbilityModalManager Instance = null;

        void Awake()
        {
            if (Instance == null)
                Instance = this;

            else if (Instance != this)
                Destroy(gameObject);
        }

        public void Init()
        {
            this._modal = GameObject.FindGameObjectWithTag(GameObjectTags.MODAL_HEADER);
            this._proto = GameObject.FindGameObjectWithTag(GameObjectTags.MODAL_BTN_PROTO);
            this._proto.SetActive(false);
        }

        public void ResetModal()
        {
            foreach (var btn in this._btns)
                GameObject.Destroy(btn);
            this._btns.Clear();
        }

        public void ProcessNewModalValues()
        {
            this.ResetModal();
            var character = CombatManager.Instance.GetCurrentlyActing();
            var abilities = character.Model.GetAbilities().GetActiveAbilities();
            this._proto.SetActive(true);
            for (int i = 0; i < abilities.Count; i++)
                this.PopulateModalList(character, abilities[i]);
            this._proto.SetActive(false);
        }

        private void PopulateModalList(CharController character, MAbility ability)
        {
            var protoBtn = this._proto.GetComponent<Button>();
            var protoImg = this._proto.GetComponent<Image>();
            var protoTxtContainer = GameObject.FindGameObjectWithTag("ProtoAbilityBtnTextTag");
            var protoTxt = protoTxtContainer.GetComponent<Text>();
            var protoRect = protoTxtContainer.GetComponent<RectTransform>();
            var content = GameObject.FindGameObjectWithTag("ActiveModalContent");

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
            txt.text = ability.Type.ToString().Replace("_", " ");
            txt.font = protoTxt.font;
            txt.fontSize = protoTxt.fontSize;
            txt.color = protoTxt.color;
            txt.transform.localScale = protoTxt.transform.localScale;
            txt.alignment = protoTxt.alignment;
            txt.transform.SetParent(cloneTxtContainer.transform);

            var txtRect = cloneTxtContainer.GetComponent<RectTransform>();
            txtRect.localPosition = new Vector2(0, 0);
            // TODO: Would really love a better way to do this, but it works for now.
            txtRect.sizeDelta = new Vector2(300, 60);

            var data = new SAbilityBtnData();
            data.Ability = ability.Type;
            data.Handle = clone;
            var script = clone.AddComponent<SAbilityBtn>();
            script.Init(data);

            this._btns.Add(clone);
        }
    }
}