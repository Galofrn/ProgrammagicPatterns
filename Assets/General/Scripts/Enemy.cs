using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Base class for every character who's a threat to the player.
/// Takes damage and dies if it runs out of health.
/// </summary>
namespace General
{
    public class Enemy : Character
    {
        [SerializeField] protected NavMeshAgent _navMeshAgent;
        public Transform destination;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            if (!_navMeshAgent)
                _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = speed;
            _navMeshAgent.angularSpeed = rotationSpeed;
        }

        protected override void Move()
        {
            if (destination)
                _navMeshAgent.SetDestination(destination.position);
        }

        public virtual void Damage(int damageAmmount)
        {
            life -= damageAmmount;
            if (life <= 0)
                Kill();
        }

        protected virtual void Kill()
        {
            Destroy(gameObject);
        }
    }
}
