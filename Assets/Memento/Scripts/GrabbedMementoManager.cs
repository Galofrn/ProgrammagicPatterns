using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

namespace Memento
{
    /// <summary>
    /// Class in charge of saving all the mementos and setting them when requested.
    /// In this case mementos are saved when the player grabs food and applied when the
    /// player steps on the magic circle.
    /// </summary>
    public class GrabbedMementoManager : MonoBehaviour
    {
        Dictionary<Grabbed, GrabbedMemento> _grabbedMementoPair;

        [SerializeField] ParticleSystem[] _effects;

        void Start()
        {
            _grabbedMementoPair = new Dictionary<Grabbed, GrabbedMemento>();
            var allGrabbeds = FindObjectsOfType<Grabbed>();

            foreach (var grabbed in allGrabbeds)
            {
                grabbed.OnGrabbed += x => _grabbedMementoPair[x] = x.GetMemento();
                _grabbedMementoPair[grabbed] = grabbed.GetMemento();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Player>())
            {
                foreach (var pair in _grabbedMementoPair)
                {
                    pair.Key.SetMemento(_grabbedMementoPair[pair.Key]);
                }

                foreach (var particle in _effects)
                {
                    particle.Play();
                }
            }
        }
    }
}
