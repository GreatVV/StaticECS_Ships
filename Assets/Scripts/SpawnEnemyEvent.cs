using FFS.Libraries.StaticEcs;

internal struct SpawnEnemyEvent : IComponent
{
    public EnemyView Prefab;
    public PackedEntity WaveEntity;
}