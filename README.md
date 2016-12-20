# Finn posisjon
**En tjeneste fra [Kartverket](https://kartverket.no) som tilbyr hjelp til å finne koordinaters posisjon i Norge**

Tjenesten tolker koordinater som oppgis og viser punkter i kartet for koordinatenes mulige posisjoner i Norge. Pr. 20.12.2016 behandler tjenesten koordinater i følgende systemer:

* EUREF89 UTM sone 32–35 pluss sone 33 utvidet for hele Norge.
* WGS84 (desimalgrader / grader, minutter, / grader, minutter, sekunder)
* NGO1948 akse 1–8 (akse 1–4 og akse 5–8 vist samlet)
* Lokalt nett, Oslo (tilnærmet)
Tjenesten er under utvikling og vil kunne utvides med flere koordinatsystemer.

**Presisjonen for posisjoner funnet av systemet varierer med usikkerhet fra flere faktorer bl.a. hvilket innmålingsutstyr som er brukt til å finne koordinatene.**

### Prototype: [finnposisjon.test.geonorge.no](http://finnposisjon.test.geonorge.no/)

#### Applikasjon

**Front-end**

* Basis: HTML5, Javascript
* Databinding: [Vue.js](https://vuejs.org/)
* Kartmotor: [Leaflet](http://leafletjs.com/)
* Kartbakgrunnstjeneste: [Toporaster 3](https://kartkatalog.geonorge.no/metadata/kartverket/toporaster-3-wms-cache/2070733b-272f-4f49-9e2d-33357b28d9d1) (Kartverket)
 
**Back-end**

* Basis: C# (.NET ASP MVC)
* Transformasjonstjeneste: [sosiTrans](https://kartkatalog.geonorge.no/metadata/uuid/b0a3c1e7-36a8-4329-9c78-e8722145fb40) (Kartverket)
    * Dokumentasjon: [SKWS2.SSR](https://baat.geonorge.no/skdokbaat/WEBSERVICES/SKWS2.SSR/index.html)
* Adressetjeneste: [ws.geonorge.no/AdresseWS/adresse](http://ws.geonorge.no/AdresseWS/adresse/) (Kartverket)

**Kildekode**

* Fri programvare / åpen kildekode
* Repository:  [GitHub](https://github.com/kartverket/finnposisjon)


*Finn posisjon er utviklet av [Arkitektum AS](http://www.arkitektum.no/) på oppdrag fra [Kartverket](https://kartverket.no).*
