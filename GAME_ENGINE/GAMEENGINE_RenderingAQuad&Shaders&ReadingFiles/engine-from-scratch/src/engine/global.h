#pragma once

#include <SDL2/SDL.h>

#include "types.h"

/*
    Estado global del sistema de render.
    Guarda información que debe poder consultarse
    desde diferentes módulos del motor.
*/
typedef struct render_global {

    /* Anchura de la ventana */
    u32 width;

    /* Altura de la ventana */
    u32 height;

    /* Puntero a la ventana creada por SDL */
    SDL_Window *window;

} Render_Global;


/*
    Estado global completo del motor.

    De momento solo contiene el sistema de render,
    pero posteriormente podría contener audio,
    input, tiempo, etc.
*/
typedef struct global_state {

    Render_Global render;

} Global;


/*
    Declaración de la variable global.

    'extern' significa:
    la variable existe, pero se crea realmente
    en otro archivo (.c).
*/
extern Global global;