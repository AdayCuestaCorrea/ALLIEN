# Documentación de la interacción

## Índice

1. [Descripción general](#descripción-general)
2. [Ficheros](#ficheros)
   - [DialogueControllerSurvivor.cs](#dialoguecontrollersurvivorcs)
   - [DialogueControllerZombie.cs](#dialoguecontrollerzombiecs)
   - [DialogueManager.cs](#dialoguemanagercs)
   - [HuggingFaceAPI.cs](#huggingfaceapics)
3. [Interacciones entre componentes](#interacciones-entre-componentes)
4. [Flujo del sistema de diálogos](#flujo-del-sistema-de-diálogos)

---

## Descripción general

Este módulo implementa un sistema de diálogos entre un superviviente y un zombie, donde ambos intercambian frases generadas dinámicamente a través de un modelo de lenguaje alojado en Hugging Face. Los diálogos son gestionados con animaciones y se adaptan dinámicamente en función de las interacciones.

---

## Ficheros

### DialogueControllerSurvivor.cs

Controla las frases y animaciones del superviviente:
- **Funciones principales**:
  - Enviar un nuevo prompt a la API de Hugging Face.
  - Esperar la respuesta de la API y mostrarla en pantalla con animaciones.
- **Atributos relevantes**:
  - `currentPrompt`: Prompt inicial enviado a la API.
  - `Result`: Frase generada por la API.
  - `DialogueAnimator`: Controla las animaciones de entrada y salida del diálogo.
  - `DialogueText`: Muestra el texto del diálogo en pantalla.
- **Interacción con otros componentes**:
  - Usa la clase **HuggingFaceAPI** para enviar solicitudes y recibir respuestas.

### DialogueControllerZombie.cs

Controla las frases y animaciones del zombie:
- **Funciones principales**:
  - Enviar un nuevo prompt a la API de Hugging Face.
  - Esperar la respuesta de la API y mostrarla en pantalla con animaciones.
- **Atributos relevantes**:
  - `currentPrompt`: Prompt inicial que simula ser un zombie aterrador.
  - `Result`: Frase generada por la API.
  - `DialogueAnimator`: Controla las animaciones de entrada y salida del diálogo.
  - `DialogueText`: Muestra el texto del diálogo en pantalla.
- **Interacción con otros componentes**:
  - Usa la clase **HuggingFaceAPI** para generar contenido dinámico.

### DialogueManager.cs

Gestiona el flujo de diálogos entre el superviviente y el zombie:
- **Funciones principales**:
  - Alternar entre el diálogo del superviviente y el zombie.
  - Actualizar los prompts dinámicamente basándose en las respuestas previas.
- **Interacción con otros componentes**:
  - Coordina las clases **DialogueControllerSurvivor** y **DialogueControllerZombie**.
  - Maneja las dependencias y asegura un flujo continuo de interacciones.

### HuggingFaceAPI.cs

Permite la interacción con el modelo de lenguaje de Hugging Face:
- **Funciones principales**:
  - Enviar solicitudes POST al modelo especificado.
  - Configurar parámetros como `temperature` y `top_p` para ajustar la generación del texto.
- **Atributos relevantes**:
  - `APIKey`: Clave para autenticar la API.
  - `modelName`: Nombre del modelo utilizado para la generación de texto.
  - `Result`: Almacena la respuesta procesada del modelo.
- **Estructuras internas**:
  - `RequestBody`: Representa la solicitud JSON enviada a la API.
  - `Message`: Define un mensaje dentro del cuerpo de la solicitud.
  - `ResponseBody`: Procesa la respuesta JSON recibida.

---

## Interacciones entre componentes

1. **HuggingFaceAPI.cs**:
   - Se encarga de la comunicación con el modelo de lenguaje.
   - Proporciona las frases generadas que serán usadas por los controladores de diálogo.

2. **DialogueControllerSurvivor.cs**:
   - Envía prompts relacionados con el superviviente a **HuggingFaceAPI**.
   - Muestra las frases en pantalla con animaciones.

3. **DialogueControllerZombie.cs**:
   - Envía prompts relacionados con el zombie a **HuggingFaceAPI**.
   - Coordina la visualización de las frases generadas.

4. **DialogueManager.cs**:
   - Alterna entre los diálogos del superviviente y el zombie.
   - Actualiza los prompts en función de las respuestas previas para mantener un diálogo coherente.

---

## Flujo del sistema de diálogos

1. **Inicio del diálogo**:
   - El `DialogueManager` inicia el proceso llamando a `SendNewPrompt` en **DialogueControllerSurvivor**.
   - El prompt inicial del superviviente se envía a la API y se recibe una respuesta.

2. **Respuesta del zombie**:
   - La respuesta del superviviente se utiliza para actualizar el prompt del zombie.
   - **DialogueControllerZombie** envía el nuevo prompt a la API, recibe la respuesta y la muestra en pantalla.

3. **Ciclo continuo**:
   - El proceso se repite alternando entre el superviviente y el zombie, creando un diálogo dinámico.

4. **Visualización**:
   - Las frases se muestran en pantalla con animaciones controladas por `DialogueAnimator`.

5. **Ajustes del modelo**:
   - Los parámetros como `temperature` y `top_p` se generan aleatoriamente para diversificar las respuestas.

---

