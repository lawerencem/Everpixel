using Assets.Model.Ability.Enum;
using Assets.Model.Action;
using Assets.Model.Combat.Hit;
using Assets.Model.Event.Combat;
using Assets.View.Script.FX;

namespace Assets.Model.Ability.Shapeshift
{
    public class Wereween : Shapeshift
    {
        public Wereween() : base(EAbility.Wereween)
        {
            this.SetStandardHumanoidInfo();
        }

        public override void Predict(MHit hit)
        {
            
        }

        public override void Process(MHit hit)
        {
            var data = new EvHoTData();
            data.Dmg = 5;   // TODO
            data.HasDur = false;
            data.Tgt = hit.Data.Source;
            var e = new EvHoT(data);
            e.TryProcess();
        }

        public override void DisplayFX(MAction a)
        {
            base.DisplayFX(a);
            foreach(var hit in a.Data.Hits)
            {
                var script = hit.Data.Source.Handle.AddComponent<SDisplayShapeshift>();
                script.Init(hit);
            }
        }
    }
}
