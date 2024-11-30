# [SUPPORT TALK] Hexagonal, Clean, Vertical Slice : 3 architectures pour construire des logiciels facile à maintenir

Ce repository est le code source utilisé pour la présentation du MTG du 30 novembre 2024 faite par [Pierre Gillon](https://github.com/pierregillon)

Il décrit le même périmètre fonctionnel (Meetup) dans plusieurs styles d'architecture.

![Slide de présentation](./resources/cover.png)

Liens :
- [Vidéo Youtube de la captation](https://www.youtube.com/watch?v=xZW-jFhAiXk)
- [Lien de la présentation](https://www.canva.com/design/DAGWbw5KI_Q/TPhIMI9piHKkpLzJi-0fLg/edit?utm_content=DAGWbw5KI_Q&utm_campaign=designshare&utm_medium=link2&utm_source=sharebutton)

## Hexagonal

Présente l'architecture ports et adapter selon le standard proposé d'Alistair Cockburn (son créateur).

- 1 seule layer : l'application
- des driving adapters
- des driven adapters

## Clean

Présente la clean architecture de Uncle Bob, simpliée pour l'usage dans une api rest.

- Présentation
- Application
- Domain
- Infrastructure / Data

## Vertical Slice

Présente la vertical slice architecture de Jimmy Bogard.

- 1 seule layer pour tout
- Découpage par fonctionnalité
- Abstraction introduite uniquement si besoin

# Points volontairement non abordés

Pour rester focus sur l'objectif du talk de présenter les différentes architectures, les points suivants ont volontairement été mis de côté :

- CQRS : séparation des commands et des queries
- DDD : organisation du métier selon les tactical patterns
- Transactionnalité
