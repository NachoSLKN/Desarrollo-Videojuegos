import { chapters as sourceChapters, palette, type Chapter, type Section } from './gddPrevious'
export type {Chapter, Section} from './gddPrevious'
export {palette}

const source = sourceChapters.flatMap(ch => ch.sections)

function original(id:string, key:string, title:string):Section {
  const found = source.find(s=>s.id===id)
  if (!found) throw new Error('Missing source section: '+id)
  return {...found,id:key,title}
}

function pending(id:string,title:string,body='Por definir.'):Section {
  return {id,title,paragraphs:[body]}
}

export const chapters:Chapter[] = [
 {id:"grid-0",title:"Sobre el juego",sections:[original("pitch","sobre-juego","Sobre el juego")]},
 {id:"grid-1",title:"Documento de diseño del juego",sections:[
  original("sobre","sobre","Introducción / Sobre este documento"),
  original("origen","origen","Origen del proyecto y reinterpretación"),
  original("pitch","grid-tactics","The Iron Giant — Descripción general"),
  original("publico","publico","Público objetivo"),
  original("objetivos","objetivo-producto","Objetivo del producto"),
  pending("mvp","MVP — Producto mínimo viable"),
  pending("mmp","MMP — Producto mínimo comercializable"),
  pending("map","MAP — Producto mínimo asombroso"),
  original("tecnica","tecnica","Especificaciones técnicas"),
  original("arquitectura","arquitectura","Arquitectura y patrones de diseño"),
  original("plan","plan","Planificación del desarrollo"),
  original("pipeline","pipeline","Pipeline de arte y control de versiones"),
  original("vision","arte","Arte / Estilo visual"),
  original("paleta","paleta","Paleta de colores"),
  original("materiales","materiales","Materiales y sombreado"),
  original("tipografia","tipografia","Tipografía"),
  original("animacion","animacion","Animación"),
    original("vfx","vfx","Efectos visuales"),
  original("hud","interfaces","Interfaces"),
  original("menus","menus","Interfaces — Pantalla de título y carga"),
  original("vision","esteticas","Estéticas y contexto de juego"),
  original("papel","papel","Papel del usuario"),
  original("navegacion","navegacion","Diagrama de navegación de usuario"),
 ]},
 {id:"grid-2",title:"Mecánicas",sections:[
  original("controles","controles","Controles"),
  original("bucle","bucle","Ciclo de juego"),
  pending("movimiento-resistencia","Movimiento por resistencia","No trabajado."),
  original("controles","orientacion","Orientación de unidad"),
  original("tornillos","tornillos","Recolección de tornillos"),
  original("mejoras","mejoras","Mejoras"),
  original("escalada","escalada","Escalada de Hogarth"),
  original("tren","tren","Tren y evento rápido"),
  original("reconstruccion","reconstruccion","Desmontaje y reconstrucción"),
 ]},
 {id:"grid-3",title:"Elementos de juego",sections:[
  original("entidades","elementos","Elementos de juego y sus relaciones"),
  pending("partida","Partida"),
  original("niveles","nivel","Nivel"),
  pending("equipo","Equipo"),
  original("papel","jugador","Jugador"),
  original("entidades","entidad","Entidad"),
  pending("unidad","Unidad / personaje","Gigante y Hogarth."),
  pending("propiedad","Propiedad / objeto interactivo","Existen objetos destructibles y elementos interactivos básicos."),
  original("escenarios-general","mapa","Escenario"),
  pending("tile","Tile / módulo de entorno","No trabajado."),
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
  pending("set1","Set 1 — Zona prototipo","Por hacer."),
  pending("nivel-1-1","1.1 — Primer contacto","Por hacer."),
  pending("anotaciones-1-1","Anotaciones 1.1",""),
  pending("nivel-1-2","1.2 — Recolección","Por hacer."),
  pending("anotaciones-1-2","Anotaciones 1.2",""),
  pending("nivel-1-3","1.3 — Amenaza y cierre","Por hacer."),
  pending("anotaciones-1-3","Anotaciones 1.3",""),
 ]},
 {id:"grid-7",title:"Diseño Visual y Arte Final — Entrega",sections:[
  original("rubrica","rubrica","Primera muestra de avance · 5 noviembre 2026"),
  original("splash","splash","Splash art"),
  original("entregables","entregables","Índice de archivos y presentación"),
  original("decisiones","decisiones","Decisiones y descartes justificados"),
  original("evidencias","evidencias","Evidencias del proceso artístico"),
 ]},
]