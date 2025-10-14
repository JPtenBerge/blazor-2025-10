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