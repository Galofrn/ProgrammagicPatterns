using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memento
{
    /// <summary>
    /// Memento: The idea is to save an object's state (Grabbed) in a memento model (GrabbedMemento) for later use.
    /// The object will modify it's state and we can return to a previous state by using this object.
    /// </summary>
    public class GrabbedMemento
    {
        public GrabbedMemento(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }

        public Vector3 position;
        public Quaternion rotation;
    }
}
