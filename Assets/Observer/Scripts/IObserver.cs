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
    public interface IObserver
    {
        void Notify();
    }
}