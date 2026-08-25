# 🟩 Game Engine — Rendering a Quad & Shaders

## 📌 Etapa 02

Segunda versión del proyecto **Game Engine From Scratch**.

Esta carpeta contiene un snapshot completo e independiente del motor después de comenzar la implementación del sistema de renderizado.

El objetivo principal de esta etapa es pasar de una simple ventana OpenGL a conseguir **renderizar geometría 2D mediante un renderer propio**.

El resultado final de esta fase es el primer **quad renderizado correctamente en pantalla**.

---

# 🧩 Sistemas añadidos

Durante esta etapa se introduce una estructura mucho más modular:

```text
src/
│
├── glad.c
├── main.c
│
└── engine/
    │
    ├── global.c
    ├── global.h
    ├── types.h
    ├── util.h
    │
    ├── io/
    │   ├── io.c
    │   └── io.h
    │
    └── render/
        ├── render.c
        ├── render.h
        ├── render_init.c
        ├── render_internal.h
        └── render_util.c
```

También se introduce:

```text
shaders/
├── default.vert
└── default.frag
```

---

# 🌐 Global State

Se comienza a separar el estado general del motor del código principal.

El renderer puede almacenar información como:

- Ventana SDL.
- Anchura.
- Altura.
- Estado necesario para el renderizado.

Esto evita mantener toda la lógica dentro de `main.c`.

---

# 📂 Sistema IO

Se implementa un pequeño sistema de entrada/salida para poder leer archivos desde disco.

Uno de sus primeros usos es cargar el código fuente de los shaders:

```text
default.vert
default.frag
```

El sistema trabaja directamente con memoria dinámica en C utilizando operaciones como:

```c
malloc()
realloc()
free()
```

---

# 🎨 Renderer

El sistema gráfico comienza a dividirse en diferentes responsabilidades.

## `render.c`

Contiene la interfaz principal utilizada para trabajar con el renderer.

Entre sus responsabilidades se encuentran:

- Inicialización.
- Inicio de frame.
- Final de frame.
- Renderizado de quads.

---

## `render_init.c`

Contiene la inicialización de los diferentes recursos gráficos.

Entre ellos:

- Ventana.
- OpenGL.
- Quad.
- Shaders.
- Textura base.
- Matriz de proyección.

---

## `render_internal.h`

Define información interna utilizada exclusivamente por el renderer.

El estado interno contiene recursos como:

```c
u32 vao_quad;
u32 vbo_quad;
u32 ebo_quad;
u32 shader_default;
u32 texture_color;
mat4x4 projection;
```

---

## `render_util.c`

Incluye utilidades internas del sistema de renderizado.

En esta etapa se utiliza principalmente para:

- Leer shaders.
- Crear shaders OpenGL.
- Compilarlos.
- Comprobar errores.
- Enlazarlos en un Shader Program.

---

# 🔺 Geometría del Quad

El quad se construye manualmente utilizando cuatro vértices.

Cada vértice almacena:

```text
x
y
z
u
v
```

Es decir:

```text
posición 3D + coordenadas UV
```

Los cuatro vértices se convierten en dos triángulos mediante índices:

```c
0, 1, 3,
1, 2, 3
```

---

# 📦 VAO, VBO y EBO

## VBO

El **Vertex Buffer Object** almacena los datos de los vértices en memoria utilizada por OpenGL.

## EBO

El **Element Buffer Object** almacena los índices utilizados para construir los triángulos.

## VAO

El **Vertex Array Object** almacena la configuración necesaria para interpretar los datos de los vértices.

La relación básica es:

```text
VAO
 │
 ├── VBO → vertices
 │
 └── EBO → indices
```

---

# 🧬 Vertex Attributes

El primer atributo contiene la posición:

```glsl
layout (location = 0) in vec3 a_pos;
```

El segundo contiene las coordenadas UV:

```glsl
layout (location = 1) in vec2 a_uvs;
```

OpenGL necesita conocer explícitamente cómo están organizados estos datos mediante:

```c
glVertexAttribPointer(...)
```

---

# 🖥️ Vertex Shader

El Vertex Shader recibe la posición de cada vértice y sus coordenadas UV.

También utiliza dos matrices:

```glsl
uniform mat4 projection;
uniform mat4 model;
```

La posición final se calcula mediante:

```glsl
gl_Position =
    projection *
    model *
    vec4(a_pos, 1.0);
```

---

# 🎨 Fragment Shader

El Fragment Shader determina el color final de cada fragmento generado.

Durante esta etapa se introduce un uniform de color y el sistema necesario para trabajar con texturas.

Esto permite finalmente obtener un quad coloreado en pantalla.

---

# 📐 Matrices

Se comienza a utilizar una matriz de proyección ortográfica:

```text
projection
```

y una matriz propia de cada objeto:

```text
model
```

La matriz `model` permite aplicar transformaciones como:

- Posición.
- Escala.

Conceptualmente:

```text
Vértice local
     ↓
Model Matrix
     ↓
Posición en el mundo
     ↓
Projection Matrix
     ↓
Pantalla
```

---

# 🟩 Renderizado del Quad

El renderer utiliza finalmente:

```c
glDrawElements(
    GL_TRIANGLES,
    6,
    GL_UNSIGNED_INT,
    NULL
);
```

para pedir a OpenGL que dibuje los dos triángulos que forman el quad.

El pipeline completo queda aproximadamente:

```text
C / CPU
   │
   ├── Vertices
   ├── Indices
   ├── Model Matrix
   ├── Projection Matrix
   └── Color
        │
        ▼
      OpenGL
        │
        ▼
       VAO
      /   \
    VBO   EBO
        │
        ▼
  Vertex Shader
        │
        ▼
 Rasterization
        │
        ▼
 Fragment Shader
        │
        ▼
       GPU
        │
        ▼
    🟩 QUAD
```

---

# 🔧 Reconstrucciones y adaptaciones

Durante esta etapa se detectó que se había omitido accidentalmente un vídeo intermedio del tutorial.

Como consecuencia, varios archivos que aparecían utilizados posteriormente todavía no existían en la copia local del proyecto.

Antes de detectar el problema se reconstruyeron o completaron diferentes componentes necesarios para continuar.

Entre ellos:

- `global.c`
- `global.h`
- `types.h`
- `util.h`
- `render.c`
- `render.h`
- `render_init.c`
- `render_internal.h`
- `render_util.c`
- Sistema IO.
- Shaders GLSL.

También fue necesario realizar diferentes correcciones durante la integración:

- Corrección de includes y rutas relativas.
- Corrección del sistema de compilación.
- Inclusión de todos los `.c` necesarios en `build.bat`.
- Corrección de errores de sintaxis procedentes de la transcripción.
- Correcciones en la creación de shaders.
- Correcciones en la configuración del quad.
- Configuración completa de VAO/VBO/EBO.
- Configuración de los Vertex Attributes.
- Configuración de uniforms.
- Depuración del sistema de color/textura.
- Adaptaciones para compilar correctamente mediante MSVC.

El vídeo omitido se revisará posteriormente para comparar la implementación original del tutorial con las soluciones reconstruidas durante esta fase.

---

# 🧠 Aprendizaje de esta etapa

Esta fase supone el primer contacto importante con el pipeline gráfico a bajo nivel.

Se han trabajado conceptos como:

- Organización modular de código C.
- Headers públicos e internos.
- Estado global.
- Gestión manual de memoria.
- Lectura de archivos.
- Buffers de GPU.
- VAO.
- VBO.
- EBO.
- Vertex Attributes.
- Shaders GLSL.
- Compilación de shaders.
- Linking.
- Uniforms.
- Matrices 4x4.
- Proyección ortográfica.
- Coordenadas UV.
- Texturas.
- Draw calls.

---

# 🐛 Problemas encontrados

Durante el desarrollo aparecieron diferentes errores de compilación, linking y renderizado.

Entre ellos:

```text
identifier not declared
unresolved external symbol
include path errors
shader compilation problems
incorrect relative paths
missing source files
```

Uno de los problemas finales permitió comprobar que la geometría estaba siendo renderizada correctamente pero el quad aparecía negro.

La depuración del Fragment Shader y del sistema de textura/color permitió finalmente obtener el resultado esperado.

---

# ✅ Resultado

El motor consigue finalmente renderizar su primera primitiva gráfica:

**un quad verde sobre la ventana OpenGL.**

```text
Window             ✅
OpenGL             ✅
GLAD               ✅
File IO            ✅
Renderer           ✅
VAO                 ✅
VBO                 ✅
EBO                 ✅
Vertex Shader       ✅
Fragment Shader     ✅
Projection Matrix   ✅
Model Matrix        ✅
Uniform Color       ✅
Quad                🟩
```

Este snapshot se conserva independientemente para representar el **segundo milestone del desarrollo del motor**.