#include <glad/glad.h> // Da acceso a las funciones de OpenGL.
#include <stdio.h>     // Biblioteca estándar de entrada/salida de C.
#include <stdlib.h>    // Necesario para free().

#include "../util.h"
#include "../io/io.h" // Conecta render con el módulo IO.
#include "render_internal.h"

u32 render_shader_create(const char *path_vert, const char *path_frag) {
    // Función que recibe dos rutas:
    // path_vert = archivo del vertex shader.
    // path_frag = archivo del fragment shader.

    int success;      // Variable centinela de comprobación.
    char log[512];    // Almacena mensajes de error producidos por OpenGL.


    // =========================================================
    // VERTEX SHADER
    // =========================================================

    File file_vertex = io_file_read(path_vert); // Cargamos el archivo del vertex shader.

    if (!file_vertex.is_valid) { // Comprueba si falló la lectura.
        ERROR_EXIT(
            "Error reading shader: %s\n",
            path_vert
        );
    }


    // Creamos un objeto OpenGL de tipo Vertex Shader.
    u32 shader_vertex = glCreateShader(GL_VERTEX_SHADER);


    // Entregamos a OpenGL el código fuente que hemos leído del archivo.
    //
    // shader_vertex -> shader al que queremos asignar el código.
    // 1             -> estamos proporcionando una única cadena.
    // &file_vertex.data -> dirección del puntero que contiene el código.
    // NULL          -> no proporcionamos explícitamente la longitud.
    glShaderSource(
        shader_vertex,
        1,
        (const char *const *)&file_vertex.data,
        NULL
    );


    // Compila el Vertex Shader.
    glCompileShader(shader_vertex);


    // Pregunta a OpenGL si la compilación ha sido correcta.
    // El resultado se guarda dentro de success.
    glGetShaderiv(
        shader_vertex,
        GL_COMPILE_STATUS,
        &success
    );


    // Si la compilación falla...
    if (!success) {

        // Recuperamos el mensaje de error producido por OpenGL.
        glGetShaderInfoLog(
            shader_vertex,
            512,
            NULL,
            log
        );

        ERROR_EXIT(
            "Error compiling vertex shader. %s\n",
            log
        );
    }


    // =========================================================
    // FRAGMENT SHADER
    // =========================================================

    // Cargamos el archivo del Fragment Shader.
    File file_fragment = io_file_read(path_frag);


    // Comprobamos que se pudo leer correctamente.
    if (!file_fragment.is_valid) {
        ERROR_EXIT(
            "Error reading shader: %s\n",
            path_frag
        );
    }


    // Creamos un objeto OpenGL de tipo Fragment Shader.
    u32 shader_fragment = glCreateShader(GL_FRAGMENT_SHADER);


    // Entregamos a OpenGL el código fuente del Fragment Shader.
    glShaderSource(
        shader_fragment,
        1,
        (const char *const *)&file_fragment.data,
        NULL
    );


    // Compila el Fragment Shader.
    glCompileShader(shader_fragment);


    // Comprueba si la compilación ha tenido éxito.
    glGetShaderiv(
        shader_fragment,
        GL_COMPILE_STATUS,
        &success
    );


    // Si falla...
    if (!success) {

        // Recuperamos el mensaje de error de compilación.
        glGetShaderInfoLog(
            shader_fragment,
            512,
            NULL,
            log
        );

        ERROR_EXIT(
            "Error compiling fragment shader. %s\n",
            log
        );
    }


    // =========================================================
    // SHADER PROGRAM
    // =========================================================

    // Crea un programa de shaders de OpenGL.
    // Este programa será el contenedor que una:
    //
    // Vertex Shader + Fragment Shader
    //
    u32 shader = glCreateProgram();


    // Conectamos el Vertex Shader al programa.
    glAttachShader(
        shader,
        shader_vertex
    );


    // Conectamos el Fragment Shader al programa.
    glAttachShader(
        shader,
        shader_fragment
    );


    // Enlaza ambos shaders en un único programa ejecutable.
    glLinkProgram(shader);


    // Pregunta a OpenGL si el linking ha funcionado.
    glGetProgramiv(
        shader,
        GL_LINK_STATUS,
        &success
    );


    // Si el linking falla...
    if (!success) {

        // Recuperamos el mensaje de error del programa.
        glGetProgramInfoLog(
            shader,
            512,
            NULL,
            log
        );

        ERROR_EXIT(
            "Error linking shader. %s\n",
            log
        );
    }


    // =========================================================
    // LIBERAR MEMORIA
    // =========================================================

    // io_file_read() reservó memoria dinámicamente para almacenar
    // el contenido del Vertex Shader.
    // Como OpenGL ya tiene el código, podemos liberarla.
    free(file_vertex.data);


    // Lo mismo para el Fragment Shader.
    free(file_fragment.data);


    // Devolvemos el ID del Shader Program ya creado y enlazado.
    return shader;
}