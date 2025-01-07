using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class ZombieController : MonoBehaviour
{
  public Transform target;
  [SerializeField] float update_speed = 0.1f;
  
  private UnityEngine.AI.NavMeshAgent Zombie;
  public WorldBehaviors world_behaviors;

  private void Awake() {
    Zombie = GetComponent<UnityEngine.AI.NavMeshAgent>();
  }

  private void Start() {
    StartCoroutine(FollowTarget());
  }
  private IEnumerator FollowTarget() {
    WaitForSeconds Wait = new WaitForSeconds(update_speed);
    while (enabled) {
      Zombie.SetDestination(target.transform.position);
      yield return Wait;
    }
  }

  public void moveZombie(Vector3 pos) {
    Zombie.Warp(pos);
  }

  private void OnDestroy() {
    world_behaviors.spawnedZombiesList.Remove(this);
  }
}
