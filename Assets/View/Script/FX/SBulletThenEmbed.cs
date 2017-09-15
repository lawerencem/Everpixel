using Assets.Controller.Character;
using Assets.Model.Action;
using Assets.Model.Combat.Hit;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.Template.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.View.Script.FX
{
    public class SBulletThenEmbedData : SRaycastMoveData
    {
        public Sprite EmbedSprite { get; set; }
        public float Offset { get; set; }
        public float Rotation { get; set; }
        public MHit Hit;
        public CharController TargetChar;
        public List<GameObject> TargetableObjects;
        public GameObject TargetObject { get; set; }

        public SBulletThenEmbedData()
        {
            this.TargetableObjects = new List<GameObject>();
        }
    }

    public class SBulletThenEmbed : AScript
    {
        private SBulletThenEmbedData _data;
        public MAction Action { get; set; }

        public void Init(SBulletThenEmbedData data)
        {
            this._data = data;
            this.AddCallback(this.Embed);
            this._data.TargetObject = ListUtil<GameObject>.GetRandomElement(this._data.TargetableObjects);
            this._data.Target = RotateTranslateUtil.Instance.RandomTranslate(
                this._data.Target,
                this._data.Offset);
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

        private void Embed(object o)
        {
            var random = ListUtil<GameObject>.GetRandomElement(this._data.TargetableObjects);
            if (random != null)
            {
                var handleRenderer = this._data.Handle.GetComponent<SpriteRenderer>();
                var targetRenderer = random.GetComponent<SpriteRenderer>();
                if (handleRenderer != null && targetRenderer != null)
                {
                    handleRenderer.transform.SetParent(random.transform);
                    handleRenderer.sortingLayerName = targetRenderer.sortingLayerName;
                    handleRenderer.sortingOrder = targetRenderer.sortingOrder + 1;
                    if (this._data.TargetChar != null)
                        this._data.TargetChar.Embedded.Add(this._data.Handle);
                }
            }
        }
    }
}
