using FFS.Libraries.StaticEcs;

public struct Team : IComponent
{
    public int Id;
    public const int Enemy = 2;
    public const int Player = 1;
}