using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
  [SerializeField] Transform spawn_point;
  [SerializeField] LineRenderer laser_line;
  [SerializeField] float laser_duration = 0.05f;
  [SerializeField] float laser_range = 600f;

  [SerializeField] WorldBehaviors world_behaviors;
  [SerializeField] int max_ammo = 10;
  [SerializeField] float reload_time = 2f;

  private int current_ammo;
  private bool is_reloading = false;

  void Start() {
    current_ammo = max_ammo;
  }
  public GameObject? shootGun() {
    if (is_reloading) {
      return null;
    }
    if (current_ammo <= 0) {
      StartCoroutine(Reload());
      return null;
    }
    current_ammo--;
    Vector3 forward = new Vector3(transform.forward.x, transform.forward.y, transform.forward.z);
    forward = Quaternion.Euler(0, 90, 0) * forward;
    if(Physics.Raycast(spawn_point.position, forward, out RaycastHit hit, laser_range)) {
      laser_line.enabled = true;
      laser_line.SetPosition(0, spawn_point.position);
      laser_line.SetPosition(1, hit.point);
      if(hit.transform.gameObject.tag == "Zombie") {
        StartCoroutine(ShootLaser());
        world_behaviors.spawnedZombiesList.Remove(hit.transform.parent.GetComponent<ZombieController>());
        return hit.transform.gameObject;
        Destroy(hit.transform.gameObject);
      } else if (hit.collider.gameObject.tag == "Wall") {
        StartCoroutine(ShootLaser());
        return null;
      }
    }
    StartCoroutine(ShootLaser());
    return null;
  }

  private IEnumerator ShootLaser() {
    yield return new WaitForSeconds(laser_duration);
    laser_line.enabled = false;
  }

  public void reload() {
    if (is_reloading) {
      is_reloading = false;
    }
    current_ammo = max_ammo;
  }

  public bool isReloading() {
    return is_reloading;
  }

  public int getCurrentAmmo() {
    return current_ammo;
  }

  private IEnumerator Reload() {
    is_reloading = true;
    yield return new WaitForSeconds(reload_time);
    current_ammo = max_ammo;
    is_reloading = false;
  }
}

