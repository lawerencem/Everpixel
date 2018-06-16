using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Model.Character.Enum;
using Assets.Model.Effect;
using UnityEngine;

namespace Assets.Model.Event.Combat
{
    public class EvStunDoneData
    {
        public CChar Target { get; set; }
    }

    public class EvStunDone : MEvCombat
    {
        private EvStunDoneData _data;

        public EvStunDone(EvStunDoneData data) : base(ECombatEv.StunDone)
        {
            this._data = data;
        }

        public override void TryProcess()
        {
            base.TryProcess();
            this.UndoStun();
        }

        private void UndoStun()
        {
            FCharacterStatus.SetStunnedFalse(this._data.Target.Proxy.GetStatusFlags());
            var view = this._data.Target.View;
            if (view.EffectParticlesDict.ContainsKey(EEffect.Stun))
            {
                GameObject.Destroy(view.EffectParticlesDict[EEffect.Stun]);
                view.EffectParticlesDict.Remove(EEffect.Stun);
            }
            VCombatController.Instance.DisplayText("Stun", this._data.Target, CombatGUIParams.GREEN);
        }
    }
}
