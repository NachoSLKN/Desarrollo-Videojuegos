#pragma once

#include <stdio.h>
#include <stdlib.h>

/*
    ERROR_EXIT

    Imprime un mensaje de error por stderr
    y termina inmediatamente el programa.

    Ejemplo:
    ERROR_EXIT("Could not open file: %s\n", path);
*/
#define ERROR_EXIT(...)                  \
    do {                                 \
        fprintf(stderr, __VA_ARGS__);    \
        exit(1);                         \
    } while (0)


/*
    ERROR_RETURN

    Imprime un mensaje de error y devuelve
    el valor indicado.

    Es útil en funciones que no queremos
    terminar con exit(), por ejemplo io_file_read().

    Ejemplo:
    ERROR_RETURN(file, "Error reading: %s\n", path);
*/
#define ERROR_RETURN(value, ...)          \
    do {                                  \
        fprintf(stderr, __VA_ARGS__);     \
        return value;                     \
    } while (0)