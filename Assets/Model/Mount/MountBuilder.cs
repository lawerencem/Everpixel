using Assets.Controller.Mount;
using Assets.Template.Other;

namespace Assets.Model.Mount
{
    public class MountBuilder
    {
        public CMount Build(MountParams mParams)
        {
            var model = new MMount(mParams.Type);
            var controller = new CMount();
            controller.SetModel(model);
            controller.SetParams(mParams);
            return controller;
        }
    }
}
