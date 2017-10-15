using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.Combat.Hit;
using System.Collections.Generic;

namespace Assets.Model.Ability.Logic
{
    public class AbilityLogic
    {
        private AoELogic _aoeLogic;
        private TileLogic _tileLogic;
        private TypeLogic _typeLogic;

        public AbilityLogic()
        {
            this._aoeLogic = new AoELogic();
            this._tileLogic = new TileLogic();
            this._typeLogic = new TypeLogic();
        }

        public List<CTile> GetAdjacentTiles(CChar c) { return this._aoeLogic.GetAdjacentTiles(c); }
        public List<CTile> GetAoETiles(AbilityArgs arg, int aoe) {return this._aoeLogic.GetAoETiles(arg, aoe);}
        public List<CTile> GetRaycastTiles(AbilityArgs arg) { return this._aoeLogic.GetRaycastTiles(arg);}
        public List<CTile> GetPotentialTargets(AbilityArgs arg) { return this._aoeLogic.GetPotentialTargets(arg);}

        public bool IsValidEmptyTile(AbilityArgs arg) { return this._tileLogic.IsValidEmptyTile(arg); }
        public bool IsValidEnemyTarget(AbilityArgs arg) { return this._tileLogic.IsValidEnemyTarget(arg); }

        public void PredictBullet(MHit hit) { this._typeLogic.PredictBullet(hit); }
        public void PredictMelee(MHit hit) { this._typeLogic.PredictMelee(hit); }
        public void PredictRay(MHit hit) { this._typeLogic.PredictRay(hit); }
        public void PredictSingle(MHit hit) { this._typeLogic.PredictSingle(hit); }
        public void ProcessBullet(MHit hit) { this._typeLogic.ProcessBullet(hit); }
        public void ProcessMelee(MHit hit) { this._typeLogic.ProcessMelee(hit); }
        public void ProcessRay(MHit hit) { this._typeLogic.ProcessRay(hit); }
        public void ProcessResist(MHit hit) { this._typeLogic.ProcessResist(hit); }
        public void ProcessShapeshift(MHit hit) { this._typeLogic.ProcessShapeshift(hit); }
        public void ProcessSingle(MHit hit) { this._typeLogic.ProcessSingle(hit); }
        public void ProcessSong(MHit hit) { this._typeLogic.ProcessSong(hit); }
        public void ProcessSummon(MHit hit) { this._typeLogic.ProcessSummon(hit); }

    }
}
