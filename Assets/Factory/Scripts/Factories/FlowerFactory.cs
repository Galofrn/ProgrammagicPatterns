using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerFactory : MonoBehaviour, IBulletFactory
{
    [SerializeField] Flower _flowerPrefab;

    public Bullet CreateBullet()
    {
        return Instantiate(_flowerPrefab);
    }
}
