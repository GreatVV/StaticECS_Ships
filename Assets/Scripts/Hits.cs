using FFS.Libraries.StaticEcs;

internal struct Hits : IMultiComponent<HitInfo>
{
    public Multi<HitInfo> Items;
    public void Access<A>(A access) where A : struct, AccessMulti<HitInfo> => access.For(ref Items);
}