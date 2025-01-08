using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Text;
using System.Text.RegularExpressions;

public class HuggingFaceAPI : MonoBehaviour
{
    // Clave de API para acceder a los servicios de Hugging Face.
    [SerializeField] private string APIKey;

    // Nombre del modelo que se desea utilizar. Puede ser modificado para cambiar el modelo.
    [SerializeField] private string modelName = "Qwen/QwQ-32B-Preview";

    // Variables privadas para almacenar el resultado y el prompt actual.
    private string result;
    private string prompt;

    // URL base de la API de Hugging Face.
    private readonly string huggingFaceAPIUrl = "https://api-inference.huggingface.co/models/";

    // Propiedad pública para acceder al resultado actual.
    public string Result => result;

    // Método para enviar una nueva solicitud al modelo con un prompt específico.
    public void SendRequest(string newPrompt)
    {
        prompt = newPrompt; // Almacena el nuevo prompt.
        result = "Loading..."; // Indica que la solicitud está en proceso.
        StartCoroutine(SendRequestToHuggingFace()); // Inicia la corrutina para enviar la solicitud.
    }

    // Corrutina que gestiona el envío de la solicitud a la API de Hugging Face.
    private IEnumerator SendRequestToHuggingFace()
    {
        // Genera valores aleatorios para los parámetros de configuración del modelo.
        float temperature = Random.Range(0.4f, 1f); // Controla la creatividad de la respuesta.
        float top_p = Random.Range(0.4f, 1f); // Controla la diversidad de la respuesta.

        // Crea el cuerpo de la solicitud con el mensaje y los parámetros generados.
        var messages = new List<Message>
        {
            new Message { role = "user", content = prompt }
        };
        var requestBody = new RequestBody 
        { 
            messages = messages,
            temperature = temperature,
            top_p = top_p
        };
        string jsonData = JsonUtility.ToJson(requestBody); // Convierte el cuerpo en formato JSON.

        // Convierte los datos JSON a bytes para enviarlos en la solicitud.
        byte[] rawData = Encoding.UTF8.GetBytes(jsonData);

        // Configura la solicitud HTTP a la API de Hugging Face.
        UnityWebRequest request = new UnityWebRequest(huggingFaceAPIUrl + modelName + "/v1/chat/completions", "POST");
        request.uploadHandler = new UploadHandlerRaw(rawData); // Datos enviados en el cuerpo.
        request.downloadHandler = new DownloadHandlerBuffer(); // Maneja la respuesta.

        // Configura las cabeceras HTTP necesarias para la solicitud.
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", $"Bearer {APIKey}");

        // Envía la solicitud y espera la respuesta.
        yield return request.SendWebRequest();

        // Procesa la respuesta si la solicitud fue exitosa.
        if (request.result == UnityWebRequest.Result.Success)
        {
            // Convierte la respuesta JSON en un objeto manejable.
            var responseBody = JsonUtility.FromJson<ResponseBody>(request.downloadHandler.text);

            // Obtiene el contenido del mensaje generado por el modelo.
            string content = responseBody.choices[0].message.content;

            // Imprime la respuesta completa para depuración.
            Debug.Log("Respuesta completa del modelo: " + request.downloadHandler.text);

            // Elimina caracteres innecesarios o no deseados del texto de respuesta.
            content = Regex.Replace(content, @"streamline|[^a-zA-Z0-9\s\p{P}\p{L}]", "");

            // Asigna el contenido procesado al resultado.
            result = content;
        }
        else
        {
            // Muestra el error en caso de que la solicitud falle.
            result = $"Error: {request.error}";
        }
    }

    // Clase que define la estructura del cuerpo de la solicitud.
    [System.Serializable]
    public class RequestBody
    {
        public List<Message> messages; // Lista de mensajes enviados al modelo.
        public float temperature; // Parámetro de creatividad.
        public float top_p; // Parámetro de diversidad.
    }

    // Clase que representa un mensaje individual en la conversación.
    [System.Serializable]
    public class Message
    {
        public string role; // Rol del mensaje, como "user" (usuario).
        public string content; // Contenido del mensaje.
    }

    // Clase que define la estructura del cuerpo de la respuesta.
    [System.Serializable]
    public class ResponseBody
    {
        public List<Choice> choices; // Lista de opciones generadas por el modelo.
    }

    // Clase que representa una opción generada por el modelo.
    [System.Serializable]
    public class Choice
    {
        public Message message; // Mensaje generado en la opción.
    }
}
