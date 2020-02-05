using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

namespace ChainOfResponsibility
{
    /// <summary>
    /// Class used for spawning zombies in the chain of responsibility example.
    /// </summary>
    public class ZombieSpawner : MonoBehaviour
    {
        InputHandler _inputHandler;
        [SerializeField] Zombie _zombiePrefab;
        [SerializeField] Transform _destination;

        void Start()
        {
            _inputHandler = FindObjectOfType<InputHandler>();
            _inputHandler.OnClick += SpawnZombie;
        }

        void SpawnZombie()
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(_inputHandler.mousePosition);
            RaycastHit rayInfo = new RaycastHit();
            if (Physics.Raycast(cameraRay, out rayInfo))
            {
                if (rayInfo.transform.name == "Floor")
                {
                    Zombie newZombie = Instantiate(_zombiePrefab, rayInfo.point, Quaternion.identity);
                    newZombie.destination = _destination;
                }
            }
        }
    }
}
