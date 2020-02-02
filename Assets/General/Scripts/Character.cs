using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for every character in the examples.
/// </summary>
namespace General
{
    public abstract class Character : MonoBehaviour
    {
        public int life;
        public float speed;
        public float rotationSpeed;
        [SerializeField] protected Rigidbody _rb;

        void Start()
        {
            if (_rb == null)
                _rb = GetComponent<Rigidbody>();
        }

        protected abstract void Move();
    }
}
