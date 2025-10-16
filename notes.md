# Notes

## Paradigmas der webdevelopment

Single Page Application - SPA
- manier van renderen is anders
- lijkt magisch
- dingen moeten worden ingeladen
- HTML dynamisch aangepast door JavaScript/client (browser)
- browser doet veel meer. server stuurt data op.
- libs/frameworks: Blazor React Angular Vue Svelte Solid Qwik Inferno  Yew (Rust)
- geen FOUC
- nadelen: loading spinner hel. initiele laadtijd.
  - Blazor WebAssembly: Hello world == 7MB 12MB
- hip

Server-side rendering (SSR)
- rendert de initiele pagina zo goed als volledig
- daarna is het gewoon weer een SPA
- complementair aan een SPA
- libs/frameworks: Next (React)   Nuxt (Vue) @angular/ssr  QwikCity SolidStart  ASP.NET Core
- complexiteit++
- hip

Static site generation (SSG)
- genereert vanuit de pipeline heul veul .html-bestanden
  - productcatalogus   product-muis-183749209.html  /product/muis-8298348943
- libs/frameworks: 11ty Astro Hugo Next
- hip

Multi Page Application - MPA
- meerdere pagina's
- elke pagina eigen URL
- eenmaal opgestuurd is het statische HTML
- server doet veel werk - hij rendert iedere pagina
  - Flash Of Unstyled Content
- bij ieder klikje moet de server de nieuwe pagina renderen
- libs/frameworks/platformen: Blazor Next ASP.NET WebForms/PHP/Spring/ASP.NET Core Razor Pages
- niet hip

## Blazor-uitvoeringen

interactief:
- Blazor WebAssembly
  - maakt gebruik van browser feature: WebAssembly
  - C# =====compile==> WebAssembly. ook andere compilers: C Rust Go
  - resulteert in een SPA
  - al jouw code draait vervolgens IN de browser
  - heule kleine .NET-runtime wordt in browser geladen
  - Hello World is 7MB
- Blazor Server
  - resulteert in een SPA
  - al jouw code draait OP de server
  - middels openstaande WebSocket-verbinding (gewrapt door SignalR) worden alle UI-updates gecommuniceerd
    - als de server uitvalt? UI dood.
    - Azure SignalR Service lijkt een maximum van 5000 connecties

niet interactief:

- Blazor Static SSR
  - traditionele MPA
    - form submits om 

Deze modi zijn te mengen:

```razor
<JouwComponent @rendermode="InteractiveWebAssembly">
```

## Blazor
- 2018-2020  .NET Core 3.1
- gemaakt door Microsoft
- waarom?
  - Microsoft had niks voor SPA
  - voor developers die niks met JS/TS hebben
  - gedeelde codebase (C# frontend, C# backend)
    - maar dat kan met JS/TS ook
  - Microsoft-minded
  - technische redenen: Blazor Server. content projection.
- waarom niet?
  - 7MB is meh
  - adoptie is mwoa, minder grote community
  - Blazor Server is meer compute en dus meer $$$
  - als je geen C# kent
  - als je niet afhankelijk wil zijn van M$
  - integratie met zaken als Tailwind of shad/cn is meh. Met pre-build commando kun je `npm build` aftrappen.
  - HMR  Hot Module Reloading  REFRESHEN VAN JE PAGINA
    - styling gaat wel
    - templates gaan soms
    - code meestal niet
  - sommige errors zijn... irritant.

## Dependency injection

- dependencies injecteren in andere classes
- niet handmatig meer instanties aanmaken
  - niet tot weinig  `new BlaService();`
- mogelijk om andere dependencies te injecteren
  - handig met TESTEN en MOCKEN

Lifetimes van dependencies?

```cs
// Blazor Static SSR - ASP.NET Core
.AddTransient(); // per usage
.AddScoped(); // per request
.AddSingleton(); // 1 instance to rule them all

// Blazor WebAssembly
.AddTransient(); // per usage
.AddScoped(); // ook singleton
.AddSingleton(); // 1 instance to rule them all

// Blazor Server
.AddTransient(); // per usage
.AddScoped(); // per "SignalR circuit"
.AddSingleton(); // 1 instance to rule them all
```

### Repository-pattern

- een laag die tegen je data storage aanpraat
- interface van data storage
- database-onafhankelijkheid
  - ORM-onafhankelijkheid
- operaties centralizeren   `.Where(x => !x.IsInactive)`
- DI   `DbContext` mocken
- huishoudelijk - codesplitting

https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/dependency-injection?view=aspnetcore-9.0#service-lifetime


## ASP.NET Core  ReST API

- API die zelf geen state heeft
- ReST: Representational State Transfer
- HTTP verbs
  - GET     ophalen
  - POST    aanmaken              / wijzigen
  - PUT     wijzigen              / aanmaken
  - PATCH   deel wijzigen
  - DELETE  verwijderen
    ```sh
    POST  api/car     { make: '...', model: '...' }
    POST  api/car     { make: '...', model: '...' }
    POST  api/car     { make: '...', model: '...' }

    PUT   api/car/16  { make: '...', model: '...' }
    PUT   api/car/16  { make: '...', model: '...' }
    PUT   api/car/16  { make: '...', model: '...' }
    ```
    idempotency

### HTTP-statuscodes

- 2xx - SUCCESS
  - 200 OK
  - 201 Created
  - 204 No Content
- 3xx - REDIRECT
  - 301/302 Temporary/permanent
- 4xx - CLIENT ERROR
  - 400 Bad Request (jij doet iets fout)
  - 401 Unauthorized (geen token)
  - 403 Forbidden (wel een token, je mag er gewoon niet bij)
  - 404 Not Found
  - 405 Method Not Allowed   POST => endpoint die geen POST ondersteunt
  - 415 Mediatype Not Supported   XML => endpoint die geen XML ondersteunt
  - 422 Unprocessable Entity
  - 418 I'm a teapot
- 5xx - SERVER ERROR
  - 500 Internal Server Error  (ik de server doe iets fout)

### ASP.NET Core en REST

- controllers - ASP.NET Web API 2010 /  ASP.NET Core 2016
  - endpoint "netjes" in classes
  - DI n constructor
  - rompslomp: ControllerFactory action filters [HttpGet] validatie [Required]
  - [Consumes()] [Produces()]
- minimal API - .NET 6
  - `app.MapGet()` `app.MapPost()`   lambdas
    - je kan hier zelf je structuur bedenken
  - lijkt sterk op Express.js
  - DI hier in de methode
  - performance++ want DI bij methode maar ook minder rompslomp eromheen
    - FluentValidation
  - OpenAPI docs: TypedResults - .NET 8

### Hoe testen we ons POST-endpoint?

- Postman - paywall
- Insomnia - paywall
- VS Code extensions
  - REST Client
  - Thunder Client
- Rider/VS ingebakken HTTPClient
- curl
- Bruno
- Hoppscotch

#### Wat is er lastig met REST APIs?

versionering! Qua URL's niet zozeer:

- api/v1/car
- api/car?v=1
- X-API-VERSION=1

Maar wel vanaf deze endpoints je services/repos/db aanspreken en logica kunnen hergebruiken.

## DTOs

- security - over-POSTing `context.Snacks.Add(watermetpostwordtmeegestuurd);`
- validatie per DTO
- loskoppeling db-entiteiten en wat je aan de buitenwereld kenbaar maakt
- consistente werkwijze
- mappers zijn benodigd om van Entity instance naar DTO instance gaan
  - AI zeer handig van pas komt
  - libs: AutoMapper (license) Mapster Mapperly
    - Meestal heb je dit niet nodig. [En wordt meestal toch foutief toegepast](https://www.reddit.com/r/csharp/comments/ykcp7a/comment/iusjix3/?utm_source=share&utm_medium=web3x&utm_name=web3xcss&utm_term=1&utm_content=share_button). Pas ook op met refactoren ivm `null` in JSON`-output:
    ```cs
    // handmatig
    entity.Name = dto.Name;
    dto.Name = entity.FullName; // refactor: rename past mapping aan

    // AutoMapper  Mapster
    // => reflection. refactor: rename past mapping NIET aan
    //JSON-output: "name": null
    ```
    - AutoMapper ondersteunt wel unittesten om dit te voorkomen: https://docs.automapper.io/en/stable/Configuration-validation.html
      - Maar nog steeds, je hebt het niet nodig
- dragen een klein beetje bij aan versionering in de zin dat je properties kan toevoegen aan je DTOs

## `HttpClient`

- prima ding, al jaren in .NET aanwezig
- heeft extensions methods om met JSON om te gaan
- vertaalt naar een `fetch()`-call via WebAssembly
- laat wel wat te wensen over
  - testen
  - JSON-returnwaarden bij POST/PUT is wat lomp/omslachtig:
    ```cs
    var response = await http.PostAsJsonAsync<decimal>("api/snacks", 12m);
    var updatedSnack = response.Content.ReadFromJsonAsync<Snack>();
    ```
    - typed HTTP clients (simpel wrapperlaagje met vriendelijkere methoden) vergemakkelijken dat
      ```cs
      var snacks = await snackClient.GetAllAsync();
      var updatedSnack = await snackClient.AddAsync(snack);
      ```
    - [lib als Flurl](https://flurl.dev/) kan helpen
      ```cs
      "https://api.com".GetFromJsonAsync<...>();
      ```

## CORS: cross-origin resource sharing

Een AJAX-call doen vanaf domein A naar domein B.
- bla.nl => ding.nl
- sub1.bla.nl => sub2.bla.nl
- bla.nl:15001 => bla.nl:8589

Is een security-feature _in de browser_
- stuurt een preflight OPTIONS-request om metadata uit te lezen via HTTP header "access-control-allow-origin: http://..."

## Testing

Unit testing
- zo klein mogelijk stukje code
  - 1 methode
- lekker snel   1ms

Integration testing
- kan verschillende vormen aannemen:
  - class A met B
  - HTML renderen van een component
  - database
  - API request

End-to-end testing
- <=================>
- frontend naar backend (inclusief db)
- op een volledig gedeployde omgeving
- Playwright

Manual testing
- mens

## OAuth en tokens

- JWT - uitgesproken als "jot"
  - de populairste in OAuth-land
- SAML - Security Application Markup Language

3 soorten JWT's

- id token: dit is jij. langer in leven   uur+
- access token: om iets te doen bij een API. meestal houdbaar/lifetime: 5 minuten tot een uur
- refresh token: refresh voor access token.
  - voor offline access zonder dat de gebruiker erbij kijken

Te herkennen aan `eyqqqq.wwwwwwwww.eeeeeeeeee`
 
- header (hash-algoritme)
- payload
- signature (zeker zijn dat token onderweg niet is aangepast)

Zonder BFF, in de browser, waar sla je dat token op?

- local storage
  - simpele API - geen bescherming voor XSS - persisted/reflected/DOM?
- session storage
  - simpele API - geen bescherming voor XSS
- indexeddb
  - complexe API - maar nog steeds geen bescherming voor XSS
- cookie
  - HttpOnly - niet meer uitleesbaar via JS (yay geen XSS)
  - XSRF
- in-memory
  - SPA 👍
  - globale variabele / closure
  - druk op F5 en je bent uitgelogd

Zeker weten dat je (persisted) XSS voorkomt kan een reden zijn om alsnog voor een van de storages te kiezen.

Zie ook OWASP: https://cheatsheetseries.owasp.org/cheatsheets/JSON_Web_Token_for_Java_Cheat_Sheet.html#token-storage-on-client-side

## Coole links

- [Awesome Blazor](https://github.com/AdrienTorris/awesome-blazor?tab=readme-ov-file#libraries--extensions)
- [MudBlazor.StaticInput](https://github.com/0phois/MudBlazor.StaticInput)
- [Dependency injection bij Blazor WebAssembly - Scoped is Singleton](https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/dependency-injection?view=aspnetcore-9.0#service-lifetime)
- [Dapper, alternatief op EF Core](https://github.com/DapperLib/Dapper)

