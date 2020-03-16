using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Observer: The idea is to have observables that inform changes or events to subsribed observers.
/// In this case guards are both observers and observables. When one of them (observable) sees the player it informs
/// the other guards (observers) about the event.
/// </summary>
namespace Observer
{
    public abstract class Observable : MonoBehaviour
    {
        List<IObserver> _observers;

        public void Register(IObserver observer)
        {
            if (_observers == null)
                _observers = new List<IObserver>();
            _observers.Add(observer);
        }

        public void Unregister(IObserver observer)
        {
            _observers.Remove(observer);
        }

        protected void NotifyListeners()
        {
            foreach (var observer in _observers)
            {
                observer.Notify();
            }
        }
    }
}
