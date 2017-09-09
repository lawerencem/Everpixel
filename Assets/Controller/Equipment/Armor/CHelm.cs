using Assets.Model.Equipment.Enum;
using Assets.View.Equipment;

namespace Assets.Model.Equipment.Armor
{
    public class CHelm
    {
        private MHelm _model;
        private ArmorParams _params;
        private VArmor _view;

        public MHelm Model { get { return this._model; } }
        public ArmorParams Params { get { return this._params; } }
        public VArmor View { get { return this._view; } }

        public void SetModel(MHelm m) { this._model = m; }
        public void SetParams(ArmorParams p) { this._params = p; }
        public void SetView(VArmor v) { this._view = v; }

        public double GetStat(EArmorStat stat)
        {
            switch (stat)
            {
                case (EArmorStat.AP_Mod): { return this._model.Data.APMod; }
                case (EArmorStat.Block_Mod): { return this._model.Data.BlockMod; }
                case (EArmorStat.Damage_Mod): { return this._model.Data.DamageMod; }
                case (EArmorStat.Dodge_Mod): { return this._model.Data.DodgeMod; }
                case (EArmorStat.Durability): { return this._model.Data.Durability; }
                case (EArmorStat.Fatigue_Mod): { return this._model.Data.FatigueMod; }
                case (EArmorStat.Flat_Damage_Ignore): { return this._model.Data.FlatDamageIgnore; }
                case (EArmorStat.Initative_Mod): { return this._model.Data.InitativeMod; }
                case (EArmorStat.Parry_Mod): { return this._model.Data.ParryMod; }
                case (EArmorStat.Stamina_Mod): { return this._model.Data.StaminaMod; }
                case (EArmorStat.Value): { return this._model.Data.Value; }
                default: return -1;
            }
        }
    }
}
