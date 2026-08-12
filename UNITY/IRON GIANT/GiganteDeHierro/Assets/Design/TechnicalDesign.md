# Diseño técnico

## Motor y render

- Unity 6.3 LTS.
- Universal Render Pipeline.
- Plataforma inicial: Windows.
- Input System.
- Cinemachine para cámara.
- TextMeshPro para interfaz.

## Personaje

### Modelo provisional

`IronGiant_Player`

### Componentes previstos

- Animator.
- CharacterController.
- PlayerInput.
- `IronGiantMovement`.
- `IronGiantCombat`.
- `Health`.
- AudioSource.
- CameraTarget.

### Animación

- Avatar humanoide.
- Blend Tree para Idle / Walk / Run.
- Animaciones de ataque como estados separados.
- Locomoción `In Place`.
- Movimiento real controlado por código.
- Root Motion reservado para ataques o movimientos especiales concretos.

## Ciudad

En el prototipo se utilizarán bloques simples. Más adelante:

- pooling para civiles y tráfico;
- LOD en edificios y vehículos;
- física limitada;
- destrucción modular;
- activación por distancia;
- eventos de pánico;
- navegación simplificada.

## Combate

- Hitboxes activadas mediante Animation Events.
- Hurtboxes en jugador y enemigos.
- Daño definido mediante ScriptableObjects.
- Cámara y audio para reforzar impactos.
- Knockback controlado.
- Sistema de agarre separado de los ataques normales.

## IA

### Civiles

Estados previstos:

- Idle.
- Walk.
- Observe.
- Panic.
- Flee.
- Injured.

### Ejército

Estados previstos:

- Patrol.
- Alert.
- Aim.
- Attack.
- Retreat.
- Destroyed.

### Dragón

Estados previstos:

- Idle.
- Chase.
- Ground Attack.
- Fly.
- Fire Breath.
- Stagger.
- Death.

## Rendimiento

- Evitar miles de rigidbodies activos.
- Desactivar IA lejana.
- Usar object pooling.
- Limitar partículas.
- Usar niveles de destrucción prefracturados.
- Perfilar antes de optimizar.
