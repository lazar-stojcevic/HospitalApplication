using System.Net;
using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using HospitalApi.Contracts.Data;

namespace HospitalApi.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly IAmazonDynamoDB _dynamoDb;
    private readonly string _tableName;

    public PatientRepository(IAmazonDynamoDB dynamoDb, string tableName)
    {
        _dynamoDb = dynamoDb;
        _tableName = tableName;
    }

    public async Task<bool> CreateAsync(PatientDto patient)
    {
        var patientAsJson = JsonSerializer.Serialize(patient);
        var itemAsDocument = Document.FromJson(patientAsJson);
        var itemAsAttributes = itemAsDocument.ToAttributeMap();
        var createItemRequest = new PutItemRequest
        {
            TableName = _tableName,
            Item = itemAsAttributes
        };

        var response = await _dynamoDb.PutItemAsync(createItemRequest);
        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    public async Task<PatientDto?> GetAsync(Guid id)
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
        return JsonSerializer.Deserialize<PatientDto>(itemAsDocument.ToJson());
    }

    public async Task<bool> UpdateAsync(PatientDto patient)
    {
        var patientAsJson = JsonSerializer.Serialize(patient);
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

    public async Task<bool> DeleteAsync(Guid id)
    {
        var deleteItemRequest = new DeleteItemRequest
        {
            TableName = _tableName,
            Key = new Dictionary<string, AttributeValue>()
            {
                { "pk", new AttributeValue { S = id.ToString() } },
                { "sk", new AttributeValue { S = id.ToString() } }
            }
        };
        var response = await _dynamoDb.DeleteItemAsync(deleteItemRequest);
        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    public async Task<ICollection<PatientDto>?> GetAllAsync()
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

        var retVal = new List<PatientDto>();

        foreach (var item in response.Items)
        {
            var docItem = Document.FromAttributeMap(item);
            var itemJson = JsonSerializer.Deserialize<PatientDto>(docItem.ToJson());
            if (itemJson != null)
            {
                retVal.Add(itemJson);
            }
        }
        return retVal!;
    }
}
