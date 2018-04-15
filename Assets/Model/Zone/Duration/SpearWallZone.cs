using Assets.Controller.Character;
using Assets.Controller.Equipment.Weapon;
using Assets.Controller.GUI.Combat;
using Assets.Controller.Manager.Combat;
using Assets.Model.Ability.Enum;
using Assets.Model.Ability.Logic.Calculator;
using Assets.Model.Effect;
using Assets.Model.Effect.Fortitude;
using Assets.Model.Event.Combat;
using Assets.Template.Util;

namespace Assets.Model.Zone.Duration
{
    public class ZoneSpearWallData : DurationZoneData
    {
        public bool LWeapon { get; set; }
        public CWeapon ParentWeapon { get; set; }
    }

    public class SpearWallZone : ADurationZone
    {
        private ZoneSpearWallData _spearWallData;

        public SpearWallZone() : base(EZone.Spear_Wall_Zone) { }

        public override void ProcessEnterZone(CChar target)
        {
            base.ProcessEnterZone(target);
            if (this._spearWallData.Source != null)
            {
                if (target.Proxy.LParty != this._spearWallData.Source.Proxy.LParty)
                {
                    var data = new EvPerformAbilityData();
                    data.Ability = EAbility.Pierce;
                    data.LWeapon = this._spearWallData.LWeapon;
                    data.ParentWeapon = this._spearWallData.ParentWeapon;
                    data.Source = this._spearWallData.Source;
                    data.Target = target.Tile;
                    data.WpnAbility = true;
                    var e = new EvPerformAbility(data);
                    e.TryProcess();
                }
            }
        }

        public void SetSpearWallZoneData(ZoneSpearWallData data)
        {
            this._spearWallData = data;
        }
    }
}
