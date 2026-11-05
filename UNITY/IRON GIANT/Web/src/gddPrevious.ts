export type Section = {
  id:string
  title:string
  status?:'Definido'|'Prototipo'|'En estudio'|'Pendiente'
  paragraphs?:string[]
  bullets?:string[]
  note?:string
  table?:{columns:string[]; rows:string[][]}
}

export type Chapter = {id:string; title:string; sections:Section[]}

export const chapters:Chapter[] = [
 {id:'introduccion',title:'01 · Introducción',sections:[
  {id:'sobre',title:'Sobre este documento',paragraphs:[
   'Este documento funciona simultáneamente como GDD y memoria de desarrollo.',
   'Las secciones aún no desarrolladas se mantienen para poder ampliar el documento durante el proyecto.'
  ]},
  {id:'origen',title:'Origen del proyecto y reinterpretación',paragraphs:[
   'El modelo fue realizado por el autor del proyecto.',
   'El proyecto actual reutiliza este material para crear una nueva propuesta visual y jugable en Unity.'
  ]},
  {id:'pitch',title:'Concepto y pitch',paragraphs:['Juego 3D en tercera persona.']},
  {id:'publico',title:'Público objetivo',paragraphs:['Por definir.']},
  {id:'objetivos',title:'Objetivo del producto',paragraphs:['Crear una demo jugable y visualmente coherente.']}
 ]},

 {id:'produccion',title:'02 · Producción y técnica',sections:[
  {id:'tecnica',title:'Especificaciones técnicas',bullets:[
   'Unity 6.3 LTS.',
   'C#.',
   'Universal Render Pipeline (URP).',
   'Blender.',
   'Krita.',
   'Git y GitHub.',
   'React, TypeScript y Vite para la web.'
  ]},
  {id:'arquitectura',title:'Arquitectura del proyecto',paragraphs:[
   'El código se seguirá reorganizando para mantener los sistemas escalables y aplicar estados y patrones de diseño cuando sean necesarios.'
  ]},
  {id:'plan',title:'Planificación del desarrollo',paragraphs:[
   'La fase actual está centrada en la dirección artística y en documentar el trabajo realizado. El resto del proyecto se ampliará en siguientes fases.'
  ]},
  {id:'pipeline',title:'Pipeline y control de versiones',paragraphs:[
   'Blender y Krita se utilizan para crear y editar recursos; Unity para integrarlos y probarlos; GitHub para control de versiones y documentación.'
  ]},
  {id:'equipo',title:'Equipo y responsabilidades',paragraphs:['Por definir.']}
 ]},

 {id:'arte',title:'03 · Dirección artística',sections:[
  {id:'vision',title:'Estilo visual y referencias',paragraphs:[
   'La dirección visual combina 3D, cel shading, contornos y recursos gráficos inspirados en el cómic y el pop-art.'
  ]},
  {id:'paleta',title:'Paleta cromática'},
  {id:'materiales',title:'Materiales, texturas y sombreado',paragraphs:[
   'El sombreado actual utiliza Flat Kit: Toon Shading and Water.',
   'Se ha aplicado cel shading y contorno al Gigante y a elementos de prueba de la escena.'
  ]},
  {id:'tipografia',title:'Tipografía y rotulación',paragraphs:[
   'Se ha creado una fuente propia basada en la tipografía original de la película para utilizarla en el proyecto.',
   'La fuente ya se utiliza en los títulos de esta web y se seguirá aplicando a los elementos gráficos donde mantenga una buena legibilidad.'
  ]},
  {id:'animacion',title:'Animación y movimiento',paragraphs:[
   'El Gigante dispone de movimiento y vuelo. Las animaciones todavía necesitan trabajo y se seguirán ajustando.'
  ]},
  {id:'vfx',title:'Efectos visuales',paragraphs:[
   'Se han probado rayos oculares y feedback gráfico de cómic dentro del gameplay.',
   'Al destruir un bloque puede aparecer brevemente un gráfico CRASH! con desvanecimiento. Se plantean variantes como BOOM! y efectos de impulso para el vuelo.'
  ]}
 ]},

 {id:'personajes',title:'04 · Personajes',sections:[
  {id:'gigante',title:'El Gigante de Hierro',paragraphs:[
   'Modelo 3D propio reutilizado para este proyecto.',
   'El personaje se documenta mediante vistas frontal, lateral, tres cuartos frontal, tres cuartos trasero y trasera.'
  ]},
  {id:'hogarth',title:'Hogarth',paragraphs:[
   'Hogarth está integrado en Unity y se utiliza también para comprobar la diferencia de escala respecto al Gigante.'
  ]},
  {id:'dean',title:'Dean y NPC',paragraphs:['Por hacer.']},
  {id:'enemigos',title:'Soldados, fauna y dragón',paragraphs:['Por hacer.']}
 ]},

 {id:'escenarios',title:'05 · Escenarios',sections:[
  {id:'escenarios-general',title:'Escenario',paragraphs:[
   'Se ha realizado una primera propuesta de escenario para situar al Gigante en la chatarrería de Dean.'
  ]},
  {id:'chatarreria',title:'Chatarrería de Dean',paragraphs:['Por hacer.']},
  {id:'bosque',title:'Bosque y secuencia de introducción',paragraphs:['Por hacer.']},
  {id:'ciudad',title:'Ciudad low-poly',paragraphs:[
   'Existe una escena urbana de prueba utilizada para integrar personajes, edificios, cel shading y destrucción de bloques.'
  ]},
  {id:'niveles',title:'Progresión de niveles',paragraphs:['Por definir.']},
  {id:'sintesis-escena',title:'Composición y documentación del escenario',paragraphs:[
   'Se seguirán incorporando capturas de las escenas conforme avance el trabajo artístico.'
  ]}
 ]},

 {id:'interfaz',title:'06 · Interfaz y navegación',sections:[
  {id:'hud',title:'HUD de juego',paragraphs:[
   'Existe un HUD integrado en Unity con información de personaje, salud, recursos, lock-on y controles de prueba.'
  ]},
  {id:'menus',title:'Title Screen, menú principal y carga',paragraphs:[
   'Existen pruebas de pantalla de título, menú y carga. Los cómics del proyecto se utilizarán también como recurso visual en las pantallas de carga.'
  ]},
  {id:'navegacion',title:'Diagrama de navegación',paragraphs:[
   'Title Screen → Menú → Carga → Partida.'
  ]},
  {id:'accesibilidad',title:'Accesibilidad y localización',paragraphs:['Por definir.']}
 ]},

 {id:'mecanicas',title:'07 · Mecánicas de juego',sections:[
  {id:'papel',title:'Papel del jugador',paragraphs:['El jugador controla al Gigante y a Hogarth.']},
  {id:'controles',title:'Controles',paragraphs:[
   'Existen controles de movimiento y diferentes acciones del Gigante.'
  ]},
  {id:'bucle',title:'Ciclo de juego',paragraphs:['Por definir.']},
  {id:'tornillos',title:'Tornillos y recursos',paragraphs:[
   'Los tornillos funcionan como recurso del proyecto y ya existe un contador integrado en el HUD.',
   'Los bloques destructibles pueden añadir tornillos al romperse.'
  ]},
  {id:'mejoras',title:'Sistema de mejoras',paragraphs:[
   'Se han preparado variantes visuales del Gigante con diferentes piezas. El sistema jugable de mejoras todavía está por definir.'
  ]},
  {id:'escalada',title:'Escalada sobre el Gigante',paragraphs:[
   'Por hacer. La idea es que Hogarth pueda utilizar el cuerpo del Gigante para alcanzar determinadas zonas.'
  ]},
  {id:'tren',title:'Tren y Quick Time Event',paragraphs:['Por hacer.']},
  {id:'reconstruccion',title:'Derrota y reconstrucción',paragraphs:['Por hacer.']},
  {id:'combate',title:'Combate y jefe de zona',paragraphs:[
   'Actualmente existen pruebas de ataque mediante rayos oculares. El resto del combate está por desarrollar.'
  ]}
 ]},

 {id:'sistemas',title:'08 · Sistemas y elementos',sections:[
  {id:'entidades',title:'Entidades y componentes',paragraphs:[
   'Actualmente se trabaja con personajes jugables, objetos destructibles y elementos interactivos básicos.'
  ]},
  {id:'reglas',title:'Reglas del juego',paragraphs:['Por definir.']},
  {id:'victoria',title:'Condiciones de victoria y derrota',paragraphs:['Por definir.']},
  {id:'ia',title:'IA y comportamiento',paragraphs:['Por hacer.']},
  {id:'expresividad',title:'Expresividad y feedback',paragraphs:[
   'Se están probando animación, efectos, UI y recursos gráficos de cómic como feedback de las acciones.'
  ]},
  {id:'guardado',title:'Guardado y persistencia',paragraphs:['Por hacer.']},
  {id:'audio',title:'Audio y música',paragraphs:['Por hacer.']},
  {id:'distribucion',title:'Publicación y distribución',paragraphs:['Por definir.']}
 ]},

 {id:'entrega',title:'09 · Arte Final: entrega',sections:[
  {id:'rubrica',title:'Primera muestra de avance · 5 noviembre 2026',paragraphs:[
   'La primera fase reúne el trabajo realizado sobre personaje, escenario, imagen de marketing e interfaz.'
  ]},
  {id:'splash',title:'Splash art',paragraphs:[
   'El cómic es un elemento presente en la película y se utiliza como punto de partida para la identidad gráfica del proyecto.'
  ]},
  {id:'entregables',title:'Índice de archivos y presentación',paragraphs:['Se completará con el material de la entrega.']},
  {id:'decisiones',title:'Decisiones y descartes justificados',paragraphs:[
   'En esta fase se ha priorizado definir y probar la dirección artística antes de ampliar el alcance jugable.'
  ]},
  {id:'evidencias',title:'Evidencias del proceso artístico',paragraphs:[
   'Las capturas y piezas de esta web documentan el trabajo realizado durante el desarrollo.'
  ]}
 ]},

 {id:'anexos',title:'10 · Anexos y referencias',sections:[
  {id:'referencias',title:'Referencias principales',bullets:['The Iron Giant.','Cómic clásico y pop-art.']},
  {id:'glosario',title:'Glosario',table:{
   columns:['Término','Significado'],
   rows:[
    ['GDD','Game Design Document'],
    ['HUD','Head-Up Display'],
    ['Turnaround','Conjunto de vistas utilizado para documentar un personaje'],
    ['Cel shading','Sombreado estilizado con transiciones marcadas entre luces y sombras']
   ]
  }},
  {id:'historial',title:'Historial de decisiones y versiones',table:{
   columns:['Fecha','Cambio'],
   rows:[
    ['29/09/2026','Propuesta inicial'],
    ['01/11/2026','Primera estructura extensa del GDD'],
    ['05/11/2026','Reescritura del GDD como memoria de desarrollo']
   ]
  }},
  {id:'pendientes',title:'Preguntas abiertas',paragraphs:[
   'Se completará cuando aparezcan decisiones pendientes que necesiten quedar registradas.'
  ]}
 ]}
]

export const palette=[
 ['Carbón','#1E1E1E'],
 ['Gris oscuro','#2F3236'],
 ['Gris UI','#4A4E54'],
 ['Acero','#70767D'],
 ['Blanco roto','#F2F2F0'],
 ['Gris claro','#C9CDD1'],
 ['Naranja pastel','#E79A62'],
 ['Naranja luz','#F2B47C'],
 ['Rojo pastel','#D66A6A'],
 ['Azul energía','#7FA9C7'],
 ['Burdeos cómic','#8F1D2C'],
 ['Rojo tinta','#C92F2A'],
 ['Naranja quemado','#E06A2A'],
 ['Mostaza','#D99B38'],
 ['Crema envejecido','#E6D1A7'],
 ['Beige papel','#B7A58A'],
 ['Azul grisáceo','#596D73'],
 ['Azul petróleo','#31566B'],
 ['Azul noche','#26323A'],
 ['Negro tinta','#201F1D']
]
