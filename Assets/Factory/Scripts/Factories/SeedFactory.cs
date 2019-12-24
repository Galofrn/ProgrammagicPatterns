using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedFactory : MonoBehaviour, IBulletFactory
{
    public enum AmmoType
    {
        Flower,
        Knife,
        Missile
    }

    [SerializeField] Seed _seedPrefab;
    [SerializeField] AmmoType _ammoType;

    public Bullet CreateBullet()
    {
        var mySeed = Instantiate(_seedPrefab);
        mySeed.bulletsToSpawn = Random.Range(1, 5);
        switch (_ammoType)
        {
            case AmmoType.Flower:
                mySeed.bulletFactory = FindObjectOfType<FlowerFactory>();
                break;
            case AmmoType.Knife:
                mySeed.bulletFactory = FindObjectOfType<KnifeFactory>();
                break;
            case AmmoType.Missile:
                mySeed.bulletFactory = FindObjectOfType<MissileFactory>();
                break;
        }

        return mySeed;
    }
}
