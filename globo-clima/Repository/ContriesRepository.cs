using Amazon.DynamoDBv2;
using globo_clima.Models;
using globo_clima.Settings;
using Microsoft.Extensions.Options;

namespace globo_clima.Repository
{
    public class ContriesRepository
    {
        private readonly IAmazonDynamoDB _dynamoDBClient;
        private readonly IOptions<DatabaseSettings> _databaseSettings;

        public ContriesRepository(IAmazonDynamoDB dynamoDBClient, IOptions<DatabaseSettings> databaseSettings)
        {
            _dynamoDBClient = dynamoDBClient;
            _databaseSettings = databaseSettings;
        }

    }
}
