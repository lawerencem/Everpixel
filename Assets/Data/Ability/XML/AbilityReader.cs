using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Ability.Magic;
using Assets.Model.Ability.Shapeshift;
using Assets.Model.Effect;
using Assets.Model.Injury;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Xml.Linq;

namespace Assets.Data.Ability.XML
{
    public class AbilityReader : XMLReader
    {
        private static AbilityReader _instance;

        private AbilityMediator _mediator;
        private AbilityTable _table;

        public AbilityReader() : base()
        {
            this._paths.Add("Assets/Data/Ability/XML/Astral.xml");
            this._paths.Add("Assets/Data/Ability/XML/Fighting.xml");
            this._paths.Add("Assets/Data/Ability/XML/Light.xml");
            this._paths.Add("Assets/Data/Ability/XML/Poison.xml");
            this._paths.Add("Assets/Data/Ability/XML/Physical.xml");
            this._paths.Add("Assets/Data/Ability/XML/Shapeshift.xml");
            this._paths.Add("Assets/Data/Ability/XML/Songs.xml");
            this._paths.Add("Assets/Data/Ability/XML/WeaponAbilities.xml");

            this._mediator = new AbilityMediator();
            this._table = AbilityTable.Instance;
        }

        public static AbilityReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AbilityReader();
                return _instance;
            }
        }

        public override void ReadFromFile()
        {
            foreach(var path in this._paths)
            {
                var abilities = XDocument.Load(path);
                this.ReadFromFileHelper(abilities);
            }
        }

        private void ReadFromFileHelper(XDocument doc)
        {
            foreach (var el in doc.Root.Elements())
            {
                foreach (var att in el.Attributes())
                {
                    var magicType = EMagicType.Physical;
                    if (EnumUtil<EMagicType>.TryGetEnumValue(att.Value, ref magicType))
                    {
                        foreach (var ele in el.Elements())
                        {
                            foreach (var attr in ele.Attributes())
                            {
                                var type = EAbility.None;
                                if (EnumUtil<EAbility>.TryGetEnumValue(attr.Value, ref type))
                                {
                                    this.HandleType(type);
                                    foreach (var elem in ele.Elements())
                                    {
                                        this.HandleIndex(type, elem, elem.Name.ToString(), elem.Value);
                                    }
                                    this._table.Table[type].Data.MagicType = magicType;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void HandleIndex(EAbility type, XElement ele, string mod, string value)
        {
            double v = 1;
            double.TryParse(value, out v);
            switch (mod)
            {
                case ("AccMod"): { this._table.Table[type].Data.AccMod = v; } break;
                case ("AoE"): { this._table.Table[type].Data.AoE = v; } break;
                case ("APCost"): { this._table.Table[type].Data.APCost = (int)v; } break;
                case ("ArmorIgnoreMod"): { this._table.Table[type].Data.ArmorIgnoreMod = v; } break;
                case ("ArmorPierceMod"): { this._table.Table[type].Data.ArmorPierceMod = v; } break;
                case ("BlockIgnoreMod"): { this._table.Table[type].Data.BlockIgnoreMod = v; } break;
                case ("ECastType"): { this.HandleCastType(type, value); } break;
                case ("CastDuration"): { this._table.Table[type].Data.CastDuration = (int)v; } break;
                case ("CustomGraphics"): { this.HandleCustomGraphics(type, value); } break;
                case ("DamageMod"): { this._table.Table[type].Data.DamageMod = double.Parse(value); } break;
                case ("Description"): { this._table.Table[type].Data.Description = value; } break;
                case ("DmgPerPower"): { this._table.Table[type].Data.DmgPerPower = double.Parse(value); } break;
                case ("Duration"): { this._table.Table[type].Data.Duration = double.Parse(value); } break;
                case ("DodgeMod"): { this._table.Table[type].Data.DodgeMod = v; } break;
                case ("EEffect"): { this.HandleEffects(ele, value, type); } break;
                case ("FatigueCost"): { this._table.Table[type].Data.FatigueCost = (int)v; } break;
                case ("FlatDamage"): { this._table.Table[type].Data.FlatDamage = v; } break;
                case ("Hostile"): { this.HandleHostile(type, value); } break;
                case ("HitsTiles"): { this.HandleHitsTiles(type, value); } break;
                case ("IconSprite"): { this._table.Table[type].Data.IconSprite = (int)v; } break;
                case ("IsHeal"): { this.HandleIsHeal(type, value); } break;
                case ("Injury"): { this.HandleInjuries(type, value); } break;
                case ("MaxSprites"): { this._table.Table[type].Data.MaxSprites = (int)v; } break;
                case ("MeleeBlockChanceMod"): { this._table.Table[type].Data.MeleeBlockChanceMod = v; } break;
                case ("MinSprites"): { this._table.Table[type].Data.MinSprites = (int)v; } break;
                case ("ParryModMod"): { this._table.Table[type].Data.ParryModMod = v; } break;
                case ("ProcessDamage"): { this.HandleProcessDamage(type, value); } break;
                case ("Range"): { this._table.Table[type].Data.Range = (int)v; } break;
                case ("RangeBlockMod"): { this._table.Table[type].Data.RangeBlockMod = v; } break;
                case ("RechargeTime"): { this._table.Table[type].Data.RechargeTime = v; } break;
                case ("EResistType"): { this.HandleResistType(type, value); } break;
                case ("ShapeshiftSprites"): { this.HandleShapeshiftSprites(ele, type); } break;
                case ("ShieldDamageMod"): { this._table.Table[type].Data.ShieldDamageMod = v; } break;
                case ("Sprites"): { this.HandleSprites(type, value); } break;
                case ("SpellLevel"): { this._table.Table[type].Data.SpellLevel = (int)v; } break;
            }
        }

        private void HandleCastType(EAbility key, string value)
        {
            var type = ECastType.None;
            if (EnumUtil<ECastType>.TryGetEnumValue(value, ref type))
                AbilityTable.Instance.Table[key].Data.CastType = type;
        }

        private void HandleCustomGraphics(EAbility key, string value)
        {
            if (value.ToLowerInvariant().Equals("true"))
                AbilityTable.Instance.Table[key].Data.CustomGraphics = true;
        }

        private void HandleEffects(XElement el, string value, EAbility key)
        {
            var type = EEffect.None;
            foreach (var att in el.Attributes())
            {
                if (EnumUtil<EEffect>.TryGetEnumValue(att.Value, ref type))
                {
                    var effect = EffectBuilder.Instance.BuildEffect(el, type);
                    this._table.Table[key].Data.Effects.Add(effect);
                }
            }
        }

        private void HandleInjuries(EAbility type, string s)
        {
            var injury = EInjury.None;
            if (EnumUtil<EInjury>.TryGetEnumValue(s, ref injury))
                AbilityTable.Instance.Table[type].Data.Injuries.Add(injury);
        }

        private void HandleProcessDamage(EAbility key, string value)
        {
            if (value.ToLowerInvariant().Equals("false"))
                AbilityTable.Instance.Table[key].Data.ProcessDamage = false;
        }

        private void HandleHostile(EAbility key, string value)
        {
            if (value.ToLowerInvariant().Equals("false"))
                AbilityTable.Instance.Table[key].Data.Hostile = false;
        }

        private void HandleHitsTiles(EAbility key, string value)
        {
            if (value.ToLowerInvariant().Equals("true"))
                AbilityTable.Instance.Table[key].Data.HitsTiles = true;
        }

        private void HandleIsHeal(EAbility key, string value)
        {
            if (value.ToLowerInvariant().Equals("true"))
                AbilityTable.Instance.Table[key].Data.IsHeal = true;
        }

        private void HandleResistType(EAbility type, string s)
        {
            var resist = EResistType.None;
            if (EnumUtil<EResistType>.TryGetEnumValue(s, ref resist))
                AbilityTable.Instance.Table[type].Data.Resist = resist;
        }

        private void HandleShapeshiftSprites(XElement element, EAbility type)
        {
            var v = AbilityTable.Instance.Table[type] as Shapeshift;
            foreach(var ele in element.Elements())
            {
                switch(ele.Name.ToString())
                {
                    case ("CharAttackHead"): { v.Info.CharAttackHead = int.Parse(ele.Value); } break;
                    case ("CharAttackTorso"): { v.Info.CharAttackTorso = int.Parse(ele.Value); } break;
                    case ("CharHeadDead"): { v.Info.CharHeadDead = int.Parse(ele.Value); } break;
                    case ("CharHeadFlinch"): { v.Info.CharHeadFlinch = int.Parse(ele.Value); } break;
                    case ("CharHead"): { v.Info.CharHead = int.Parse(ele.Value); } break;
                    case ("CharTorso"): { v.Info.CharTorso = int.Parse(ele.Value); } break;
                }
            }
        }

        private void HandleSprites(EAbility key, string value)
        {
            var csv = value.Split(',');
            foreach (var v in csv)
            {
                int parsed = 0;
                if (int.TryParse(v, out parsed))
                    AbilityTable.Instance.Table[key].Data.Sprites.Add(parsed);
            }
        }

        private void HandleType(EAbility type)
        {
            this._mediator.HandleType(type);
        }
    }
}
