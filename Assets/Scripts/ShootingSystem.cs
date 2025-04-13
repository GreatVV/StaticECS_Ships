using FFS.Libraries.StaticEcs;
using UnityEngine;

internal class ShootingSystem : IUpdateSystem
{
    public void Update()
    {
        foreach (var entity in W.QueryEntities.For<All<Position, Direction, Shooting, Team>>())
        {
            ref var shooting = ref entity.RefMut<Shooting>();
            if (Time.time - shooting.LastShootingTime > shooting.Interval)
            {
                var staticData = E.Context<StaticData>.Get();
                var position = entity.Ref<Position>().Value;
                var direction = entity.Ref<Direction>().Value;
             
                var bulletView = Object.Instantiate(staticData.BulletView, position, Quaternion.FromToRotation(Vector3.up, direction));
                var bulletEntity = E.Entity.New(new Bullet()
                    {
                        View = bulletView
                    }, new Position()
                    {
                        Value = position
                    }, new Direction()
                    {
                        Value = direction
                    }, new Velocity()
                    {
                        Value = bulletView.StartSpeed
                    },
                    new TransformRef()
                    {
                        Value = bulletView.transform
                    }
                );
                bulletEntity.TryAdd<Team>().Id = entity.Ref<Team>().Id;
                if (entity.HasAllOfTags<PlayerTag>())
                {
                    bulletEntity.SetTag<PlayerTag>();
                }
                else
                {
                    if (entity.HasAllOfTags<EnemyTag>())
                    {
                        bulletEntity.SetTag<EnemyTag>();
                    }
                }
                shooting.LastShootingTime = Time.time;
            }
        }
    }
}