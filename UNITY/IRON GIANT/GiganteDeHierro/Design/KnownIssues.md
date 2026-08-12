# Problemas conocidos

## IG-001 — Normales incorrectas en varias piezas

**Estado:** Pendiente  
**Prioridad:** Alta  
**Detectado:** Primera sesión de desarrollo

### Síntoma

Algunas partes del Gigante desaparecen según el ángulo desde el que se miran en Unity. Al configurar el material para renderizar ambas caras, el problema deja de verse.

### Diagnóstico

Backface culling causado por normales invertidas, transformaciones negativas o piezas duplicadas mediante espejo.

### Pruebas realizadas

- Activación de `Face Orientation` en Blender.
- Recalcular normales hacia fuera.
- Voltear algunas caras.
- Probar `Render Face: Both` en Unity.
- Reimportar distintos FBX.
- Probar versiones exportadas por AccuRIG y Mixamo.

### Resultado

El problema sigue presente en varias versiones, por lo que debe corregirse en el archivo fuente de Blender.

### Próximos pasos

1. Guardar copia del `.blend`.
2. Aplicar `Rotation & Scale`.
3. Revisar piezas con escala negativa.
4. Aplicar modificadores Mirror.
5. Recalcular normales.
6. Exportar una malla limpia.
7. Transferir rig y pesos si fuera necesario.

---

## IG-002 — Mixamo no aceptaba inicialmente el modelo

**Estado:** Resuelto parcialmente

### Síntoma

Mixamo mostraba errores durante el autorig:

- `Unknown error while generating motion`.
- `Please place all markers on the character`.

### Solución aplicada

Se utilizó AccuRIG como herramienta externa para generar un esqueleto funcional. Más tarde, una nueva subida a Mixamo sí fue aceptada y permitió aplicar animaciones.

### Resultado

Actualmente existen variantes con rig de AccuRIG y Mixamo. Debe elegirse un avatar base definitivo y mantener las demás como respaldo.

---

## IG-003 — Exceso de FBX con malla duplicada

**Estado:** En proceso

Todas las animaciones se descargaron inicialmente con `Skin`, por lo que cada FBX contiene otra copia completa del personaje.

### Solución prevista

- Mantener un único modelo base en escena.
- Usar los demás FBX solo como fuente de clips.
- Extraer o reutilizar los clips mediante avatar humanoide.
- Descargar futuras animaciones `Without Skin` cuando sea posible.
