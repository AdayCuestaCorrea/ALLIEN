using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBehaviors : MonoBehaviour
{
  [SerializeField] AgentController agent_controller;
  [SerializeField] ZombieController zombie_controller;
  [SerializeField] public List<ZombieController> spawnedZombiesList = new List<ZombieController>();
  [SerializeField] int zombie_count;

  [SerializeField] Transform environment_location;

  [SerializeField] int time_between_spawns = 1;
  [SerializeField] Transform spawn_zone_1;
  [SerializeField] Transform spawn_zone_2;
  [SerializeField] Transform spawn_zone_3;
  [SerializeField] Transform spawn_zone_4;

  public void spawnAgent() {
    agent_controller.transform.localPosition = new Vector3(13.37f, -46.23f, 81.52f);
    agent_controller.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
  }

  public void callSpawnZombie() {
    StartCoroutine(spawnZombie());
  }
  IEnumerator spawnZombie() {
    Vector3 spawn_zone_1_coords = spawn_zone_1.transform.position;
    Vector3 spawn_zone_2_coords = spawn_zone_2.transform.position;
    Vector3 spawn_zone_3_coords = spawn_zone_3.transform.position;
    Vector3 spawn_zone_4_coords = spawn_zone_4.transform.position;
    Vector3[] spawn_zones = {spawn_zone_1_coords, spawn_zone_2_coords, spawn_zone_3_coords, spawn_zone_4_coords};


    if (spawnedZombiesList.Count != 0) {
      removeZombie(spawnedZombiesList);
    }

    for(int i = 0; i < zombie_count; i++) {
      Vector3 random_spawn_zone_1_coords = spawn_zone_1_coords + new Vector3(
        Random.Range(-3.8f,3.8f), 0f, Random.Range(-3.8f,3.8f));
      Vector3 random_spawn_zone_2_coords = spawn_zone_2_coords + new Vector3(
        Random.Range(-3.8f,3.8f), 0f, Random.Range(-3.8f,3.8f));
      Vector3 random_spawn_zone_3_coords = spawn_zone_3_coords + new Vector3(
        Random.Range(-3.8f,3.8f), 0f, Random.Range(-3.8f,3.8f));
      Vector3 random_spawn_zone_4_coords = spawn_zone_4_coords + new Vector3(
        Random.Range(-3.8f,3.8f), 0f, Random.Range(-3.8f,3.8f));

      Vector3[] random_area = {
        random_spawn_zone_1_coords, random_spawn_zone_2_coords,
        random_spawn_zone_3_coords, random_spawn_zone_4_coords
      };

      int counter = 0;
      bool distanceGood;

      ZombieController new_zombie = Instantiate(zombie_controller);
      new_zombie.target = agent_controller.transform;
      new_zombie.world_behaviors = this;
      new_zombie.transform.parent = environment_location;
      int random_number = Random.Range(0, random_area.Length);
      
      Vector3 zombie_location = random_area[random_number];
      Vector3 zombie_spawn = spawn_zones[random_number];
      new_zombie.moveZombie(zombie_spawn);
      spawnedZombiesList.Add(new_zombie);
      yield return new WaitForSeconds(time_between_spawns);
    }

  }

  public bool CheckOverlap(Vector3 objectWeWantToAvoidOverlapping, Vector3 alreadyExistingObject, float minDistanceWanted) {
    float distance_between_objects = Vector3.Distance(objectWeWantToAvoidOverlapping, alreadyExistingObject);
    if(minDistanceWanted <= distance_between_objects) {
      return true;
    } 
    return true;
  }

  public int getZombieCount() {
    return spawnedZombiesList.Count;
  }

  public void removeAllZombies() {
    removeZombie(spawnedZombiesList);
  }

  private void removeZombie(List<ZombieController> to_be_deleted_game_object_list) {
    foreach (ZombieController i in to_be_deleted_game_object_list) {
      Destroy(i.gameObject);
    }
    to_be_deleted_game_object_list.Clear();
  }
}
