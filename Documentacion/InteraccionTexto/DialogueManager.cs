using System.Collections;
using UnityEngine;

public class DialogueManager : MonoBehaviour {
  // Controlador del diálogo para el superviviente
  public DialogueControllerSurvivor survivorController;

  // Controlador del diálogo para el zombie
  public DialogueControllerZombie zombieController;

  // Método inicial que se ejecuta al iniciar el script
  private void Start() {
      StartCoroutine(HandleDialogue()); // Inicia la corrutina que gestiona el flujo del diálogo
  }

  // Corrutina que gestiona el flujo continuo de diálogos entre el superviviente y el zombie
  private IEnumerator HandleDialogue() {
    while (true) { // Bucle infinito para mantener el flujo de diálogos
      // *** Superviviente habla ***
      // Obtiene el prompt actual del superviviente
      string survivorPrompt = survivorController.currentPrompt;

      // Envía el prompt del superviviente a la API para generar su respuesta
      survivorController.SendNewPrompt(survivorPrompt);

      // Espera hasta que el diálogo del superviviente haya finalizado
      yield return new WaitUntil(() => survivorController.IsDialogueComplete);

      // Obtiene el resultado generado por la API para el superviviente
      string survivorResult = survivorController.Result;

      // *** Zombie responde ***
      // Limpia cualquier información previa del prompt del zombie relacionada con lo que dijo el superviviente
      int index = zombieController.currentPrompt.IndexOf("The survivor said:");
      if (index != -1) {
        zombieController.currentPrompt = zombieController.currentPrompt.Substring(0, index).Trim();
      }

      // Actualiza el prompt del zombie con la respuesta del superviviente
      string zombiePrompt = zombieController.currentPrompt + " The survivor said: " + survivorResult;

      // Envía el prompt actualizado del zombie a la API
      zombieController.SendNewPrompt(zombiePrompt);

      // Espera hasta que el diálogo del zombie haya finalizado
      yield return new WaitUntil(() => zombieController.IsDialogueComplete);

      // Obtiene el resultado generado por la API para el zombie
      string zombieResult = zombieController.Result;

      // *** Actualizar el prompt del superviviente con la respuesta del zombie ***
      // Limpia cualquier información previa del prompt del superviviente relacionada con lo que dijo el zombie
      index = survivorController.currentPrompt.IndexOf("The zombie told you:");
      if (index != -1){
        survivorController.currentPrompt = survivorController.currentPrompt.Substring(0, index).Trim();
      }

      // Actualiza el prompt del superviviente con la respuesta del zombie
      survivorController.currentPrompt = survivorController.currentPrompt + " The zombie told you: " + zombieResult;
    }
  }
}
