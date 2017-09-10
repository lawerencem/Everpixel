using Assets.Template.Utility;
using UnityEngine;

namespace Assets.Template.Script
{
    public class SRaycastMoveThenEmbedData : SRaycastMoveData
    {
        public Sprite EmbedSprite { get; set; }
        public float Offset { get; set; }
        public float Rotation { get; set; }
        public GameObject TargetObject { get; set; }   
    }

    public class SRaycastMoveThenEmbed : AScript
    {
        private SRaycastMoveThenEmbedData _data;
        private Vector3 _origin;

        public void Init(SRaycastMoveThenEmbedData data)
        {
            this._data = data;
            this._data.Target = RotateTranslateUtil.Instance.RandomTranslate(
                this._data.Target,
                this._data.Offset);
            this._origin = this._data.Handle.transform.position;
        }

        public void Update()
        {
            float move = this._data.Speed * Time.deltaTime;
            var newPosition = Vector3.Lerp(
                this._data.Handle.transform.position,
                this._data.Target,
                move);
            this._data.Handle.transform.position = newPosition;
            if (Vector3.Distance(this._data.Handle.transform.position, this._data.Target) <= this._data.Epsilon)
            {
                this._data.Handle.transform.position = this._data.Target;
                RotateTranslateUtil.Instance.RandomRotateRange(
                    this._data.Handle, 
                    this._data.Rotation);
                this._data.Handle.transform.SetParent(this._data.TargetObject.transform);
                var renderer = this._data.Handle.GetComponent<SpriteRenderer>();
                renderer.sprite = this._data.EmbedSprite;
                this.DoCallbacks();
                Destroy(this);
            }
        }

        public SRaycastMoveData GetData() { return this._data; }
    }
}
