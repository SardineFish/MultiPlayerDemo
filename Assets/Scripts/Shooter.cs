using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{
    public Vector2 Offset;
    public GameObject BulletPrefab;
    public float CoolDown = 0.2f;

    float lastFireTime = 0;

    public void Fire()
    {
        if (Time.time - lastFireTime < CoolDown)
            return;
        lastFireTime = Time.time;
        var bullet = Instantiate(BulletPrefab);
        bullet.transform.rotation = transform.rotation;
        bullet.transform.position = transform.position + transform.localToWorldMatrix.MultiplyVector(Offset.ToVector3());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.localToWorldMatrix.MultiplyVector(Offset.ToVector3()), .1f);
    }
}
