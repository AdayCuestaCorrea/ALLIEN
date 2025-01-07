using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Text;
using System.Text.RegularExpressions;

public class HuggingFaceAPI : MonoBehaviour
{
    [SerializeField] private string APIKey; // Introduce tu API Key aquí.
    [SerializeField] private string modelName = "Qwen/QwQ-32B-Preview"; // Cambia el modelo si lo deseas.
    private string result;
    private string prompt;
    private readonly string huggingFaceAPIUrl = "https://api-inference.huggingface.co/models/";

    public string Result => result; // Propiedad pública para obtener el resultado

    public void SendRequest(string newPrompt)
    {
        prompt = newPrompt;
        result = "Loading...";
        StartCoroutine(SendRequestToHuggingFace());
    }

    private IEnumerator SendRequestToHuggingFace()
    {
        // Genera valores aleatorios para temperature y top_p
        float temperature = Random.Range(0.4f, 1f);
        float top_p = Random.Range(0.4f, 1f);

        // Crea el cuerpo de la solicitud.
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
        string jsonData = JsonUtility.ToJson(requestBody);

        byte[] rawData = Encoding.UTF8.GetBytes(jsonData);

        // Configura la solicitud HTTP.
        UnityWebRequest request = new UnityWebRequest(huggingFaceAPIUrl + modelName + "/v1/chat/completions", "POST");
        request.uploadHandler = new UploadHandlerRaw(rawData);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", $"Bearer {APIKey}");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var responseBody = JsonUtility.FromJson<ResponseBody>(request.downloadHandler.text);
            string content = responseBody.choices[0].message.content;

            // Imprime la respuesta completa para depuración
            Debug.Log("Respuesta completa del modelo: " + request.downloadHandler.text);

            // Elimina "streamline" y caracteres extraños, pero permite caracteres Unicode
            content = Regex.Replace(content, @"streamline|[^a-zA-Z0-9\s\p{P}\p{L}]", "");

            result = content;
        }
        else
        {
            result = $"Error: {request.error}";
        }
    }

    [System.Serializable]
    public class RequestBody
    {
        public List<Message> messages;
        public float temperature;
        public float top_p;
    }

    [System.Serializable]
    public class Message
    {
        public string role;
        public string content;
    }

    [System.Serializable]
    public class ResponseBody
    {
        public List<Choice> choices;
    }

    [System.Serializable]
    public class Choice
    {
        public Message message;
    }
}