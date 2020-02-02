using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class FlowerFactory : MonoBehaviour, IBulletFactory
    {
        [SerializeField] Flower _flowerPrefab;

        public Bullet CreateBullet()
        {
            return Instantiate(_flowerPrefab);
        }
    }
}
