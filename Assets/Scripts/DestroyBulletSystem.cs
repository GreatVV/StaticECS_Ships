using FFS.Libraries.StaticEcs;

internal class DestroyBulletSystem : IUpdateSystem
{
    public void Update()
    {
        foreach (var e in W.QueryEntities.For<All<Bullet>, TagAll<Destroy>>())
        {
            UnityEngine.Object.Destroy(e.Ref<Bullet>().View.gameObject);
            e.Destroy();
        }
    }
}