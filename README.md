# Gestão de Resíduos – API ESG com .NET 8

API RESTful desenvolvida em **.NET 8** para gestão de resíduos sólidos, com foco em um cenário ESG: controle de coletas, pontos de coleta, resíduos, veículos e coletores, com autenticação JWT, paginação e boas práticas de arquitetura.

---

## Objetivo do Projeto

O projeto simula uma solução de gestão de resíduos para apoiar empresas/municípios a:

- Registrar **coletas de resíduos** em pontos específicos.
- Controlar **tipos de resíduos**, **veículos** e **coletores**.
- Aplicar **regras de alerta** quando o ponto ultrapassar um limite de peso.
- Disponibilizar **endpoints paginados** para consulta.
- Proteger endpoints sensíveis com **JWT**.

---

## Arquitetura e Organização

A solução segue uma arquitetura em camadas, alinhada ao padrão de separação de responsabilidades:

- **Controllers** → Camada de entrada HTTP (endpoints REST).
- **Services** → Regras de negócio e orquestração (`ColetaService`, `AlertaService`, `RotaService`, `AuthService`).
- **Data** → `AppDbContext` com **Entity Framework Core**.
- **Models / ViewModels** → Entidades de domínio e modelos de requisição/resposta.
- **Config** → Configurações de JWT (`JwtSettings`).
- **Middleware** → `ErrorHandlerMiddleware` para tratamento global de erros.

---

## Tecnologias Utilizadas

- **.NET 8 / ASP.NET Core 8**
- **Entity Framework Core 8**
  - SQL Server LocalDB (ambiente local)
  - InMemoryDatabase (ambiente Docker, para testes)
- **JWT (JSON Web Token)** para autenticação/autorização
- **xUnit** para testes de integração/unidade dos endpoints
- **Swagger / Swashbuckle** para documentação e teste da API
- **Docker** e `docker compose`

---

## Como rodar o projeto (Docker)

Siga os passos abaixo para iniciar a API localmente utilizando Docker:

---

### 🔧 1. Certifique-se de que o **Docker Desktop** está instalado e em execução  
Sem ele, o container não sobe!

### 📁 2. Acesse a pasta raiz do projeto

```bash
cd GestaoResiduosAPI
```

### 📁 3. Execute os comandos Docker
```bash
docker compose build
docker compose up
```

### 📁 4. Acesse a API
Após iniciar os containers, a API estará disponível em:
- **Link** → http://localhost:8080/swagger
Lá você poderá testar todos os endpoints diretamente pelo Swagger UI.

---

# Endpoints da API

## 1. **Autenticação**

### POST `/auth/login`
Gera um token JWT para acessar endpoints protegidos.

#### 📥 Exemplo de Request

```json
{
  "username": "admin",
  "password": "123"
}
```
### Exemplo de Response
```json
{
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6Ikp..."
}
}
```

## 2. **Coletas**
Gerencia os registros de coleta de resíduos, gerando alertas automáticos caso o peso exceda o limite do ponto de coleta.

### POST `/Coletas`
Cria uma nova coleta. Se ultrapassar o limite, um alerta automático é criado.

#### 📥 Exemplo de Request
```json
{
  "residuoId": 1,
  "pontoColetaId": 1,
  "veiculoId": 1,
  "coletorId": 1,
  "pesoKg": 80
}
```

### Exemplo de Response
```json
{
  "id": 1,
  "dataHora": "2025-11-29T01:20:00Z",
  "pesoKg": 80,
  "residuoId": 1,
  "pontoColetaId": 1,
  "veiculoId": 1,
  "coletorId": 1
}
```

### GET /Coletas
Lista as coletas com paginação.

### Exemplo de Response
```json
{
  "page": 1,
  "pageSize": 10,
  "totalItems": 3,
  "totalPages": 1,
  "data": [
    {
      "id": 1,
      "residuo": "Plástico",
      "ponto": "Ponto Central",
      "veiculo": "ABC-1234",
      "coletor": "João da Silva",
      "pesoKg": 80,
      "dataHora": "2025-11-29T01:20:00Z"
    }
  ]
}
```

### GET /Coletas{id}
Retorna o detalhe de uma coleta específica.
#### Exemplo: GET /Coletas/1

---

## 3. **Alertas**
Alertas são criados automaticamente quando o peso de uma coleta ultrapassa o limite do ponto de coleta.

### GET /Coletas
Retorna todos os alertas gerados no sistema.

### Exemplo de Response
```json
[
  {
    "id": 1,
    "pontoColetaId": 1,
    "mensagem": "Limite de 100 kg excedido! Peso coletado: 120 kg.",
    "dataHora": "2025-11-29T02:10:00Z"
  }
]
```

---

## 3. **Rotas**
Calcula uma rota otimizada entre pontos de coleta.

#### 📥 Exemplo de Request

```json
{
  "pontos": [
    { "id": 1, "latitude": -23.5, "longitude": -46.6 },
    { "id": 2, "latitude": -23.6, "longitude": -46.65 }
  ]
}
```

### Exemplo de Response
```json
{
  "distanciaTotalKm": 5.3,
  "estimativaTempoMin": 9,
  "ordemColeta": [1, 2]
}
```

---

