using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Factory: Creational pattern. In this example this factory creates a Bullet object. We can implement
/// this interface in different factories to return different kinds of bullet (KnifeFactory,
/// SeedFactory, MissileFactory). This way the user (Shooter) can ask for a bullet and get 
/// different kinds of the same object without specifying wich.
/// </summary>
namespace Factory
{
    public interface IBulletFactory
    {
        Bullet CreateBullet();
    }
}
