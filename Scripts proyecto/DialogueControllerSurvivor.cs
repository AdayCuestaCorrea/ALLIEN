using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueControllerSurvivor : MonoBehaviour
{
    public Animator DialogueAnimator;
    public TextMeshProUGUI DialogueText;
    public float DialogueSpeed = 0.05f;
    public float AnimationDelay = 0.5f;
    public HuggingFaceAPI huggingFaceAPI;
    public string currentPrompt = "You are one of the last survivors on Earth. Zombies are everywhere and you are killing as many as you can. Say a phrase that suits the situation. Only send me the phrase.";
    public string Result { get; private set; }
    public bool IsDialogueComplete { get; private set; }

    private void Start()
    {
      IsDialogueComplete = false;
    }

    public void SendNewPrompt(string prompt)
    {
      huggingFaceAPI.SendRequest(prompt);
      StartCoroutine(WaitForResult());
    }

    private IEnumerator WaitForResult()
    {
      IsDialogueComplete = false;
      yield return new WaitUntil(() => huggingFaceAPI.Result != "Loading...");
      Result = huggingFaceAPI.Result;
      DialogueAnimator.SetTrigger("Enter_Survivor");
      yield return new WaitForSeconds(AnimationDelay);
      StartCoroutine(WriteSentence(Result));
    }

    private IEnumerator WriteSentence(string sentence) {
      yield return new WaitForSeconds(AnimationDelay);
      DialogueText.text = "";
      foreach (char character in sentence.ToCharArray())
      {
        DialogueText.text += character;
        yield return new WaitForSeconds(DialogueSpeed);
      }
      yield return new WaitForSeconds(sentence.Length * DialogueSpeed);
      DialogueText.text = "";
      DialogueAnimator.SetTrigger("Exit_Survivor");
      IsDialogueComplete = true;
    }
}