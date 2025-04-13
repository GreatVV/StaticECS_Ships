using System;
using UnityEngine;

public class BulletView : MonoBehaviour
{
    public float StartSpeed = 20;
    public float Radius;
    public int Damage;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}