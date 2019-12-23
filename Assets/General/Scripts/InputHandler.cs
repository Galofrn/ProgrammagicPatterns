using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Class for input handling.
/// </summary>
public class InputHandler : MonoBehaviour {

    public event Action<float> OnVerticalAxisChanged = delegate { };
    public event Action<float> OnHorizontalAxisChanged = delegate { };
    public event Action OnSteadyAxis = delegate { };

    public event Action<Vector2> OnMouseMoved = delegate { };

    public event Action OnClick = delegate { };

    public float verticalAxis
    {
        get { return Input.GetAxis("Vertical"); }
    }

    public float horizontalAxis
    {
        get { return Input.GetAxis("Horizontal"); }
    }

    void Start()
    {
        Cursor.visible = false;
    }

    void Update ()
    {
        if (Input.GetAxis("Vertical") != 0)
            OnVerticalAxisChanged(Input.GetAxis("Vertical"));        

        if (Input.GetAxis("Horizontal") != 0)
            OnHorizontalAxisChanged(Input.GetAxis("Horizontal"));

        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
            OnSteadyAxis();

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            OnMouseMoved(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));

        if (Input.GetMouseButtonDown(0))
            OnClick();
    }
}