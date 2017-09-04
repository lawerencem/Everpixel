using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability;
using Assets.Model.Character.Container;
using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Class;
using Assets.Model.Class.Enum;
using Assets.Model.Equipment.Type;
using System.Collections.Generic;

namespace Assets.Model.Character
{
    public class PChar
    {
        private MChar _model;
        public PChar(MChar c) { this._model = c; }

        public bool LParty { get { return this._model.LParty; } }
        public ERace Race { get { return this._model.Race; } }
        public ECharType Type { get { return this._model.Type; } }

        public void AddPoints(ESecondaryStat s, double v) { this._model.GetPoints().AddValue(s, v); }
        public List<MAbility> GetActiveAbilities() { return this._model.GetAbilities().GetActiveAbilities(); }
        public MArmor GetArmor() { return this._model.GetEquipment().GetArmor(); }
        public Dictionary<EClass, MClass> GetBaseClasses() { return this._model.GetBaseClasses(); }
        public List<MAbility> GetDefaultAbilities() { return this._model.GetAbilities().GetDefaultAbilities(); }
        public CharEffects<ECharType> GetEffects() { return this._model.GetEffects(); }        
        public FCharacterStatus GetFlags() { return this._model.GetFlags(); }
        public MHelm GetHelm() { return this._model.GetEquipment().GetHelm(); }
        public MWeapon GetLWeapon() { return this._model.GetEquipment().GetLWeapon(); }
        public double GetPoints(ESecondaryStat s) { return this._model.GetPoints().GetCurrValue(s); }
        public Mods GetMods() { return this._model.GetMods(); }
        public CharParams GetParams() { return this._model.GetParams(); }
        public CharPerks GetPerks() { return this._model.GetPerks(); }
        public MWeapon GetRWeapon() { return this._model.GetEquipment().GetRWeapon(); }
        public double GetStat(ESecondaryStat s) { return this._model.GetStats().GetStatValue(s); }
        public double GetStat(EPrimaryStat s) { return this._model.GetStats().GetStatValue(s); }
        public int GetTileTraversalAPCost(TileController t) { return this._model.GetTileTraversalAPCost(t); }
        public void ModifyPoints(ESecondaryStat s, int v, bool isHeal) { this._model.ModifyPoints(s, v, isHeal); }
        public void ProcessEndOfTurn() { this._model.ProcessEndOfTurn(); }
        public void SetController(CharController c) { this._model.SetController(c); }
        public void SetLParty(bool lParty) { this._model.SetLParty(lParty); }
        public void SetPoints(ESecondaryStat s, double v) { this._model.GetPoints().SetValue(s, v); }
        public void SetPointsToMax(ESecondaryStat s)
        {
            double max = this._model.GetStats().GetStatValue(s);
            this._model.GetPoints().SetValue(s, max);
        }
        
    }
}
