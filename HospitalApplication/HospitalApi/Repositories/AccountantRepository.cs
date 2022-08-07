using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using HospitalApi.Contracts.Data;
using HospitalApi.Repositories.Interfaces;
using System.Net;
using System.Text.Json;

namespace HospitalApi.Repositories;

public class AccountantRepository : IAccountantRepository
{
    private readonly IAmazonDynamoDB _dynamoDb;
    private readonly string _tableName;

    public AccountantRepository(IAmazonDynamoDB dynamoDb, string tableName)
    {
        _dynamoDb = dynamoDb;
        _tableName = tableName;
    }
    public async Task<bool> CreateAsync(AccountantDto accountant)
    {
        var accountantAsJson = JsonSerializer.Serialize(accountant);
        var itemAsDocument = Document.FromJson(accountantAsJson);
        var itemAsAttributes = itemAsDocument.ToAttributeMap();
        var createItemRequest = new PutItemRequest
        {
            TableName = _tableName,
            Item = itemAsAttributes
        };

        var response = await _dynamoDb.PutItemAsync(createItemRequest);
        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    public async Task<ICollection<AccountantDto>?> GetAllAsync()
    {
        ScanRequest scanFilter = new()
        {
            TableName = _tableName,
            ConsistentRead = true
        };
        var response = await _dynamoDb.ScanAsync(scanFilter);
        if (response.Count == 0)
        {
            return null;
        }

        var retVal = new List<AccountantDto>();

        foreach (var item in response.Items)
        {
            var docItem = Document.FromAttributeMap(item);
            var itemJson = JsonSerializer.Deserialize<AccountantDto>(docItem.ToJson());
            if (itemJson != null)
            {
                retVal.Add(itemJson);
            }
        }
        return retVal!;
    }

    public async Task<AccountantDto?> GetAsync(Guid id)
    {
        var getItemRequest = new GetItemRequest
        {
            TableName = _tableName,
            Key = new Dictionary<string, AttributeValue>()
            {
                { "pk", new AttributeValue { S = id.ToString() } },
                { "sk", new AttributeValue { S = id.ToString() } }
            },
            ConsistentRead = true
        };

        var response = await _dynamoDb.GetItemAsync(getItemRequest);
        if (response.Item.Count == 0)
        {
            return null;
        }

        var itemAsDocument = Document.FromAttributeMap(response.Item);
        return JsonSerializer.Deserialize<AccountantDto>(itemAsDocument.ToJson());
    }

    public async Task<AccountantDto?> GetByUsername(string username)
    {
        ScanRequest scanFilter = new()
        {
            TableName = _tableName,
            ConsistentRead = true,
            ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                {":username", new AttributeValue { S = username }}
            },
            FilterExpression = "Username = :username",
        };
        var response = await _dynamoDb.ScanAsync(scanFilter);
        if (response.Count == 0)
        {
            return null;
        }

        var docItem = Document.FromAttributeMap(response.Items.FirstOrDefault());
        var itemJson = JsonSerializer.Deserialize<AccountantDto>(docItem.ToJson());

        return itemJson;
    }

    public async Task<bool> UpdateAsync(AccountantDto accountant)
    {
        var patientAsJson = JsonSerializer.Serialize(accountant);
        var itemAsDocument = Document.FromJson(patientAsJson);
        var itemAsAttributes = itemAsDocument.ToAttributeMap();
        var updateItemRequest = new PutItemRequest
        {
            TableName = _tableName,
            Item = itemAsAttributes
        };

        var response = await _dynamoDb.PutItemAsync(updateItemRequest);
        return response.HttpStatusCode == HttpStatusCode.OK;
    }
}

