using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Ability.Magic.Astral;
using Assets.Model.Ability.Magic.Fighting;
using Assets.Model.Ability.Magic.Light;
using Assets.Model.Ability.Magic.Poison;
using Assets.Model.Ability.Magic.Psychic;
using Assets.Model.Ability.Magic.Water;
using Assets.Model.Ability.Music;
using Assets.Model.Ability.Physical;
using Assets.Model.Ability.Shapeshift;
using Assets.Model.Weapon.Abilities;

namespace Assets.Data.Ability.XML
{
    public class AbilityMediator
    {
        public void HandleType(EAbility type)
        {
            var table = AbilityTable.Instance.Table;

            switch (type)
            {
                case (EAbility.Aim): { table.Add(type, new Aim()); } break;
                case (EAbility.Attack_Of_Opportunity): { table.Add(type, new AttackOfOpportunity()); } break;
                case (EAbility.Bite): { table.Add(type, new Bite()); } break;
                case (EAbility.Bulldoze): { table.Add(type, new Bulldoze()); } break;
                case (EAbility.Break_Armor): { table.Add(type, new BreakArmor()); } break;
                case (EAbility.Break_Shield): { table.Add(type, new BreakShield()); } break;
                case (EAbility.Charge): { table.Add(type, new Charge()); } break;
                case (EAbility.Cerebral_Nova): { table.Add(type, new CerebralNova()); } break;
                case (EAbility.Chop): { table.Add(type, new Chop()); } break;
                case (EAbility.Crush): { table.Add(type, new Crush()); } break;
                case (EAbility.Eldritch_Chomp): { table.Add(type, new EldrtichChomp()); } break;
                case (EAbility.Feeblemind): { table.Add(type, new FeebleMind()); } break;
                case (EAbility.Fire): { table.Add(type, new Fire()); } break;
                case (EAbility.Healing_Touch): { table.Add(type, new HealingTouch()); } break;
                case (EAbility.Gash): { table.Add(type, new Gash()); } break;
                case (EAbility.Great_Strike): { table.Add(type, new GreatStrike()); } break;
                case (EAbility.Hadoken): { table.Add(type, new Hadoken()); } break;
                case (EAbility.Haste_Song): { table.Add(type, new HasteSong()); } break;
                case (EAbility.Hold_Person): { table.Add(type, new HoldPerson()); } break;
                case (EAbility.Intellect): { table.Add(type, new Intellect()); } break;
                case (EAbility.Kamehameha): { table.Add(type, new Kamehameha()); } break;
                case (EAbility.Maim): { table.Add(type, new Maim()); } break;
                case (EAbility.Mental_Laceration): { table.Add(type, new MentalLaceration()); } break;
                case (EAbility.Mind_Blast): { table.Add(type, new MindBlast()); } break;
                case (EAbility.Mind_Hunt): { table.Add(type, new MindHunt()); } break;
                case (EAbility.Orc_Metal): { table.Add(type, new OrcMetal()); } break;
                case (EAbility.Pain_Link): { table.Add(type, new PainLink()); } break;
                case (EAbility.Pierce): { table.Add(type, new Pierce()); } break;
                case (EAbility.Psychic_Artillery): { table.Add(type, new PsychicArtillery()); } break;
                case (EAbility.Pull): { table.Add(type, new Pull()); } break;
                case (EAbility.Quick_Heal): { table.Add(type, new QuickHeal()); } break;
                case (EAbility.Riposte): { table.Add(type, new Riposte()); } break;
                case (EAbility.Scatter): { table.Add(type, new Scatter()); } break;
                case (EAbility.Sever): { table.Add(type, new Sever()); } break;
                case (EAbility.Soothing_Mist): { table.Add(type, new SoothingMist()); } break;
                case (EAbility.Shield_Bash): { table.Add(type, new ShieldBash()); } break;
                case (EAbility.Shield_Wall): { table.Add(type, new ShieldWall()); } break;
                case (EAbility.Shove): { table.Add(type, new Shove()); } break;
                case (EAbility.Slash): { table.Add(type, new Slash()); } break;
                case (EAbility.Slime_Rain): { table.Add(type, new SlimeRain()); } break;
                case (EAbility.Spear_Wall): { table.Add(type, new SpearWall()); } break;
                case (EAbility.Stab): { table.Add(type, new Stab()); } break;
                case (EAbility.Stun): { table.Add(type, new Stun()); } break;
                case (EAbility.Suppress_Area): { table.Add(type, new SuppressArea()); } break;
                case (EAbility.Summon_Shoggoth): { table.Add(type, new SummonShoggoth()); } break;
                case (EAbility.Weenlight_Sonata): { table.Add(type, new WeenlightSonata()); } break;
                case (EAbility.Wereween): { table.Add(type, new Wereween()); } break;
                case (EAbility.Wide_Strike): { table.Add(type, new WideStrike()); } break;
                case (EAbility.Whirlwind): { table.Add(type, new Whirlwind()); } break;
                case (EAbility.Wrap): { table.Add(type, new Wrap()); } break;
            }
        }
    }
}
