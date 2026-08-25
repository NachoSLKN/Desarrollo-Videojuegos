#ifndef RENDER_INTERNAL_H //Include Guard: Evita que este header se procese varias veces en la misma compilación. 
#define RENDER_INTERNAL_H //Include Guard: Evita que este header se procese varias veces en la misma compilación. 


#include <SDL2/SDL.h> //Incluye SDL porque este módulo trabaja con SDL_Window
#include "../types.h" //Incluye tipos personalizados del motor.
#include "render.h" //Incluye el header público del módulo render.

typedef struct render_state_internal { //Creamos una estructura que representa el estado interno del renderer. 

    u32 vao_quad; //Guarda el id de un VAO de OpenGL. Vertex Array Object. 
    u32 vbo_quad; //Vertex Buffer Object.
    u32 ebo_quad; //Element Buffer Object.
    u32 shader_default; //Guarda el buffer de un shader por defecto. 
    u32 texture_color; //Guarda el identificador de una textura.
    mat4x4 projection; //Guarda una matriz de proyección 4x4. 


} Render_State_Internal; //Terminamos la estructura.

SDL_Window *render_init_window(u32 width, u32 height);
void render_init_quad(u32 *vao, u32 *vbo, u32 *ebo); //Inicializa la geometría de un quad, recibe punteros a u32_vao, u32_vbo, u32_ebo...
void render_init_color_texture(u32 *texture);
void render_init_shaders(Render_State_Internal *state); //Inicializa los shaders del renderer. Recibe el estado completo.
u32 render_shader_create(const char *path_vert, const char *path_frag); //Función que crea un shader program completo a partir de dos archivos. 

#endif //Cierra el bloque.