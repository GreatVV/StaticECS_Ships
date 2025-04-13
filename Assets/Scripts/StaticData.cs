using UnityEngine;

[CreateAssetMenu]
public class StaticData : ScriptableObject
{
    public CharacterView CharacterView;
    public BulletView BulletView;
 
    public float ShootingInterval = 1f;
    public float StartVelocity;

    public Waves Waves;
}