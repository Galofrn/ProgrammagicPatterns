using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

namespace ChainOfResponsibility
{
    /// <summary>
    /// Chain of responsibility impelementation: In this case each turret shoots 
    /// the zombie if it's in range and if it has no zombie in target. Otherwise it handles
    /// the zombie to the nearest turret.
    /// </summary>
    public class TurretProcessor : Processor<Zombie>
    {
        [SerializeField] float _radiusRange;
        [SerializeField] Zombie _currentTarget;
        float _currentTimeToShoot;
        [SerializeField] float _onTimeToShoot;
        [SerializeField] Transform _turret;
        [SerializeField] Transform _shootPoint;
        [SerializeField] Bullet _bulletPrefab;

        //Unity doesn't serialize generics so we have to override our processor field with this in order to set it via inspector.
        [SerializeField] TurretProcessor _turretSuccessor;
        void Start()
        {
            _successor = _turretSuccessor;
        }

        void Update()
        {
            if (_currentTarget)
            {
                _turret.LookAt(new Vector3(_currentTarget.transform.position.x, _turret.position.y, _currentTarget.transform.position.z));
                _currentTimeToShoot += Time.deltaTime;
                if(_currentTimeToShoot >= _onTimeToShoot)
                {
                    Shoot(_currentTarget);
                    _currentTimeToShoot = 0;
                }
            }
        }

        protected override bool Process(Zombie toProcess)
        {
            if(Vector3.Distance(transform.position, toProcess.transform.position) < _radiusRange)
            {
                if (_currentTarget == null)
                {
                    _currentTarget = toProcess;
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if(toProcess == _currentTarget)
                    _currentTarget = null;
                return false;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radiusRange);
        }

        void Shoot(Zombie target)
        {
            var bullet = Instantiate(_bulletPrefab);
            bullet.target = _currentTarget.transform;
            bullet.transform.position = _shootPoint.position;
            bullet.transform.up = _shootPoint.up;
        }
    }
}
