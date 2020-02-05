using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

/// <summary>
/// Zombie enemy that walks towards a destination and announces movement. 
/// Used in the chain of responsibility example.
/// </summary>
namespace ChainOfResponsibility
{
    public class Zombie : Enemy
    {
        [SerializeField] Animator _animator;
        Action onMoved = delegate { };

        protected override void Start()
        {
            base.Start();
            foreach (var turret in FindObjectsOfType<TurretProcessor>())
            {
                onMoved += () => turret.HandleRequest(this);
            }
            Move();
        }

        void Update()
        {
            _animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude);
        }

        protected override void Move()
        {
            base.Move();
            StartCoroutine(CheckMovement());
        }

        IEnumerator CheckMovement()
        {
            while (true)
            {
                yield return new WaitForSeconds(.001f);
                onMoved();
            }
        }
    }
}
