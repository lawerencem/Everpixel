using Assets.Model.Ability.Enum;
using Assets.Model.Action;
using Assets.Model.Combat.Hit;
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
