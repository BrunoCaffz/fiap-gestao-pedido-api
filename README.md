# 🌱 Gestão de Resíduos – API ESG com .NET 8

API RESTful desenvolvida em **.NET 8** para gestão de resíduos sólidos, com foco em um cenário ESG: controle de coletas, pontos de coleta, resíduos, veículos e coletores, com autenticação JWT, paginação e boas práticas de arquitetura.

---

##Objetivo do Projeto

O projeto simula uma solução de gestão de resíduos para apoiar empresas/municípios a:

- Registrar **coletas de resíduos** em pontos específicos.
- Controlar **tipos de resíduos**, **veículos** e **coletores**.
- Aplicar **regras de alerta** quando o ponto ultrapassar um limite de peso.
- Disponibilizar **endpoints paginados** para consulta.
- Proteger endpoints sensíveis com **JWT**.

---

##Arquitetura e Organização

A solução segue uma arquitetura em camadas, alinhada ao padrão de separação de responsabilidades:

- **Controllers** → Camada de entrada HTTP (endpoints REST).
- **Services** → Regras de negócio e orquestração (`ColetaService`, `AlertaService`, `RotaService`, `AuthService`).
- **Data** → `AppDbContext` com **Entity Framework Core**.
- **Models / ViewModels** → Entidades de domínio e modelos de requisição/resposta.
- **Config** → Configurações de JWT (`JwtSettings`).
- **Middleware** → `ErrorHandlerMiddleware` para tratamento global de erros.

---

## 🛠 Tecnologias Utilizadas

- **.NET 8 / ASP.NET Core 8**
- **Entity Framework Core 8**
  - SQL Server LocalDB (ambiente local)
  - InMemoryDatabase (ambiente Docker, para testes)
- **JWT (JSON Web Token)** para autenticação/autorização
- **xUnit** para testes de integração/unidade dos endpoints
- **Swagger / Swashbuckle** para documentação e teste da API
- **Docker** e `docker compose`

---

## 🔐 Autenticação (JWT)

A autenticação é feita via **JWT Bearer**.

### Configuração

As configurações ficam em `appsettings.json`:

```json
"JwtSettings": {
  "SecretKey": "SUA_CHAVE_SECRETA_AQUI_GRANDE_O_SUFICIENTE",
  "ExpirationMinutes": 60,
  "Issuer": "GestaoResiduosAPI",
  "Audience": "GestaoResiduosAPIUsers"
}
