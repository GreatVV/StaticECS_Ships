using FFS.Libraries.StaticEcs;
using UnityEngine;

public readonly struct VelocitySystem : IUpdateSystem
{
    public void Update()
    {
        var delta = Time.deltaTime;
        foreach (var entity in W.QueryEntities.For<All<Position, Velocity, Direction>>())
        {
            entity.RefMut<Position>().Value += entity.Ref<Velocity>().Value * entity.Ref<Direction>().Value * delta;
        }
    }
}