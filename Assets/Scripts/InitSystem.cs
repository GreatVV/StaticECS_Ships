using FFS.Libraries.StaticEcs;
using Unity.Mathematics;
using UnityEngine;

internal class InitSystem : IInitSystem
{
    public void Init()
    {
        var staticData = W.Context<StaticData>.Get();
        var sceneData = W.Context<SceneData>.Get();
        var characterView = Object.Instantiate(staticData.CharacterView, sceneData.SpawnPosition.position,
            Quaternion.identity);
        var playerEntity = W.Entity.New(new Character()
            {
                View = characterView
            }, new Shooting
            {
                Interval = staticData.ShootingInterval
            }, new Position()
            {
                Value = sceneData.SpawnPosition.position
            }, new Velocity()
            {
                Value = staticData.StartVelocity
            },
            new Direction()
            {
                Value = new float3(0, 1, 0)
            }
        );
        playerEntity.Add<TransformRef>().Value = characterView.transform;
        playerEntity.Add<Team>().Id = Team.Player;
        playerEntity.Add<Health>().Immortal = true;

        W.Entity.New(new WaveInfo()
        {
            Waves = staticData.Waves
        });
    }
}