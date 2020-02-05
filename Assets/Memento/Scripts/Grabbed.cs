using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memento
{
    /// <summary>
    /// Memento: In this case, this object will save it's state in order to return to them when requested.
    /// The object that wants to be "remembered" is responsible for creating their state memories (GetMemento()) and 
    /// setting their state with given memories (SetMemento()), but this can also be done in another class.
    /// </summary>
    public class Grabbed : MonoBehaviour
    {
        [SerializeField] Rigidbody _rb;
        public event Action<Grabbed> OnGrabbed = delegate { };

        void Start()
        {
            if (!_rb)
                _rb = GetComponent<Rigidbody>();
        }

        public void Grab()
        {
            _rb.isKinematic = true;
            OnGrabbed(this);
        }

        public void UnGrab()
        {
            transform.SetParent(null);
            _rb.isKinematic = false;
        }

        public GrabbedMemento GetMemento()
        {
            return new GrabbedMemento(transform.position, transform.rotation);
        }

        public void SetMemento(GrabbedMemento memento)
        {
            transform.rotation = memento.rotation;
            StartCoroutine(StepByStepMementoRecovery(memento));
        }

        IEnumerator StepByStepMementoRecovery(GrabbedMemento memento)
        {
            float passedTime = 0;
            _rb.isKinematic = true;
            while(Vector3.Distance(transform.position, memento.position) > .01f)
            {
                transform.position = Vector3.Lerp(transform.position, memento.position, passedTime);
                yield return null;
                passedTime += Time.deltaTime;
            }
            _rb.isKinematic = false;
        }
    }
}
