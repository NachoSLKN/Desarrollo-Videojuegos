@echo off

REM --- Modulo Render ---
set RENDER=src\engine\render\render.c ^
           src\engine\render\render_init.c ^
           src\engine\render\render_util.c

REM --- Modulo IO ---
set IO=src\engine\io\io.c

REM --- Todos los archivos C ---
set FILES=src\glad.c ^
          src\main.c ^
          src\engine\global.c ^
          %RENDER% ^
          %IO%

REM --- Librerias ---
set LIBS="E:\NACHO\ESTUDIO\PROYECTOS\PROGRAMACION\GAME ENGINE\GAMEENGINE_GAME\lib\SDL2main.lib" ^
         "E:\NACHO\ESTUDIO\PROYECTOS\PROGRAMACION\GAME ENGINE\GAMEENGINE_GAME\lib\SDL2.lib" ^
         "E:\NACHO\ESTUDIO\PROYECTOS\PROGRAMACION\GAME ENGINE\GAMEENGINE_GAME\lib\freetype.lib"

REM --- Compilacion ---
CL /Zi /FS /I "E:\NACHO\ESTUDIO\PROYECTOS\PROGRAMACION\GAME ENGINE\GAMEENGINE_GAME\include" %FILES% /link %LIBS% /OUT:mygame.exe