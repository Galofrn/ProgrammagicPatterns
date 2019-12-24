using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player that shoots and switches between ammo. Used for the factory example.
/// </summary>
public class Shooter : Player
{
    IBulletFactory _factory;
    [SerializeField] Transform _shootPoint;
    public float shootForce;
    [SerializeField] TextMesh _ammoText;

    protected override void Start()
    {
        base.Start();

        _inputHandler.On1Pressed += x => SwitchAmmo(x);
        _inputHandler.On2Pressed += x => SwitchAmmo(x);
        _inputHandler.On3Pressed += x => SwitchAmmo(x);
    }

    protected override void Action()
    {
        if(_factory != null)
        {
            var myBullet = _factory.CreateBullet();
            myBullet.transform.position = _shootPoint.position;
            myBullet.transform.forward = _shootPoint.forward;
            myBullet.rb.AddForce(myBullet.transform.forward * shootForce, ForceMode.Impulse);
        }
    }

    void SwitchAmmo(int ammoType)
    {
        switch (ammoType)
        {
            case 1:
                _factory = FindObjectOfType<KnifeFactory>();
                _ammoText.text = "Knife";
                break;

            case 2:
                _factory = FindObjectOfType<MissileFactory>();
                _ammoText.text = "Missile";
                break;

            case 3:
                _factory = FindObjectOfType<SeedFactory>();
                _ammoText.text = "Seed";
                break;
        }
    }
}
