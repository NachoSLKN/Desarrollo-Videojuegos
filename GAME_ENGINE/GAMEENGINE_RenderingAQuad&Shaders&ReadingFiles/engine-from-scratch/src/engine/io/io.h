/*io.h es el header. Normalmente se declarará que ofrece el módulo al resto del motor.
Por ejemplo: void io_init(); o sea, existe una función por ejemplo llamada io_init que otros
archivos pueden utilizar.*/

#pragma once //Incluye este header solo una vez por compilación, para evitar problemas si es usado desde diferentes archivos.
#include <stdlib.h> //Incluye utilidades estándar de C
#include <stdbool.h> //Permite utilizar bool, true, false..Sin esto C tradicional no tiene el tipo bool.

typedef struct file { //Creamos una estructura que permite agrupar varios datos relacionados dentro de un único tipo.
    char *data; //char *data es un puntero a caracteres. Apunta a los datos que hemos leído del archivo.
    size_t len; //Guarda la longitud del archivo, es decir, cuántos bytes hemos leído. size_t es un tipo entero pensado especificamente para tamaños de memoria y buffers.
    bool is_valid; //is_valid indica si la estructura del archivo ha sido válida.
} File; //Typedef: gracias a typedef struct, nombramos como File a esta estructura. 

File io_file_read(const char *path); //Declara una función llamada io_file_read que leerá un archivo del disco. Recibe la ruta del archivo y devuelve File.
//Es decir, una estructura que contiene data, len, is_valid...

int io_file_write(void *buffer, size_t size, const char *path); //Esta función hará lo contrario, escribir datos en un archivo recibiendo 3 cosas:
// - void * buffer: un puntero genérico a los datos que queremos guardar. void * significa cualquier tipo de dato.
// - size_t size Cantidad de bytes que queremos escribir.
// - const char *path Ruta donde queremos guardar el archivo. 


