using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
    // Punto desde donde se disparan los rayos del arma
    [SerializeField] Transform spawn_point;

    // Línea del láser utilizada para mostrar el disparo
    [SerializeField] LineRenderer laser_line;

    // Duración de visibilidad del láser tras disparar
    [SerializeField] float laser_duration = 0.05f;

    // Alcance máximo del láser
    [SerializeField] float laser_range = 600f;

    // Comportamientos generales del mundo (como control de zombies)
    [SerializeField] WorldBehaviors world_behaviors;

    // Capacidad máxima del cargador
    [SerializeField] int max_ammo = 10;

    // Tiempo necesario para recargar el arma
    [SerializeField] float reload_time = 2f;

    // Munición actual disponible en el cargador
    private int current_ammo;

    // Indica si el arma está en proceso de recarga
    private bool is_reloading = false;

    // Inicializa el arma con munición completa al inicio
    void Start() {
        current_ammo = max_ammo;
    }

    // Método para disparar el arma
    public GameObject? shootGun() {
        // Si el arma está recargando, no puede disparar
        if (is_reloading) {
            return null;
        }

        // Si no hay munición, inicia la recarga y no dispara
        if (current_ammo <= 0) {
            StartCoroutine(Reload());
            return null;
        }

        // Reduce la munición disponible
        current_ammo--;

        // Calcula la dirección del disparo
        Vector3 forward = new Vector3(transform.forward.x, transform.forward.y, transform.forward.z);
        forward = Quaternion.Euler(0, 90, 0) * forward;

        // Realiza un raycast para detectar colisiones con objetos dentro del rango del láser
        if (Physics.Raycast(spawn_point.position, forward, out RaycastHit hit, laser_range)) {
            // Habilita el láser y muestra el disparo en la escena
            laser_line.enabled = true;
            laser_line.SetPosition(0, spawn_point.position);
            laser_line.SetPosition(1, hit.point);

            // Si el disparo impacta contra un zombie
            if (hit.transform.gameObject.tag == "Zombie") {
                StartCoroutine(ShootLaser());
                world_behaviors.spawnedZombiesList.Remove(hit.transform.parent.GetComponent<ZombieController>());
                return hit.transform.gameObject; // Retorna el zombie impactado
                Destroy(hit.transform.gameObject); // Destruye el objeto impactado
            }
            // Si el disparo impacta contra una pared
            else if (hit.collider.gameObject.tag == "Wall") {
                StartCoroutine(ShootLaser());
                return null;
            }
        }

        // Desactiva el láser tras una breve duración si no hay impacto
        StartCoroutine(ShootLaser());
        return null;
    }

    // Corrutina para desactivar el láser tras un breve periodo
    private IEnumerator ShootLaser() {
        yield return new WaitForSeconds(laser_duration);
        laser_line.enabled = false;
    }

    // Método para recargar manualmente el arma
    public void reload() {
        if (is_reloading) {
            is_reloading = false;
        }
        current_ammo = max_ammo;
    }

    // Devuelve si el arma está en proceso de recarga
    public bool isReloading() {
        return is_reloading;
    }

    // Devuelve la munición actual disponible en el cargador
    public int getCurrentAmmo() {
        return current_ammo;
    }

    // Corrutina para gestionar el proceso de recarga automática
    private IEnumerator Reload() {
        is_reloading = true;
        yield return new WaitForSeconds(reload_time);
        current_ammo = max_ammo; // Restaura la munición al máximo
        is_reloading = false; // Finaliza el estado de recarga
    }
}
