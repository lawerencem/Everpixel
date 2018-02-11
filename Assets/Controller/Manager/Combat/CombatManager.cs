using Assets.Controller.Character;
using Assets.Controller.Equipment.Weapon;
using Assets.Controller.Map.Combat;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Character.Enum;
using Assets.Model.Event.Combat;
using System.Collections.Generic;

namespace Assets.Controller.Manager.Combat
{
    public class CombatManager
    {
        private CurrentlyActingData _currActingData;
        private CombatManagerData _combatData;

        private static CombatManager _instance;
        public static CombatManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CombatManager();
                return _instance;
            }
        }

        public EAbility GetCurrentAbility() { return this._currActingData.Ability; }
        public CChar GetCurrentlyActing() { return this._currActingData.CurrentlyActing; }
        public CWeapon GetCurrentWeapon() { return this._currActingData.CurrentWeapon; }
        public CombatManagerData GetData() { return this._combatData; }
        public bool GetLWeapon() { return this._combatData.LWeapon; }
        public List<CTile> GetPotentialTgtTiles() { return this._combatData.PotentialTgtTiles; }
        public bool GetIsWpnAbility() { return this._currActingData.IsWpnAbility; }
        
        public void SetCurrentData(CurrentlyActingData d) { this._currActingData = d; }
        public void SetCurrentlyActing(CChar c) { this._currActingData.CurrentlyActing = c; }
        public void SetPotentialTgtTiles(List<CTile> t) { this._combatData.PotentialTgtTiles = t; }

        public CombatManager()
        {
            this._combatData = new CombatManagerData();
            this._currActingData = new CurrentlyActingData();
        }

        public void Init(MMapController map)
        {
            this._combatData.Map = map;
            this._combatData.LParties = map.GetLParties();
            this._combatData.RParties = map.GetRParties();
            foreach(var party in this._combatData.LParties)
            {
                this._combatData.Characters.AddRange(party.GetChars());
            }
            foreach (var party in this._combatData.RParties)
            {
                this._combatData.Characters.AddRange(party.GetChars());
            }
            if (this._combatData.Characters.Count > 0)
            {
                this._combatData.Characters.Sort(
                    (x, y) =>
                    y.Proxy.GetStat(ESecondaryStat.Initiative)
                    .CompareTo(x.Proxy.GetStat(ESecondaryStat.Initiative)));
            }
            foreach (var character in this._combatData.Characters)
                this._combatData.InitiativeOrder.Add(character);
            this.ProcessTakingAction();
        }

        public bool IsValidActionClick(CTile t)
        {
            if (this.TryProcessTileWithChar(t))
                return true;
            if (this.TryProcessEmptyTile(t))
                return true;
            return false;
        }

        public void ProcessCharDeath(CChar c)
        {
            this._combatData.Characters.Remove(c);
            foreach (var party in this._combatData.LParties)
                party.GetChars().Remove(c);
            foreach (var party in this._combatData.RParties)
                party.GetChars().Remove(c);
            this._combatData.InitiativeOrder.Remove(c);
        }

        public void ProcessEndTurn()
        {
            this._currActingData.CurrentlyActing.Proxy.ProcessEndOfTurn();
            this._combatData.InitiativeOrder.Remove(this._currActingData.CurrentlyActing);
            this._currActingData.Ability = EAbility.None;
            if (this._combatData.InitiativeOrder.Count > 0)
            {
                this.ProcessTakingAction();
            }
            else
            {
                var e = new EvNewRound();
                e.TryProcess();
            }
        }

        public void ProcessNewRound()
        {
            this._combatData.InitiativeOrder.Clear();
            foreach (var character in this._combatData.Characters)
                this._combatData.InitiativeOrder.Add(character);
            this.ProcessTakingAction();
        }

        public void ProcessSummon(CChar c)
        {
            this._combatData.Characters.Add(c);
            this._combatData.InitiativeOrder.Add(c);
        }

        private void ProcessTakingAction()
        {
            if (this._combatData.InitiativeOrder.Count > 0)
            {
                var data = new EvTakingActionData();
                data.Target = this._combatData.InitiativeOrder[0];
                var acting = new EvTakingAction(data);
                acting.TryProcess();
            }
        }

        private bool TryProcessEmptyTile(CTile t)
        {
            if (this._currActingData.Ability != EAbility.None)
            {
                var ability = AbilityTable.Instance.Table[this._currActingData.Ability];
                return ability.Data.HitsTiles;
            }
            else
                return false;
            
        }

        private bool TryProcessTileWithChar(CTile t)
        {
            if (t.Current != null && 
                t.Current.GetType().Equals(typeof(CChar)) &&
                this._currActingData.Ability != EAbility.None)
            {
                var ability = AbilityTable.Instance.Table[this._currActingData.Ability];
                var target = t.Current as CChar;
                var tile = this._combatData.PotentialTgtTiles.Find(x => x.Equals(t));
                if (tile != null)
                {
                    if (this._currActingData.CurrentlyActing.Proxy.LParty == target.Proxy.LParty)
                    {
                        if (!ability.Data.Hostile)
                            return true;
                    }
                    else
                    {
                        if (ability.Data.Hostile)
                            return true;
                    }
                }
            }
            return false;
        }
    }
}
