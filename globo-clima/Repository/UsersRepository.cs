using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using globo_clima.Models;
using globo_clima.Services;
using globo_clima.Settings;
using Microsoft.Extensions.Options;
using System.Net;

namespace globo_clima.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IAmazonDynamoDB _dynamoDBClient;
        private readonly IOptions<DatabaseSettings> _databaseSettings;

        public UsersRepository(IAmazonDynamoDB dynamoDBClient, IOptions<DatabaseSettings> databaseSettings)
        {
            _dynamoDBClient = dynamoDBClient;
            _databaseSettings = databaseSettings;
        }

        public async Task<bool> CreateUserAsync(UserModel user)
        {
            try
            {
                var userExistente = await GetUserByEmailAsync(user.Email);

                if (userExistente != null)
                {
                    throw new ReplicatedWriteConflictException("Email já cadastrado.");
                }


                user.Senha = BCrypt.Net.BCrypt.HashPassword(user.Senha);

                var itemAsAttributeMap = new Dictionary<string, AttributeValue>
                {
                    { "id", new AttributeValue { S = Guid.NewGuid().ToString() } },
                    { "name", new AttributeValue { S = user.Name } },
                    { "email", new AttributeValue { S = user.Email } },
                    { "senha", new AttributeValue { S = user.Senha } },
                };

                var createItemRequest = new PutItemRequest
                {
                    TableName = _databaseSettings.Value.UsersTableName,
                    Item = itemAsAttributeMap
                };

                var response = await _dynamoDBClient.PutItemAsync(createItemRequest);

                return response.HttpStatusCode == HttpStatusCode.OK;
            }
            catch (ReplicatedWriteConflictException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar Usuario: {ex.Message}");
                throw new Exception("Erro ao criar usuário, tente novamente mais tarde.", ex);
            }
        }

        private async Task<UserModel?> GetUserByEmailAsync(string email)
        {
            var request = new ScanRequest
            {
                TableName = _databaseSettings.Value.UsersTableName,
                FilterExpression = "email = :emailValue",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":emailValue", new AttributeValue { S = email } }
                }
            };

            var response = await _dynamoDBClient.ScanAsync(request);
            var existingUser = response.Items
                .Select(item => new UserModel
                {
                    Id = Guid.Parse(item["id"].S),
                    Name = item["name"].S,
                    Email = item["email"].S,
                    Senha = item["senha"].S,
                })
                .FirstOrDefault();

            return existingUser;
        }

    }

}
