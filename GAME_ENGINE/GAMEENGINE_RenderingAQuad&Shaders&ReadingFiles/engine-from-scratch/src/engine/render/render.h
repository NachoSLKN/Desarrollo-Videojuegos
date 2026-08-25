#pragma once

#include "../types.h"

/*
    Inicializa el sistema de render:
    ventana, OpenGL, buffers, shaders y textura base.
*/
void render_init(void);

/*
    Comienza un nuevo frame.
*/
void render_begin(void);

/*
    Termina el frame y presenta la imagen en pantalla.
*/
void render_end(void);

/*
    Dibuja un quad.

    pos   -> posición x,y
    size  -> ancho,alto
    color -> RGBA
*/
void render_quad(vec2 pos, vec2 size, vec4 color);