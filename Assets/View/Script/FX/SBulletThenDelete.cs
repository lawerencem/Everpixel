using Assets.Model.Action;
using Assets.Template.Script;

namespace Assets.View.Script.FX
{
    public class SBulletThenDelete : SRaycastMoveThenDelete
    {
        public MAction Action { get; set; }
    }
}
