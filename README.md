# ALLIEN

## Introducción

Este documento organiza la estructura del proyecto **ALLIEN**, que incluye sistemas de IA avanzados, interacciones textuales dinámicas y un entorno inmersivo desarrollado con Unity. Aquí se describe la ubicación de los principales archivos y documentos relacionados con el desarrollo y funcionamiento del proyecto.

---

## Estructura del proyecto

La organización del proyecto **ALLIEN** está dividida en las siguientes carpetas:

### 1. **Documentacion**

#### **Entrenamiento**
Esta carpeta contiene los archivos relacionados con el entrenamiento de los agentes, incluyendo scripts y configuraciones YAML para ML-Agents.

- **Archivos principales**:
  - `AgentController.cs`: Control del comportamiento del agente.
  - `BetterConf.yaml`: Configuración optimizada para el modelo.
  - `CameraSwitch.cs`: Cambio de cámaras en el juego.
  - `EnemyAI.cs`: Control del comportamiento de los enemigos.
  - `EnemySpawner.cs`: Generación de enemigos.
  - `GunController.cs`: Control del arma del superviviente.
  - `ModelSwitcher.cs`: Cambio de modelos de IA para el agente.
  - `RunConf.yaml`: Configuración para la ejecución del modelo.
  - `WorldBehaviors.cs`: Gestión del entorno y zombies.
  - `ZombieController.cs`: Comportamiento de los zombies.
  - `midConf.yaml`: Configuración intermedia para el modelo.

- **Documentación**:
  - `Entrenamiento.md`: Explicación del proceso de entrenamiento, configuración y uso de los agentes en el entorno.

#### **Interaccion.md**
Este archivo detalla cómo interactúan los diversos componentes del proyecto, como el agente, los zombies y el entorno, describiendo sus relaciones y dependencias.

#### **InteraccionTexto**
Contiene scripts y módulos específicos para el sistema de interacción textual:

- **Archivos principales**:
  - `DialogueControllerSurvivor.cs`: Controlador de diálogo para el superviviente.
  - `DialogueControllerZombie.cs`: Controlador de diálogo para el zombie.
  - `DialogueManager.cs`: Gestión de los diálogos entre el superviviente y el zombie.
  - `HuggingFaceAPI.cs`: Comunicación con la API de Hugging Face para generar frases dinámicas.

---

### 2. **Informe.md**

El informe del proyecto **ALLIEN** contiene:

- **Propuesta**: Descripción, objetivos y planificación del proyecto.
- **Desarrollo**: Explicación técnica de los sistemas implementados, incluyendo el uso de herramientas como ML-Agents, NavMesh y Hugging Face.
- **Resultados y conclusiones**: Evaluación del cumplimiento de los objetivos y requisitos establecidos.
- **Anexos**: Recursos adicionales como enlaces a repositorios, presentaciones y documentación de referencia.

---

## Descripción de archivos

### **Entrenamiento**

#### Scripts principales:
- `AgentController.cs`: Control del comportamiento del superviviente.
- `GunController.cs`: Gestión del arma y disparos.
- `WorldBehaviors.cs`: Control general del entorno.
- `ZombieController.cs`: Comportamiento y navegación de los zombies.

#### Configuraciones:
- `BetterConf.yaml`, `midConf.yaml`, `RunConf.yaml`: Configuración de entrenamiento para modelos basados en aprendizaje por refuerzo.

---

### **InteraccionTexto**

#### Scripts:
- `DialogueControllerSurvivor.cs`: Muestra frases generadas para el superviviente.
- `DialogueControllerZombie.cs`: Muestra frases generadas para el zombie.
- `DialogueManager.cs`: Alterna entre los diálogos del superviviente y el zombie.
- `HuggingFaceAPI.cs`: Realiza solicitudes a la API de Hugging Face para obtener frases dinámicas.

---

## Guía de navegación

1. **Entrenamiento de IA**:
   - Consulte los archivos en `Documentacion/Entrenamiento` para entender cómo los agentes son entrenados y configurados.
   - Lea `Entrenamiento.md` para una descripción detallada del proceso de entrenamiento.

2. **Interacciones textuales**:
   - Los scripts de `Documentacion/InteraccionTexto` gestionan los diálogos entre el superviviente y los zombies.
   - Consulte `HuggingFaceAPI.cs` para conocer cómo se integran los modelos de lenguaje.

3. **Informe del proyecto**:
   - Diríjase a `Informe.md` para una visión completa del proyecto, sus objetivos, problemas encontrados y resultados alcanzados.

---

## Recomendaciones

- Utilice los documentos técnicos en `Documentacion` para comprender y extender el sistema.
- Consulte las configuraciones YAML en `Entrenamiento` para realizar ajustes en los modelos de aprendizaje por refuerzo.
- Explore los scripts en `InteraccionTexto` para implementar o modificar el sistema de interacción textual.

---
