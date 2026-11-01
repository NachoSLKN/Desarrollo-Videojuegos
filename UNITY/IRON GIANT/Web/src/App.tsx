import { useEffect, useMemo, useState } from 'react'
import { chapters, palette, type Section } from './gdd'
import './index.css'

type Page = 'inicio'|'documentacion'|'arte'|'desarrollo'
const nav: {id:Page;label:string}[]=[{id:'inicio',label:'Inicio'},{id:'documentacion',label:'Documentación'},{id:'arte',label:'Dirección artística'},{id:'desarrollo',label:'Desarrollo'}]
const allSections=chapters.flatMap(c=>c.sections)
const getInitial=():{page:Page;section:string}=>{
  const h=decodeURIComponent(window.location.hash.slice(1))
  if(h.startsWith('docs/'))return {page:'documentacion',section:h.slice(5)||'sobre'}
  if(h==='arte'||h==='desarrollo')return {page:h,section:'sobre'}
  return {page:'inicio',section:'sobre'}
}
function Display({children,className=''}:{children:string;className?:string}){return <span className={'display '+className}>{children.toUpperCase()}</span>}
function SectionBody({s}:{s:Section}){return <>
  {s.paragraphs?.map((p,i)=><p key={i}>{p}</p>)}
  {s.bullets&&<ul className="doc-list">{s.bullets.map((b,i)=><li key={i}>{b}</li>)}</ul>}
  {s.table&&<div className="table-scroll"><table><thead><tr>{s.table.columns.map(c=><th key={c}>{c}</th>)}</tr></thead><tbody>{s.table.rows.map((r,i)=><tr key={i}>{r.map((v,j)=><td key={j}>{v.startsWith('#')&&/^#[0-9A-F]{6}$/i.test(v)?<><span className="dot-color" style={{background:v}}/>{v}</>:v}</td>)}</tr>)}</tbody></table></div>}
  {s.id==='paleta'&&<div className="palette-inline">{palette.map(([name,hex])=><div className="palette-chip" key={hex} title={`${name}: ${hex}`}><span style={{background:hex}}/><small>{hex}</small></div>)}</div>}
  {s.id==='bucle'&&<div className="flow-graphic">{['EXPLORAR','RECOGER TORNILLOS','CAMBIAR PERSONAJE','COMPLETAR OBJETIVO','JEFE SIN TIEMPO'].map((a,i)=><div key={a}><strong>{a}</strong>{i<4&&<span aria-hidden="true">↓</span>}</div>)}</div>}
  {s.id==='navegacion'&&<div className="flow-graphic horizontal">{['TITLE SCREEN','MAIN MENU','LOADING','PARTIDA'].map((a,i)=><div key={a}><strong>{a}</strong>{i<3&&<span aria-hidden="true">→</span>}</div>)}</div>}
  {s.note&&<div className="editorial-note"><strong>Nota de diseño</strong><p>{s.note}</p></div>}
</>}
function App(){
  const [initial]=useState(getInitial)
  const [page,setPage]=useState<Page>(initial.page)
  const [active,setActive]=useState(initial.section)
  const [search,setSearch]=useState('')
  const [indexOpen,setIndexOpen]=useState(true)
  const [expanded,setExpanded]=useState<string[]>(chapters.map(c=>c.id))
  const selected=allSections.find(x=>x.id===active)??allSections[0]
  const filtered=useMemo(()=>chapters.map(ch=>({...ch,sections:ch.sections.filter(s=>(s.title+' '+(s.paragraphs??[]).join(' ')).toLowerCase().includes(search.toLowerCase()))})).filter(ch=>ch.sections.length>0),[search])
  function navigate(to:Page,section='sobre'){
    setPage(to);setActive(section)
    window.location.hash=to==='documentacion'?'docs/'+section:to==='inicio'?'':to
    window.scrollTo({top:0,behavior:'instant'})
  }
  useEffect(()=>{
    const onHash=()=>{const next=getInitial();setPage(next.page);setActive(next.section)}
    window.addEventListener('hashchange',onHash)
    return ()=>window.removeEventListener('hashchange',onHash)
  },[])
  function move(delta:number){const idx=allSections.findIndex(s=>s.id===selected.id);const dest=allSections[idx+delta];if(dest)navigate('documentacion',dest.id)}
  return <div className="site">
    <header className="topbar">
      <button className="brand" onClick={()=>navigate('inicio')}><img src={`${import.meta.env.BASE_URL}images/iron-giant-icon.png`} alt="Icono del Gigante"/><span><strong>IRON GIANT</strong><small> PROJECT</small></span></button>
      <nav className="topnav" aria-label="Navegación principal">{nav.map(n=><button key={n.id} className={page===n.id?'active':''} onClick={()=>navigate(n.id)}>{n.label}</button>)}</nav>
      <div className="topmeta">GDD / v0.2 <span className="accent-dot"/></div>
    </header>
    {page==='inicio'&&<main className="home">
      <div className="hero"><div className="hero-copy"><div className="eyebrow"><i/> PROYECTO ACADÉMICO NO OFICIAL · 2026</div>
        <h1><Display>THE</Display><Display className="hero-iron">IRON</Display><Display className="hero-giant">GIANT</Display></h1>
        <p className="hero-lead">Dos personajes. Dos escalas. Un mundo por descubrir.</p>
        <p className="muted hero-text">Una reinterpretación artística e interactiva del universo de El Gigante de Hierro. Explora, alterna entre Hogarth y el Gigante y descubre un mundo construido a dos escalas.</p>
        <div className="hero-buttons"><button className="primary" onClick={()=>navigate('documentacion')}>EXPLORAR EL GDD <span>↗</span></button><button className="outline-button" onClick={()=>navigate('arte')}>DIRECCIÓN ARTÍSTICA ↗</button></div>
      </div><div className="hero-art"><div className="orbit one"/><div className="orbit two"/><img src={`${import.meta.env.BASE_URL}images/iron-giant-icon.png`} alt="Rostro ilustrado del Gigante de Hierro"/><span>FIG. 01 / IDENTIDAD VISUAL</span></div></div>
      <div className="chapter-strip"><button onClick={()=>navigate('documentacion','gigante')}>01 — PERSONAJES</button><button onClick={()=>navigate('documentacion','chatarreria')}>02 — ESCENARIOS</button><button onClick={()=>navigate('documentacion','bucle')}>03 — MECÁNICAS</button><button onClick={()=>navigate('documentacion','rubrica')}>04 — ARTE FINAL</button></div>
      <div className="vision"><div><span className="eyebrow">LA VISIÓN</span><h2><Display>UN GIGANTE, DOS FORMAS DE EXPLORAR.</Display></h2></div><p>Un videojuego en desarrollo que combina personajes 3D, escenarios low-poly, arte inspirado en el cómic, Grease Pencil e interfaz propia. El documento refleja tanto el trabajo real como sus siguientes pasos.</p></div>
      <div className="quick-grid"><div><span className="mini-label">DOCUMENTACIÓN</span><h3>Un GDD vivo</h3><p>Arquitectura, objetivos, mecánicas, diseño visual y planificación organizados por capítulos.</p><button onClick={()=>navigate('documentacion')}>CONSULTAR ÍNDICE →</button></div><div><span className="mini-label">ARTE FINAL</span><h3>Cuatro entregables</h3><p>Personaje, escenario, imagen de marketing e interfaz para la primera muestra del proyecto.</p><button onClick={()=>navigate('documentacion','rubrica')}>VER RÚBRICA →</button></div></div>
    </main>}
    {page==='documentacion'&&<main className="docs-layout">
      <aside className={'sidebar '+(!indexOpen?'closed':'')} aria-label="Índice de contenidos">
        <div className="index-title"><strong>ÍNDICE DE CONTENIDOS</strong><button onClick={()=>setIndexOpen(v=>!v)} aria-label="Mostrar u ocultar índice">{indexOpen?'×':'☰'}</button></div>
        {indexOpen&&<><input value={search} onChange={e=>setSearch(e.target.value)} placeholder="Buscar capítulos o contenidos..." aria-label="Buscar documentación"/>
        <div className="index-list">{filtered.map(ch=><div key={ch.id}><button className="chapter-button" onClick={()=>setExpanded(p=>p.includes(ch.id)?p.filter(x=>x!==ch.id):[...p,ch.id])}><span>{ch.title}</span><span>{expanded.includes(ch.id)?'−':'+'}</span></button>
        {(expanded.includes(ch.id)||search)&&ch.sections.map(s=><button key={s.id} className={'section-button '+(selected.id===s.id?'selected':'')} onClick={()=>navigate('documentacion',s.id)}>{s.title}</button>)}</div>)}</div></>}
      </aside>
      <article className="article"><div className="breadcrumb">DOCUMENTACIÓN <span>/</span> {chapters.find(ch=>ch.sections.includes(selected))?.title}</div>
        <div className="article-heading"><span className={'state '+selected.status?.toLowerCase().replace(' ','-')}>{selected.status??'Pendiente'}</span><h2><Display>{selected.title}</Display></h2></div>
        <div className="accent-rule"/><SectionBody s={selected}/>
        <div className="article-bottom"><button onClick={()=>move(-1)} disabled={selected.id===allSections[0].id}>← ANTERIOR</button><span>{allSections.findIndex(s=>s.id===selected.id)+1} / {allSections.length} · DOCUMENTO VIVO</span><button onClick={()=>move(1)} disabled={selected.id===allSections[allSections.length-1].id}>SIGUIENTE →</button></div>
      </article>
    </main>}
    {page==='arte'&&<main className="standalone"><div className="eyebrow">ART BIBLE / IDENTIDAD VISUAL</div><h2><Display>DIRECCIÓN ARTÍSTICA</Display></h2><p className="intro">Una identidad construida sobre contraste de escala, superficies metálicas, luz cálida y elementos ilustrados. Nuestra paleta está presente en la web, las interfaces y los experimentos de materiales.</p><div className="palette-grid">{palette.map(([n,c])=><div className="swatch" key={c}><div style={{background:c}}/><strong>{n}</strong><code>{c}</code></div>)}</div><div className="feature-grid"><div><span className="mini-label">PERSONAJES</span><h3>Gigante y Hogarth</h3><p>Modelo 3D propio del Gigante; Hogarth generado mediante Meshy e integrado en Unity.</p><button onClick={()=>navigate('documentacion','gigante')}>VER DISEÑO →</button></div><div><span className="mini-label">ESCENARIOS</span><h3>3D + ilustración</h3><p>Pruebas controladas con Grease Pencil, texturas en Krita y elementos low-poly.</p><button onClick={()=>navigate('documentacion','escenarios-general')}>VER ESCENARIOS →</button></div><div><span className="mini-label">MARKETING</span><h3>Lenguaje de cómic</h3><p>Carátula, splash art y rotulación con textura de impresión y composición fuerte.</p><button onClick={()=>navigate('documentacion','rubrica')}>VER ENTREGA →</button></div></div></main>}
    {page==='desarrollo'&&<main className="standalone"><div className="eyebrow">DEVLOG / PROYECTO VIVO</div><h2><Display>DESARROLLO</Display></h2><p className="intro">El progreso se documenta con estados honestos: implementado, prototipo, en revisión y planificado.</p><div className="roadmap">{[['HECHO / PROTOTIPO','Modelo del Gigante · movimiento · vuelo · lock-on · rayos · HUD · pantalla de título'],['EN REVISIÓN','Cambio de personaje · entrada de habilidades tras Tab · partículas · controladores'],['ARTE FINAL · 5 NOV.','Personaje · escenario · imagen de marketing · interfaz'],['SIGUIENTE MVP','Tornillos · temporizador · interacción · objetivos de nivel'],['VISIÓN FUTURA','Soldados · dragón · escalada · tren · reconstrucción por piezas']].map(([name,desc])=><div key={name}><strong>{name}</strong><p>{desc}</p></div>)}</div><button className="primary" onClick={()=>navigate('documentacion','plan')}>CONSULTAR PLANIFICACIÓN ↗</button></main>}
    <footer><span>THE IRON GIANT · PROYECTO ACADÉMICO NO OFICIAL</span><span>REACT · TYPESCRIPT · UNITY · 2026</span></footer>
  </div>
}
export default App
