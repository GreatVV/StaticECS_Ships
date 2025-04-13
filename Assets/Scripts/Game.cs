using System;
using FFS.Libraries.StaticEcs;
using FFS.Libraries.StaticEcs.Unity;
using UnityEngine;

public class Game : MonoBehaviour
{
    public SceneData SceneData;
    public StaticData StaticData;
    void Start()
    {
        E.Create(EcsConfig.Default());
        
        EcsDebug<WT>.AddWorld();
        
        // Registering components
        W.RegisterComponentType<Position>();
        W.RegisterComponentType<Velocity>();
        W.RegisterComponentType<Direction>();
        W.RegisterComponentType<Character>();
        W.RegisterComponentType<Shooting>();
        W.RegisterComponentType<Bullet>();
        W.RegisterComponentType<Delay>();
        W.RegisterComponentType<WaveRef>();
        W.RegisterComponentType<Enemy>();
        W.RegisterComponentType<WaveInfo>();
        W.RegisterComponentType<Team>();
        W.RegisterComponentType<Health>();
        W.RegisterComponentType<TransformRef>();
        W.RegisterComponentType<SpawnEnemyEvent>();
        W.RegisterTagType<PlayerTag>();
        W.RegisterTagType<EnemyTag>();
        W.RegisterTagType<Destroy>();
        W.RegisterMultiComponentType<SpawnedEnemiesForWave, EnemyRef>(10);
        W.RegisterMultiComponentType<Hits, HitInfo>(10);
        
        E.Context<SceneData>.Set(SceneData);
        E.Context<StaticData>.Set(StaticData);
        
        // Initializing the world
        E.Initialize();
        
        // Creating systems
        S.Create();
        S.AddCallOnce(new InitSystem());
        S.AddUpdate(new VelocitySystem());
        S.AddUpdate(new ShootingSystem());
        S.AddUpdate(new InputSystem());
        S.AddUpdate(new SpawnEnemyWaveSystem());
        S.AddUpdate(new DelaySystem());
        S.AddUpdate(new SpawnEnemySystem());
        S.AddUpdate(new EnemyLogicSystem());
        S.AddUpdate(new BulletCollisionSystem<PlayerTag, EnemyTag>());
        S.AddUpdate(new BulletCollisionSystem<EnemyTag, PlayerTag>());
        S.AddUpdate(new HitAnalyzeSystem());
        S.AddUpdate(new SyncPositionToTransformSystem());
        S.AddUpdate(new DestroyBulletSystem());
        S.AddUpdate(new DestroyEnemySystem());

        EcsDebug<WT>.AddSystem<MySystemsType>();
        // Initializing systems
        S.Initialize();
    }
    
    void Update()
    {
        S.Update();
    }

    private void OnDestroy()
    {
        S.Destroy();
        E.Destroy();
    }
}