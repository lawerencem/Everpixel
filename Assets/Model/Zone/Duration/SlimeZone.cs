﻿using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability.Logic.Calculator;
using Assets.Model.Effect;
using Assets.Model.Effect.Fortitude;
using Assets.Template.CB;
using Assets.Template.Util;

namespace Assets.Model.Zone.Duration
{
    public class SlimeZone : AZone
    {
        public SlimeZone() : base(EZone.Slime_Zone) { }

        public override void ProcessEnterZone(TileMoveData moveData)
        {
            base.ProcessEnterZone(moveData);
            var resist = new ResistCalculator();
            if (!resist.DidResist(moveData.Target, this._data.Effect, this._data.ResistBase))
            {
                var slime = this.GetSlimeEffect();
                this._data.Effect.ApplyEffectFx(moveData.Target.Tile);
                moveData.Target.Proxy.AddEffect(slime);
            }
        }

        private EffectSlime GetSlimeEffect()
        {
            var slime = new EffectSlime();
            var data = new MEffectData();
            data.Duration = (int)this._data.X;
            data.X = this._data.Y;
            data.ParticlePath = StringUtil.PathBuilder(
                CombatGUIParams.EFFECTS_PATH,
                "Slime",
                CombatGUIParams.PARTICLES_EXTENSION);
            data.Type = EEffect.Slime;
            slime.SetData(data);
            return slime;
        }
    }
}
