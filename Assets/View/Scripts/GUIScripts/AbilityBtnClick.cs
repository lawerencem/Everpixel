using System;
using Controller.Managers;
using Model.Abilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Model.Events.Combat;

namespace View.Scripts
{
    public class AbilityBtnClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private EAbility _ability;

        private void OnClick()
        {
            var e = new AttackSelectedEvent(CombatEventManager.Instance, this._ability);
        }

        public void Init(GameObject toAdd, EAbility ability)
        {
            this._ability = ability;
            var btnContainer = toAdd.GetComponent<Button>();
            btnContainer.onClick.AddListener(this.OnClick);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            CombatEventManager.Instance.LockGUI();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CombatEventManager.Instance.UnlockGUI();
        }
    }
}
