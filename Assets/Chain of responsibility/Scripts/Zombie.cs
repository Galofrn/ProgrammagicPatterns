using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Zombie enemy that walks towards a destination. Used in the chain of responsibility example.
/// </summary>
public class Zombie : Enemy
{
    [SerializeField] Animator _animator;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Move();
    }

    void Update()
    {
        _animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude);
    }

    protected override void Move()
    {
        base.Move();
    }
}
