﻿using Controller.Characters;
using Controller.Map;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
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

        public List<TileController> GetAdjacentTiles(CharController c) { return this._aoeLogic.GetAdjacentTiles(c); }
        public List<TileController> GetAoETiles(AbilityArgContainer arg, int aoe) {return this._aoeLogic.GetAoETiles(arg, aoe);}
        public List<TileController> GetRaycastTiles(AbilityArgContainer arg) { return this._aoeLogic.GetRaycastTiles(arg);}
        public List<TileController> GetPotentialTargets(AbilityArgContainer arg) { return this._aoeLogic.GetPotentialTargets(arg);}

        public bool IsValidEmptyTile(AbilityArgContainer arg) { return this._tileLogic.IsValidEmptyTile(arg); }
        public bool IsValidEnemyTarget(AbilityArgContainer arg) { return this._tileLogic.IsValidEnemyTarget(arg); }

        public void PredictBullet(Hit hit) { this._typeLogic.PredictBullet(hit); }
        public void PredictInstant(Hit hit) { this._typeLogic.PredictInstant(hit); }
        public void PredictMelee(Hit hit) { this._typeLogic.PredictMelee(hit); }
        public void PredictRay(Hit hit) { this._typeLogic.PredictRay(hit); }
        public void ProcessBullet(Hit hit) { this._typeLogic.ProcessBullet(hit); }
        public void ProcessInstant(Hit hit) { this._typeLogic.ProcessInstant(hit); }
        public void ProcessMelee(Hit hit) { this._typeLogic.ProcessMelee(hit); }
        public void ProcessRay(Hit hit) { this._typeLogic.ProcessRay(hit); }
        public void ProcessResist(Hit hit) { this._typeLogic.ProcessResist(hit); }
        public void ProcessShapeshift(Hit hit) { this._typeLogic.ProcessShapeshift(hit); }
        public void ProcessSong(Hit hit) { this._typeLogic.ProcessSong(hit); }
        public void ProcessSummon(Hit hit) { this._typeLogic.ProcessSummon(hit); }

    }
}
