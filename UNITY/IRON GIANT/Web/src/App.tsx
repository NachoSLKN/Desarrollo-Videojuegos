import { useEffect, useMemo, useState } from 'react'
import { chapters, palette, type Section } from './gdd'
import './index.css'
import './gdd-media.css'

import turnaroundFront from './assets/gdd/turnaround-front.png'
import turnaroundSide from './assets/gdd/turnaround-side.png'
import turnaroundFront45 from './assets/gdd/turnaround-front-45.png'
import turnaroundBack45 from './assets/gdd/turnaround-back-45.png'
import turnaroundBack from './assets/gdd/turnaround-back.png'
import scenarioImage from './assets/gdd/scenario.jpg'
import comicIronMan from './assets/gdd/comic-iron-man.png'
import comicMagnus from './assets/gdd/comic-magnus.png'
import comicLoading from './assets/gdd/comic-loading.png'
import comicBigLeague from './assets/gdd/comic-big-league.png'
import screwImage from './assets/gdd/screw.png'
import crashImage from './assets/gdd/crash.png'
import shaderFlatKitDemo from './assets/gdd/shader-flatkit-demo.png'
import splashComicV01 from './assets/gdd/splash-comic-v01.png'

type Page = 'inicio'|'documentacion'|'arte'|'desarrollo'
const nav: {id:Page;label:string}[]=[{id:'inicio',label:'Inicio'},{id:'documentacion',label:'Documentación'},{id:'arte',label:'Dirección artística'},{id:'desarrollo',label:'Desarrollo'}]
const allSections=chapters.flatMap(c=>c.sections)

const getInitial=():{page:Page;section:string}=>{
  const h=decodeURIComponent(window.location.hash.slice(1))
  if(h.startsWith('docs/'))return {page:'documentacion',section:h.slice(5)||'sobre'}
  if(h==='arte'||h==='desarrollo')return {page:h,section:'sobre'}
  return {page:'inicio',section:'sobre'}
}

function Display({children,className=''}:{children:string;className?:string}){
  return <span className={'display '+className}>{children.toUpperCase()}</span>
}

const turnaround: [string, string][] = [
  ['Frontal',turnaroundFront],
  ['Lateral',turnaroundSide],
  ['3/4 frontal',turnaroundFront45],
  ['3/4 trasero',turnaroundBack45],
  ['Trasera',turnaroundBack],
]

const comics: [string, string][] = [
  ['Iron Man vs. Iron Giant',comicIronMan],
  ['Magnus / Iron Giant',comicMagnus],
  ['Loading screen',comicLoading],
  ['The Big League',comicBigLeague],
  ['Splash Comic v01',splashComicV01],
]

function ImageGallery({items,className=''}:{items:[string,string][];className?:string}){
  return <div className={'media-grid '+className}>
    {items.map(([label,src])=><figure key={src}>
      <img src={src} alt={label}/>
      <figcaption>{label}</figcaption>
    </figure>)}
  </div>
}

function SectionBody({s}:{s:Section}){return <>
  {s.paragraphs?.map((p,i)=><p key={i}>{p}</p>)}

  {s.id==='gigante'&&<>
    <div className="media-title">TURNAROUND</div>
    <ImageGallery items={turnaround}/>
    <div className="media-title">APLICACIÓN DEL SHADER</div>
    <div className="media-single">
      <img src={shaderFlatKitDemo} alt="Aplicación de Flat Kit: Toon Shading and Water en el Gigante"/>
    </div>
  </>}

  {s.id==='mapa'&&<>
    <div className="media-title">ESCENARIO</div>
    <div className="media-single"><img src={scenarioImage} alt="Escenario de la chatarrería de Dean"/></div>
  </>}

  {s.id==='splash'&&<>
    <div className="media-title">REFERENCIAS Y PRUEBAS DE PORTADA</div>
    <ImageGallery items={comics} className="comic-grid"/>
  </>}

  {s.id==='tornillos'&&
    <div className="media-icon"><img src={screwImage} alt="Icono de tornillo utilizado como recurso"/></div>
  }

  {s.id==='vfx'&&
    <div className="media-icon crash-media"><img src={crashImage} alt="Gráfico CRASH utilizado como feedback visual"/></div>
  }

  {s.bullets&&<ul className="doc-list">{s.bullets.map((b,i)=><li key={i}>{b}</li>)}</ul>}
  {s.table&&<div className="table-scroll"><table><thead><tr>{s.table.columns.map(c=><th key={c}>{c}</th>)}</tr></thead><tbody>{s.table.rows.map((r,i)=><tr key={i}>{r.map((v,j)=><td key={j}>{v.startsWith('#')&&/^#[0-9A-F]{6}$/i.test(v)?<><span className="dot-color" style={{background:v}}/>{v}</>:v}</td>)}</tr>)}</tbody></table></div>}
  {s.id==='paleta'&&<div className="palette-inline">{palette.map(([name,hex])=><div className="palette-chip" key={hex} title={`${name}: ${hex}`}><span style={{background:hex}}/><small>{hex}</small></div>)}</div>}
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

  function move(delta:number){
    const idx=allSections.findIndex(s=>s.id===selected.id)
    const dest=allSections[idx+delta]
    if(dest)navigate('documentacion',dest.id)
  }

  return <div className="site">
    <header className="topbar">
      <button className="brand" onClick={()=>navigate('inicio')}>
        <img src={`${import.meta.env.BASE_URL}images/iron-giant-icon.png`} alt="Icono del Gigante"/>
        <span><strong>IRON GIANT</strong><small> PROJECT</small></span>
      </button>
      <nav className="topnav" aria-label="Navegación principal">{nav.map(n=><button key={n.id} className={page===n.id?'active':''} onClick={()=>navigate(n.id)}>{n.label}</button>)}</nav>
      <div className="topmeta">GDD / v0.3 <span className="accent-dot"/></div>
    </header>

    {page==='inicio'&&<main className="home">
      <div className="hero"><div className="hero-copy">
        <div className="eyebrow"><i/> PROYECTO EN UNITY · 2026</div>
        <h1><Display>THE</Display><Display className="hero-iron">IRON</Display><Display className="hero-giant">GIANT</Display></h1>
        <p className="hero-lead">Proyecto en Unity sobre El Gigante de Hierro.</p>
        <p className="muted hero-text">Desarrollo 3D centrado en personaje, dirección artística, interacción y gameplay.</p>
        <div className="hero-buttons"><button className="primary" onClick={()=>navigate('documentacion')}>EXPLORAR EL GDD <span>↗</span></button><button className="outline-button" onClick={()=>navigate('arte')}>DIRECCIÓN ARTÍSTICA ↗</button></div>
      </div><div className="hero-art"><div className="orbit one"/><div className="orbit two"/><img src={`${import.meta.env.BASE_URL}images/iron-giant-icon.png`} alt="Rostro ilustrado del Gigante de Hierro"/><span>FIG. 01 / IDENTIDAD VISUAL</span></div></div>

      <div className="chapter-strip">
        <button onClick={()=>navigate('documentacion','gigante')}>01 — PERSONAJES</button>
        <button onClick={()=>navigate('documentacion','mapa')}>02 — ESCENARIOS</button>
        <button onClick={()=>navigate('documentacion','controles')}>03 — MECÁNICAS</button>
        <button onClick={()=>navigate('documentacion','splash')}>04 — ARTE FINAL</button>
      </div>

      <div className="vision"><div><span className="eyebrow">EL PROYECTO</span><h2><Display>GDD + MEMORIA DE DESARROLLO</Display></h2></div><p>La documentación recoge el trabajo realizado y mantiene las secciones pendientes para ampliarlas conforme avance el proyecto.</p></div>
    </main>}

    {page==='documentacion'&&<main className="docs-layout">
      <aside className={'sidebar '+(!indexOpen?'closed':'')} aria-label="Índice de contenidos">
        <div className="index-title"><strong>ÍNDICE DE CONTENIDOS</strong><button onClick={()=>setIndexOpen(v=>!v)} aria-label="Mostrar u ocultar índice">{indexOpen?'×':'☰'}</button></div>
        {indexOpen&&<><input value={search} onChange={e=>setSearch(e.target.value)} placeholder="Buscar capítulos o contenidos..." aria-label="Buscar documentación"/>
        <div className="index-list">{filtered.map(ch=><div key={ch.id}><button className="chapter-button" onClick={()=>setExpanded(p=>p.includes(ch.id)?p.filter(x=>x!==ch.id):[...p,ch.id])}><span>{ch.title}</span><span>{expanded.includes(ch.id)?'−':'+'}</span></button>
        {(expanded.includes(ch.id)||search)&&ch.sections.map(s=><button key={s.id} className={'section-button '+(selected.id===s.id?'selected':'')} onClick={()=>navigate('documentacion',s.id)}>{s.title}</button>)}</div>)}</div></>}
      </aside>
      <article className="article">
        <div className="breadcrumb">DOCUMENTACIÓN <span>/</span> {chapters.find(ch=>ch.sections.includes(selected))?.title}</div>
        <div className="article-heading"><h2><Display>{selected.title}</Display></h2></div>
        <div className="accent-rule"/>
        <SectionBody s={selected}/>
        <div className="article-bottom"><button onClick={()=>move(-1)} disabled={selected.id===allSections[0].id}>← ANTERIOR</button><span>{allSections.findIndex(s=>s.id===selected.id)+1} / {allSections.length} · DOCUMENTO VIVO</span><button onClick={()=>move(1)} disabled={selected.id===allSections[allSections.length-1].id}>SIGUIENTE →</button></div>
      </article>
    </main>}

    {page==='arte'&&<main className="standalone">
      <div className="eyebrow">ART BIBLE / IDENTIDAD VISUAL</div>
      <h2><Display>DIRECCIÓN ARTÍSTICA</Display></h2>
      <p className="intro">3D, cel shading, contornos, tipografía propia y recursos gráficos inspirados en el cómic.</p>
      <div className="feature-grid">
        <div><span className="mini-label">PERSONAJE</span><h3>El Gigante</h3><p>Modelo propio, turnaround y pruebas con Flat Kit.</p><button onClick={()=>navigate('documentacion','gigante')}>VER PERSONAJE →</button></div>
        <div><span className="mini-label">ESCENARIO</span><h3>Entorno</h3><p>Primera propuesta visual de escenario integrada en la memoria.</p><button onClick={()=>navigate('documentacion','mapa')}>VER ESCENARIO →</button></div>
        <div><span className="mini-label">MARKETING</span><h3>Cómic / Splash</h3><p>Portadas y recursos gráficos como identidad y pantallas de carga.</p><button onClick={()=>navigate('documentacion','splash')}>VER SPLASH ART →</button></div>
      </div>
    </main>}

    {page==='desarrollo'&&<main className="standalone">
      <div className="eyebrow">DEVLOG / PROYECTO VIVO</div>
      <h2><Display>DESARROLLO</Display></h2>
      <p className="intro">Esta sección se actualizará con el trabajo realizado durante las siguientes fases.</p>
      <button className="primary" onClick={()=>navigate('documentacion','plan')}>CONSULTAR PLANIFICACIÓN ↗</button>
    </main>}

    <footer><span>THE IRON GIANT · UNITY PROJECT</span><span>REACT · TYPESCRIPT · UNITY · 2026</span></footer>
  </div>
}

export default App
