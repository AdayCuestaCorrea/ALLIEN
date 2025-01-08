using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    // Referencias a diferentes cámaras que se pueden alternar
    public CameraSwitch fullViewCamera; // Cámara que muestra la vista completa
    public CameraSwitch behindCamera; // Cámara detrás del personaje o escena
    public CameraSwitch cinamaticCamera; // Cámara con enfoque cinematográfico

    // Activa la cámara de vista completa y desactiva las demás
    public void ShowFullViewCamera() {
        behindCamera.enabled = false; // Desactiva la cámara detrás del personaje
        cinamaticCamera.enabled = false; // Desactiva la cámara cinematográfica
        fullViewCamera.enabled = true; // Activa la cámara de vista completa
    }

    // Activa la cámara cinematográfica y desactiva las demás
    public void ShowCinamaticCamera() {
        behindCamera.enabled = false; // Desactiva la cámara detrás del personaje
        cinamaticCamera.enabled = true; // Activa la cámara cinematográfica
        fullViewCamera.enabled = false; // Desactiva la cámara de vista completa
    }

    // Activa la cámara detrás del personaje y desactiva las demás
    public void ShowBehindCamera() {
        behindCamera.enabled = true; // Activa la cámara detrás del personaje
        cinamaticCamera.enabled = false; // Desactiva la cámara cinematográfica
        fullViewCamera.enabled = false; // Desactiva la cámara de vista completa
    }

    // Detecta las entradas del teclado para alternar entre cámaras
    void Update() {
        if (Input.GetKeyDown("[1]")) {
            ShowFullViewCamera(); // Cambia a la cámara de vista completa si se presiona la tecla 1
        }
        if (Input.GetKeyDown("[2]")) {
            ShowBehindCamera(); // Cambia a la cámara detrás del personaje si se presiona la tecla 2
        }
        if (Input.GetKeyDown("[3]")) {
            ShowCinamaticCamera(); // Cambia a la cámara cinematográfica si se presiona la tecla 3
        }
    }
}
