using Assets.Model.Ability.Logic.Calculator;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Ability.Logic
{
    public class TypeLogic
    {
        private CalculatorContainer _calcContainer;
        
        public TypeLogic()
        {
            this._calcContainer = new CalculatorContainer();
        }

        public void PredictBullet(MHit hit)
        {
            this._calcContainer.DodgeCalc.Predict(hit);
            this._calcContainer.BlockCalc.Predict(hit);
            this._calcContainer.DmgCalc.Predict(hit);
            this._calcContainer.ResistCalc.Predict(hit);
        }

        public void PredictMelee(MHit hit)
        {
            this._calcContainer.DodgeCalc.Predict(hit);
            this._calcContainer.BlockCalc.Predict(hit);
            this._calcContainer.CritCalc.Predict(hit);
            this._calcContainer.ParryCalc.Predict(hit);
            this._calcContainer.DmgCalc.Predict(hit);
            this._calcContainer.ResistCalc.Predict(hit);
        }

        public void PredictRay(MHit hit)
        {
            this._calcContainer.DodgeCalc.Predict(hit);
            this._calcContainer.CritCalc.Predict(hit);
            this._calcContainer.DmgCalc.Predict(hit);
            this._calcContainer.ResistCalc.Predict(hit);
        }

        public void PredictSingle(MHit hit)
        {
            this._calcContainer.CritCalc.Predict(hit);
            this._calcContainer.DmgCalc.Predict(hit);
            this._calcContainer.ResistCalc.Predict(hit);
        }

        public void ProcessBullet(MHit hit)
        {
            this.ProcessBulletFlags(hit);
            this._calcContainer.DmgCalc.CalculateAbilityDmg(hit);
            this._calcContainer.DmgCalc.ModifyDmgViaDefender(hit);
        }

        public void ProcessSingle(MHit hit)
        {
            this._calcContainer.DmgCalc.CalculateAbilityDmg(hit);
            this._calcContainer.DmgCalc.ModifyDmgViaDefender(hit);
        }

        public void ProcessMelee(MHit hit)
        {
            this.ProcessMeleeFlags(hit);
            this._calcContainer.DmgCalc.CalculateAbilityDmg(hit);
            this._calcContainer.DmgCalc.ModifyDmgViaDefender(hit);
        }

        public void ProcessRay(MHit hit)
        {
            this.ProcessBulletFlags(hit);
            this._calcContainer.DmgCalc.CalculateAbilityDmg(hit);
            this._calcContainer.DmgCalc.ModifyDmgViaDefender(hit);
        }

        public bool ProcessResist(MHit hit)
        {
            // TODO
            return false;
        }

        public void ProcessShapeshift(MHit hit)
        {
            //var shapeshiftEvent = new ShapeshiftEvent(CombatEventManager.Instance, hit);
        }

        public void ProcessSong(MHit hit)
        {

        }

        public void ProcessSummon(MHit hit)
        {
            //var summonEvent = new EvSummon(CombatEventManager.Instance, hit);
        }

        private void ProcessBulletFlags(MHit hit)
        {
            this._calcContainer.DodgeCalc.Process(hit);
            this._calcContainer.BlockCalc.Process(hit);
            this._calcContainer.CritCalc.Process(hit);
            this._calcContainer.HeadShotCalc.Process(hit);
            this._calcContainer.ResistCalc.Process(hit);
        }

        private void ProcessMeleeFlags(MHit hit)
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
