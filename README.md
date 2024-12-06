# API de Favoritos

Esta é uma API RESTful que gerencia favoritos de usuários e fornece dados climáticos de diferentes locais. A API foi construída com C# utilizando o ASP.NET Core.

## Funcionalidades

- **Gerenciamento de Favoritos**: Os usuários podem adicionar, listar e excluir seus favoritos.
- **Obtenção de Dados Climáticos**: A API obtém informações sobre o clima de locais favoritos, utilizando uma integração com a API do OpenWeather.

## Endpoints

### 1. `GET /v1/Favorites`

Retorna todos os favoritos do usuário.

**Resposta:**

```json
[
  {
    "id": "304e5fb0-b3fe-476a-a104-6556c9583fc8",
    "user_id": "565bfc50-f3a5-44c6-ad2f-27cfe6562142",
    "city": "Lajedo",
    "country": "Brazil",
    "lat": "-8.6636",
    "lon": "-36.32",
    "temp_c": "26.4",
    "temp_f": "79.52"
  }
]
