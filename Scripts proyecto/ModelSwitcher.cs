using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents.Policies;
using Unity.Barracuda;
using TMPro;

public class ModelSwitcher : MonoBehaviour {
  public NNModel model;
  public float speed = 2f;
  public AgentController agentController;
  public TextMeshProUGUI modelText;

  public void switchModel() {
    if (modelText != null) {
      modelText.text = "Switching to " + model.name;
    }
    agentController.applyModel(model, speed);
  }
}
