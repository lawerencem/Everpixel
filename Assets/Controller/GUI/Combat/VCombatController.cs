using Assets.Controller.Character;
using Assets.Model.Ability.Enum;
using Assets.Model.Action;
using Assets.Model.Combat.Hit;
using Assets.Template.CB;
using Assets.Template.Script;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Controller.GUI.Combat
{
    public class VCombatController : ICallback
    {
        private List<Callback> _callbacks;

        private static VCombatController _instance;
        public static VCombatController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new VCombatController();
                return _instance;
            }
        }

        public VCombatController()
        {
            this._callbacks = new List<Callback>();
        }

        public void AddCallback(Callback callback)
        {
            this._callbacks.Add(callback);
        }

        public void DisplayNewAction(MAction a)
        {
            this._callbacks.Clear();
            switch (a.ActiveAbility.Data.CastType)
            {
                case (ECastType.Arc): { VHitController.Instance.ProcessCustomFX(a); } break;
                case (ECastType.Bullet): { VHitController.Instance.ProcessBulletFX(a); } break;
                case (ECastType.Custom): { VHitController.Instance.ProcessCustomFX(a); } break;
                case (ECastType.Melee): { VHitController.Instance.ProcessMeleeHitFX(a); } break;
                case (ECastType.Melee_Raycast): { VHitController.Instance.ProcessMeleeHitFX(a); } break;
                case (ECastType.Raycast): { VHitController.Instance.ProcessRaycastFX(a); } break;
                case (ECastType.Ringcast): { VHitController.Instance.ProcessCustomFX(a); } break;
                case (ECastType.Shapeshift): { VHitController.Instance.ProcessShapeshiftFX(a); } break;
                case (ECastType.Single): { VHitController.Instance.ProcessSingleHitFX(a); } break;
                case (ECastType.Song): { VHitController.Instance.ProcessSongFX(a); } break;
            }
        }

        public void DisplayActionEventName(MAction a)
        {
            var data = new HitDisplayData();
            data.Color = CombatGUIParams.WHITE;
            data.Hit = null;
            data.Target = a.Data.Source.GameHandle;
            data.Text = a.ActiveAbility.Type.ToString().Replace("_", " ");
            data.YOffset = CombatGUIParams.FLOAT_OFFSET;
            data.Display();
        }

        public void DisplayText(string text, CChar tgt)
        {
            var data = new HitDisplayData();
            data.Color = CombatGUIParams.WHITE;
            data.Hit = null;
            data.Target = tgt.GameHandle;
            data.Text = text;
            data.YOffset = CombatGUIParams.FLOAT_OFFSET;
            data.Display();
        }

        public void DisplayText(HitDisplayData data)
        {
            var parent = GameObject.FindGameObjectWithTag("WorldSpaceCanvas");
            var display = new GameObject();
            display.transform.position = (data.Target.transform.position);
            var text = display.AddComponent<Text>();
            var position = data.Target.transform.position;
            position.y += data.YOffset;
            text.alignment = TextAnchor.MiddleCenter;
            text.color = data.Color;
            text.fontSize = 16;
            text.rectTransform.SetParent(parent.transform);
            text.rectTransform.position = position;
            text.rectTransform.sizeDelta = new Vector2(200f, 200f);
            text.rectTransform.localScale = new Vector3(0.0075f, 0.0075f);
            text.name = "Hit Text";
            text.text = data.Text;
            Font fontToUse = Resources.Load("Fonts/8bitOperatorPlus8-Bold") as Font;
            text.font = fontToUse;
            var script = display.AddComponent<SDestroyByLifetime>();
            script.AddCallback(data.CallbackHandler);
            script.Init(display, data.Dur);
            var floating = display.AddComponent<SFloatingText>();
            floating.Init(display, 0.0015f, data.Delay);


        }

        public void DoCallbacks()
        {
            foreach (var callback in this._callbacks)
                callback(this);
        }

        public void SetCallback(Callback callback)
        {
            this._callbacks = new List<Callback>() { callback };
        }
    }
}
