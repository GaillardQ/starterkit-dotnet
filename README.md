# Template .Net

Ceci est le template qui doit être utilisé lors de démarrage de projet d'API en .Net

# Installation
- Renommer les sous-projets 
- Changer les namespaces
- Changer les csproj
- Changer le nom de la solution
- Créer un repo pour la CI/CD 
- Sortir la CI/CD du template et la mettre dans le repo

# Architecture
## Présentation
TODO : schéma mermaid dépendances projets

## Sous-projets
TODO : liste des sous-projets et présentation de chacuns

# Aide
## Migrations
- Créer fichier : ```dotnet ef migrations add AddLineTable```
- Update du schéma : ```dotnet ef database update```
# Roadmap
- [X] Init
- [X] Pipeline CI/CD
- [X] Sonar
- [ ] Migrations
- [ ] Documentation
    - [ ] Installation
    - [ ] Architecture
- [ ] Exemple CRUD
- [ ] Script installation
    - [ ] Renommage sous-projets
    - [ ] Renommage namespaces
    - [ ] Renommage csproj
    - [ ] Renommage solution