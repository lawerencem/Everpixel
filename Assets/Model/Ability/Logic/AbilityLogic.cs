using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.Combat.Hit;
using System.Collections.Generic;

namespace Assets.Model.Ability.Logic
{
    public class AbilityLogic
    {
        public List<CTile> GetAdjacentTiles(CChar c)
        {
            var logic = new AoELogic();
            return logic.GetAdjacentTiles(c);
        }

        public List<CTile> GetArcCastTiles(AbilityArgs arg)
        {
            var logic = new AoELogic();
            return logic.GetArcCastTiles(arg);
        }

        public List<CTile> GetAoETiles(AbilityArgs arg, int aoe)
        {
            var logic = new AoELogic();
            return logic.GetAoETiles(arg, aoe);
        }

        public List<CTile> GetTargetableRaycastTiles(AbilityArgs arg)
        {
            var logic = new AoELogic();
            return logic.GetTargetableRaycastTiles(arg);
        }

        public List<CTile> GetTargetedRaycastTiles(AbilityArgs arg)
        {
            var logic = new AoELogic();
            return logic.GetRaycastTilesViaSourceAndTarget(arg);
        }

        public List<CTile> GetRingCastTiles(AbilityArgs arg)
        {
            var logic = new AoELogic();
            return logic.GetRingCastTiles(arg);
        }

        public List<CTile> GetPotentialTargets(AbilityArgs arg)
        {
            var logic = new AoELogic();
            return logic.GetPotentialTargets(arg);
        }

        public void PredictBullet(MHit hit)
        {
            var logic = new AbilityCalcLogic();
            logic.PredictBullet(hit);
        }

        public void PredictMelee(MHit hit)
        {
            var logic = new AbilityCalcLogic();
            logic.PredictMelee(hit);
        }

        public void PredictRay(MHit hit)
        {
            var logic = new AbilityCalcLogic();
            logic.PredictRay(hit);
        }

        public void PredictSingle(MHit hit)
        {
            var logic = new AbilityCalcLogic();
            logic.PredictSingle(hit);
        }

        public void ProcessBullet(MHit hit)
        {
            var logic = new AbilityCalcLogic();
            logic.ProcessBullet(hit);
        }

        public void ProcessBulletStrayPossible(MHit hit)
        {
            var logic = new AbilityCalcLogic();
            logic.ProcessBulletStrayPossible(hit);
        }

        public void ProcessMelee(MHit hit)
        {
            var logic = new AbilityCalcLogic();
            logic.ProcessMelee(hit);
        }

        public void ProcessRay(MHit hit)
        {
            var logic = new AbilityCalcLogic();
            logic.ProcessRay(hit);
        }

        public void ProcessResist(MHit hit)
        {
            var logic = new AbilityCalcLogic();
            logic.ProcessResist(hit);
        }

        public void ProcessShapeshift(MHit hit)
        {
            var logic = new AbilityCalcLogic();
            logic.ProcessShapeshift(hit);
        }

        public void ProcessSingle(MHit hit)
        {
            var logic = new AbilityCalcLogic();
            logic.ProcessSingle(hit);
        }

        public void ProcessSong(MHit hit)
        {
            var logic = new AbilityCalcLogic();
            logic.ProcessSong(hit);
        }

        public void ProcessSummon(MHit hit)
        {
            var logic = new AbilityCalcLogic();
            logic.ProcessSummon(hit);
        }
    }
}
