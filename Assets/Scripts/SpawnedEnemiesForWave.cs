using FFS.Libraries.StaticEcs;

public struct SpawnedEnemiesForWave : IMultiComponent<EnemyRef> {
    // Определим значения мультикомпонента
    public Multi<EnemyRef> Items;
    public void Access<A>(A access) where A : struct, AccessMulti<EnemyRef> => access.For(ref Items);
}