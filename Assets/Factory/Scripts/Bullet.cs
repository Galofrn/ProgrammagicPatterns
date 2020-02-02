using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Factory: Creational pattern. The idea is to let the user (Shooter) ask for and object (Bullet)
/// without specifying what type of object he wants. In this example Shooter asks for a Bullet
/// to the IBulletFactory and he can get different kinds of bullets (Knife, Missile, Seed) depending
/// on wich specific implementation of IBulletFactory he chooses.
/// </summary> 
namespace Factory
{
    public abstract class Bullet : MonoBehaviour
    {
        public int damage;
        public Rigidbody rb;
    }
}
