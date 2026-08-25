#include <stdbool.h>

#define SDL_MAIN_HANDLED
#include <SDL2/SDL.h>

#include "engine/render/render.h"

int main(int argc, char *argv[]) {

    // Inicializa ventana, OpenGL, shaders, quad, textura, etc.
    render_init();

    bool should_quit = false;

    while (!should_quit) {

        SDL_Event event;

        while (SDL_PollEvent(&event)) {

            switch (event.type) {

                case SDL_QUIT:
                    should_quit = true;
                    break;

                default:
                    break;
            }
        }

        // Comienza el frame y limpia la pantalla.
        render_begin();

        // Posición del quad: centro aproximado de una ventana 800x600.
        vec2 pos = {
            400.0f,
            300.0f
        };

        // Tamaño del quad.
        vec2 size = {
            200.0f,
            200.0f
        };

        // Color verde RGBA.
        vec4 color = {
            0.0f,
            1.0f,
            0.0f,
            1.0f
        };

        // Dibuja el quad.
        render_quad(pos, size, color);

        // Presenta el frame.
        render_end();
    }

    return 0;
}