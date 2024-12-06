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
```

### 2. `POST /v1/Favorites`

Cria um novo favorito para o usuário.

**Corpo da Requisição:**

```json
{
  "user_id": "565bfc50-f3a5-44c6-ad2f-27cfe6562142",
  "city": "Lajedo",
  "country": "Brazil",
  "lat": "-8.6636",
  "lon": "-36.32"
}
```

**Resposta:**

```json
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
```

### 3. `DELETE /v1/Favorites/{id}`

Exclui um favorito pelo ID.

**Resposta:**

```json
{
  "message": "Favorito excluído com sucesso."
}
```

### 4. `GET /v1/Weather?lat={latitude}&lon={longitude}`

Retorna as informações do clima para as coordenadas fornecidas.

**Exemplo de resposta:**

```json
{
  "coord": {
    "lon": -8.6636,
    "lat": -36.32
  },
  "weather": [
    {
      "id": 800,
      "main": "Clear",
      "description": "clear sky",
      "icon": "01d"
    }
  ],
  "main": {
    "temp": 289.87,
    "pressure": 1014,
    "humidity": 81
  },
  "wind": {
    "speed": 11.31,
    "deg": 288
  }
}
```

## Instruções de Execução

1. **Instalação**: 
    - Clone o repositório.
    - Instale o .NET SDK: [Instalar .NET](https://dotnet.microsoft.com/download/dotnet).
    - Abra o projeto na sua IDE favorita.
    
2. **Configuração**:
    - Configure suas credenciais da API OpenWeather no arquivo `appsettings.json`.

    ```json
    {
      {
        "AWS": {
          "Region": "us-east-1",
          "AccessKey": "SUA_KEY_AWS",
          "SecretKey": "SUA_SECRET_KEY_AWS"
        },
        "Database": {
          "FavoritesTableName": "NOME_TABELA_PARA_FAVORITOS_DYNAMO_DB",
          "UsersTableName": "NOME_TABELA_PARA_USUARIOS_DYNAMO_DB"
        },
        "Jwt": {
          "Key": "KEY_PARA_AUTENTICACAO",
          "Issuer": "MeuEmissor",
          "Audience": "MeuPublico"
        },
        "Logging": {
          "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
          }
        },
        "Envs": {
          "TokenWeather": "SEU_TOKEN_OPENWEATHERMAP"
        },
        "AllowedHosts": "*"
      }

    }
    ```

3. **Executar a API**:
    - Execute o projeto usando o comando `dotnet run`.
    - A API estará disponível em `https://localhost:5001`.
