using FFS.Libraries.StaticEcs;

internal struct WaveInfo : IComponent
{
    public int CurrentWave;
    public Waves Waves;
}