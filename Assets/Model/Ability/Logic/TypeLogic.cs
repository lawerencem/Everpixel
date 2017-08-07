using Assets.Model.Ability.Logic.Calculator;
using Assets.Model.Combat;

namespace Assets.Model.Ability.Logic
{
    public class TypeLogic
    {
        private CalculatorContainer _calcContainer;
        
        public TypeLogic()
        {
            this._calcContainer = new CalculatorContainer();
        }

        public void PredictBullet(Hit hit)
        {
            this._calcContainer.DodgeCalc.Predict(hit);
            this._calcContainer.BlockCalc.Predict(hit);
            this._calcContainer.DmgCalc.Predict(hit);
            this._calcContainer.ResistCalc.Predict(hit);
        }

        public void PredictInstant(Hit hit)
        {
            this._calcContainer.CritCalc.Predict(hit);
            this._calcContainer.DmgCalc.Predict(hit);
            this._calcContainer.ResistCalc.Predict(hit);
        }

        public void PredictMelee(Hit hit)
        {
            this._calcContainer.DodgeCalc.Predict(hit);
            this._calcContainer.BlockCalc.Predict(hit);
            this._calcContainer.CritCalc.Predict(hit);
            this._calcContainer.ParryCalc.Predict(hit);
            this._calcContainer.DmgCalc.Predict(hit);
            this._calcContainer.ResistCalc.Predict(hit);
        }

        public void PredictRay(Hit hit)
        {
            this._calcContainer.DodgeCalc.Predict(hit);
            this._calcContainer.CritCalc.Predict(hit);
            this._calcContainer.DmgCalc.Predict(hit);
            this._calcContainer.ResistCalc.Predict(hit);
        }

        public void ProcessBullet(Hit hit)
        {
            this.ProcessBulletFlags(hit);
            this._calcContainer.DmgCalc.CalculateAbilityDmg(hit);
            this._calcContainer.DmgCalc.ModifyDmgViaDefender(hit);
        }

        public void ProcessInstant(Hit hit)
        {

        }

        public void ProcessMelee(Hit hit)
        {
            this.ProcessMeleeFlags(hit);
            this._calcContainer.DmgCalc.CalculateAbilityDmg(hit);
            this._calcContainer.DmgCalc.ModifyDmgViaDefender(hit);
        }

        public void ProcessRay(Hit hit)
        {
            this.ProcessBulletFlags(hit);
            this._calcContainer.DmgCalc.CalculateAbilityDmg(hit);
            this._calcContainer.DmgCalc.ModifyDmgViaDefender(hit);
        }

        public bool ProcessResist(Hit hit)
        {
            // TODO
            return false;
        }

        public void ProcessShapeshift(Hit hit)
        {
            //var shapeshiftEvent = new ShapeshiftEvent(CombatEventManager.Instance, hit);
        }

        public void ProcessSong(Hit hit)
        {

        }

        public void ProcessSummon(Hit hit)
        {
            //var summonEvent = new EvSummon(CombatEventManager.Instance, hit);
        }

        private void ProcessBulletFlags(Hit hit)
        {
            this._calcContainer.DodgeCalc.Process(hit);
            this._calcContainer.BlockCalc.Process(hit);
            this._calcContainer.CritCalc.Process(hit);
            this._calcContainer.HeadShotCalc.Process(hit);
            this._calcContainer.ResistCalc.Process(hit);
        }

        private void ProcessMeleeFlags(Hit hit)
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
