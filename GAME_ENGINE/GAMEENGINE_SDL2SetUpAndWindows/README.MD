# 🪟 Game Engine — SDL2 Setup & Window

## 📌 Etapa 01

Primera versión del proyecto **Game Engine From Scratch**.

Esta carpeta representa un snapshot independiente del motor al finalizar la configuración inicial del entorno gráfico.

El objetivo de esta etapa es conseguir una aplicación mínima en C capaz de crear una ventana, inicializar OpenGL y mantener un bucle principal de ejecución.

---

# 🧩 Tecnologías utilizadas

- C
- SDL2
- OpenGL
- GLAD
- MSVC

---

# ⚙️ Implementación

## SDL2

SDL2 se utiliza para inicializar el subsistema de vídeo y crear la ventana de la aplicación.

```c
SDL_Init(SDL_INIT_VIDEO);
```

La ventana se crea mediante:

```c
SDL_CreateWindow(...)
```

con soporte para OpenGL mediante:

```c
SDL_WINDOW_OPENGL
```

---

## OpenGL Context

Antes de crear el contexto se solicita un contexto **OpenGL Core**.

```c
SDL_GL_SetAttribute(SDL_GL_CONTEXT_PROFILE_MASK,
                    SDL_GL_CONTEXT_PROFILE_CORE);

SDL_GL_SetAttribute(SDL_GL_CONTEXT_MAJOR_VERSION, 3);
SDL_GL_SetAttribute(SDL_GL_CONTEXT_MINOR_VERSION, 3);
```

Posteriormente se crea el contexto:

```c
SDL_GL_CreateContext(window);
```

---

## GLAD

GLAD permite cargar las funciones de OpenGL proporcionadas por el driver.

```c
gladLoadGLLoader(
    (GLADloadproc)SDL_GL_GetProcAddress
);
```

Una vez cargado OpenGL, el programa consulta información de la GPU:

```c
glGetString(GL_VENDOR);
glGetString(GL_RENDERER);
glGetString(GL_VERSION);
```

Resultado obtenido durante el desarrollo:

```text
OpenGL Loaded
Vendor:   NVIDIA Corporation
Renderer: NVIDIA GeForce RTX 3060 Ti/PCIe/SSE2
Version:  4.6.0 NVIDIA 610.74
```

---

# 🔄 Main Loop

Se implementa un bucle principal que mantiene activa la aplicación hasta recibir un evento de cierre.

```c
while (!should_quit) {

    SDL_Event event;

    while (SDL_PollEvent(&event)) {

        if (event.type == SDL_QUIT) {
            should_quit = true;
        }
    }
}
```

Esta estructura constituye la base sobre la que posteriormente se ejecutarán los diferentes sistemas del motor.

---

# 🔨 Compilación

La compilación se realiza mediante MSVC utilizando un archivo:

```text
build.bat
```

El script compila los archivos C y enlaza las librerías necesarias.

Entre las dependencias iniciales se encuentran:

```text
SDL2.lib
SDL2main.lib
freetype.lib
```

---

# 📚 Aprendizaje de esta etapa

Esta primera versión permite trabajar directamente con conceptos que normalmente gestiona automáticamente un motor comercial:

- Inicialización de SDL2.
- Creación manual de una ventana.
- Contexto gráfico.
- Carga de OpenGL.
- Comunicación con el driver gráfico.
- Event loop.
- Compilación manual.
- Linking de librerías.

---

# ✅ Estado

Al finalizar esta etapa el programa es capaz de:

```text
Inicializar SDL2
      ↓
Crear ventana
      ↓
Crear contexto OpenGL
      ↓
Cargar OpenGL con GLAD
      ↓
Consultar la GPU
      ↓
Ejecutar Main Loop
```

Esta versión se conserva de manera independiente para representar el **primer milestone del desarrollo del motor**.