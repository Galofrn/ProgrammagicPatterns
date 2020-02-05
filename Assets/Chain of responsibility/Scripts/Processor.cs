using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainOfResponsibility
{
    /// <summary>
    /// Chain of responsibility: Behaviour pattern. The idea is to handle an object (Zombie) to a group of operators (TurretProcessor).
    /// Each operator in the chain will decide if it processes the object or if it handles it to it's successor.
    /// </summary>
    /// <typeparam name="T">Object to process.</typeparam>
    public abstract class Processor<T> : MonoBehaviour
    {
        protected Processor<T> _successor;

        public virtual void HandleRequest(T toProcess)
        {
            if (!Process(toProcess) && _successor != null)
                _successor.HandleRequest(toProcess);
        }

        protected abstract bool Process(T toProcess);
    }
}
