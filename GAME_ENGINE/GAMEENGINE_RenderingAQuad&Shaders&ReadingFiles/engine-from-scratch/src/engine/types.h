#pragma once

#include <stdint.h>


/* ============================================================
   TIPOS BÁSICOS DEL MOTOR
   ============================================================ */

/* Entero sin signo de 8 bits: 0 - 255 */
typedef uint8_t u8;

/* Entero sin signo de 32 bits */
typedef uint32_t u32;

/* Número decimal de 32 bits */
typedef float f32;


/* ============================================================
   TIPOS MATEMÁTICOS
   ============================================================ */

/* Vector de dos componentes */
typedef f32 vec2[2];

/* Vector de cuatro componentes */
typedef f32 vec4[4];

/* Matriz 4x4 */
typedef f32 mat4x4[4][4];



/* ============================================================
   OPERACIONES CON MATRICES
   ============================================================ */


/*
    Convierte una matriz en la matriz identidad.

    Resultado:

    1 0 0 0
    0 1 0 0
    0 0 1 0
    0 0 0 1
*/
static inline void mat4x4_identity(mat4x4 M)
{
    for (int c = 0; c < 4; c++) {
        for (int r = 0; r < 4; r++) {

            M[c][r] = (c == r) ? 1.0f : 0.0f;

        }
    }
}


/*
    Crea una matriz de traslación.

    x = desplazamiento horizontal
    y = desplazamiento vertical
    z = desplazamiento en profundidad
*/
static inline void mat4x4_translate(
    mat4x4 T,
    f32 x,
    f32 y,
    f32 z
)
{
    mat4x4_identity(T);

    T[3][0] = x;
    T[3][1] = y;
    T[3][2] = z;
}


/*
    Escala una matriz de forma independiente
    en los tres ejes.

    sx = escala X
    sy = escala Y
    sz = escala Z

    R = matriz resultante
    M = matriz original
*/
static inline void mat4x4_scale_aniso(
    mat4x4 R,
    mat4x4 M,
    f32 sx,
    f32 sy,
    f32 sz
)
{
    f32 scale[4] = {
        sx,
        sy,
        sz,
        1.0f
    };

    for (int c = 0; c < 4; c++) {
        for (int r = 0; r < 4; r++) {

            R[c][r] = M[c][r] * scale[c];

        }
    }
}


/*
    Construye una matriz de proyección ortográfica.

    left/right   = límites horizontales
    bottom/top   = límites verticales
    near/far     = límites de profundidad

    Es especialmente útil para renderizado 2D.
*/
static inline void mat4x4_ortho(
    mat4x4 M,
    f32 left,
    f32 right,
    f32 bottom,
    f32 top,
    f32 near,
    f32 far
)
{
    mat4x4_identity(M);

    M[0][0] = 2.0f / (right - left);
    M[1][1] = 2.0f / (top - bottom);
    M[2][2] = -2.0f / (far - near);

    M[3][0] = -(right + left) / (right - left);
    M[3][1] = -(top + bottom) / (top - bottom);
    M[3][2] = -(far + near) / (far - near);
}