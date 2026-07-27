# Configuración de escenas

## Menu

### Objetivo

Pantalla principal temporal que permita comenzar la partida y acceder en el futuro a opciones, créditos y salida.

### Jerarquía prevista

```text
Menu
├── Main Camera
├── Canvas
│   ├── Background
│   ├── Title
│   ├── MenuPanel
│   │   ├── Button_Play
│   │   ├── Button_Options
│   │   ├── Button_Credits
│   │   └── Button_Quit
│   └── FadePanel
├── EventSystem
└── MenuManager
```

### Canvas

- Render Mode: Screen Space - Overlay.
- UI Scale Mode: Scale With Screen Size.
- Reference Resolution: 1920 × 1080.
- Match: 0.5.

### Funciones

- `PlayGame()` carga `CharacterTest`.
- `Options()` abre un panel provisional.
- `Credits()` abre un panel provisional.
- `QuitGame()` cierra el juego.

---

## CharacterTest

### Objetivo

Laboratorio para probar modelo, rig, animaciones, cámara, control y combate sin romper la escena de juego.

### Jerarquía prevista

```text
CharacterTest
├── Main Camera
├── Directional Light
├── Environment
│   └── Ground
├── IronGiant_Player
│   ├── Mesh
│   ├── Armature
│   └── CameraTarget
└── TestManager
```

### Normas

- Solo un Gigante completo en la escena.
- Los demás FBX se usan como fuentes de animación.
- No añadir assets de ciudad definitivos aquí.
- Mantener esta escena como sandbox permanente.

---

## CityPrototype

### Objetivo

Primera escena jugable con una ciudad hecha mediante bloques simples.

### Contenido inicial

- Plano o terreno.
- Calles anchas.
- Cubos como edificios.
- Gigante.
- Cámara.
- Un enemigo provisional.
- Algunos props a escala.
