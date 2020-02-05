using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

namespace Memento
{
    /// <summary>
    /// Player that can grab stuff. Used for the memento example.
    /// </summary>
    public class Grabber : Player
    {
        [SerializeField] float _grabbingRange;
        Grabbed _grabbed;

        protected override void Action()
        {
            if (!_grabbed)
            {
                Ray myRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                RaycastHit rayInfo = new RaycastHit();

                if (Physics.Raycast(myRay, out rayInfo, _grabbingRange))
                {
                    Grabbed grabbed = rayInfo.transform.GetComponent<Grabbed>();
                    if (grabbed)
                    {
                        grabbed.transform.SetParent(_pov);
                        grabbed.Grab();
                        _grabbed = grabbed;
                    }
                }
            }
            else
            {
                _grabbed.UnGrab();
                _grabbed = null;
            }
        }
    }
}
