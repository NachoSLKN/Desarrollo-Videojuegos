# Plan de devlogs

## Formato habitual

- Duración: 8–12 minutos.
- Introducción: 15–25 segundos.
- Objetivo del episodio.
- Problemas encontrados.
- Proceso resumido.
- Resultado.
- Próximo objetivo.

## Devlog #1 — El Gigante cobra vida

### Gancho inicial

Montaje rápido:

- modelo en Blender;
- error de Mixamo;
- AccuRIG;
- Gigante caminando;
- problema de normales;
- menú cargando la escena.

### Estructura sugerida

**00:00 — Resultado final**  
Mostrar durante unos segundos al Gigante animándose en Unity.

**00:20 — Presentación del proyecto**  
Explicar la idea: juego de combate gigante, ciudad cartoon, dragón, civiles, ejército y destrucción.

**01:10 — El modelo original**  
Enseñar el modelado propio y comentar que estaba compuesto por muchas piezas independientes.

**02:00 — Primer problema: el rig**  
Mostrar cómo Mixamo reconocía la silueta pero fallaba al generar el rig.

**03:00 — AccuRIG como solución**  
Explicar que se utilizó una herramienta externa y que produjo un esqueleto funcional.

**04:00 — Mixamo finalmente funciona**  
Contar que, después del rig alternativo y nuevas pruebas, Mixamo aceptó al personaje y permitió descargar animaciones.

**05:10 — Primeras animaciones**  
Mostrar caminar, idle, golpes, vuelo o alguna animación graciosa.

**06:20 — Problema pendiente: las normales**  
Enseñar cómo ciertas piezas desaparecen según el ángulo y explicar brevemente el backface culling.

**07:15 — Organización profesional**  
Mostrar carpetas de Unity, documentación Markdown, roadmap, problemas conocidos y lista de animaciones.

**08:10 — Menú principal temporal**  
Mostrar la ilustración de fondo, botones y transición a `CharacterTest`.

**09:10 — Resultado y próximo episodio**  
Cerrar con el Gigante en escena y anunciar movimiento jugable, cámara e Idle/Walk/Run.

### Frase de cierre posible

> El personaje ya tiene esqueleto y animaciones. En el próximo episodio dejará de ser una simple prueba y empezará a convertirse en el jugador.

## Devlog #2 — Primer movimiento jugable

- Input System.
- Character Controller.
- Cámara.
- Idle, caminar y correr.
- Blend Tree.
- Primer paseo por un escenario de pruebas.

## Devlog #3 — Construyendo una ciudad para gigantes

- Escala.
- Blockout.
- Calles.
- Edificios.
- Coches provisionales.
- Primer plano general.
