using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class MissileFactory : MonoBehaviour, IBulletFactory
    {
        [SerializeField] Missile _missilePrefab;

        public Bullet CreateBullet()
        {
            return Instantiate(_missilePrefab);
        }
    }
}
