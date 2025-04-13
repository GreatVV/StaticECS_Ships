using FFS.Libraries.StaticEcs;
using Unity.Mathematics;
using UnityEngine;

internal class BulletCollisionSystem : IUpdateSystem
{
    public void Update()
    {
        foreach (var bulletEntity in W.QueryEntities.For<All<Bullet, Position, Team>>())
        {
            var bulletView = bulletEntity.Ref<Bullet>().View;
            var position = bulletEntity.Ref<Position>().Value;
            var team = bulletEntity.Ref<Team>().Id;
            foreach (var otherEntity in W.QueryEntities.For<All<Team, Position>, None<Bullet>>())
            {
                if (otherEntity.Ref<Team>().Id != team)
                {
                    var enemyPosition = otherEntity.Ref<Position>().Value;
                    if (math.distancesq(position, enemyPosition) < bulletView.Radius)
                    {
                        otherEntity.TryAdd<Hits>().Items.Add(new HitInfo()
                        {
                            From = bulletEntity.Pack(),
                            Damage = bulletView.Damage
                        });
                    }
                }
            }
        }
    }
}

public struct Team : IComponent
{
    public int Id;
    public const int Enemy = 2;
    public const int Player = 1;
}