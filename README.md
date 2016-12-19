# Finn posisjon
**En tjeneste fra [Kartverket](https://kartverket.no) som tilbyr hjelp til å finne koordinaters posisjon i Norge**

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
