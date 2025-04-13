using FFS.Libraries.StaticEcs;
using Unity.Mathematics;

internal class EnemyLogicSystem : IUpdateSystem
{
    public void Update()
    {
        foreach (var entity in W.QueryEntities.For<All<Enemy, Position>>())
        {
            var enemyPosition = entity.Ref<Position>().Value;
            
            var minDistance = float.MaxValue;
            float3 target = default;
            foreach (var playerEntity in W.QueryEntities.For<All<Character, Position>>())
            {
                var transformPosition = playerEntity.Ref<Position>().Value;
                var distance = math.distancesq(enemyPosition, transformPosition);
                if (distance < minDistance)
                {
                    target = transformPosition;
                    minDistance = distance;
                }
            }

            var direction = math.normalize(target - enemyPosition);
            entity.TryAdd<Direction>().Value = direction;
        }
    }
}