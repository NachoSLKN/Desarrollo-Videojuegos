import { chapters as sourceChapters, palette, type Chapter, type Section } from './gddPrevious'
export type {Chapter, Section} from './gddPrevious'
export {palette}
const source = sourceChapters.flatMap(ch => ch.sections)
function original(id:string, key:string, title:string):Section {
  const found = source.find(s=>s.id===id)
  if (!found) throw new Error('Missing source section: '+id)
  return {...found,id:key,title}
}
function additional(id:string,title:string,body:string):Section {
 return {id,title,status:'En estudio',paragraphs:[body]}
}
export const chapters:Chapter[] = [
 {id:"grid-0",title:"Sobre el juego",sections:[
  original("pitch","sobre-juego","Sobre el juego"),
 ]},
 {id:"grid-1",title:"Documento de diseño del juego",sections:[
  original("sobre","sobre","Introducción / Sobre este documento"),
  original("origen","origen","Origen del proyecto y reinterpretación"),
  original("pitch","grid-tactics","The Iron Giant — Descripción general"),
  original("publico","publico","Público objetivo"),
  original("objetivos","objetivo-producto","Objetivo del producto"),
  additional("mvp","MVP — Producto mínimo viable","Mínimo para probar una partida completa: controlar a ambos personajes con estabilidad, alternar entre ellos, recoger tornillos (incluidos algunos exclusivos de Hogarth), mostrar el progreso en el HUD y terminar la zona según una condición verificable. Hoy el controlador y el cambio de personaje siguen requiriendo pulido."),
  additional("mmp","MMP — Producto mínimo comercializable","Visión posterior al MVP, no compromiso de la entrega académica: nivel completo con dirección artística consistente, mejoras, soldados funcionales, audio propio/licenciado, accesibilidad básica y menú de opciones. La comercialización real dependería de resolver los derechos de la IP."),
  additional("map","MAP — Producto mínimo asombroso","Visión a largo plazo: escalada de Hogarth por el Gigante, eventos de tren, reconstrucción por piezas y jefe dragón, además de más zonas, comportamiento de NPC y animación pulida. No está implementado."),
  original("tecnica","tecnica","Especificaciones técnicas"),
  original("arquitectura","arquitectura","Arquitectura y patrones de diseño"),
  original("plan","plan","Planificación del desarrollo"),
  original("pipeline","pipeline","Pipeline de arte y control de versiones"),
  original("vision","arte","Arte / Estilo visual"),
  original("paleta","paleta","Paleta de colores"),
  original("materiales","materiales","Materiales, sombreado y Grease Pencil"),
  original("tipografia","tipografia","Tipografía"),
  original("animacion","animacion","Animación"),
  original("hud","interfaces","Interfaces"),
  original("menus","menus","Interfaces — Pantalla de título y carga"),
  original("vision","esteticas","Estéticas y contexto de juego"),
  original("papel","papel","Papel del usuario"),
  original("navegacion","navegacion","Diagrama de navegación de usuario"),
 ]},
 {id:"grid-2",title:"Mecánicas",sections:[
  original("controles","controles","Controles"),
  original("bucle","bucle","Ciclo de juego"),
  additional("fatiga","Fatiga","Descartada para el MVP. Grid Tactics usa fatiga como núcleo de su sistema por turnos; El Gigante de Hierro es una aventura de acción en tiempo real. Si necesitamos limitar habilidades, estudiaremos energía o enfriamientos ligados a vuelo y combate."),
  additional("movimiento-resistencia","Movimiento por resistencia","Adaptación conceptual: el Gigante puede atravesar obstáculos por fuerza o tamaño; Hogarth accede a pasadizos y zonas estrechas. No adoptamos una cuadrícula con costes numéricos por casilla porque no encaja con una aventura 3D de movimiento libre."),
  additional("orientacion","Orientación de unidad","Adaptación: dirección de la mirada, cámara, selección de objetivo (Lock-On) y orientación de los personajes. Ya existe un prototipo de fijación y AimPoint. No hay una mecánica de orientación por casillas."),
  additional("niebla","Niebla de guerra","Descartada actualmente: es una técnica vinculada a información oculta en estrategia táctica. Nuestro juego usa visibilidad 3D natural, cámara y percepción de enemigos. Podríamos añadir niebla ambiental como recurso artístico, que no es lo mismo."),
  original("tornillos","tornillos","Recolección de tornillos"),
  original("mejoras","mejoras","Mejoras en la chatarrería"),
  original("escalada","escalada","Escalada de Hogarth"),
  original("tren","tren","Tren y evento rápido"),
  original("reconstruccion","reconstruccion","Desmontaje y reconstrucción"),
 ]},
 {id:"grid-3",title:"Elementos de juego",sections:[
  original("entidades","elementos","Elementos de juego y sus relaciones"),
  additional("partida","Partida","Una partida es la sesión activa de una zona: escenario, personajes, tornillos disponibles, tornillos conseguidos, temporizador, amenazas, mejoras aplicables y condición de finalización. Todavía falta implementar este gestor de partida."),
  original("niveles","nivel","Nivel"),
  additional("equipo","Equipo","En lugar de equipos tácticos por turnos, distinguimos protagonistas controlables y actores no controlables: soldados hostiles, fauna y NPC neutrales. No hay multijugador previsto para el alcance inicial."),
  additional("jugador","Jugador","Una sola persona alterna entre el Gigante y Hogarth mediante Tab. El personaje no seleccionado debe desactivar sus acciones de entrada o adoptar un comportamiento autónomo claramente definido."),
  original("entidades","entidad","Entidad"),
  additional("unidad","Unidad / personaje","Las unidades de nuestro juego son personajes del escenario (Gigante, Hogarth, soldados, ciervos y potencialmente Dean). Usamos el término personaje o NPC en Unity, no una unidad táctica sobre casillas."),
  additional("propiedad","Propiedad / objeto interactivo","Adaptamos las propiedades de Grid Tactics a objetos del mundo: tornillos, puertas, piezas, elementos de taller, obstáculos y partes del escenario. Cada objeto expone solo las interacciones que necesita."),
  original("escenarios-general","mapa","Mapa / escenario"),
  additional("tile","Tile / módulo de entorno","No utilizaremos un tablero de tiles para el juego principal. Sí podremos emplear piezas modulares para edificios low-poly, vallas, suelo y chatarra. El detalle de módulos y prefabs se diseñará en Blender/Unity."),
  original("gigante","gigante","Personaje — El Gigante de Hierro"),
  original("hogarth","hogarth","Personaje — Hogarth"),
  original("chatarreria","chatarreria","Escenario — Chatarrería de Dean"),
  original("bosque","bosque","Escenario — Bosque"),
  original("ciudad","ciudad","Escenario — Ciudad low-poly"),
 ]},
 {id:"grid-4",title:"Reglas de juego",sections:[
  original("reglas","reglas","Reglas de juego"),
  original("victoria","victoria","Condición de victoria"),
  original("combate","combate","Combate y jefe de zona"),
  original("ia","ia","IA de juego"),
  original("expresividad","expresividad","Expresividad de entidad"),
  original("guardado","guardado","Guardado y persistencia"),
  original("audio","audio","Audio y música"),
  original("distribucion","distribucion","Monetización y distribución"),
 ]},
 {id:"grid-5",title:"Recursos del documento",sections:[
  original("referencias","referencias","Referencias y recursos del documento"),
  original("glosario","glosario","Glosario"),
  original("historial","historial","Versionado del GDD"),
  original("pendientes","pendientes","Preguntas abiertas"),
 ]},
 {id:"grid-6",title:"Niveles de juego",sections:[
  original("niveles","niveles","Introducción / Niveles de juego"),
  additional("set1","Set 1 — Zona prototipo","Documento de diseño de niveles inspirado en el desglose por sets de Grid Tactics. Objetivo inicial: una zona pequeña, jugable y mensurable, sin necesidad de ciudad completa."),
  additional("nivel-1-1","1.1 — Primer contacto","Escenario de bosque: introducir movimiento del Gigante y Hogarth y cambio de personaje. Sin soldados ni cuenta atrás durante la enseñanza inicial. Diseño propuesto, no implementado."),
  additional("anotaciones-1-1","Anotaciones 1.1","Validar primero controladores, cámara y diferencia de escala. Deben poderse completar los objetivos del tutorial sin combate ni escalada compleja."),
  additional("nivel-1-2","1.2 — Recolección","Introducir tornillos, zonas exclusivas de Hogarth y contador. Añadir una cuenta atrás solo después de comprobar la claridad del objetivo."),
  additional("anotaciones-1-2","Anotaciones 1.2","La ruta debe ofrecer varias oportunidades visibles de cambiar de personaje. Evitar frustración por temporizador antes de equilibrar distancias y controles."),
  additional("nivel-1-3","1.3 — Amenaza y cierre","Añadir oposición de soldados y finalizar la recolección. El jefe dragón sin tiempo queda como extensión futura, no parte necesaria de la primera iteración."),
  additional("anotaciones-1-3","Anotaciones 1.3","Primero enemigos de comportamiento simple y estados visibles. No construir un jefe complejo mientras el movimiento no esté pulido."),
 ]},
 {id:"grid-7",title:"Diseño Visual y Arte Final — Entrega",sections:[
  original("rubrica","rubrica","Primera muestra de avance · 5 noviembre 2026"),
  original("entregables","entregables","Índice de archivos y presentación"),
  original("decisiones","decisiones","Decisiones y descartes justificados"),
  original("evidencias","evidencias","Evidencias del proceso artístico"),
 ]},
]
