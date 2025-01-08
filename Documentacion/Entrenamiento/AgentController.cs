using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using Unity.Barracuda;
using TMPro;

public class AgentController : Agent
{
    // Velocidad de movimiento del agente
    [SerializeField] float move_speed = 2f;
    Rigidbody rb;

    // Referencia al controlador del arma
    [SerializeField] GunController gunObject;
    bool can_shoot, has_shot = false; // Control de disparos
    GameObject hit_target; // Último objetivo impactado
    int time_until_next_bullet = 0; // Tiempo de espera entre disparos
    int min_time_until_next_bullet = 30; // Tiempo mínimo entre disparos
    private List<GameObject> zombies_hear = new List<GameObject>(); // Lista de zombies que el agente "escucha"

    // Referencias al comportamiento global del mundo y al texto que indica el modelo usado
    [SerializeField] WorldBehaviors world_behaviors;
    [SerializeField] TextMeshProUGUI modelText;
    private float survival_time_reward = 1f; // Recompensa por tiempo de supervivencia
    [SerializeField] NNModel model_to_use; // Modelo de red neuronal a usar
    private float speed_to_be_applied = 2f; // Velocidad configurada por el modelo
    private BehaviorParameters behaviorParameters; // Parámetros del comportamiento del agente

    // Inicialización del agente
    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        behaviorParameters = GetComponent<BehaviorParameters>();
    }

    // Reinicia el episodio al comenzar uno nuevo
    public override void OnEpisodeBegin() {
        behaviorParameters.Model = model_to_use; // Asigna el modelo actual
        move_speed = speed_to_be_applied; // Ajusta la velocidad según el modelo
        modelText.text = "Using: " + model_to_use.name; // Muestra el modelo en pantalla
        world_behaviors.spawnAgent(); // Genera al agente en el escenario
        world_behaviors.removeAllZombies(); // Limpia todos los zombies existentes
        world_behaviors.callSpawnZombie(); // Genera nuevos zombies
        gunObject.reload(); // Recarga el arma
        zombies_hear.Clear(); // Limpia la lista de zombies escuchados
    }

    // Recolecta observaciones del entorno para el modelo
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition); // Posición del agente
        sensor.AddObservation(gunObject.isReloading()); // Estado de recarga del arma
        sensor.AddObservation(gunObject.getCurrentAmmo()); // Munición actual
        foreach(GameObject zombie in zombies_hear) {
            sensor.AddObservation(zombie.transform.localPosition); // Posición de cada zombie escuchado
        }
    }

    // Recibe las acciones del modelo y actúa en consecuencia
    public override void OnActionReceived(ActionBuffers actions)
    {
        can_shoot = false;

        // Lee las acciones continuas y discretas
        float move_rotate = actions.ContinuousActions[0]; // Rotación
        float move_forward = actions.ContinuousActions[1]; // Movimiento hacia adelante
        bool shoot = actions.DiscreteActions[0] > 0; // Disparo

        // Calcula y aplica el movimiento
        Vector3 move = new Vector3(
            transform.position.x + transform.forward.x * move_forward * move_speed * Time.deltaTime,
            0, 
            transform.position.z + transform.forward.z * move_forward * move_speed * Time.deltaTime);
        rb.MovePosition(move);
        transform.Rotate(0f, move_rotate * move_speed, 0f, Space.Self);

        // Lógica de disparo
        if(shoot && !has_shot) {
            can_shoot = true;
        }
        if(can_shoot) {
            hit_target = gunObject.shootGun(); // Intenta disparar
            time_until_next_bullet = min_time_until_next_bullet;
            has_shot = true;
            if(hit_target != null) { // Impacto exitoso
                AddReward(50f); // Recompensa por acertar
                if (zombies_hear.Contains(hit_target.transform.parent.gameObject)) {
                    zombies_hear.Remove(hit_target.transform.parent.gameObject); // Elimina zombie escuchado
                }
                Destroy(hit_target); // Elimina el objetivo
                if(world_behaviors.getZombieCount() <= 1) { // Todos los zombies eliminados
                    AddReward(200f); // Recompensa adicional
                    EndEpisode(); // Finaliza el episodio
                }
            } else {
                AddReward(-25f); // Penalización por fallar
            }
        }
    }

    // Configura controles manuales para pruebas
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;

        continuousActions[0] = Input.GetAxisRaw("Horizontal"); // Rotación
        continuousActions[1] = Input.GetAxisRaw("Vertical"); // Movimiento hacia adelante
        discreteActions[0] = Input.GetKey(KeyCode.Space) ? 1 : 0; // Disparo
    }

    // Detecta colisiones con otros objetos
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Wall") {
            AddReward(-50f); // Penalización por chocar con una pared
            EndEpisode();
        } else if(other.gameObject.tag == "Zombie") {
            AddReward(-1000f); // Penalización severa por ser alcanzado por un zombie
            EndEpisode();
        }
    }

    // Detecta cuando un zombie entra en el rango del agente
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Zombie") {
            GameObject zombie = other.transform.parent.gameObject;
            if(!zombies_hear.Contains(zombie)) {
                zombies_hear.Add(zombie); // Añade el zombie a la lista de escuchados
            }
        }
    }

    // Detecta cuando un zombie sale del rango del agente
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Zombie") {
            GameObject zombie = other.transform.parent.gameObject;
            if(zombies_hear.Contains(zombie)) {
                zombies_hear.Remove(zombie); // Elimina el zombie de la lista de escuchados
            }
        }
    }

    // Control del tiempo de espera entre disparos
    private void FixedUpdate() {
        if (has_shot) {
            time_until_next_bullet--;
            if(time_until_next_bullet <= 0) {
                has_shot = false; // Habilita disparar nuevamente
            }
        }
    }

    // Aplica un nuevo modelo de IA y ajusta la velocidad
    public void applyModel(NNModel model, float speed) {
        model_to_use = model;
        speed_to_be_applied = speed;
    }
}
