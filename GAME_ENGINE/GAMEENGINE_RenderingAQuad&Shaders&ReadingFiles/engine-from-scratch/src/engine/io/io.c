//------------Implementación del módulo ENTRADA y SALIDA-----------------//
/*Función que abre un archivo, lo lee por bloques y devuelve sus datos dentro de la estructura FILE*/

#include <stdio.h> //Incluye las funciones estándar de entrada/salida de C. Ej: FILE, fopen()...,ferror()...
#include <stdlib.h> //Incluye utilidades generales de C. Especialmente importante para memoria dinámica. Porque el motor no sabe de antemano cuanto ocupará el archivo que va a leer.
#include <errno.h> //Introduce una variable "errno" que es una variable que contiene información sobre el último error producido por determinadas funciones del sistema/C estándar.
#include "../types.h" // types.h contendrá tipos propios del motor que le autor utiliza en varios módulos.
#include "../util.h" // Igual, busca. 
#include "io.h" //El header propio del módulo. 



// 2MiB, probablemente se puede aumentar la cantidad sin problema alguno.
// Chekeo de la plataforma objetivo.

#define IO_READ_CHUNK_SIZE 2097151 //Constantes del módulo, crea una constante del preprocesador. 2097151 son 2 MiB. Esto es importante ya que el archivo se leerá por bloques de 2MiB, en vez de intentar cargar una cantidad arbitraria de golpe.
//Estamos empezando a trabajar con buffers y memoria dinámica, algo típico en programación de motores.
#define IO_READ_ERROR_GENERAL "Error reading file: %s.errno: %d\n" //Define un mensaje reutilizable de error con dos placeholders.
#define IO_READ_ERROR_MEMORY "Not enough free memory to read file: %s\n" //Otro mensaje de error. Lanzado si se intenta reservar memoria para que el archivo cargue y no se consigue.

// Adaptado de https://stackoverflow.com/a/44894946 gracias a @NominalAnimal. Solución adaptada de @StackOverflow.
File io_file_read(const char *path){ //Inicio de la función que implementa io_file_read() que recibe const char *path.
    File file = { .is_valid = false}; //Crea el FILE que finalmente devolverá la función. 

    FILE *fp = fopen(path, "rb"); //Abrimos el archivo. FILE *p es un puntero a una estructura que representa un archivo abierto. RB significa "Read Binary".
    if(ferror(fp)) { //Pregunta si existe un error asociado al stream del archivo. FP: File Pointer. 
        ERROR_RETURN(file, IO_READ_ERROR_GENERAL, path, errno); 
    }

    char *data = NULL; //Preparación del buffer. Data será el puntero a la memoria donde vas a guardar todo el contenido del archivo.
    char *tmp; //Otro puntero. tmp=temporary. 
    size_t used =0; //Número de bytes que ya están utilizados dentro del buffer.
    size_t size = 0; //Probablemente represente el tamaño total actualmente reservado para el buffer.
    size_t n; //Declara otra variable de tamaño. Probablemente almacenará cuántos bytes se han leído en la última operación. 

    while(true) {

        //Comprueba si queda suficiente memoria en el buffer para leer
        //otro bloque de IO_READ_CHUNK_SIZE bytes + 1 byte extra.

        if (used + IO_READ_CHUNK_SIZE +1 > size) {

            //Aumenta el tamaño del buffer para poder almacenar los datos ya usados + un nuevo bloque + el byte final '\0'
            size = used + IO_READ_CHUNK_SIZE +1;


            //Protección contra overflow:
            //Si al calcular el nuevo tamaño este termina siendo menor o igual que lo ya usado, el archivo es demasiado grande.
            if(size<= used) {

                //Libera la memoria que ya habíamos reservado.
                free(data);

                //Devuelve el File como inválido e informa del error.
                ERROR_RETURN(file, "Input file too large: %s\n", path);
            }


            //Intenta redimensionar el buffer 'data' al nuevo tamaño.
            //Se guarda primero en tmp para no perder data si realloc falla.
            tmp = realloc(data, size);

            //Si realloc devuelve NULL, no se pudo reservar memoria.
            if(!tmp) {

                //Liberamos el buffer original.
                free(data);

                //Informamos de que no hay memoria suficiente.
                ERROR_RETURN(file, IO_READ_ERROR_MEMORY, path);
            }

            //Realloc ha funcionado. Data pasa a apuntar al nuevo bloque de memoria.
            data = tmp;

        }

        //Lee hasta IO_READ_CHUNK_SIZE bytes del archivo.
        //Los escribe a partir de data + used para no sobrescribir los datos que ya habíamos leído.
        //n almacena el número REAL de bytes leídos.
        n = fread(data+used,1,IO_READ_CHUNK_SIZE, fp);
        
        //Si no se ha leído ningún byte más, dejamos de leer.
        if(n==0)
        break;

        //Sumamos los nuevos bytes a la cantidad total utilizada. 
        used += n;

    }

    if(ferror(fp)){
        free(data);
        ERROR_RETURN(file, IO_READ_ERROR_GENERAL, path, errno);
    }

    tmp = realloc(data,used+1);
    if(!tmp){
        free(data);
        ERROR_RETURN(file, IO_READ_ERROR_MEMORY, path);
    }

    data=tmp;
    data[used] = 0;

    file.data = data;
    file.len = used;
    file.is_valid=true;

    return file;




}

int io_file_write(void *buffer, size_t size, const char *path); 