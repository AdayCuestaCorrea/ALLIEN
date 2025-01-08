using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents.Policies; // Para manejar políticas de ML-Agents
using Unity.Barracuda; // Para trabajar con modelos de redes neuronales
using TMPro; // Para manejar elementos de texto en UI

// Clase que permite cambiar dinámicamente el modelo de red neuronal asociado a un agente
public class ModelSwitcher : MonoBehaviour {
    // Modelo de red neuronal que será aplicado al agente
    public NNModel model;

    // Velocidad que se aplicará al agente al cambiar el modelo
    public float speed = 2f;

    // Referencia al controlador del agente que usará el modelo
    public AgentController agentController;

    // Elemento de texto en pantalla para mostrar información sobre el modelo actual
    public TextMeshProUGUI modelText;

    // Método que cambia el modelo del agente y actualiza la información en pantalla
    public void switchModel() {
        // Si hay un elemento de texto disponible, actualiza su contenido con el nombre del nuevo modelo
        if (modelText != null) {
            modelText.text = "Switching to " + model.name; // Muestra el nombre del modelo actual en pantalla
        }

        // Aplica el nuevo modelo y la velocidad al agente a través de su controlador
        agentController.applyModel(model, speed);
    }
}
