# The Iron Giant — GDD adaptado desde Grid Tactics

Esta versión conserva el **orden de las grandes secciones y numerosos epígrafes del documento de Grid Tactics**, incluyendo Mecánicas, Elementos de juego, Reglas de juego, Recursos del documento y Niveles de juego. Cada epígrafe contiene información propia del prototipo, o una explicación honesta de por qué un mecanismo de estrategia por turnos no encaja en nuestra aventura 3D.

No se copia la redacción, el código o la identidad visual de Grid Studio: se utiliza el índice y enfoque documental como referencia.

## Instalar en el React existente

Extrae las carpetas `src/` y `public/` del ZIP sobre `E:\NACHO\GIT\Desarrollo-Videojuegos\UNITY\IRON GIANT\Web` (sobrescribiendo los archivos anteriores). Mantén `package.json`, `tsconfig.json` y configuración Vite de tu instalación. Ejecuta `npm run dev`.

## Arquitectura de los archivos

- `src/gddPrevious.ts`: contenido sustantivo de nuestro GDD anterior, conservado sin pérdida.
- `src/gdd.ts`: índice basado en el orden de Grid Tactics; remapea las secciones existentes y añade los epígrafes faltantes.
- `src/App.tsx`: web navegable con índice por capítulos y búsqueda.
- `src/index.css`: estilos y paleta previamente establecidos.

## Entrega académica

El GDD es documentación complementaria. Los cuatro criterios evaluables del 5 de noviembre son personaje, escenario, imagen de marketing e interfaz. El apartado final los documenta expresamente.
