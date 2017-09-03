using Assets.Controller.Character;
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
        private CombatManagerData _data;

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

        public EAbility GetCurrentAbility() { return this._data.CurrentAbility; }
        public CharController GetCurrentlyActing() { return this._data.CurrentlyActing; }
        public CombatManagerData GetData() { return this._data; }
        public bool GetLWeapon() { return this._data.LWeapon; }
        public List<TileController> GetPotentialTgtTiles() { return this._data.PotentialTgtTiles; }

        public void SetCurrentAbility(EAbility a) { this._data.CurrentAbility = a; }
        public void SetCurrentlyActing(CharController c) { this._data.CurrentlyActing = c; }
        public void SetLWeapon(bool lWeapon) { this._data.LWeapon = lWeapon; }
        public void SetPotentialTgtTiles(List<TileController> t) { this._data.PotentialTgtTiles = t; }

        public CombatManager()
        {
            this._data = new CombatManagerData();
        }

        public void Init(MMapController map)
        {
            this._data.Map = map;
            this._data.LParties = map.GetLParties();
            this._data.RParties = map.GetRParties();
            foreach(var party in this._data.LParties)
            {
                this._data.Characters.AddRange(party.GetChars());
            }
            foreach (var party in this._data.RParties)
            {
                this._data.Characters.AddRange(party.GetChars());
            }
            if (this._data.Characters.Count > 0)
            {
                this._data.Characters.Sort(
                    (x, y) =>
                    y.Model.GetCurrentStats().GetStatValue(ESecondaryStat.Initiative)
                    .CompareTo(x.Model.GetCurrentStats().GetStatValue(ESecondaryStat.Initiative)));
            }
            foreach (var character in this._data.Characters)
            {
                this._data.InitiativeOrder.Add(character);

                // TODO: Try reading .xml values, or set to max...
                character.Model.SetCurrentHP(character.Model.GetCurrentStats().GetSecondaryStats().MaxHP);
                character.Model.SetCurrentAP(character.Model.GetCurrentStats().GetSecondaryStats().MaxAP);
                character.Model.SetCurrentMorale(character.Model.GetCurrentStats().GetSecondaryStats().Morale);
                character.Model.SetCurrentStam(character.Model.GetCurrentStats().GetSecondaryStats().Stamina);
            }
                
            this.ProcessTakingAction();
        }

        public bool IsValidActionClick(TileController t)
        {
            if (t.Current != null && t.Current.GetType().Equals(typeof(CharController)))
            {
                var ability = AbilityTable.Instance.Table[this._data.CurrentAbility];
                var target = t.Current as CharController;
                var tile = this._data.PotentialTgtTiles.Find(x => x.Equals(t));
                if (tile != null)
                {
                    if (this._data.CurrentlyActing.Model.LParty == target.Model.LParty)
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

        public void ProcessCharDeath(CharController c)
        {
            this._data.Characters.Remove(c);
            foreach (var party in this._data.LParties)
                party.GetChars().Remove(c);
            foreach (var party in this._data.RParties)
                party.GetChars().Remove(c);
            this._data.InitiativeOrder.Remove(c);
        }

        public void ProcessEndTurn()
        {
            this._data.CurrentlyActing.Model.ProcessEndOfTurn();
            this._data.InitiativeOrder.Remove(this._data.CurrentlyActing);
            if (this._data.InitiativeOrder.Count > 0)
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
            this._data.InitiativeOrder.Clear();
            foreach (var character in this._data.Characters)
                this._data.InitiativeOrder.Add(character);
            this.ProcessTakingAction();
        }

        private void ProcessTakingAction()
        {
            if (this._data.InitiativeOrder.Count > 0)
            {
                var data = new EvTakingActionData();
                data.Target = this._data.InitiativeOrder[0];
                var acting = new EvTakingAction(data);
                acting.TryProcess();
            }
        }
    }
}
