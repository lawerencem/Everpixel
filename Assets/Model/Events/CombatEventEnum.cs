﻿namespace Model.Events
{
    public enum CombatEventEnum
    {
        None,
        ApplyInjury,
        AttackSelected,
        Casting,
        CharacterKilled,
        DamageCharacter,
        DisplayHitStats,
        EndTurn,
        HexSelectedForMove,
        MapDoneLoading,
        PathTraversed,
        PerformActionEvent,
        WeaponAbilityPerformed,
        ShowPotentialPath,
        Shapeshift,
        Summon,
        TakingAction,
        TileDoubleClick,
        TileHoverDeco,
        TileSelected,
        TraversePath,
        TraverseTile,
    }
}
