using System;

namespace Assets.View.Event
{
    public class MGuiEv : AGuiEv<EGuiEv>
    {
        public MGuiEv(EGuiEv t) : base(t)
        {
            
        }

        public override void Register()
        {
            this._manager.RegisterEvent(this);
        }

        public override void TryProcess()
        {
            throw new NotImplementedException();
        }
    }
}
