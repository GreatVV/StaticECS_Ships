using FFS.Libraries.StaticEcs;
using Unity.Mathematics;
using UnityEngine;

internal class BulletCollisionSystem<Tag1, Tag2> : IUpdateSystem where Tag1 : struct, ITag where Tag2 : struct ,ITag
{
    public void Update()
    {
        foreach (var bulletEntity in W.QueryEntities.For<All<Bullet, Position>, TagAll<Tag1>>())
        {
            var bulletView = bulletEntity.Ref<Bullet>().View;
            var position = bulletEntity.Ref<Position>().Value;
            foreach (var otherEntity in W.QueryEntities.For<All<Position>, None<Bullet>, TagAll<Tag2>>())
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