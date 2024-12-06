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

        public async Task<List<FavoritesModel>> GetAllFavoritesAsync()
        {
            var request = new ScanRequest
            {
                TableName = _databaseSettings.Value.TableName
            };

            var response = await _dynamoDBClient.ScanAsync(request);

            if (response.Items.Count == 0)
            {
                return new List<FavoritesModel>();
            }

            var favorites = new List<FavoritesModel>();
            foreach (var item in response.Items)
            {
                var document = Document.FromAttributeMap(item);
                var favoritesModel = JsonSerializer.Deserialize<FavoritesModel>(document.ToJson());
                if (favoritesModel != null)
                {
                    favorites.Add(favoritesModel);
                }
            }

            return favorites;
        }

        public async Task<bool> CreateFavoritesAsync(List<FavoritesModel> favorites)
        {
            var tasks = favorites.Select(async favorite =>
            {
                var itemAsAttributeMap = new Dictionary<string, AttributeValue>
                {
                    { "id", new AttributeValue { S = Guid.NewGuid().ToString() } }, 
                    { "user_id", new AttributeValue { S = Guid.NewGuid().ToString() } },
                    { "city", new AttributeValue { S = favorite.City } },
                    { "country", new AttributeValue { S = favorite.Country } },
                    { "lat", new AttributeValue { S = favorite.Lat } },
                    { "lon", new AttributeValue { S = favorite.Lon } },
                };

                var createItemRequest = new PutItemRequest
                {
                    TableName = _databaseSettings.Value.TableName,
                    Item = itemAsAttributeMap
                };

                var response = await _dynamoDBClient.PutItemAsync(createItemRequest);
                return response.HttpStatusCode == HttpStatusCode.OK;
            });

            var results = await Task.WhenAll(tasks);

            return results.All(success => success);
        }

        public async Task<bool> DeleteFavoritesAsync(List<Guid> favoritesIDs)
        {
            var tasks = favoritesIDs.Select(async favoriteID =>
            {
                var deleteItem = new DeleteItemRequest
                {
                    TableName = _databaseSettings.Value.TableName,
                    Key = new Dictionary<string, AttributeValue>
                    {
                        { "id", new AttributeValue { S = favoriteID.ToString() } }
                    }
                };

                var response = await _dynamoDBClient.DeleteItemAsync(deleteItem);
                return response.HttpStatusCode == HttpStatusCode.OK;
            });

            var results = await Task.WhenAll(tasks);

            return results.All(success => success);
        }

        public async Task<bool> UpdateFavoritesAsync(List<FavoritesModel> favorites)
        {
            var tasks = favorites.Select(async favorite =>
            {
                var itemAsAttributeMap = new Dictionary<string, AttributeValue>
                {
                    { "id", new AttributeValue { S = Guid.NewGuid().ToString() } },
                    { "user_id", new AttributeValue { S = Guid.NewGuid().ToString() } },
                    { "city", new AttributeValue { S = favorite.City } },
                    { "country", new AttributeValue { S = favorite.Country } },
                    { "lat", new AttributeValue { S = favorite.Lat } },
                    { "lon", new AttributeValue { S = favorite.Lon } },
                };

                var createItemRequest = new PutItemRequest
                {
                    TableName = _databaseSettings.Value.TableName,
                    Item = itemAsAttributeMap
                };

                var response = await _dynamoDBClient.PutItemAsync(createItemRequest);
                return response.HttpStatusCode == HttpStatusCode.OK;
            });

            var results = await Task.WhenAll(tasks);

            return results.All(success => success);
        }

    }

}
