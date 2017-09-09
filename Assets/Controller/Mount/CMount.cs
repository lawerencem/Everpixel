using Assets.Model.Mount;
using Assets.View.Mount;

namespace Assets.Controller.Mount
{
    public class CMount
    {
        private MMount _model;
        private MountParams _params;
        private VMount _view;

        public MMount Model { get { return this._model; } }
        public MountParams Params { get { return this._params; } }
        public VMount View { get { return this._view; } }

        public void SetModel(MMount m) { this._model = m; }
        public void SetParams(MountParams p) { this._params = p; }
        public void SetView(VMount v) { this._view = v; }
    }
}
