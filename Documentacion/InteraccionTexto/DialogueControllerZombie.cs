using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueControllerZombie : MonoBehaviour
{
    // Controlador de animaciones para las transiciones de entrada y salida de los diálogos del zombie
    public Animator DialogueAnimator;

    // Componente de texto para mostrar las frases generadas por el zombie
    public TextMeshProUGUI DialogueText;

    // Velocidad a la que se escribe cada letra en el diálogo (en segundos por letra)
    public float DialogueSpeed = 0.05f;

    // Tiempo de espera antes de que se active la animación de entrada/salida del diálogo
    public float AnimationDelay = 0.5f;

    // Interfaz para enviar prompts y recibir respuestas de Hugging Face
    public HuggingFaceAPI huggingFaceAPI;

    // Prompt inicial que describe el contexto para el zombie
    public string currentPrompt = "You are a zombie in a fictional setting, there are many like you chasing a man who is fighting for its life. Say a phrase to make him scared. Only send me the phrase.";

    // Resultado devuelto por la API después de procesar el prompt
    public string Result { get; private set; }

    // Indica si el diálogo actual ha finalizado
    public bool IsDialogueComplete { get; private set; }

    // Inicializa el estado del diálogo al iniciar el script
    private void Start()
    {
        IsDialogueComplete = false; // Marca que no hay diálogo activo
    }

    // Envia un nuevo prompt a la API para generar una respuesta
    public void SendNewPrompt(string prompt)
    {
        huggingFaceAPI.SendRequest(prompt); // Envía el prompt a la API
        StartCoroutine(WaitForResult()); // Inicia la corrutina para esperar el resultado
    }

    // Corrutina que espera a que la API devuelva un resultado
    private IEnumerator WaitForResult()
    {
        IsDialogueComplete = false; // Indica que el diálogo aún no está completo
        yield return new WaitUntil(() => huggingFaceAPI.Result != "Loading..."); // Espera a que la API termine de cargar
        Result = huggingFaceAPI.Result; // Almacena el resultado generado por la API
        DialogueAnimator.SetTrigger("Enter_Zombie"); // Activa la animación de entrada del zombie
        yield return new WaitForSeconds(AnimationDelay); // Espera antes de mostrar el texto
        StartCoroutine(WriteSentence(Result)); // Inicia la escritura progresiva del texto
    }

    // Corrutina que escribe el texto letra por letra para simular una escritura dinámica
    private IEnumerator WriteSentence(string sentence)
    {
        DialogueText.text = ""; // Limpia cualquier texto anterior
        foreach (char character in sentence.ToCharArray()) // Recorre cada letra de la frase
        {
            DialogueText.text += character; // Añade la letra actual al texto mostrado
            yield return new WaitForSeconds(DialogueSpeed); // Espera antes de mostrar la siguiente letra
        }
        yield return new WaitForSeconds(sentence.Length * DialogueSpeed); // Espera un tiempo adicional tras completar la frase
        DialogueText.text = ""; // Limpia el texto tras completarlo
        DialogueAnimator.SetTrigger("Exit_Zombie"); // Activa la animación de salida del zombie
        IsDialogueComplete = true; // Marca que el diálogo ha finalizado
    }
}
