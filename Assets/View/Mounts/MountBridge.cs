using Generics;
using Generics.Utilities;
using Model.Mounts;
using View.Equipment;
using View.Mount;

namespace View.Mounts
{
    public class MountBridge : AbstractSingleton<MountBridge>
    {
        public MountBridge() { }

        public MountView GetMountSprites(MountParams m)
        {
            var view = new MountView();
            view.Name = m.Type.ToString().Replace("_", " ");
            view.Sprites = CharacterSpriteLoader.Instance.GetMountSprites(m);
            return view;
        }
    }
}
