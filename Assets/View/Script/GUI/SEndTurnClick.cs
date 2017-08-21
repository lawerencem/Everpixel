using Assets.Model.Event.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Script.GUI
{
    public class SEndTurnClick : SGuiButton
    {
        public override void OnClick()
        {
            if (base.IsValid())
            {
                var e = new EvEndTurn();
                e.TryProcess();
            }
        }

        public void Start()
        {
            var btnContainer = GameObject.FindGameObjectWithTag("EndTurnBtnTag");
            var btn = btnContainer.GetComponent<Button>();
            btn.onClick.AddListener(this.OnClick);
        }
    }
}
