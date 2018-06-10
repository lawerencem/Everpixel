using Assets.Controller.Character;
using Assets.Controller.Equipment.Weapon;
using Assets.Controller.Mount;
using Assets.Model.Ability;
using Assets.Model.Barrier;
using Assets.Model.Character.Container;
using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Characters.Params;
using Assets.Model.Class;
using Assets.Model.Class.Enum;
using Assets.Model.Effect;
using Assets.Model.Equipment.Armor;
using Assets.Model.Injury;
using Assets.Model.OTE.DoT;
using Assets.Model.OTE.HoT;
using Assets.Model.Party;
using Assets.Model.Party.Enum;
using Assets.Model.Zone;
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

        public EStartCol StartCol { get; set; }

        public void AddEffect(MEffect e)
        {
            this.GetEffects().AddEffect(e);
        }

        public void AddBuff(StatMod buff)
        {
            this._model.GetStatMods().AddBuff(buff);
            this._model.GetCurStats().ResetCurStats(this._model.GetStatMods(), this._model.GetBaseStats());
        }

        public void AddDebuff(StatMod debuff)
        {
            this._model.GetStatMods().AddBuff(debuff);
            this._model.GetCurStats().ResetCurStats(this._model.GetStatMods(), this._model.GetBaseStats());
        }

        public void AddDoT(MDoT dot)
        {
            this._model.GetEffectsContainer().AddDoT(dot);
        }

        public void AddHoT(MHoT hot)
        {
            this._model.GetEffectsContainer().AddHoT(hot);
        }

        public void AddInjury(MInjury i)
        {
            this._model.GetStatMods().AddInjury(i);
            this._model.GetCurStats().ResetCurStats(this._model.GetStatMods(), this._model.GetBaseStats());
        }

        public void AddPoints(ESecondaryStat s, double v)
        {
            this._model.GetPoints().AddValue(s, v);
        }

        public void AddBarrier(MBarrier shield)
        {
            this._model.GetEffectsContainer().AddBarrier(shield);
        }

        public void AddZone(AZone zone)
        {
            this._model.AddLinkedZone(zone);
        }

        public FActionStatus GetActionFlags()
        {
            return this._model.GetActionFlags();
        }

        public List<MAbility> GetActiveAbilities()
        {
            return this._model.GetAbilitiesContainer().GetNonWpnAbilities();
        }

        public CArmor GetArmor()
        {
            return this._model.GetEquipment().GetArmor();
        }

        public Dictionary<EClass, MClass> GetBaseClasses()
        {
            return this._model.GetBaseClasses();
        }

        public List<MAbility> GetDefaultAbilities()
        {
            return this._model.GetAbilitiesContainer().GetWpnAbilities();
        }

        public CharEffects GetEffects()
        {
            return this._model.GetEffectsContainer();
        }    

        public CHelm GetHelm()
        {
            return this._model.GetEquipment().GetHelm();
        }

        public CWeapon GetLWeapon()
        {
            return this._model.GetEquipment().GetLWeapon();
        }

        public double GetPoints(ESecondaryStat s)
        {
            return this._model.GetPoints().GetCurrValue(s);
        }

        public MChar GetModel()
        {
            return this._model;
        }

        public CharStatMods GetMods()
        {
            return this._model.GetStatMods();
        }

        public CMount GetMount()
        {
            return this._model.Mount;
        }

        public PreCharParams GetParams()
        {
            return this._model.GetParams();
        }

        public MParty GetParentParty()
        {
            return this._model.GetParentParty();
        }

        public CharPerks GetPerks()
        {
            return this._model.GetPerks();
        }

        public CWeapon GetRWeapon()
        {
            return this._model.GetEquipment().GetRWeapon();
        }

        public double GetStat(ESecondaryStat s)
        {
            return this._model.GetCurStats().GetStatValue(s);
        }

        public double GetStat(EPrimaryStat s)
        {
            return this._model.GetCurStats().GetStatValue(s);
        }

        public FCharacterStatus GetStatusFlags()
        {
            return this._model.GetStatusFlags();
        }

        public List<AZone> GetZones()
        {
            return this._model.GetLinkedZones();
        }

        public void HandleCharDeath()
        {
            var deleteZones = new List<AZone>();
            foreach (var zone in this._model.GetLinkedZones())
                deleteZones.Add(zone);
            foreach (var zone in deleteZones)
                zone.HandleSourceDeath();
        }

        public void ModifyPoints(ESecondaryStat s, int v, bool isHeal)
        {
            this._model.ModifyPoints(s, v, isHeal);
        }

        public void ProcessEndOfTurn()
        {
            this._model.ProcessEndOfTurn();
        }

        public void RemoveZone(AZone zone)
        {
            this._model.GetLinkedZones().Remove(zone);
        }

        public void SetController(CChar c)
        {
            this._model.SetController(c);
        }

        public void SetLParty(bool lParty)
        {
            this._model.SetLParty(lParty);
        }

        public void SetParentParty(MParty p)
        {
            this._model.SetParentParty(p);
        }

        public void SetPoints(ESecondaryStat s, double v)
        {
            this._model.GetPoints().SetValue(s, v);
        }

        public void SetPointsToMax(ESecondaryStat s)
        {
            double max = this._model.GetBaseStats().GetStatValue(s);
            this._model.GetPoints().SetValue(s, max);
        }
    }
}
