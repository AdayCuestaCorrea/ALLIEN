# ALLIEN

# Propuesta

## Descripción

El objetivo del proyecto **ALLIEN** (acrónimo de “ally”, “intelligence”, “enemy” y “nemesis”) es desarrollar un sistema de inteligencia artificial que permita la interacción dinámica entre enemigos (zombies) y un superviviente controlado por una IA, en un juego tipo shooter. Además, se ha implementado un módulo de interacción por texto. Los componentes clave del proyecto son los siguientes:

* **Sistema némesis**: Este sistema de IA permite la evolución adaptativa de los enemigos, basándose en la interacción y el análisis de las estrategias empleadas por el superviviente (controlado por una IA). 

* **Superviviente controlado por IA:** Se ha diseñado una IA que simula las decisiones tácticas y estratégicas de un jugador humano. Esta IA evalúa el entorno, interactúa con los enemigos y emplea armas para defenderse. Sirve como oponente para el sistema de IA de los enemigos, lo que permite pruebas y ajustes continuos.

* **Sistema de generación dinámica de texto:** Las interacciones entre los zombies y el jugador IA se desarrollan mediante prompts que definen sus textos. Por ejemplo:  
  * Para el jugador: *"Eres un superviviente, responde a lo que dice el zombie."*  
  * Para el zombie: *"Eres un zombie que va a atacar al jugador, dile algo."*

# Recursos a usar

El desarrollo de nuestro proyecto está basado en el motor [*Unity*](https://unity.com/es), que proporciona un entorno potente para crear y gestionar entornos 3D complejos. Además, utilizamos el *control de versiones de Unity*, una herramienta integrada que nos permite colaborar de manera eficiente en el desarrollo del juego. Esto asegura que los cambios realizados por diferentes miembros del equipo se gestionen de forma centralizada, evitando conflictos y manteniendo un historial claro de modificaciones en el proyecto.

Usamos [*ClickUp*](https://sharing.clickup.com/9012487720/g/h/8cjz9h8-332/b5b7f445d8762ea) para organizar las tareas, lo que nos permite coordinar en equipo los avances y las prioridades de cada sprint semanal. Cada miembro tiene tareas específicas, y ClickUp facilita el seguimiento del progreso de cada fase.

Utilizamos [*ML-Agents*](https://github.com/Unity-Technologies/ml-agents), una herramienta específica de Unity que facilita la integración de redes neuronales en el juego mediante aprendizaje por refuerzo. Gracias a esta tecnología, hemos entrenado al superviviente para realizar acciones complejas como el movimiento estratégico, el seguimiento preciso de los enemigos y el disparo eficiente para defenderse de los zombies. 

Otra herramienta esencial es [*NavMesh*](https://docs.unity3d.com/es/2019.4/Manual/nav-BuildingNavMesh.html), también de Unity. Con *NavMesh*, hemos implementado un sistema de navegación inteligente para los personajes, permitiendo movimientos fluidos y realistas en diversos entornos. Este sistema se utiliza tanto para el desplazamiento de los zombies como para las rutas del superviviente.

# Planificación

## Lista de tareas

Las tareas del proyecto **ALLIEN** se organizaron en seis grandes apartados que abarcan desde la investigación inicial hasta la documentación final. A continuación, se presenta una visión general de estas etapas:

1. **Investigación inicial**: Se realizó una exploración sobre sistemas de inteligencia artificial adaptativa para enemigos y asistentes en videojuegos (aunque posteriormente se descartó el asistente).  
2. **Desarrollo del sistema némesis**: Diseño, implementación e integración inicial del sistema de enemigos, asegurando su capacidad de adaptación y aprendizaje en tiempo real.  
3. **Desarrollo del superviviente controlado por IA**: Creación de la arquitectura del jugador IA, con su implementación básica y pruebas iniciales para garantizar comportamientos tácticos eficaces en combate.  
4. **Pruebas de integración**: Combinación de los sistemas de enemigos y el jugador IA en un entorno unificado.  
5. **Revisión y optimización**: Refinamiento del código, ajuste de la dificultad y optimización de la experiencia de usuario mediante pruebas finales.  
6. **Documentación**: Redacción de documentación técnica, preparación de la presentación del proyecto y elaboración del informe final.

# Requisitos y entregables

| *R01* | Sistema de IA para enemigos adaptativos con capacidad de aprender de las interacciones con el superviviente. |
| :---- | :---- |
| ***R02*** | Superviviente controlado por IA con capacidad de moverse, seguir objetivos y disparar para defenderse de los zombies, utilizando ML-Agents. |
| ***R03*** | Modularidad para expansiones futuras y escalabilidad en los comportamientos de IA, permitiendo añadir nuevos roles o interacciones. |
| ***R04*** | Sistema de navegación basado en *NavMesh* para movimientos fluidos en entornos complejos. |
| ***R05*** | Sistema de generación de interacciones textuales dinámicas para los diálogos entre el superviviente y los enemigos, utilizando prompts basados en modelos de lenguaje. |
| ***R06*** | Documentación completa en GitHub y en Unity. |
| ***R07*** | Interfaz realizada en inglés. |

| *E01* | Sistema némesis con enemigos adaptativos. |
| :---- | :---- |
| ***E02*** | Superviviente controlado por IA con comportamiento defensivo, incluyendo disparos y movimientos estratégicos. |
| ***E03*** | Sistema modular de IA documentado, preparado para integraciones y mejoras futuras. |
| ***E04*** | Integración completa con Unity utilizando *NavMesh* para navegación y *ML-Agents* para acciones avanzadas como disparar y seguimiento. |
| ***E05*** | Sistema de interacción textual dinámico integrado. |
| ***E06*** | Informe completo del proyecto, detallando los objetivos, el diseño de IA, los procesos de desarrollo y los resultados alcanzados. |
| ***E07*** | Presentación final del proyecto. |

# Objetivos del proyecto

| *OB01* | Implementar un sistema de enemigos adaptativos que evolucionen y mejoren sus ataques según las interacciones con el superviviente. |
| :---- | :---- |
|  | **Criterio de éxito:** Los enemigos adaptan su comportamiento y tácticas con 3 modelos distintos, incluyendo cambios seguimiento. |
| ***OB02*** | Desarrollar un superviviente controlado por IA que utilice *ML-Agents* para moverse, seguir objetivos y disparar. |
|  | **Criterio de éxito:** El jugador IA demuestra comportamiento defensivo eficaz en al menos 3 modelos diferentes, con precisión en sus acciones. |
| ***OB03*** | Integrar un sistema de navegación basado en *NavMesh* para movimientos realistas y fluidos. |
|  | **Criterio de éxito:** Todos los personajes (enemigos y jugador) navegan correctamente sin errores evidentes. |
| ***OB04*** | Asegurar la modularidad y documentación del sistema de IA. |
|  | **Criterio de éxito:** Un desarrollador externo puede añadir nuevos comportamientos o funciones en menos de 2 horas, utilizando la documentación proporcionada. |
| ***OBJ5*** | Implementar un sistema de generación de interacciones textuales dinámicas entre el superviviente y los enemigos, utilizando modelos de lenguaje basados en prompts. |
|  | Las interacciones textuales generadas enriquecen la narrativa del juego y son coherentes. |

# Desarrollo y funcionamiento

## Creación del escenario en Unity

El primer paso del desarrollo fue familiarizarnos con Unity y su entorno de trabajo. Para ello, aprovechamos los conocimientos adquiridos en la asignatura de **Interfaces Inteligentes**, donde abordamos los siguientes conceptos clave:

* **Gráficos 3D**: Modelado, texturizado y renderizado de objetos tridimensionales para crear entornos visualmente atractivos.  
* **Iluminación**: Configuración de luces y sombras para mejorar la ambientación y el realismo de los escenarios.  
* **C\#**: Programación orientada a objetos, aplicando este lenguaje en scripts para Unity.  
* **Motor de físicas**: Simulación de interacciones físicas entre objetos para garantizar coherencia en los movimientos y colisiones.  
* **Eventos y gestión de eventos en Unity**: Implementación de sistemas dinámicos que controlan la interacción entre elementos del juego.

Tras aprender estos conceptos, desarrollamos un escenario utilizando elementos disponibles en la **Asset Store** de Unity y **prefabs** para representar al superviviente y a los enemigos. El diseño del escenario se optimizó para facilitar la navegación de los personajes y maximizar la fluidez de las interacciones.

## Utilización de ML-Agents

En el desarrollo del proyecto, **ML-Agents** de Unity se utilizó para implementar comportamientos avanzados mediante aprendizaje automático. Este enfoque se centró en tareas tácticas y específicas relacionadas con el combate, mientras que el movimiento y la navegación fueron gestionados mediante **NavMesh**, debido a su mayor eficiencia para manejar obstáculos y rutas complejas.

### Entrenamiento de los agentes con ML-Agents

1. **Tareas principales entrenadas**:  
   * **Disparo del superviviente**: El jugador controlado por IA fue entrenado para disparar de manera precisa, utilizando un sistema de recompensas y castigos. Esto permitió optimizar su capacidad para eliminar zombies mientras conservaba munición y evitaba desperdiciar disparos.  
   * **Gestión del combate**: La IA aprendió a identificar y priorizar objetivos cercanos, así como a ajustar su posición para maximizar su eficiencia en combate.  
2. **Sistema de recompensas y castigos**:  
   * **Recompensas**:  
     * Acertar un disparo en un zombie.  
     * Eliminar todos los zombies del entorno.  
   * **Castigos**:  
     * Fallar un disparo.  
     * Colisionar con un zombie.  
     * Colisionar con un obstáculo como paredes.

## Utilización de NavMesh

Para abordar las limitaciones identificadas con **ML-Agents** en la gestión del movimiento, implementamos **NavMesh** como solución principal para la navegación de los enemigos. Esta herramienta de Unity permite generar una malla de navegación que facilita el desplazamiento fluido y eficiente de los personajes en entornos complejos.

[Entrenamiento del movimiento sin los modelos finales](https://drive.google.com/file/d/1XyQU-wJDfFUTAIP0pbdohPz0usQGKuHM/view?usp=drive_link)

Gracias a **NavMesh**, los enemigos pudieron:

* **Seguir objetivos dinámicamente**: Los zombies utilizan `ZombieController` para fijar como objetivo al jugador controlado por IA, actualizando continuamente su destino para mantener la persecución​.  
* **Esquivar obstáculos**: La malla de navegación permite que los enemigos encuentren rutas alternativas alrededor de paredes, muebles y otros elementos del entorno​​.  
* **Spawn y posicionamiento eficiente**: Los zombies se generan en zonas específicas del mapa y se integran automáticamente a la navegación utilizando puntos aleatorios dentro de las áreas de spawn configuradas​.  
* **Adaptación a escenarios variados**: Los enemigos ajustan sus trayectorias para evitar colisiones y desplazarse de forma realista incluso en mapas con diferentes niveles de complejidad.

## Enemigos y superviviente en detalle

### Enemigos

Los enemigos en el proyecto, representados por zombies, se diseñaron para ofrecer un desafío dinámico y adaptativo al jugador IA. Su comportamiento incluye:

* **Movimiento y navegación**: Los zombies utilizan **NavMesh** para navegar de manera eficiente por el entorno, evitando obstáculos y persiguiendo al jugador IA en tiempo real. Las rutas se actualizan continuamente para mantener la persecución, gracias al método `FollowTarget` implementado en `ZombieController.cs`​.  
* **Generación dinámica**: Los zombies se generan en ubicaciones predefinidas dentro del escenario, utilizando áreas de spawn aleatorias configuradas en `WorldBehaviors.cs`. Esto permite mantener la variedad en las ubicaciones y evitar superposiciones entre ellos​.  
* **Comportamiento agresivo**: Los enemigos cuentan con animaciones y estrategias diseñadas para atacar al jugador, aprovechando su capacidad de perseguirlo constantemente. En futuras mejoras, podrían incluirse comportamientos más complejos como emboscadas o tácticas grupales.  
* **Interacciones textuales**: Los zombies también participan en un sistema de diálogo dinámico, generando frases para intimidar o interactuar con el jugador IA. Esto se gestiona mediante prompts en `DialogueControllerZombie.cs`, utilizando un modelo de lenguaje alojado en Hugging Face​.

### Superviviente

El superviviente está representado por el jugador controlado por IA, que toma decisiones estratégicas para sobrevivir en un entorno hostil. Sus capacidades incluyen:

* **Movimiento táctico**: El jugador IA utiliza **ML-Agents** para optimizar su desplazamiento dentro del entorno, ajustándose a las amenazas presentes y evitando colisiones. Aunque el movimiento básico se gestiona con **NavMesh**, las decisiones estratégicas de posicionamiento se entrenaron mediante aprendizaje por refuerzo​.  
* **Defensa y ataque**: El jugador cuenta con una funcionalidad avanzada para disparar a los zombies. Este comportamiento se entrena utilizando un sistema de recompensas y castigos que mejora su precisión y eficiencia en combate. Se gestiona mediante `AgentController.cs` y configuraciones específicas del entrenamiento en ficheros como `BetterConf.yaml`​​.  
* **Respuesta al entorno**: El jugador recopila observaciones sobre su entorno, como la posición de los zombies que escucha (zombies en un rango cercano) y el estado de su arma (munición y recarga). Esto permite decisiones informadas en tiempo real, integrando observaciones físicas y tácticas​.  
* **Adaptabilidad**: Gracias a la modularidad del sistema, el superviviente puede cambiar entre diferentes modelos de redes neuronales para ajustar su comportamiento, según lo implementado en `ModelSwitcher.cs`​.

### Interacciones por texto

El sistema de interacciones textuales introduce una capa narrativa y de inmersión mediante el intercambio de frases generadas dinámicamente entre el superviviente IA y los zombies. Las características principales son:

* **Prompts personalizados**: Ambos personajes cuentan con prompts que guían la generación de sus respuestas. Por ejemplo:  
  * **Zombie:** *"You are a zombie in a fictional setting, there are many like you chasing a man who is fighting for its life. Say a phrase to make him scared."*  
  * **Superviviente:** *"You are one of the last survivors on Earth. Zombies are everywhere, and you are killing as many as you can. Say a phrase that suits the situation."*  
  * Estos prompts se gestionan en `DialogueControllerZombie.cs` y `DialogueControllerSurvivor.cs`​​.  
* **Flujo de diálogo**: El `DialogueManager.cs` coordina el intercambio entre ambos personajes, asegurando que las frases generadas por uno se conviertan en el contexto del otro, creando una interacción fluida y continua​.  
* **Animaciones y visualización**: Las frases se presentan al usuario acompañadas de animaciones específicas para cada personaje, lo que añade una dimensión visual al sistema. Esto se logra utilizando animadores y efectos visuales en los controladores de diálogo​​.  
* **Integración con modelos de lenguaje**: El sistema utiliza la API de [Hugging Face](https://huggingface.co/) para generar respuestas, asegurando que el contenido del diálogo sea variado, dinámico y contextual​​. Los zombies hacen uso de un modelo diferente al del superviviente para asegurar una mayor variedad en las interacciones.  
* **Video del resultado:**  
  * [Sistema\_Dialogos.mp4](https://drive.google.com/file/d/1T2fsfUvP_5zmVUu4B9AndgUnuAczHMxV/view?usp=sharing)

# Problemas encontrados

* ***Adaptación para realidad virtual (VR)***

Uno de los primeros desafíos fue adaptar el proyecto a VR. Esta transición no se realiza de forma automática y requirió la implementación de un script personalizado, algo que no habíamos aprendido en la asignatura de Interfaces Inteligentes. Este proceso fue especialmente complicado, ya que implicaba el uso de técnicas avanzadas como **corrutinas** y otros elementos que difieren significativamente de los métodos previamente utilizados.

Sin embargo, el mayor obstáculo radicó en el entrenamiento de los sistemas de inteligencia artificial en un entorno VR. Para entrenar a los modelos con un jugador humano real, habría sido necesario realizar millones de iteraciones, un proceso inviable en términos de tiempo y recursos. Por esta razón, decidimos descartar la implementación en VR y replantear el enfoque del proyecto.

En lugar de un asistente para un jugador humano, se introdujo un jugador controlado por IA, que permitió realizar el entrenamiento de manera automatizada y eficiente. Este cambio no solo simplificó el desarrollo, sino que también proporcionó una solución escalable y adaptable para futuras mejoras.

* ***Limitaciones con ML-Agents***

Otro problema importante surgió al utilizar **ML-Agents**. Actualmente, esta herramienta presenta ciertas limitaciones debido a su desactualización, lo que complicó su integración en nuestro proyecto. En particular, experimentamos numerosas dificultades durante el entrenamiento de los enemigos para moverse de forma coherente en función del entorno y la posición del jugador.

Otro gran problema que nos encontramos con ML-Agents es que el entrenamiento es muy inestable lo que produce cuelgues inesperados en Unity. Por este problema perdimos el progreso del entrenamiento en reiteradas ocasiones así cómo algún que otro avance en el proyecto.

Aunque conseguimos resultados iniciales para el movimiento, estos fueron inconsistentes y poco fiables. Decidimos explorar alternativas como un NavMesh manejado por ML-Agents que resultó ser la opción que acabamos implementando.

* ***Intento manual de simulación de una Red Neuronal***

Además, intentamos diseñar manualmente una aproximación al funcionamiento de una red neuronal para controlar el comportamiento de los enemigos. Sin embargo, esta solución también presentó problemas significativos, especialmente al combinarse con **NavMesh**. Los enemigos a menudo ignoraban la malla de navegación, moviéndose de forma errática, incluso a través del aire, en lugar de seguir el camino definido en la malla. Debido a estos resultados insatisfactorios, optamos por descartar esta aproximación. 

## Resultados conseguidos

El proyecto **ALLIEN** logró cumplir con los requisitos establecidos, destacando por la implementación de un sistema de enemigos adaptativos (R01) que ajustan su comportamiento en tiempo real, basándose en las acciones del superviviente controlado por IA. Este sistema fue posible gracias a la utilización de **NavMesh** (R04) para garantizar una navegación fluida y coherente en entornos complejos, permitiendo a los enemigos evitar obstáculos y moverse de manera realista. Por su parte, el superviviente demostró capacidades avanzadas como movimiento estratégico, seguimiento de objetivos y defensa mediante disparos, empleando aprendizaje por refuerzo con **ML-Agents** (R02). Además, se implementó un sistema de generación de interacciones textuales dinámicas (R05), que enriqueció la narrativa al permitir diálogos en tiempo real entre los zombies y el jugador IA. Finalmente, se aseguró que la interfaz estuviera completamente en inglés (R07)

La modularidad del sistema (R03) fue garantizada, facilitando futuras expansiones y la integración de nuevos comportamientos y roles. Este diseño modular fue validado mediante pruebas que demostraron la facilidad para añadir nuevas funcionalidades en menos de dos horas. Por último, se elaboró una documentación (R06), accesible en [GitHub](https://github.com/AdayCuestaCorrea/ALLIEN), que detalla los procesos, configuraciones y diseños implementados, asegurando la escalabilidad y el mantenimiento del sistema en el futuro.

En cuanto a los objetivos, el sistema de enemigos adaptativos (OB01) cumplió con éxito, demostrando comportamientos dinámicos y ajustando tácticas en distintos escenarios. El jugador controlado por IA también alcanzó los objetivos propuestos (OB02), operando bajo tres modelos de comportamiento (un modelo que permanece en el centro, un modelo inteligente y uno miedoso), validados con éxito en simulaciones de combate. 

Finalmente, se logró la integración de un sistema de navegación eficiente basado en **NavMesh** (OB03), logrando movimientos fluidos y realistas tanto para los enemigos como para el jugador IA. Asimismo, el sistema de generación textual dinámico (OBJ5) añadió una dimensión narrativa innovadora, asegurando interacciones coherentes y variadas entre los personajes.

# Conclusiones

El proyecto **ALLIEN** ha demostrado la viabilidad y el impacto de integrar inteligencia artificial avanzada en videojuegos, logrando un equilibrio entre sistemas de navegación, combate y narrativa interactiva. A través de herramientas como **NavMesh** y **ML-Agents**, se han alcanzado resultados sólidos, incluyendo enemigos adaptativos, un jugador controlado por IA eficiente y un sistema de interacción textual inmersivo.

A pesar de haber tenido que reenfocar el proyecto, eliminando la implementación en VR debido a las limitaciones logísticas y técnicas para entrenar modelos con un jugador humano, el cambio hacia un jugador controlado por IA permitió automatizar y optimizar los procesos de desarrollo. Este ajuste no solo simplificó el proyecto, sino que también potenció su capacidad para cumplir los objetivos planteados.

El enfoque modular y escalable asegura que **ALLIEN** pueda evolucionar en futuros desarrollos, permitiendo la adición de nuevos comportamientos y funcionalidades. Además, la documentación detallada facilita su mantenimiento y expansión por otros desarrolladores.

En definitiva, el proyecto no solo cumplió los objetivos y requisitos iniciales, sino que también sentó una base sólida para explorar nuevas formas de interacción en videojuegos, combinando tecnología innovadora con experiencias inmersivas y dinámicas.

# Anexos

* [Cronograma en ClickUp](https://sharing.clickup.com/9012487720/g/h/8cjz9h8-332/b5b7f445d8762ea)  
* [ALLIEN: Acta de constitución](https://campusingenieriaytecnologia2425.ull.es/mod/wiki/view.php?wid=8&title=Acta+de+Constituci%C3%B3n&group=1028)  
* [ALLIEN: Proyecto de curso](https://campusingenieriaytecnologia2425.ull.es/mod/wiki/view.php?id=13903)  
* [Repositorio del proyecto](https://github.com/AdayCuestaCorrea/ALLIEN)  
* [Presentación de posibles tecnologías](https://www.canva.com/design/DAGWTF5eTCk/d62COT4WE71H5yYnwon0jw/view?utm_content=DAGWTF5eTCk&utm_campaign=designshare&utm_medium=link2&utm_source=uniquelinks&utlId=h161d03b735)  
* [IA COD](https://as.com/meristation/2022/09/12/noticias/1662999997_375417.html)  
* [IA Halo](https://tecnogaming.com/halo-infinite-se-reinventa-de-la-mano-de-la-inteligencia-artificial/)  
* [Behavior Designer \- Behavior Trees for Everyone](https://assetstore.unity.com/packages/tools/visual-scripting/behavior-designer-behavior-trees-for-everyone-15277?srsltid=AfmBOoq1PlYKdwSrbqYtqHUv8DxjexH7_4uTGWeFvIynJk_WuTu3PD2Z)  
* [AI en Unity](https://gamedevbeginner.com/enemy-ai-in-unity/)  
* [El organismo perfecto de Alien Isolation](https://www.gamedeveloper.com/design/the-perfect-organism-the-ai-of-alien-isolation)  
* [Cómo funciona el Xenomorfo](https://asms.sa.edu.au/app/uploads/The-AI-of-Alien-Isolation-1.pdf)  
* [Cómo se usa la IA en "Alien Isolation"](https://www.linkedin.com/pulse/how-ai-used-within-alien-isolation-daniel-bethel)  
* [Cómo funciona exactamente la IA del Alien](https://www.reddit.com/r/alienisolation/comments/117nyas/how_does_the_xenomorphs_ai_work_exactly_should_i/)  
* [La combinación de dos IA hacen la IA perfecta](https://2game.com/community/how-does-the-xenomorph-work-in-alien-isolation/?srsltid=AfmBOooSlSWdInolCw8Sjsg6qKrGJrG4lEi9isxtehLaNm-690bYX1Ty)
