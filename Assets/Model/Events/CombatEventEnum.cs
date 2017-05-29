namespace Model.Events
{
    public enum CombatEventEnum
    {
        None,
        ActionCofirmed,
        AttackSelected,
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
        TakingAction,
        TileDoubleClick,
        TileHoverDeco,
        TileSelected,
        TraversePath,
        TraverseTile,
    }
}
