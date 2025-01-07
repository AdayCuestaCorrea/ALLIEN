using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
  public CameraSwitch fullViewCamera;
  public CameraSwitch behindCamera;
  public CameraSwitch cinamaticCamera;

  public void ShowFullViewCamera() {
    behindCamera.enabled = false;
    cinamaticCamera.enabled = false;
    fullViewCamera.enabled = true;
  }

  public void ShowCinamaticCamera() {
    behindCamera.enabled = false;
    cinamaticCamera.enabled = true;
    fullViewCamera.enabled = false;
  }

  public void ShowBehindCamera() {
    behindCamera.enabled = true;
    cinamaticCamera.enabled = false;
    fullViewCamera.enabled = false;
  }

  void Update() {
    if(Input.GetKeyDown("[1]")) {
      ShowFullViewCamera();
    }
    if(Input.GetKeyDown("[2]")) {
      ShowBehindCamera();
    }
    if(Input.GetKeyDown("[3]")) {
      ShowCinamaticCamera();
    }
  }
}
