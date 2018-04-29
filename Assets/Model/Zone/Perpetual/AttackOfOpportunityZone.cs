using Assets.Controller.Map.Tile;
using Assets.Model.Ability.Enum;
using Assets.Model.Action;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Zone.Perpetual
{
    public class AttackOfOpportunityZone : AZone
    {
        private MAction _action;

        public AttackOfOpportunityZone() : base(EZone.Attack_Of_Opportunity_Zone)
        {
            
        }

        public override void ProcessExitZone(TileMoveData moveData)
        {
            base.ProcessExitZone(moveData);
            if (this._data.Source != null && moveData.DoAttackOfOpportunity)
            {
                if (moveData.Target.Proxy.LParty != this._data.Source.Proxy.LParty)
                {
                    var data = new ActionData();
                    data.Ability = EAbility.Attack_Of_Opportunity;
                    data.DisplayDefended = false;
                    data.Source = this._data.Source;
                    data.Target = moveData.Target.Tile;
                    data.WpnAbility = false;
                    this._action = new MAction(data);
                    this._action.TryProcessNoDisplay();
                    foreach (var hit in this._action.Data.Hits)
                    {
                        if (!FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Block) &&
                            !FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Dodge) &&
                            !FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Parry))
                        {
                            moveData.Callback(this);
                        }
                    }
                    this._action.DisplayAction();
                }
            }
        }
    }
}
