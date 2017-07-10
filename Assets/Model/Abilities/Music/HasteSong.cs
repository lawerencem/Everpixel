using Characters.Params;
using Controller.Characters;
using Controller.Managers;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities.Music
{
    public class HasteSong : GenericSong
    {
        // TODO Shift to .xml file
        public int APMod = 2;

        public HasteSong() : base(AbilitiesEnum.Haste_Song)
        {
            this.CastType = CastTypeEnum.Song;
            this._songType = SongTypeEnum.BlueMusic;
        }

        public override void ProcessAbility(PerformActionEvent e, HitInfo hit)
        {
            base.ProcessAbility(e, hit);
            base.ProcessSong(hit);
            var team = hit.Source.LParty;
            var tiles = hit.TargetTile.Model.GetAoETiles((int)this.AoE);
            foreach(var tile in tiles)
            {
                if (tile.Current != null && tile.Current.GetType().Equals(typeof(GenericCharacterController)))
                {
                    var toBuff = tile.Current as GenericCharacterController;
                    if (toBuff.LParty == team)
                    {
                        var buff = new FlatSecondaryStatModifier(SecondaryStatsEnum.AP, (int)this.Duration, this.APMod);
                        var buffEvent = new BuffEvent(CombatEventManager.Instance, buff, toBuff);
                    }
                }
            }
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return true;
        }
    }
}
