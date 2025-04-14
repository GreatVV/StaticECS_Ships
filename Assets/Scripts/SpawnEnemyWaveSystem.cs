using System;
using System.Linq;
using FFS.Libraries.StaticEcs;

internal class SpawnEnemyWaveSystem : IUpdateSystem
{
    public void Update()
    {
        foreach (var entity in W.QueryEntities.For<All<WaveInfo>, None<SpawnedEnemiesForWave>>())
        {
            ref var waveInfo = ref entity.Ref<WaveInfo>();
            var wave = waveInfo.Waves.WaveDescs[waveInfo.CurrentWave % waveInfo.Waves.WaveDescs.Length];
            var packedEntity = entity.Pack();
            foreach (var desc in wave.EnemySpawnDesc)
            {
                var spawnEnemyEvent = new SpawnEnemyEvent();
                spawnEnemyEvent.Prefab = desc.EnemyView;
                spawnEnemyEvent.WaveEntity = packedEntity;

                var delay = new Delay();
                delay.Value = wave.SpawnTime;
                for (int i = 0; i < desc.Amount; i++)
                {
                    W.Entity.New(spawnEnemyEvent, delay);
                }
                
            }

            entity.Add<SpawnedEnemiesForWave>();
            waveInfo.CurrentWave++;
        }
    }
}