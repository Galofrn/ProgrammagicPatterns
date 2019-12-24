using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In this case, Seed can also use IBulletFactory to spawn any type of Bullet.
/// </summary>
public class Seed : Bullet
{
    public int bulletsToSpawn;
    public IBulletFactory bulletFactory;

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < bulletsToSpawn; i++)
        {
            Vector3 randomPosition = new Vector3(Random.insideUnitCircle.x * i, 0, Random.insideUnitCircle.y * i);
            var newBullet = bulletFactory.CreateBullet();
            newBullet.transform.position = this.transform.position + randomPosition;
        }
        Destroy(gameObject);
    }
}
