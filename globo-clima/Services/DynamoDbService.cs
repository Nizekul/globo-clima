using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace globo_clima.Services
{
    public class DynamoDbService
    {
        private readonly IAmazonDynamoDB _dynamoDb;

        public DynamoDbService(IAmazonDynamoDB dynamoDb)
        {
            _dynamoDb = dynamoDb;
        }

        public async Task<List<string>> GetTableNamesAsync()
        {
            var response = await _dynamoDb.ListTablesAsync();
            return response.TableNames;
        }

    }
}
