using Assets.Controller.Equipment.Weapon;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability.Enum;
using Assets.Model.Ability.Logic.Calculator;
using Assets.Model.Action;
using Assets.Model.Character.Enum;

namespace Assets.Model.Zone.Duration
{
    public class SuppressionZoneData : ZoneData
    {
        public bool LWeapon { get; set; }
        public CWeapon ParentWeapon { get; set; }
        public CTile ParentTile { get; set; }
    }

    public class SuppressionZone : AZone
    {
        private MAction _action;
        private SuppressionZoneData _suppressionZoneData;

        public SuppressionZone() : base(EZone.Suppression_Zone)
        {
            
        }

        public override void ProcessEnterZone(TileMoveData moveData)
        {
            base.ProcessEnterZone(moveData);
            if (this._suppressionZoneData.Source != null)
            {
                if (moveData.Target.Proxy.LParty != this._suppressionZoneData.Source.Proxy.LParty)
                {
                    var data = new ActionData();
                    data.Ability = EAbility.Aim;
                    data.LWeapon = this._suppressionZoneData.LWeapon;
                    data.ParentWeapon = this._suppressionZoneData.ParentWeapon;
                    data.Source = this._suppressionZoneData.Source;
                    data.Target = moveData.Target.Tile;
                    data.WpnAbility = true;
                    this._action = new MAction(data);
                    var staminaCalc = new StaminaCalculator();
                    var cost = staminaCalc.Process(this._action);
                    if (cost <= this._action.Data.Source.Proxy.GetPoints(ESecondaryStat.Stamina))
                    {
                        moveData.Callback(this);
                        this._action.TryProcessNoDisplay();
                        this.HandleAim(null);
                    }
                }
            }
        }

        public void SetSuppressionZoneData(SuppressionZoneData data)
        {
            this._suppressionZoneData = data;
            this._data.DependsOnSourceChar = true;
            this._data.Source = data.Source;
            this._data.Source.Proxy.AddZone(this);
        }

        private void HandleAim(object o)
        {
            this._action.DisplayAction();
        }
    }
}
