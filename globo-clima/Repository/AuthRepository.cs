using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using globo_clima.Models;
using globo_clima.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;

namespace globo_clima.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IAmazonDynamoDB _dynamoDBClient;
        private readonly IOptions<DatabaseSettings> _databaseSettings;

        public AuthRepository(IAmazonDynamoDB dynamoDBClient, IOptions<DatabaseSettings> databaseSettings)
        {
            _dynamoDBClient = dynamoDBClient;
            _databaseSettings = databaseSettings;
        }

        public async Task<UserModel?> GetUserByLoginAsync(LoginModel login)
        {
            try
            {
                var request = new ScanRequest
                {
                    TableName = _databaseSettings.Value.UsersTableName,
                    FilterExpression = "email = :emailValue",
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                    {
                        { ":emailValue", new AttributeValue { S = login.Username } }
                    }
                };

                var response = await _dynamoDBClient.ScanAsync(request);

                var user = response.Items.Select(item => new UserModel
                {
                    Id = Guid.Parse(item["id"].S),
                    Name = item["name"].S,
                    Email = item["email"].S,
                    Senha = item["senha"].S, 
                }).FirstOrDefault();

                if (user == null)
                    return null;

                if (!BCrypt.Net.BCrypt.Verify(login.Password, user.Senha))
                {
                    return null; 
                }

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar usuário: {ex.Message}");
                return null;
            }
        }
    }

}
