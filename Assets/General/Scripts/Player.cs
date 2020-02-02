using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for every character controlled by the player. Has the ability to move and look around.
/// </summary>
namespace General
{
    public class Player : Character
    {
        protected InputHandler _inputHandler;
        [SerializeField] Transform _pov;

        protected virtual void Start()
        {
            _inputHandler = FindObjectOfType<InputHandler>();

            //Movement
            _inputHandler.OnVerticalAxisChanged += y => Move();
            _inputHandler.OnHorizontalAxisChanged += x => Move();
            _inputHandler.OnSteadyAxis += () => _rb.velocity = Vector3.zero;

            //Rotation
            _inputHandler.OnMouseMoved += delta => Look(delta);

            //Action
            _inputHandler.OnClick += Action;
        }

        protected override void Move()
        {
            Vector3 movement = transform.forward * _inputHandler.verticalAxis + transform.right * _inputHandler.horizontalAxis;
            _rb.velocity = movement.normalized * speed;
        }

        void Look(Vector2 rot)
        {
            _rb.AddTorque(new Vector3(0, rot.x, 0) * rotationSpeed);
            //The camera looks at the POV with cinemachine.
            _pov.transform.position += Vector3.up * rot.y * rotationSpeed * Time.deltaTime;
        }

        protected virtual void Action() { }
    }
}
