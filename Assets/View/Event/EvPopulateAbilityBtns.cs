using Assets.Controller.Manager.Combat;
using Assets.Controller.Manager.GUI;
using Assets.Model.Ability;
using Assets.Model.Character.Enum;
using Assets.View.Script.GUI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Event
{
    public class EvPopulateAbilityBtns : MGuiEv
    {
        private class EvPopulateAbilityBtnData
        {
            public MAbility Ability { get; set; }
            public bool LWeapon { get; set; }
            public bool IsWeapon { get; set; }
        }

        private readonly Vector3 SCALE = new Vector3(0.75f, 0.75f);
        private readonly float X_AXIS_INCREMENT = 15f;
        private readonly float X_AXIS_ORIGIN = -72.5f;
        private readonly float Y_AXIS_ORIGIN = 57.5f;

        public EvPopulateAbilityBtns() : base(EGuiEv.PopulateWpnBtns) {  }

        public override void TryProcess()
        {
            base.TryProcess();
            GUIManager.Instance.DestroyAbilityBtns();
            var curr = CombatManager.Instance.GetCurrentlyActing();
            var abilities = new List<EvPopulateAbilityBtnData>();
            if (curr.Proxy.Type == ECharType.Humanoid)
            {
                var left = curr.Proxy.GetLWeapon();
                var right = curr.Proxy.GetRWeapon();

                if (left != null)
                {
                    foreach (var ability in left.Model.Data.Abilities)
                    {
                        var data = new EvPopulateAbilityBtnData();
                        data.Ability = ability;
                        data.IsWeapon = true;
                        data.LWeapon = true;
                        abilities.Add(data);
                    }
                }
                if (right != null)
                {
                    foreach (var ability in right.Model.Data.Abilities)
                    {
                        var data = new EvPopulateAbilityBtnData();
                        data.Ability = ability;
                        data.IsWeapon = true;
                        data.LWeapon = false;
                        abilities.Add(data);
                    }
                }
            }
            foreach (var ability in curr.Proxy.GetActiveAbilities())
            {
                var data = new EvPopulateAbilityBtnData();
                data.Ability = ability;
                data.IsWeapon = false;
                data.LWeapon = false;
                abilities.Add(data);
            }
            this.ProcessAbilities(abilities);
        }

        private void ProcessAbilities(List<EvPopulateAbilityBtnData> abilities)
        {
            var actingBox = GameObject.FindGameObjectWithTag(GameObjectTags.ACTING_BOX);
            var protoBtnContainer = GameObject.FindGameObjectWithTag(GameObjectTags.PROTO_ABILITY_BTN);
            var protoBtn = protoBtnContainer.GetComponent<Button>();
            var protoBtnTransform = protoBtn.GetComponent<RectTransform>();
            var protoImg = protoBtnContainer.GetComponent<Image>();

            for (int i = 0; i < abilities.Count; i++)
            {
                var clone = new GameObject(abilities[i].Ability.Type.ToString());
                clone.transform.SetParent(actingBox.transform);
                clone.layer = Layers.UI;
                clone.transform.localScale = SCALE;
                clone.transform.localPosition = new Vector3(X_AXIS_ORIGIN + (X_AXIS_INCREMENT * i), Y_AXIS_ORIGIN);
                var transform = clone.AddComponent<RectTransform>();
                transform.sizeDelta = protoBtnTransform.sizeDelta;
                var btn = clone.AddComponent<Button>();
                btn.colors = protoBtn.colors;
                btn.transition = protoBtn.transition;
                var img = clone.AddComponent<Image>();
                img.sprite = protoImg.sprite;
                img.color = protoImg.color;
                btn.targetGraphic = img;
                var script = clone.AddComponent<SAbilityBtn>();
                script.Init(clone);
                script.SetAbility(abilities[i].Ability.Type, abilities[i].LWeapon, abilities[i].IsWeapon);
                GUIManager.Instance.AddAbilityBtn(clone);
            }
        }
    }
}
