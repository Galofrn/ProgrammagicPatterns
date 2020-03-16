using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using General;

/// <summary>
/// Used in the observer example. Guards are both observers and observables
/// because they have to inform about the event of seeing the player and be informed if
/// other guards see the player.
/// </summary>
namespace Observer
{
    public class Guard : Observable, IObserver
    {
        EventFSM<GuardActions> _fsm;

        [SerializeField] float _rangeOfVision = 15;
        [SerializeField] float _angleOfVision = 90;

        Player _player;

        [SerializeField] ParticleSystem _alertParticles;
        Animator _animator;

        NavMeshAgent _navMeshAgent;

        // Start is called before the first frame update
        void Start()
        {
            _player = FindObjectOfType<Player>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();

            #region FSM
            var idle = new State<GuardActions>("Idle");
            var chasing = new State<GuardActions>("Chasing");
            var lookingForPlayer = new State<GuardActions>("Looking for player");
            var backingToSpot = new State<GuardActions>("Going back to spot");

            //Idle
            idle.AddTransition(GuardActions.PlayerInSight, chasing);
            idle.AddTransition(GuardActions.Alert, lookingForPlayer);

            var initialRotation = transform.rotation;
            idle.OnEnter += () =>
            {
                ChangeParticleColor(Color.white);
                transform.rotation = initialRotation;
            };
            idle.OnUpdate += () =>
            {
                if (LineOfSight())
                {
                    _fsm.Feed(GuardActions.PlayerInSight);
                    NotifyListeners();
                }
            };

            //Chasing
            chasing.AddTransition(GuardActions.PlayerOutOfSigth, lookingForPlayer);

            chasing.OnEnter += () =>
            {
                ChangeParticleColor(Color.red);
                _navMeshAgent.stoppingDistance = 5;
                StartCoroutine(UpdateDestinationToPlayer());
            };          
            chasing.OnUpdate += () =>
            {
                if (!LineOfSight())
                    _fsm.Feed(GuardActions.PlayerOutOfSigth);
            };
            chasing.OnExit += () => _navMeshAgent.stoppingDistance = 0;

            //Looking for player
            lookingForPlayer.AddTransition(GuardActions.PlayerInSight, chasing);
            lookingForPlayer.AddTransition(GuardActions.PlayerOutOfSigth, backingToSpot);
            lookingForPlayer.AddTransition(GuardActions.Alert, lookingForPlayer);

            lookingForPlayer.OnEnter += () =>
            {
                ChangeParticleColor(Color.yellow);
                _navMeshAgent.SetDestination(_player.transform.position);
            };

            lookingForPlayer.OnUpdate += () =>
            {
                if (LineOfSight())
                    _fsm.Feed(GuardActions.PlayerInSight);

                //If guard arrives destiny without seeing anyone return
                if (_navMeshAgent.remainingDistance <= .1f)
                    _fsm.Feed(GuardActions.PlayerOutOfSigth);
            };

            //Backing to spot
            backingToSpot.AddTransition(GuardActions.Arrived, idle);
            backingToSpot.AddTransition(GuardActions.PlayerInSight, chasing);

            var initialPosition = transform.position;
            backingToSpot.OnEnter += () =>
            {
                ChangeParticleColor(Color.white);
                _navMeshAgent.SetDestination(initialPosition);
            };
            backingToSpot.OnUpdate += () =>
            {
                if (LineOfSight())
                    _fsm.Feed(GuardActions.PlayerInSight);

                if (_navMeshAgent.remainingDistance <= .1f)
                    _fsm.Feed(GuardActions.Arrived);
            };

            _fsm = new EventFSM<GuardActions>(idle);
            #endregion

            #region Observer
            var guards = FindObjectsOfType<Observable>().Where(x => x != this);

            foreach (var guard in guards)
            {
                guard.Register(this);
            }
            #endregion
        }

        // Update is called once per frame
        void Update()
        {
            _fsm.Update();
            _animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude);
        }

        bool LineOfSight()
        {
            var direction = (_player.transform.position - transform.position).normalized;

            var inRange = Vector3.Distance(transform.position, _player.transform.position) <= _rangeOfVision;
            var inFOV = Vector3.Angle(transform.forward, direction) < _angleOfVision / 2;

            bool nothingBetween = false;
            RaycastHit rayInfo = new RaycastHit();
            if (Physics.Raycast(transform.position + Vector3.up * .8f, direction, out rayInfo, LayerMask.GetMask("Player")))
                nothingBetween = rayInfo.transform.GetComponent<Player>();

            bool inSight = inRange && inFOV && nothingBetween;

            return inSight;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            float halfAngle = _angleOfVision / 2.0f;
            Quaternion leftRayRotation = Quaternion.AngleAxis(-halfAngle, Vector3.up);
            Quaternion rightRayRotation = Quaternion.AngleAxis(halfAngle, Vector3.up);
            Vector3 leftRayDirection = leftRayRotation * transform.forward;
            Vector3 rightRayDirection = rightRayRotation * transform.forward;
            Gizmos.DrawRay(transform.position, leftRayDirection * _rangeOfVision);
            Gizmos.DrawRay(transform.position, rightRayDirection * _rangeOfVision);
        }

        void ChangeParticleColor(Color newColor)
        {
            var mainParticleSystem = _alertParticles.main;
            mainParticleSystem.startColor = newColor;
        }

        IEnumerator UpdateDestinationToPlayer()
        {
            while (LineOfSight())
            {
                _navMeshAgent.SetDestination(_player.transform.position);
                NotifyListeners();
                yield return new WaitForSeconds(.1f);
            }
        }

        public void Notify()
        {
            //When a guard sees the player it triggers the state machine of each of it's subsribers.
            _fsm.Feed(GuardActions.Alert);
        }
    }
}
