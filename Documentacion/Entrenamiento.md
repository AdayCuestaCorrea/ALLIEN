# Documentación del entrenamiento

## Índice

1. [Descripción general](#descripción-general)
2. [Ficheros](#ficheros)
   - [AgentController.cs](#agentcontrollercs)
   - [CameraSwitch.cs](#cameraswitchcs)
   - [EnemyAI.cs](#enemyaics)
   - [EnemySpawner.cs](#enemyspawnercs)
   - [GunController.cs](#guncontrollercs)
   - [ModelSwitcher.cs](#modelswitchercs)
   - [WorldBehaviors.cs](#worldbehaviorscs)
   - [ZombieController.cs](#zombiecontrollercs)
   - [BetterConf.yaml](#betterconfyaml)
   - [midConf.yaml](#midconfyaml)
   - [RunConf.yaml](#runconfyaml)
3. [Interacciones entre componentes](#interacciones-entre-componentes)

---

## Descripción general

Este proyecto utiliza Unity para simular un entorno en el que un agente controlado por inteligencia artificial debe sobrevivir en un escenario lleno de zombies. El agente puede moverse, disparar y utilizar modelos de redes neuronales para mejorar su comportamiento. Los zombies son controlados por inteligencia artificial y aparecen en el mundo de manera controlada.

---

## Ficheros

### AgentController.cs

Controla el comportamiento del agente principal del juego:
- Usa ML-Agents para procesar observaciones del entorno y tomar decisiones.
- Interactúa con:
  - **GunController**: Para disparar y gestionar la munición.
  - **WorldBehaviors**: Para gestionar eventos como reinicio de episodios o generación de zombies.
  - **ModelSwitcher**: Permite cambiar el modelo de red neuronal del agente.

### CameraSwitch.cs

Permite cambiar entre diferentes cámaras para ver el escenario desde distintas perspectivas:
- Configura cámaras como:
  - Vista completa.
  - Cámara detrás del personaje.
  - Cámara cinemática.

### EnemyAI.cs

Controla el comportamiento de los enemigos:
- Se basa en un **NavMeshAgent** para moverse hacia su objetivo.
- Gestiona disparos, cobertura y daños recibidos.
- Interactúa con el jugador y se conecta a eventos como su propia destrucción.

### EnemySpawner.cs

Gestión de la generación de enemigos:
- Genera enemigos en puntos específicos del mapa en intervalos de tiempo.
- Suscribe a los enemigos al evento `OnDead` para eliminarlos de la lista cuando son destruidos.

### GunController.cs

Controla las mecánicas del arma del agente:
- Dispara rayos láser que eliminan objetivos.
- Gestiona la munición y el proceso de recarga.
- Interactúa con **WorldBehaviors** para actualizar la lista de zombies eliminados.

### ModelSwitcher.cs

Cambia dinámicamente el modelo de red neuronal usado por el agente:
- Actualiza los parámetros del agente para usar un nuevo modelo y ajustar su velocidad.

### WorldBehaviors.cs

Gestiona el entorno del juego:
- Controla la generación y eliminación de zombies.
- Coloca al agente en el escenario y gestiona su interacción con los objetos.
- Proporciona funciones para reiniciar el estado del mundo.

### ZombieController.cs

Controla el comportamiento de los zombies:
- Usa **NavMeshAgent** para seguir al agente principal.
- Se asegura de que el zombie sea eliminado correctamente al ser destruido.

### BetterConf.yaml

Contiene la configuración de entrenamiento para un agente con parámetros optimizados:
- Algoritmo: **PPO** (Proximal Policy Optimization).
- Hiperparámetros:
  - Tamaño de lote: 4096.
  - Buffer de experiencia: 32768.
  - Tasa de aprendizaje: 5e-05.
  - Número de épocas: 5.
- Configuración de red:
  - 3 capas ocultas con 1024 unidades.
  - Uso de memoria recurrente con una secuencia de 64 pasos.
- Recompensas:
  - Extrínsecas con un descuento gamma de 0.995.
- Identificador de ejecución: `shoot_5_better_parameters_no_punish_hearing`.

### midConf.yaml

Similar a **BetterConf.yaml**, pero con ajustes en el identificador de ejecución:
- Identificador: `shoot_5_better_parameters_and_survival_rewards_no_punish_hearing_vector_solved`.

### RunConf.yaml

Similar a **BetterConf.yaml**, pero con un enfoque en configuraciones específicas para velocidad:
- Identificador: `shoot_5_better_parameters_no_punish_hearing_vector_solved_speed_5`.

---

## Interacciones entre componentes

### Relación principal

1. **AgentController.cs**:
   - Recibe información de:
     - **GunController** (estado del arma y munición).
     - **WorldBehaviors** (estado del entorno y zombies).
   - Controla el disparo del arma y utiliza modelos de inteligencia artificial para moverse.

2. **EnemySpawner.cs**:
   - Genera enemigos que son instancias de **EnemyAI**.
   - Gestiona el evento de destrucción de los enemigos.

3. **GunController.cs**:
   - Permite al agente disparar y eliminar zombies.
   - Comunica los impactos exitosos a **WorldBehaviors** para actualizar el estado del entorno.

4. **WorldBehaviors.cs**:
   - Centraliza la gestión del escenario:
     - Generación de zombies mediante **ZombieController**.
     - Reposicionamiento del agente y limpieza del entorno.

5. **ZombieController.cs**:
   - Sigue al agente principal utilizando rutas generadas con **NavMeshAgent**.
   - Es controlado y eliminado por **WorldBehaviors** cuando es destruido.

6. **ModelSwitcher.cs**:
   - Permite cambiar el modelo de red neuronal del agente, afectando su velocidad y comportamiento.

7. **CameraSwitch.cs**:
   - Cambia entre distintas perspectivas de cámara para observar el escenario desde diferentes ángulos.

---

## Ejemplo de flujo

1. **Inicio del juego**:
   - **WorldBehaviors** inicializa el escenario.
   - **EnemySpawner** genera los zombies.
   - **AgentController** se posiciona y comienza el episodio.

2. **Durante el juego**:
   - **AgentController** recoge observaciones del entorno (zombies, posición, munición).
   - Utiliza **GunController** para disparar a los zombies.
   - Los zombies generados por **EnemySpawner** son controlados por **ZombieController** para atacar al agente.

3. **Cambio de modelo**:
   - **ModelSwitcher** actualiza el modelo de red neuronal del agente, afectando su comportamiento.

4. **Finalización**:
   - Cuando el agente es alcanzado por un zombie o elimina a todos, **WorldBehaviors** reinicia el episodio.

---
