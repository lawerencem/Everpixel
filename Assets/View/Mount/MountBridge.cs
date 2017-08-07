using Assets.Model.Mount;
using Assets.View.Character;
using Template.Other;

namespace Assets.View.Mount
{
    public class MountBridge : ASingleton<MountBridge>
    {
        public MountBridge() { }

        public VMount GetMountSprites(MountParams m)
        {
            var view = new VMount();
            view.Name = m.Type.ToString().Replace("_", " ");
            view.Sprites = CharSpriteLoader.Instance.GetMountSprites(m);
            return view;
        }
    }
}
