using Assets.Model.Ability.Enum;
using Assets.Model.Action;
using Assets.Model.Combat.Hit;
using Assets.Model.Event.Combat;
using Assets.Model.OTE;
using Assets.Model.OTE.HoT;
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
            base.PredictSingle(hit);
        }

        public override void Process(MHit hit)
        {
            base.ProcessSingle(hit);
        }

        public override void DisplayFX(MAction a)
        {
            base.DisplayFX(a);
            foreach(var hit in a.Data.Hits)
            {
                var script = hit.Data.Source.GameHandle.AddComponent<SDisplayShapeshift>();
                script.Init(hit);
            }
        }
    }
}
