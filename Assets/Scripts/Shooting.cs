using FFS.Libraries.StaticEcs;

internal struct Shooting : IComponent
{
    public float Interval;
    public float LastShootingTime;
}