using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability.Enum;
using Assets.Model.Action;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Template.CB;

namespace Assets.Model.Zone.Perpetual
{
    public class AttackOfOpportunityZone : AZone
    {
        private MAction _action;

        public AttackOfOpportunityZone() : base(EZone.Attack_Of_Opportunity_Zone)
        {
            
        }

        public override void ProcessExitZone(CChar target, bool doAttackOfOpportunity, Callback cb)
        {
            base.ProcessExitZone(target, doAttackOfOpportunity, cb);
            if (this._data.Source != null && doAttackOfOpportunity)
            {
                if (target.Proxy.LParty != this._data.Source.Proxy.LParty)
                {
                    var data = new ActionData();
                    data.Ability = EAbility.Attack_Of_Opportunity;
                    data.DisplayDefended = false;
                    data.Source = this._data.Source;
                    data.Target = target.Tile;
                    data.WpnAbility = false;
                    this._action = new MAction(data);
                    this._action.TryProcessNoDisplay();
                    foreach (var hit in this._action.Data.Hits)
                    {
                        if (!FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Block) &&
                            !FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Dodge) &&
                            !FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Parry))
                        {
                            cb(this);
                        }
                    }
                    this._action.DisplayAction();
                }
            }
        }
    }
}
