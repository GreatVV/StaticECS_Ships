using FFS.Libraries.StaticEcs;
using UnityEngine;

public class DelaySystem : IUpdateSystem
{
    public void Update()
    {
        foreach (var entity in W.QueryEntities.For<All<Delay>>())
        {
            ref var delay = ref entity.Ref<Delay>();
            delay.Value -= Time.deltaTime;
            if (delay.Value <= 0)
            {
                entity.Delete<Delay>();
            }
        }
    }
}