using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Clase que gestiona el comportamiento del mundo, incluyendo el spawn de agentes y zombies
public class WorldBehaviors : MonoBehaviour
{
    // Referencia al controlador del agente
    [SerializeField] AgentController agent_controller;

    // Prefab del controlador de zombies
    [SerializeField] ZombieController zombie_controller;

    // Lista de zombies generados en el mundo
    [SerializeField] public List<ZombieController> spawnedZombiesList = new List<ZombieController>();

    // Cantidad de zombies a generar
    [SerializeField] int zombie_count;

    // Ubicación base del entorno en el que se desarrollan los comportamientos
    [SerializeField] Transform environment_location;

    // Tiempo entre la generación de cada zombie
    [SerializeField] int time_between_spawns = 1;

    // Puntos de spawn definidos para los zombies
    [SerializeField] Transform spawn_zone_1;
    [SerializeField] Transform spawn_zone_2;
    [SerializeField] Transform spawn_zone_3;
    [SerializeField] Transform spawn_zone_4;

    // Método que posiciona al agente en una ubicación inicial específica
    public void spawnAgent()
    {
        agent_controller.transform.localPosition = new Vector3(13.37f, -46.23f, 81.52f);
        agent_controller.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
    }

    // Método público para iniciar la generación de zombies
    public void callSpawnZombie()
    {
        StartCoroutine(spawnZombie());
    }

    // Corrutina que se encarga de generar zombies en las zonas de spawn
    IEnumerator spawnZombie()
    {
        // Coordenadas base de las zonas de spawn
        Vector3 spawn_zone_1_coords = spawn_zone_1.transform.position;
        Vector3 spawn_zone_2_coords = spawn_zone_2.transform.position;
        Vector3 spawn_zone_3_coords = spawn_zone_3.transform.position;
        Vector3 spawn_zone_4_coords = spawn_zone_4.transform.position;

        // Arreglo de coordenadas de las zonas de spawn
        Vector3[] spawn_zones = { spawn_zone_1_coords, spawn_zone_2_coords, spawn_zone_3_coords, spawn_zone_4_coords };

        // Elimina zombies existentes antes de generar nuevos
        if (spawnedZombiesList.Count != 0)
        {
            removeZombie(spawnedZombiesList);
        }

        // Genera la cantidad definida de zombies
        for (int i = 0; i < zombie_count; i++)
        {
            // Genera posiciones aleatorias dentro de cada zona de spawn
            Vector3 random_spawn_zone_1_coords = spawn_zone_1_coords + new Vector3(Random.Range(-3.8f, 3.8f), 0f, Random.Range(-3.8f, 3.8f));
            Vector3 random_spawn_zone_2_coords = spawn_zone_2_coords + new Vector3(Random.Range(-3.8f, 3.8f), 0f, Random.Range(-3.8f, 3.8f));
            Vector3 random_spawn_zone_3_coords = spawn_zone_3_coords + new Vector3(Random.Range(-3.8f, 3.8f), 0f, Random.Range(-3.8f, 3.8f));
            Vector3 random_spawn_zone_4_coords = spawn_zone_4_coords + new Vector3(Random.Range(-3.8f, 3.8f), 0f, Random.Range(-3.8f, 3.8f));

            // Array de posiciones aleatorias de spawn
            Vector3[] random_area = { random_spawn_zone_1_coords, random_spawn_zone_2_coords, random_spawn_zone_3_coords, random_spawn_zone_4_coords };

            // Instancia un nuevo zombie y lo configura
            ZombieController new_zombie = Instantiate(zombie_controller);
            new_zombie.target = agent_controller.transform;
            new_zombie.world_behaviors = this;
            new_zombie.transform.parent = environment_location;

            // Asigna una posición aleatoria dentro de las zonas de spawn
            int random_number = Random.Range(0, random_area.Length);
            Vector3 zombie_location = random_area[random_number];
            Vector3 zombie_spawn = spawn_zones[random_number];
            new_zombie.moveZombie(zombie_spawn);

            // Añade el zombie a la lista de zombies generados
            spawnedZombiesList.Add(new_zombie);

            // Espera antes de generar el siguiente zombie
            yield return new WaitForSeconds(time_between_spawns);
        }
    }

    // Verifica si dos objetos están a una distancia mínima especificada
    public bool CheckOverlap(Vector3 objectWeWantToAvoidOverlapping, Vector3 alreadyExistingObject, float minDistanceWanted)
    {
        float distance_between_objects = Vector3.Distance(objectWeWantToAvoidOverlapping, alreadyExistingObject);
        if (minDistanceWanted <= distance_between_objects)
        {
            return true;
        }
        return true; // Devuelve true por defecto
    }

    // Devuelve la cantidad actual de zombies generados
    public int getZombieCount()
    {
        return spawnedZombiesList.Count;
    }

    // Elimina todos los zombies generados
    public void removeAllZombies()
    {
        removeZombie(spawnedZombiesList);
    }

    // Método privado para eliminar una lista específica de zombies
    private void removeZombie(List<ZombieController> to_be_deleted_game_object_list)
    {
        foreach (ZombieController i in to_be_deleted_game_object_list)
        {
            Destroy(i.gameObject); // Destruye el objeto del zombie en el mundo
        }
        to_be_deleted_game_object_list.Clear(); // Limpia la lista de zombies
    }
}
