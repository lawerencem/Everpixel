using Assets.Model.Ability.Enum;
using Assets.Model.Ability.Logic.Calculator;
using Controller.Managers;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;

namespace Assets.Model.Ability.Logic
{
    public class TypeLogic
    {
        private CalculatorContainer _calcContainer;
        
        public TypeLogic()
        {
            this._calcContainer = new CalculatorContainer();
        }

        public void PredictBullet(HitInfo hit)
        {
            this._calcContainer.DodgeCalc.Predict(hit);
            this._calcContainer.BlockCalc.Predict(hit);
            this._calcContainer.DmgCalc.Predict(hit);
            this._calcContainer.ResistCalc.Predict(hit);
        }

        public void PredictInstant(HitInfo hit)
        {
            this._calcContainer.CritCalc.Predict(hit);
            this._calcContainer.DmgCalc.Predict(hit);
            this._calcContainer.ResistCalc.Predict(hit);
        }

        public void PredictMelee(HitInfo hit)
        {
            this._calcContainer.DodgeCalc.Predict(hit);
            this._calcContainer.BlockCalc.Predict(hit);
            this._calcContainer.CritCalc.Predict(hit);
            this._calcContainer.ParryCalc.Predict(hit);
            this._calcContainer.DmgCalc.Predict(hit);
            this._calcContainer.ResistCalc.Predict(hit);
        }

        public void PredictRay(HitInfo hit)
        {
            this._calcContainer.DodgeCalc.Predict(hit);
            this._calcContainer.CritCalc.Predict(hit);
            this._calcContainer.DmgCalc.Predict(hit);
            this._calcContainer.ResistCalc.Predict(hit);
        }

        public void ProcessBullet(HitInfo hit)
        {
            this.ProcessBulletFlags(hit);
            this._calcContainer.DmgCalc.CalculateAbilityDmg(hit);
            this._calcContainer.DmgCalc.ModifyDmgViaDefender(hit);
        }

        public void ProcessInstant(HitInfo hit)
        {

        }

        public void ProcessMelee(HitInfo hit)
        {
            this.ProcessMeleeFlags(hit);
            this._calcContainer.DmgCalc.CalculateAbilityDmg(hit);
            this._calcContainer.DmgCalc.ModifyDmgViaDefender(hit);
        }

        public void ProcessRay(HitInfo hit)
        {
            this.ProcessBulletFlags(hit);
            this._calcContainer.DmgCalc.CalculateAbilityDmg(hit);
            this._calcContainer.DmgCalc.ModifyDmgViaDefender(hit);
        }

        public bool ProcessResist(HitInfo hit)
        {
            // TODO
            return false;
        }

        public void ProcessShapeshift(HitInfo hit)
        {
            var shapeshiftEvent = new ShapeshiftEvent(CombatEventManager.Instance, hit);
        }

        public void ProcessSong(HitInfo hit)
        {

        }

        public void ProcessSummon(HitInfo hit)
        {
            var summonEvent = new SummonEvent(CombatEventManager.Instance, hit);
        }

        private void ProcessBulletFlags(HitInfo hit)
        {
            this._calcContainer.DodgeCalc.Process(hit);
            this._calcContainer.BlockCalc.Process(hit);
            this._calcContainer.CritCalc.Process(hit);
            this._calcContainer.HeadShotCalc.Process(hit);
            this._calcContainer.ResistCalc.Process(hit);
        }

        private void ProcessMeleeFlags(HitInfo hit)
        {
            this._calcContainer.DodgeCalc.Process(hit);
            this._calcContainer.ParryCalc.Process(hit);
            this._calcContainer.BlockCalc.Process(hit);
            this._calcContainer.CritCalc.Process(hit);
            this._calcContainer.HeadShotCalc.Process(hit);
            this._calcContainer.ResistCalc.Process(hit);
        }
    }
}
