using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class Missile : Bullet
    {
        [SerializeField] ParticleSystem _explosionPrefab;
        private void OnCollisionEnter(Collision collision)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
