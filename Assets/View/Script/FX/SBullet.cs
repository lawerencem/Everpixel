using Assets.Model.Action;
using Assets.Template.Script;

namespace Assets.View.Script.FX
{
    public class SBullet : SRaycastMoveThenDelete
    {
        public MAction Action { get; set; }
    }
}
