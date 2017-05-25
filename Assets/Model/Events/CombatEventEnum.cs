namespace Model.Events
{
    public enum CombatEventEnum
    {
        None,
        AttackSelected,
        EndTurn,
        HexSelectedForMove,
        MapDoneLoading,
        PathTraversed,
        WeaponAbilityPerformed,
        ShowPotentialPath,
        TakingAction,
        TileDoubleClick,
        TraversePath,
        TraverseTile,
    }
}
