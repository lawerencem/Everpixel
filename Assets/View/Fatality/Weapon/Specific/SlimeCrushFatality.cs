using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Model.Combat.Hit;
using Assets.Model.Event.Combat;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.Template.Utility;
using Assets.View.Character;
using Assets.View.Event;
using Assets.View.Map;
using Assets.View.Script.FX;
using Template.Script;
using UnityEngine;

namespace Assets.View.Fatality.Weapon.Ability
{
    public class SlimeCrushFatality : MFatality
    {
        private const string BURSTER = "Burster";
        private const string SLIME = "Slime";

        public SlimeCrushFatality(FatalityData data) : base(EFatality.Slime_Crush, data)
        {
            this._data.CustomPreFatalityBarks.Add("I'm a little teapot short and stout...");
        }

        public override void Init()
        {
            base.Init();
            base.Start(this.ProcessJolt);
        }

        private void ProcessJolt(object o)
        {
            var pos = Vector3.Lerp(
                this._data.Source.GameHandle.transform.position,
                this._data.Target.Handle.transform.position,
                FatalityParams.FATALITY_MELEE_LERP);
            var attack = this._data.Source.GameHandle.AddComponent<SAttackerJolt>();
            attack.Action = this._data.Action;
            attack.AddCallback(this.ProcessSlime);
            attack.Init(this._data.Source, pos, FatalityParams.FATALITY_ATTACK_SPEED);
        }

        private void ProcessSlime(object o)
        {
            var script = o as SAttackerJolt;
            var tgt = this._data.Target.Current as CChar;
            var slimeSprite = CharSpriteLoader.Instance.GetFatalitySprite(SLIME);
            var slime = new GameObject();
            var renderer = slime.AddComponent<SpriteRenderer>();
            renderer.sprite = slimeSprite;
            renderer.sortingLayerName = Layers.PARTICLES;
            var position = script.Action.Data.Target.Handle.transform.position;
            position.y += 10;
            slime.transform.position = position;
            slime.transform.SetParent(script.Action.Data.Target.Handle.transform);
            slime.transform.localScale = new Vector3(3, 3, 3);
            var move = slime.AddComponent<SRaycastMoveThenDelete>();
            var data = new SRaycastMoveData();
            data.Handle = slime;
            data.Speed = 3.5f;
            data.Target = script.Action.Data.Target.Handle.transform.position;
            move.AddCallback(this.ProcessEnd);
            move.AddObjectToList(script);
            move.Init(data);
        }

        private void ProcessEnd(object o)
        {
            var tgt = this._data.Target.Current as CChar;
            var position = tgt.GameHandle.transform.position;
            var move = o as SRaycastMoveThenDelete;
            var jolt = move.GetObjectList()[0] as SAttackerJolt;

            var slimePath = StringUtil.PathBuilder(
                CombatGUIParams.EFFECTS_PATH,
                "SlimeCrushSlimeFatality",
                CombatGUIParams.PARTICLES_EXTENSION);

            var bloodPath = StringUtil.PathBuilder(
                CombatGUIParams.EFFECTS_PATH,
                "SlimeCrushBloodFatality",
                CombatGUIParams.PARTICLES_EXTENSION);

            var slimePrefab = Resources.Load(slimePath);
            var slimeParticles = GameObject.Instantiate(slimePrefab) as GameObject;
            slimeParticles.transform.position = position;
            slimeParticles.name = CombatGUIParams.SLIME_FATALITY + " Slime Particles";

            var bloodPrefab = Resources.Load(bloodPath);
            var bloodParticles = GameObject.Instantiate(bloodPrefab) as GameObject;
            bloodParticles.transform.position = position;
            bloodParticles.name = CombatGUIParams.SLIME_FATALITY + " Blood Particles";

            var slimeLifetime = slimeParticles.AddComponent<SDestroyByLifetime>();
            slimeLifetime.Init(slimeParticles, 5f);
            var bloodLifetime = bloodParticles.AddComponent<SDestroyByLifetime>();
            bloodLifetime.Init(bloodParticles, 5f);
            bloodLifetime.AddCallback(this.CallbackHandler);
            foreach (var hit in jolt.Action.Data.Hits)
                bloodLifetime.AddCallback(hit.CallbackHandler);

            for (int i = 0; i < 5; i++)
            {
                var sprite = MapSpriteLoader.Instance.GetSlimeSplatter(3);
                this.LayFatalityDecoRandomPosition(sprite, tgt);
            }

            var data = new EvSplatterData();
            data.DmgPercent = 1.0;
            data.Fatality = true;
            data.Target = tgt.GameHandle;
            var e = new EvSplatter(data);
            e.TryProcess();

            VCharUtil.Instance.ProcessDeadChar(this._data.Target.Current as CChar);
        }
    }
}
