using FFS.Libraries.StaticEcs;
using UnityEngine;

internal class HitAnalyzeSystem : IUpdateSystem
{
    public void Update()
    {
        foreach (var e in W.QueryEntities.For<All<Hits, Health>>())
        {
            ref readonly var hits = ref e.Ref<Hits>();
            ref var health = ref e.Ref<Health>();
            foreach (var hitInfo in hits.Items)
            {
                //Debug.Log($"Hit to {e} from {hitInfo.From} at {Time.frameCount}");
                if (!health.Immortal)
                {
                    health.Value -= hitInfo.Damage;
                    if (health.Value <= 0)
                    {
                        e.SetTag<Destroy>();
                    }
                }
                if (hitInfo.From.TryUnpack<WT>(out var from))
                {
                    from.SetTag<Destroy>();
                }
            }
            e.Delete<Hits>();
        }
    }
}

internal struct Health : IComponent
{
    public int Value;
    public bool Immortal;
}