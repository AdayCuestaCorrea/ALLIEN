using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Asegura que el objeto tenga un componente NavMeshAgent para navegación
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class ZombieController : MonoBehaviour
{
    // Transform del objetivo que el zombie perseguirá
    public Transform target;

    // Frecuencia en segundos con la que el zombie actualiza su posición hacia el objetivo
    [SerializeField] float update_speed = 0.1f;

    // Referencia al componente NavMeshAgent del zombie
    private UnityEngine.AI.NavMeshAgent Zombie;

    // Referencia a los comportamientos del mundo para interactuar con otros sistemas
    public WorldBehaviors world_behaviors;

    // Inicializa el componente NavMeshAgent al despertar el objeto
    private void Awake()
    {
        Zombie = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Comienza la corrutina para seguir al objetivo al iniciar el objeto
    private void Start()
    {
        StartCoroutine(FollowTarget());
    }

    // Corrutina que actualiza constantemente la posición del zombie hacia el objetivo
    private IEnumerator FollowTarget()
    {
        // Define un tiempo de espera entre actualizaciones basado en `update_speed`
        WaitForSeconds Wait = new WaitForSeconds(update_speed);

        // Mientras el script esté habilitado, actualiza la posición del zombie hacia el objetivo
        while (enabled)
        {
            Zombie.SetDestination(target.transform.position);
            yield return Wait; // Espera el tiempo definido antes de la siguiente actualización
        }
    }

    // Método para mover al zombie a una posición específica instantáneamente
    public void moveZombie(Vector3 pos)
    {
        Zombie.Warp(pos); // Cambia directamente la posición del zombie sin considerar colisiones
    }

    // Cuando el zombie es destruido, se elimina de la lista de zombies en `WorldBehaviors`
    private void OnDestroy()
    {
        world_behaviors.spawnedZombiesList.Remove(this);
    }
}
