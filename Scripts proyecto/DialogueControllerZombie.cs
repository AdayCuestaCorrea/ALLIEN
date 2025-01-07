using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueControllerZombie : MonoBehaviour
{
    public Animator DialogueAnimator;
    public TextMeshProUGUI DialogueText;
    public float DialogueSpeed = 0.05f;
    public float AnimationDelay = 0.5f;
    public HuggingFaceAPI huggingFaceAPI;
    public string currentPrompt = "You are a zombie in a fictional setting, there are many like you chasing a man who is fighting for its life. Say a phrase to make him scared. Only send me the phrase.";
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
        DialogueAnimator.SetTrigger("Enter_Zombie");
        yield return new WaitForSeconds(AnimationDelay);
        StartCoroutine(WriteSentence(Result));
    }

    private IEnumerator WriteSentence(string sentence)
    {
        DialogueText.text = "";
        foreach (char character in sentence.ToCharArray())
        {
            DialogueText.text += character;
            yield return new WaitForSeconds(DialogueSpeed);
        }
        yield return new WaitForSeconds(sentence.Length * DialogueSpeed);
        DialogueText.text = "";
        DialogueAnimator.SetTrigger("Exit_Zombie");
        IsDialogueComplete = true;
    }
}