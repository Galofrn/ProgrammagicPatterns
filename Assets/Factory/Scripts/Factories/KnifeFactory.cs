using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeFactory : MonoBehaviour, IBulletFactory
{
    [SerializeField] Knife _knifePrefab;

    public Bullet CreateBullet()
    {
        var myKnife = Instantiate(_knifePrefab);
        myKnife.rb.AddTorque(transform.right * myKnife.rotationSpeed);
        return myKnife;
    }

}
