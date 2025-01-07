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
  [SerializeField] float move_speed = 2f;
  Rigidbody rb;

  [SerializeField] GunController gunObject;
  bool can_shoot, has_shot = false;
  GameObject hit_target;
  int time_until_next_bullet = 0;
  int min_time_until_next_bullet = 30;
  private List<GameObject> zombies_hear = new List<GameObject>();

  [SerializeField] WorldBehaviors world_behaviors;
  [SerializeField] TextMeshProUGUI modelText;
  private float survival_time_reward = 1f;
  [SerializeField] NNModel model_to_use;
  private float speed_to_be_applied = 2f;
  private BehaviorParameters behaviorParameters;

  public override void Initialize()
  {
    rb = GetComponent<Rigidbody>();
  }

  private void Start() {
    behaviorParameters = GetComponent<BehaviorParameters>();
  }

  public override void OnEpisodeBegin() {
    behaviorParameters.Model = model_to_use;
    move_speed = speed_to_be_applied;
    modelText.text = "Using: " + model_to_use.name;
    world_behaviors.spawnAgent();
    world_behaviors.removeAllZombies();
    world_behaviors.callSpawnZombie();
    gunObject.reload();
    zombies_hear.Clear();
  }
  public override void CollectObservations(VectorSensor sensor)
  {
    sensor.AddObservation(transform.localPosition);
    sensor.AddObservation(gunObject.isReloading());
    sensor.AddObservation(gunObject.getCurrentAmmo());
    foreach(GameObject zombie in zombies_hear) {
      sensor.AddObservation(zombie.transform.localPosition);
    }
  }

  public override void OnActionReceived(ActionBuffers actions)
  {
    can_shoot = false;
    float move_rotate = actions.ContinuousActions[0];
    float move_forward = actions.ContinuousActions[1];
    bool shoot = actions.DiscreteActions[0] > 0;
    Vector3 move = new Vector3(transform.position.x + transform.forward.x * move_forward * move_speed * Time.deltaTime,
      0, transform.position.z + transform.forward.z * move_forward * move_speed * Time.deltaTime);
    rb.MovePosition(move);
    transform.Rotate(0f, move_rotate * move_speed, 0f, Space.Self);

    if(shoot && !has_shot) {
      can_shoot = true;
    }
    if(can_shoot) {
      hit_target = gunObject.shootGun();
      time_until_next_bullet = min_time_until_next_bullet;
      has_shot = true;
      if(hit_target != null) {
        AddReward(50f);
        if (zombies_hear.Contains(hit_target.transform.parent.gameObject)) {
          zombies_hear.Remove(hit_target.transform.parent.gameObject);
        }
        Destroy(hit_target);
        if(world_behaviors.getZombieCount() <= 1) {
          AddReward(200f);
          EndEpisode();
        }
      } else {
        AddReward(-25f);
      }
    }
  }

  public override void Heuristic(in ActionBuffers actionsOut)
  {
    ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
    ActionSegment<int> discreteActions = actionsOut.DiscreteActions;

    continuousActions[0] = Input.GetAxisRaw("Horizontal");
    continuousActions[1] = Input.GetAxisRaw("Vertical");

    discreteActions[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
  }

  private void OnCollisionEnter(Collision other)
  {
    if(other.gameObject.tag == "Wall") {
      AddReward(-50f);
      EndEpisode();
    } else if(other.gameObject.tag == "Zombie") {
      AddReward(-1000f); // 30 * 30 + 100 = 1000 (hay 30 zombies y cada uno vale 30 puntos por lo que si le dan pierde el equivalente a los 30 zombies que hay mas 100 por el golpe)
      EndEpisode();
    }
  }

  private void OnTriggerEnter(Collider other) {
    if(other.gameObject.tag == "Zombie") {
      GameObject zombie = other.transform.parent.gameObject;
      if(!zombies_hear.Contains(zombie)) {
        zombies_hear.Add(zombie);
      }
    }
  }

  private void OnTriggerExit(Collider other) {
    if(other.gameObject.tag == "Zombie") {
      GameObject zombie = other.transform.parent.gameObject;
      if(zombies_hear.Contains(zombie)) {
        zombies_hear.Remove(zombie);
      }
    }
  }

  private void FixedUpdate() {
    if (has_shot) {
      time_until_next_bullet--;
      if(time_until_next_bullet <= 0) {
        has_shot = false;
      }
    }
  }

  public void applyModel(NNModel model, float speed) {
  model_to_use = model;
  speed_to_be_applied = speed;
  }
}