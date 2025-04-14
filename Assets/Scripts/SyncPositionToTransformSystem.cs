using FFS.Libraries.StaticEcs;

internal class SyncPositionToTransformSystem : IUpdateSystem
{
    public void Update()
    {
        foreach (var e in W.QueryEntities.For<All<TransformRef, Position, Direction>>())
        {
            var transform = e.Ref<TransformRef>().Value;
            transform.position = e.Ref<Position>().Value;
            transform.up = e.Ref<Direction>().Value;
        }
    }
}