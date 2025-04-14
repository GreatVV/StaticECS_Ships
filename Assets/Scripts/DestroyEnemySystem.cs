using FFS.Libraries.StaticEcs;

internal class DestroyEnemySystem : IUpdateSystem
{
    public void Update()
    {
        foreach (var e in W.QueryEntities.For<All<Enemy>, TagAll<Destroy>>())
        {
            if (e.HasAllOf<WaveRef>())
            {
                ref readonly var waveRef = ref e.Ref<WaveRef>();
                var waveInfoRef = waveRef.WaveInfoRef;
                if (waveInfoRef.TryUnpack<WT>(out var waveEntity))
                {
                    ref var items = ref waveEntity.Ref<SpawnedEnemiesForWave>().Items;
                    
                    for (int i = items.Count - 1; i >= 0; i--)
                    {
                        var item = items[(ushort)i];
                        if (item.entity.Equals(e.Pack()))
                        {
                            items.RemoveAt((ushort)i);
                        }
                    }

                    if (items.Count == 0)
                    {
                        waveEntity.Delete<SpawnedEnemiesForWave>();
                    }
                    
                }
            }
            
            UnityEngine.Object.Destroy(e.Ref<Enemy>().View.gameObject);
            e.Destroy();
        }
    }
}