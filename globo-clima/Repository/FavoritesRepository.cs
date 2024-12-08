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
    public class FavoritesRepository : IFavoritesRepository
    {
        private readonly IAmazonDynamoDB _dynamoDBClient;
        private readonly IOptions<DatabaseSettings> _databaseSettings;

        public FavoritesRepository(IAmazonDynamoDB dynamoDBClient, IOptions<DatabaseSettings> databaseSettings)
        {
            _dynamoDBClient = dynamoDBClient;
            _databaseSettings = databaseSettings;
        }

        public async Task<List<FavoriteModel>> GetFavoritesByUserIdAsync(Guid userId)
        {
            var request = new ScanRequest
            {
                TableName = _databaseSettings.Value.FavoritesTableName,
                FilterExpression = "user_id = :userId",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":userId", new AttributeValue { S = userId.ToString() } }
                }
            };

            var response = await _dynamoDBClient.ScanAsync(request);

            if (response.Items.Count == 0)
            {
                return new List<FavoriteModel>();
            }

            var favorites = new List<FavoriteModel>();
            foreach (var item in response.Items)
            {
                var document = Document.FromAttributeMap(item);
                var favoriteModel = JsonSerializer.Deserialize<FavoriteModel>(document.ToJson());

                if (favoriteModel != null)
                {
                    favorites.Add(favoriteModel);
                }
            }

            return favorites;
        }

        public async Task<bool> CreateFavoriteAsync(FavoriteModel favorite)
        {
            try
            {
                var itemAsAttributeMap = new Dictionary<string, AttributeValue>
                {
                    { "id", new AttributeValue { S = Guid.NewGuid().ToString() } },
                    { "user_id", new AttributeValue { S = (favorite.UserId).ToString() } },
                    { "city", new AttributeValue { S = favorite.City } },
                    { "lat", new AttributeValue { S = favorite.Lat } },
                    { "lon", new AttributeValue { S = favorite.Lon } },
                };

                var createItemRequest = new PutItemRequest
                {
                    TableName = _databaseSettings.Value.FavoritesTableName,
                    Item = itemAsAttributeMap
                };

                var response = await _dynamoDBClient.PutItemAsync(createItemRequest);

                // Retorna verdadeiro se o status da resposta for OK
                return response.HttpStatusCode == HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Adiciona tratamento de erro, caso necessário
                Console.WriteLine($"Erro ao criar favorito: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteFavoritesAsync(Guid favoriteId, Guid userID)
        {
            var deleteItem = new DeleteItemRequest
            {
                TableName = _databaseSettings.Value.FavoritesTableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "id", new AttributeValue { S = favoriteId.ToString() } },
                    { "user_id", new AttributeValue { S = userID.ToString() } }
                }
            };

            var response = await _dynamoDBClient.DeleteItemAsync(deleteItem);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }

    }

}
