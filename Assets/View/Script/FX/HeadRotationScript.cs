//using Controller.Characters;
//using Controller.Managers.Map;
//using Controller.Map;
//using UnityEngine;

//namespace Generics.Scripts
//{
//    public class HeadRotationScript : RotationScript
//    {
//        private CMapGUIControllerHit _cmap;
//        private TileController _tile;

//        public void InitHeadRotation(TileController t, CMapGUIControllerHit cmap)
//        {
//            this._cmap = cmap;
//            this._tile = t;
//        }

//        public override void Done()
//        {
//            if (this._callBack != null)
//                this._callBack();
//            this._cmap.ProcessSplatter(5, this._tile);
//            GameObject.Destroy(this);
//        }
//    }
//}
