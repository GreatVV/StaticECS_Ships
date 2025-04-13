using FFS.Libraries.StaticEcs;
using Unity.Mathematics;
using UnityEngine;

internal class InputSystem : IUpdateSystem
{
    public void Update()
    {
      
        foreach (var entity in W.QueryEntities.For<All<Character, Direction>>())
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            if (x != 0 || y != 0)
            {
                entity.RefMut<Direction>().Value = math.normalize(new float3(x, y, 0));
                entity.RefMut<Velocity>().Value = entity.Ref<Character>().View.Speed;
            }
            else
            {
                entity.RefMut<Velocity>().Value = 0;
            }
        }
    }
}