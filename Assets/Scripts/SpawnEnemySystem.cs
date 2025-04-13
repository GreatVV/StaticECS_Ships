using FFS.Libraries.StaticEcs;
using UnityEngine;

internal class SpawnEnemySystem : IUpdateSystem
{
    public void Update()
    {
        foreach (var entity in W.QueryEntities.For<All<SpawnEnemyEvent>, None<Delay>>())
        {
            var spawnEnemyEvent = entity.Ref<SpawnEnemyEvent>();
            if (spawnEnemyEvent.WaveEntity.TryUnpack<WT>(out var waveEntity))
            {
                var sceneData = E.Context<SceneData>.Get();
                var position = sceneData.SpawnPosition.position +
                               (Vector3)Random.insideUnitCircle.normalized * sceneData.EnemySpawnRadius;
                var enemy = Object.Instantiate(spawnEnemyEvent.Prefab,
                    position, Quaternion.identity);
                
                ref var items = ref waveEntity.RefMut<SpawnedEnemiesForWave>().Items;

                var enemyEntity = E.Entity.New(new Enemy()
                    {
                        View = enemy,
                    },
                    new Position()
                    {
                        Value = enemy.transform.position
                    },
                    new Velocity()
                    {
                        Value = enemy.StartSpeed
                    },
                    new Shooting()
                    {
                        Interval = enemy.ShootingInterval
                    },
                    new WaveRef()
                    {
                        WaveInfoRef = spawnEnemyEvent.WaveEntity,
                    }
                );
                enemyEntity.Add<TransformRef>().Value = enemy.transform;
                enemyEntity.Add<Team>().Id = Team.Enemy;
                enemyEntity.Add<Health>().Value = enemy.StartHealth;

               
                items.Add(new EnemyRef()
                {
                    entity = enemyEntity.Pack()
                });
            }

            entity.Delete<SpawnEnemyEvent>();
        }
    }
}