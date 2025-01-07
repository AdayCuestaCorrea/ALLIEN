using System.Collections;
using UnityEngine;

public class DialogueManager : MonoBehaviour {
  public DialogueControllerSurvivor survivorController;
  public DialogueControllerZombie zombieController;

  private void Start() {
      StartCoroutine(HandleDialogue());
  }

  private IEnumerator HandleDialogue() {
    while (true) {
      // Superviviente habla
      string survivorPrompt = survivorController.currentPrompt;
      survivorController.SendNewPrompt(survivorPrompt);
      yield return new WaitUntil(() => survivorController.IsDialogueComplete);
      string survivorResult = survivorController.Result;

      // Zombie responde
      int index = zombieController.currentPrompt.IndexOf("The survivor said:");
      if (index != -1) {
        zombieController.currentPrompt = zombieController.currentPrompt.Substring(0, index).Trim();
      }
      string zombiePrompt = zombieController.currentPrompt + " The survivor said: " + survivorResult;
      zombieController.SendNewPrompt(zombiePrompt);
      yield return new WaitUntil(() => zombieController.IsDialogueComplete);
      string zombieResult = zombieController.Result;

      // Actualizar el prompt del superviviente con la respuesta del zombie
      index = survivorController.currentPrompt.IndexOf("The zombie told you:");
      if (index != -1){
        survivorController.currentPrompt = survivorController.currentPrompt.Substring(0, index).Trim();
      }
      survivorController.currentPrompt = survivorController.currentPrompt + " The zombie told you: " + zombieResult;
    }
  }
}