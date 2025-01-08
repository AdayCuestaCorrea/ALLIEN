using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    // Puntos en el escenario donde pueden aparecer los enemigos
    [SerializeField] private Transform[] spawnPoints;

    // Prefab del enemigo que se generará
    [SerializeField] private EnemyAI enemyPrefab;

    // Intervalo de tiempo entre la generación de enemigos
    [SerializeField] private float spawnInterval;

    // Número máximo de enemigos que pueden estar activos al mismo tiempo
    [SerializeField] private int maxEnemiesNumber;

    // Referencia al jugador para inicializar los enemigos
    [SerializeField] private Player player;

    // Lista de enemigos que han sido generados
    private List<EnemyAI> spawnedEnemies = new List<EnemyAI>();

    // Tiempo transcurrido desde la última generación de un enemigo
    private float timeSinceLastSpawn;

    private void Start() {
        // Inicializa el temporizador para generar el primer enemigo
        timeSinceLastSpawn = spawnInterval;

        // Se suscribe al evento OnDead de EnemyAI para actualizar el conteo de enemigos
        EnemyAI.OnDead += DecreaseEnemiesCount;
    }

    private void Update() {
        // Incrementa el temporizador en función del tiempo transcurrido desde el último frame
        timeSinceLastSpawn += Time.deltaTime;

        // Comprueba si ha pasado suficiente tiempo para generar un nuevo enemigo
        if (timeSinceLastSpawn >= spawnInterval) {
            timeSinceLastSpawn = 0f; // Reinicia el temporizador
            if (spawnedEnemies.Count < maxEnemiesNumber) { // Comprueba que no se exceda el límite de enemigos
                SpawnEnemy(); // Genera un nuevo enemigo
            }
        }
    }

    private void SpawnEnemy() {
        // Crea una nueva instancia del prefab del enemigo en la posición del spawner
        EnemyAI enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);

        // Determina el punto de aparición utilizando el índice actual y la cantidad de spawnPoints
        int spawnPointindex = spawnedEnemies.Count % spawnPoints.Length;

        // Inicializa el enemigo con el jugador y su posición de cobertura
        enemy.Init(player, spawnPoints[spawnPointindex]);

        // Añade el nuevo enemigo a la lista de enemigos generados
        spawnedEnemies.Add(enemy);
    }

    private void DecreaseEnemiesCount() {
        // Elimina todos los enemigos nulos de la lista (por ejemplo, enemigos destruidos)
        spawnedEnemies.RemoveAll(enemy => enemy == null);
    }
}
