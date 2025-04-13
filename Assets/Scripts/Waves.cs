using System;

[Serializable]
public class Waves 
{
    [Serializable]
    public class WaveDesc
    {
        public float SpawnTime;
        public EnemySpawnDesc[] EnemySpawnDesc;
    }
    
    [Serializable]
    public class EnemySpawnDesc
    {
        public EnemyView EnemyView;
        public int Amount;
    }

    public WaveDesc[] WaveDescs;
}