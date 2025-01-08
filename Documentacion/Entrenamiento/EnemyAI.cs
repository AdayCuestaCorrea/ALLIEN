using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Este componente requiere un NavMeshAgent para gestionar la navegación del enemigo
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour, ITakeDamage {
    // Constantes para los nombres de los triggers de animación
    const string RUN_TRIGGER = "Run";
    const string CROUCH_TRIGGER = "Crouch";
    const string SHOOT_TRIGGER = "Shoot";

    // Variables configurables desde el editor
    [SerializeField] private float startingHealth; // Salud inicial del enemigo
    [SerializeField] private float minTimeUnderCover; // Tiempo mínimo escondido antes de disparar
    [SerializeField] private float maxTimeUnderCover; // Tiempo máximo escondido antes de disparar
    [SerializeField] private int minShotsToTake; // Disparos mínimos por turno de ataque
    [SerializeField] private int maxShotsToTake; // Disparos máximos por turno de ataque
    [SerializeField] private float rotationSpeed; // Velocidad de rotación hacia el jugador
    [SerializeField] private float damage; // Daño causado por los disparos del enemigo
    [Range(0,100)]
    [SerializeField] private float shootingAccuracy; // Precisión de los disparos (0-100%)
    [SerializeField] private Transform shootingPosition; // Posición desde donde dispara el enemigo
    [SerializeField] private ParticleSystem bloodSplatterFX; // Efecto visual de sangre al recibir daño

    // Variables internas para gestionar el estado del enemigo
    private bool isShooting; // Indica si el enemigo está disparando
    private int currentShotsTaken; // Cantidad de disparos realizados en la ronda actual
    private int currentMaxShotsToTake; // Máximo de disparos permitidos en la ronda actual
    private NavMeshAgent agent; // Agente para la navegación del enemigo
    private Player player; // Referencia al jugador
    private Transform occupiedCoverSpot; // Posición de cobertura ocupada por el enemigo
    private Animator animator; // Controlador de animaciones del enemigo

    private float _health; // Salud actual del enemigo

    // Delegado y evento para notificar la muerte del enemigo
    public delegate void dead();
    public static event dead OnDead;

    // Propiedad para gestionar la salud del enemigo con límites
    public float health {
        get {
            return _health;
        }
        set {
            _health = Mathf.Clamp(value, 0, startingHealth); // Clampa el valor entre 0 y la salud inicial
        }
    }

    private void Awake() {
        // Inicializa referencias y activa la animación de correr al inicio
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        animator.SetTrigger(RUN_TRIGGER);
        _health = startingHealth;
    }

    public void Init(Player player, Transform coverSpot) {
        // Inicializa el enemigo con referencia al jugador y posición de cobertura
        this.player = player;
        occupiedCoverSpot = coverSpot;
        GetToCover(); // Mueve al enemigo a su posición de cobertura
    }

    private void GetToCover() {
        // Activa el movimiento del agente y establece la posición de destino como la cobertura asignada
        agent.isStopped = false;
        agent.SetDestination(occupiedCoverSpot.position);
    }

    private void Update() {
        // Comprueba si el enemigo ha llegado a la cobertura
        if (agent.isStopped == false && (transform.position - occupiedCoverSpot.position).sqrMagnitude <= 0.1f) {
            agent.isStopped = true; // Detiene al agente
            StartCoroutine(InitializeShootingCO()); // Inicia el ciclo de disparo
        }

        // Si está disparando, rota hacia el jugador
        if (isShooting) {
            RotateTowardsPlayer();
        }
    }

    private void RotateTowardsPlayer() {
        // Calcula la dirección hacia el jugador y rota gradualmente hacia él
        Vector3 direction = player.GetHeadPosition() - transform.position;
        direction.y = 0; // Ignora el eje vertical para la rotación
        Quaternion rotation = Quaternion.LookRotation(direction);
        rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        transform.rotation = rotation;
    }

    private IEnumerator InitializeShootingCO() {
        // Inicia el proceso de disparo después de esconderse
        HideBehindCover();
        yield return new WaitForSeconds(Random.Range(minTimeUnderCover, maxTimeUnderCover)); // Espera un tiempo aleatorio
        StartShooting();
    }

    private void HideBehindCover() {
        // Activa la animación de esconderse detrás de la cobertura
        animator.SetTrigger(CROUCH_TRIGGER);
    }

    private void StartShooting() {
        // Configura el estado para comenzar a disparar
        isShooting = true;
        currentMaxShotsToTake = Random.Range(minShotsToTake, maxShotsToTake); // Determina el número de disparos de esta ronda
        currentShotsTaken = 0;
        animator.SetTrigger(SHOOT_TRIGGER); // Activa la animación de disparo
    }

    public void Shoot() {
        // Determina si el disparo impacta al jugador según la precisión
        bool hitPlayer = Random.Range(0, 100) <= shootingAccuracy;
        if (hitPlayer) {
            RaycastHit hit;
            Vector3 direction = player.GetHeadPosition() - shootingPosition.position;
            if (Physics.Raycast(shootingPosition.position, direction, out hit)) {
                Player player = hit.collider.GetComponentInParent<Player>();
                if (player) {
                    player.TakeDamage(damage); // Aplica daño al jugador si el disparo acierta
                }
            }
        }

        currentShotsTaken++;
        // Si se alcanzó el número máximo de disparos, reinicia el ciclo de disparo
        if (currentShotsTaken >= currentMaxShotsToTake) {
            StartCoroutine(InitializeShootingCO());
        }
    }

    public void TakeDamage(Weapon weapon, Projectile projectile, Vector3 contactPoint) {
        // Reduce la salud del enemigo al recibir daño
        health -= weapon.GetDamage();

        // Si la salud llega a 0, activa el evento de muerte y destruye al enemigo
        if (health <= 0) {
            OnDead?.Invoke();
            Destroy(gameObject);
        }

        // Genera un efecto visual de sangre en el punto de impacto
        ParticleSystem effect = Instantiate(bloodSplatterFX, contactPoint, Quaternion.LookRotation(weapon.transform.position - contactPoint));
        effect.Stop();
        effect.Play();
    }
}
