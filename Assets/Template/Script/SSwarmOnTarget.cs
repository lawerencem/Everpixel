using Assets.Template.Util;
using Assets.View;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Template.Script
{
    public class SSwarmOnTargetData
    {
        public float Dur { get; set; }
        public float Interval { get; set; }
        public float MaxOffset { get; set; }
        public float Speed { get; set; }
        public Sprite Sprite { get; set; }
        public List<GameObject> Swarm { get; set; }
        public Vector3 Target { get; set; }

        public SSwarmOnTargetData()
        {
            this.Swarm = new List<GameObject>();
        }
    }

    public class SSwarmOnTarget : AScript
    {
        private float _curInterval;
        private float _curTime;
        private SSwarmOnTargetData _data;

        public void Update()
        {
            this._curInterval += Time.deltaTime;
            this._curTime += Time.deltaTime;
            if (this._curTime >= this._data.Dur)
            {
                this.DoCallbacks();
                GameObject.Destroy(this);
            }
            else
            {
                if (this._curInterval >= this._data.Interval)
                {
                    this._curInterval = 0;
                    var swarm = new GameObject();
                    this._data.Swarm.Add(swarm);
                    var renderer = swarm.AddComponent<SpriteRenderer>();
                    renderer.sprite = this._data.Sprite;
                    swarm.transform.position = RandomPositionOffset.RandomOffset(
                        this._data.Target,
                        this._data.MaxOffset);
                    renderer.sortingLayerName = Layers.PARTICLES;
                    var raycastData = new SRaycastMoveData();
                    raycastData.Epsilon = 0.05f;
                    raycastData.Handle = swarm;
                    raycastData.Speed = 8f;
                    raycastData.Target = RandomPositionOffset.RandomOffset(
                        this._data.Target,
                        0.3f);
                    var raycast = swarm.AddComponent<SRaycastMove>();
                    raycast.Init(raycastData);
                }
            }
        }

        public List<GameObject> GetSwarm() { return this._data.Swarm; }
        public void Init(SSwarmOnTargetData data) { this._data = data; }
    }
}
