using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class Flower : Bullet
    {
        private void OnCollisionEnter(Collision collision)
        {
            GetComponent<Animator>().Play(0);
            Destroy(GetComponent<Collider>());
            Destroy(rb);
        }
    }
}
