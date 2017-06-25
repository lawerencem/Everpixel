using System;
using Controller.Managers;
using Model.Abilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using View.Events;
using View.GUI;
using Model.Events.Combat;

namespace View.Scripts
{
    public class AbilityBtnClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private ActiveAbilitiesEnum _ability;

        private void OnClick()
        {
            bool tileSelectable = ActiveAbilityFactory.Instance.TileSelectable(this._ability);
            var e = new AttackSelectedEvent(CombatEventManager.Instance, this._ability);
        }

        public void Init(GameObject toAdd, ActiveAbilitiesEnum ability)
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
