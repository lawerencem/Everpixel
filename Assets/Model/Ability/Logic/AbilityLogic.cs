using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.Combat.Hit;
using System.Collections.Generic;

namespace Assets.Model.Ability.Logic
{
    public class AbilityLogic
    {
        private AoELogic _aoeLogic;
        private AbilityCalcLogic _calcLogic;

        public AbilityLogic()
        {
            this._aoeLogic = new AoELogic();
            this._calcLogic = new AbilityCalcLogic();
        }

        public List<CTile> GetAdjacentTiles(CChar c)
        {
            return this._aoeLogic.GetAdjacentTiles(c);
        }

        public List<CTile> GetArcCastTiles(AbilityArgs arg)
        {
            return this._aoeLogic.GetArcCastTiles(arg);
        }

        public List<CTile> GetAoETiles(AbilityArgs arg, int aoe)
        {
            return this._aoeLogic.GetAoETiles(arg, aoe);
        }

        public List<CTile> GetRaycastTiles(AbilityArgs arg)
        {
            return this._aoeLogic.GetRaycastTiles(arg);
        }

        public List<CTile> GetRingCastTiles(AbilityArgs arg)
        {
            return this._aoeLogic.GetRingCastTiles(arg);
        }

        public List<CTile> GetPotentialTargets(AbilityArgs arg)
        {
            return this._aoeLogic.GetPotentialTargets(arg);
        }

        public void PredictBullet(MHit hit)
        {
            this._calcLogic.PredictBullet(hit);
        }

        public void PredictMelee(MHit hit)
        {
            this._calcLogic.PredictMelee(hit);
        }

        public void PredictRay(MHit hit)
        {
            this._calcLogic.PredictRay(hit);
        }

        public void PredictSingle(MHit hit)
        {
            this._calcLogic.PredictSingle(hit);
        }

        public void ProcessBullet(MHit hit)
        {
            this._calcLogic.ProcessBullet(hit);
        }

        public void ProcessMelee(MHit hit)
        {
            this._calcLogic.ProcessMelee(hit);
        }

        public void ProcessRay(MHit hit)
        {
            this._calcLogic.ProcessRay(hit);
        }

        public void ProcessResist(MHit hit)
        {
            this._calcLogic.ProcessResist(hit);
        }

        public void ProcessShapeshift(MHit hit)
        {
            this._calcLogic.ProcessShapeshift(hit);
        }

        public void ProcessSingle(MHit hit)
        {
            this._calcLogic.ProcessSingle(hit);
        }

        public void ProcessSong(MHit hit)
        {
            this._calcLogic.ProcessSong(hit);
        }

        public void ProcessSummon(MHit hit)
        {
            this._calcLogic.ProcessSummon(hit);
        }
    }
}
