using Assets.Model.Mount;
using Assets.Template.Other;
using Assets.View.Character;

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
