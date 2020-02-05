using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] float _speed;
        [SerializeField] int _damage;
        public Transform target;

        // Update is called once per frame
        void Update()
        {
            transform.position += transform.up * _speed * Time.deltaTime;
            if (target)
                transform.up = Vector3.Slerp(transform.up, (target.position - transform.position).normalized, .8f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy)
                enemy.Damage(_damage);
            Destroy(gameObject);
        }
    }
}
