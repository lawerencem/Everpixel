using Assets.Controller.AI.Particle.Combat;
using Assets.Controller.Character;
using Assets.Controller.Equipment.Weapon;
using Assets.Controller.Map.Combat;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Action;
using Assets.Model.Character.Enum;
using Assets.Model.Event.Combat;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Controller.Manager.Combat
{
    public class CombatManager
    {
        private CCombatParticle _ai;
        private CurrentlyActingData _currActingData;
        private List<Pair<CChar, MAction>> _currentlyCasting;
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

        public void AddCurrentlyCasting(Pair<CChar, MAction> casting)
        {
            var current = this._currentlyCasting.Find(x => x.X.Equals(casting.X));
            if (current.X == null && current.Y == null)
                this._currentlyCasting.Add(casting);
        }

        public EAbility GetCurrentAbility()
        {
            return this._currActingData.Ability;
        }

        public CChar GetCurrentlyActing()
        {
            return this._currActingData.CurrentlyActing;
        }

        public CWeapon GetCurrentWeapon()
        {
            return this._currActingData.CurrentWeapon;
        }

        public CombatManagerData GetData()
        {
            return this._combatData;
        }

        public bool GetLWeapon()
        {
            return this._currActingData.LWeapon;
        }

        public List<CTile> GetPotentialTgtTiles()
        {
            return this._combatData.PotentialTgtTiles;
        }

        public bool GetIsWpnAbility()
        {
            return this._currActingData.IsWpnAbility;
        }
        
        public void SetCurrentAbilityNone()
        {
            this._currActingData.Ability = EAbility.None;
        }

        public void SetCurrentData(CurrentlyActingData d)
        {
            this._currActingData = d;
        }

        public void SetCurrentlyActing(CChar c)
        {
            this._currActingData.CurrentlyActing = c;
        }

        public void SetPotentialTgtTiles(List<CTile> t)
        {
            this._combatData.PotentialTgtTiles = t;
        }

        public CombatManager()
        {
            this._combatData = new CombatManagerData();
            this._currActingData = new CurrentlyActingData();
            this._currentlyCasting = new List<Pair<CChar, MAction>>();
        }

        public void Init(CMap map)
        {
            this._combatData.Map = map;
            this._combatData.LParties = map.GetLParties();
            this._combatData.RParties = map.GetRParties();
            foreach(var party in this._combatData.LParties)
                this._combatData.Characters.AddRange(party.GetChars());
            foreach (var party in this._combatData.RParties)
                this._combatData.Characters.AddRange(party.GetChars());
            if (this._combatData.Characters.Count > 0)
            {
                this._combatData.Characters.Sort(
                    (x, y) =>
                    y.Proxy.GetStat(ESecondaryStat.Initiative)
                    .CompareTo(x.Proxy.GetStat(ESecondaryStat.Initiative)));
            }
            foreach (var character in this._combatData.Characters)
                this._combatData.InitiativeOrder.Add(character);
            this.InitAI(map);
            this.ProcessTakingAction();
        }

        public bool IsValidActionClick(CTile t)
        {
            if (this.IsAITurn())
                return false;
            else if (this.TryProcessTileWithChar(t))
                return true;
            else if (this.TryProcessEmptyTile(t))
                return true;
            else
                return false;
        }

        public bool IsAITurn()
        {
            return this.GetCurrentlyActing().Proxy.GetParentParty().GetAIControlled();
        }

        public void ProcessCharDeath(CChar c)
        {
            this._combatData.Characters.Remove(c);
            foreach (var party in this._combatData.LParties)
                party.GetChars().Remove(c);
            foreach (var party in this._combatData.RParties)
                party.GetChars().Remove(c);
            this._combatData.InitiativeOrder.Remove(c);
            this._ai.RemoveAgentParticles(c, c.Proxy.LParty);
        }

        public void ProcessEndTurn()
        {
            this._combatData.InitiativeOrder.Remove(this._currActingData.CurrentlyActing);
            this._currActingData.Ability = EAbility.None;
            var agent = this._currActingData.CurrentlyActing;
            this._ai.RemoveAgentParticles(agent, agent.Proxy.LParty);
            this._ai.SetAgentParticlePoints(agent, agent.Proxy.LParty);
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

        private void CharCastDone(object o)
        {
            var data = new EvNewTurnData();
            data.Target = this._combatData.InitiativeOrder[0];
            var acting = new EvNewTurn(data);
            acting.TryProcess();
        }

        private void InitAI(CMap map)
        {
            this._ai = new CCombatParticle(map.GetMap().GetTiles());
            foreach (var party in this._combatData.LParties)
            {
                foreach (var agent in party.GetChars())
                    this._ai.SetAgentParticlePoints(agent, true);
            }
            foreach (var party in this._combatData.RParties)
            {
                foreach (var agent in party.GetChars())
                    this._ai.SetAgentParticlePoints(agent, false);
            }
        }

        private void ProcessTakingAction()
        {
            if (this._combatData.InitiativeOrder.Count > 0)
            {
                var tgt = this._combatData.InitiativeOrder[0];
                tgt.Proxy.ProcessNewTurn();
                if (FActionStatus.HasFlag(tgt.Proxy.GetActionFlags().CurFlags, FActionStatus.Flags.Casting))
                {
                    var pair = this._currentlyCasting.Find(x => x.X.Equals(tgt));
                    if (pair.X != null && pair.Y != null)
                    {
                        this._currentlyCasting.Remove(pair);
                        pair.Y.DecrementCastingTurnsRemaining();
                        if (pair.Y.GetCastingTurnsRemaining() <= 0)
                            pair.Y.AddCallback(this.CharCastDone);
                        pair.Y.TryProcess();
                    }
                    else
                        throw new System.Exception("Error: Cannot find casting character, but it is required.");
                }
                else
                {
                    var data = new EvNewTurnData();
                    data.Target = this._combatData.InitiativeOrder[0];
                    var acting = new EvNewTurn(data);
                    acting.TryProcess();
                }
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
