using Assets.Controller.Managers;
using Controller.Characters;
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

        public List<TileController> GetAdjacentTiles(GenericCharacterController c)
        {
            return this._aoeLogic.GetAdjacentTiles(c);
        }

        public List<TileController> GetAoETiles(TileController source, TileController target, int range)
        {
            return this._aoeLogic.GetAoETiles(source, target, range);
        }

        public List<TileController> GetRaycastTiles(TileController source, TileController target, int range)
        {
            return this._aoeLogic.GetRaycastTiles(source, target, range);
        }

        public double GetSpellDurViaMod(GenericCharacter character)
        {
            // TODO:
            return 1.0;
        }

        public List<TileController> GetStandardAttackTiles(AttackSelectedEvent e, GenericCharacterController c, CombatManager m)
        {
            return this._aoeLogic.GetStandardAttackTiles(e, c, m);
        }

        public bool IsValidEmptyTile(PerformActionEvent e) { return this._tileLogic.IsValidEmptyTile(e); }
        public bool IsValidEnemyTarget(PerformActionEvent e) { return this._tileLogic.IsValidEnemyTarget(e); }

        public void PredictBullet(HitInfo hit) { this._typeLogic.PredictBullet(hit); }
        public void PredictInstant(HitInfo hit) { this._typeLogic.PredictInstant(hit); }
        public void PredictMelee(HitInfo hit) { this._typeLogic.PredictMelee(hit); }
        public void PredictRay(HitInfo hit) { this._typeLogic.PredictRay(hit); }
        public void ProcessBullet(HitInfo hit) { this._typeLogic.ProcessBullet(hit); }
        public void ProcessInstant(HitInfo hit) { this._typeLogic.ProcessInstant(hit); }
        public void ProcessMelee(HitInfo hit) { this._typeLogic.ProcessMelee(hit); }
        public void ProcessRay(HitInfo hit) { this._typeLogic.ProcessRay(hit); }
        public void ProcessResist(HitInfo hit) { this._typeLogic.ProcessResist(hit); }
        public void ProcessShapeshift(HitInfo hit) { this._typeLogic.ProcessShapeshift(hit); }
        public void ProcessSong(HitInfo hit) { this._typeLogic.ProcessSong(hit); }
        public void ProcessSummon(HitInfo hit) { this._typeLogic.ProcessSummon(hit); }

    }
}
