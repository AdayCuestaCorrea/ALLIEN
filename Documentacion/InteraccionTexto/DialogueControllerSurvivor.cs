using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueControllerSurvivor : MonoBehaviour
{
    // Animator para controlar las transiciones de entrada y salida de los diálogos
    public Animator DialogueAnimator;

    // Componente de texto para mostrar las frases del superviviente
    public TextMeshProUGUI DialogueText;

    // Velocidad de escritura de las letras del diálogo (en segundos por letra)
    public float DialogueSpeed = 0.05f;

    // Tiempo de espera antes de que la animación de diálogo comience o termine
    public float AnimationDelay = 0.5f;

    // Referencia al sistema de API de Hugging Face para generar respuestas dinámicas
    public HuggingFaceAPI huggingFaceAPI;

    // Prompt actual que se envía al sistema de lenguaje para generar una frase
    public string currentPrompt = "You are one of the last survivors on Earth. Zombies are everywhere and you are killing as many as you can. Say a phrase that suits the situation. Only send me the phrase.";

    // Resultado generado por la API
    public string Result { get; private set; }

    // Bandera que indica si el diálogo actual ha finalizado
    public bool IsDialogueComplete { get; private set; }

    // Inicializa el estado del diálogo al inicio del script
    private void Start()
    {
        IsDialogueComplete = false;
    }

    // Envia un nuevo prompt al sistema de Hugging Face para generar una respuesta
    public void SendNewPrompt(string prompt)
    {
        huggingFaceAPI.SendRequest(prompt); // Solicita una respuesta usando el nuevo prompt
        StartCoroutine(WaitForResult()); // Inicia la espera de la respuesta
    }

    // Corrutina que espera hasta que Hugging Face devuelva un resultado
    private IEnumerator WaitForResult()
    {
        IsDialogueComplete = false; // Marca que el diálogo está en proceso
        yield return new WaitUntil(() => huggingFaceAPI.Result != "Loading..."); // Espera hasta que la API deje de cargar
        Result = huggingFaceAPI.Result; // Almacena el resultado de la API
        DialogueAnimator.SetTrigger("Enter_Survivor"); // Inicia la animación de entrada del superviviente
        yield return new WaitForSeconds(AnimationDelay); // Espera antes de mostrar el texto
        StartCoroutine(WriteSentence(Result)); // Escribe la frase letra por letra
    }

    // Corrutina que escribe una frase de manera progresiva, mostrando cada letra con un retraso
    private IEnumerator WriteSentence(string sentence)
    {
        yield return new WaitForSeconds(AnimationDelay); // Espera inicial antes de empezar a escribir
        DialogueText.text = ""; // Limpia el texto existente
        foreach (char character in sentence.ToCharArray()) // Recorre cada letra de la frase
        {
            DialogueText.text += character; // Añade la letra al texto mostrado
            yield return new WaitForSeconds(DialogueSpeed); // Espera antes de añadir la siguiente letra
        }
        yield return new WaitForSeconds(sentence.Length * DialogueSpeed); // Espera un tiempo adicional después de escribir la frase
        DialogueText.text = ""; // Limpia el texto después de mostrarlo
        DialogueAnimator.SetTrigger("Exit_Survivor"); // Inicia la animación de salida del superviviente
        IsDialogueComplete = true; // Marca que el diálogo ha finalizado
    }
}
