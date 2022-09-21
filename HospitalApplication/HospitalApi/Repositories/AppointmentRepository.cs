using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using HospitalApi.Contracts.Data;
using HospitalApi.Repositories.Interfaces;
using System.Net;
using System.Text.Json;

namespace HospitalApi.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly IAmazonDynamoDB _dynamoDb;
    private readonly string _tableName;

    public AppointmentRepository(IAmazonDynamoDB dynamoDb, string tableName)
    {
        _dynamoDb = dynamoDb;
        _tableName = tableName;
    }

    public async Task<bool> CreateAsync(AppointmentDto appointment)
    {
        var accountAsJson = JsonSerializer.Serialize(appointment);
        var itemAsDocument = Document.FromJson(accountAsJson);
        var itemAsAttributes = itemAsDocument.ToAttributeMap();
        var createItemRequest = new PutItemRequest
        {
            TableName = _tableName,
            Item = itemAsAttributes
        };

        var response = await _dynamoDb.PutItemAsync(createItemRequest);
        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    public async Task<ICollection<AppointmentDto>?> GetAllAsync()
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

        var retVal = new List<AppointmentDto>();

        foreach (var item in response.Items)
        {
            var docItem = Document.FromAttributeMap(item);
            var itemJson = JsonSerializer.Deserialize<AppointmentDto>(docItem.ToJson());
            if (itemJson != null)
            {
                retVal.Add(itemJson);
            }
        }
        return retVal!;
    }

    public async Task<AppointmentDto?> GetAsync(Guid id)
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
        return JsonSerializer.Deserialize<AppointmentDto>(itemAsDocument.ToJson());
    }

    public async Task<bool> UpdateAsync(AppointmentDto patient)
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

    public async Task<ICollection<AppointmentDto>?> GetAppointmentsForPatient(Guid patientId)
    {
        ScanRequest scanFilter = new()
        {
            TableName = _tableName,
            ConsistentRead = true,
            ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                {":patient", new AttributeValue { S = patientId.ToString() }}
            },
            FilterExpression = "PatientId = :patient",
        };
        var response = await _dynamoDb.ScanAsync(scanFilter);
        if (response.Count == 0)
        {
            return null;
        }

        var retVal = new List<AppointmentDto>();

        foreach (var item in response.Items)
        {
            var docItem = Document.FromAttributeMap(item);
            var itemJson = JsonSerializer.Deserialize<AppointmentDto>(docItem.ToJson());
            if (itemJson != null)
            {
                retVal.Add(itemJson);
            }
        }
        return retVal!;
    }

    public async Task<ICollection<AppointmentDto>?> GetAppointmentsForPatientByDate(Guid patientId, DateTime date)
    {
        var startOfDay = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        var endOfDay = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

        ScanRequest scanFilter = new()
        {
            TableName = _tableName,
            ConsistentRead = true,
            ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                {":patient", new AttributeValue { S = patientId.ToString() } },
                {":start", new AttributeValue { S = startOfDay.ToString("o") } },
                {":end", new AttributeValue { S = endOfDay.ToString("o") } }
            },
            FilterExpression = "PatientId = :patient AND StartTime > :start AND EndTime < :end",
        };
        var response = await _dynamoDb.ScanAsync(scanFilter);
        if (response.Count == 0)
        {
            return null;
        }

        var retVal = new List<AppointmentDto>();

        foreach (var item in response.Items)
        {
            var docItem = Document.FromAttributeMap(item);
            var itemJson = JsonSerializer.Deserialize<AppointmentDto>(docItem.ToJson());
            if (itemJson != null)
            {
                retVal.Add(itemJson);
            }
        }
        return retVal!;
    }

    public async Task<ICollection<AppointmentDto>?> GetAppointmentsForDoctorByDate(Guid doctorId, DateTime date)
    {
        var startOfDay = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        var endOfDay = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

        ScanRequest scanFilter = new()
        {
            TableName = _tableName,
            ConsistentRead = true,
            ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                {":doctor", new AttributeValue { S = doctorId.ToString() } },
                {":start", new AttributeValue { S = startOfDay.ToString("o") } },
                {":end", new AttributeValue { S = endOfDay.ToString("o") } }
            },
            FilterExpression = "DoctorId = :doctor AND StartTime > :start AND EndTime < :end",
        };
        var response = await _dynamoDb.ScanAsync(scanFilter);
        if (response.Count == 0)
        {
            return null;
        }

        var retVal = new List<AppointmentDto>();

        foreach (var item in response.Items)
        {
            var docItem = Document.FromAttributeMap(item);
            var itemJson = JsonSerializer.Deserialize<AppointmentDto>(docItem.ToJson());
            if (itemJson != null)
            {
                retVal.Add(itemJson);
            }
        }
        return retVal!;
    }

    public async Task<ICollection<AppointmentDto>?> GetAppointmentsForDoctor(Guid doctorId)
    {
        ScanRequest scanFilter = new()
        {
            TableName = _tableName,
            ConsistentRead = true,
            ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                {":doctor", new AttributeValue { S = doctorId.ToString() }}
            },
            FilterExpression = "DoctorId = :doctor",
        };
        var response = await _dynamoDb.ScanAsync(scanFilter);
        if (response.Count == 0)
        {
            return null;
        }

        var retVal = new List<AppointmentDto>();

        foreach (var item in response.Items)
        {
            var docItem = Document.FromAttributeMap(item);
            var itemJson = JsonSerializer.Deserialize<AppointmentDto>(docItem.ToJson());
            if (itemJson != null)
            {
                retVal.Add(itemJson);
            }
        }
        return retVal!;
    }

    public async Task<ICollection<AppointmentDto>?> GetUndoneDoctorAppointments(Guid doctorId)
    {
        ScanRequest scanFilter = new()
        {
            TableName = _tableName,
            ConsistentRead = true,
            ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                {":doctor", new AttributeValue { S = doctorId.ToString() }},
            },
            FilterExpression = "DoctorId = :doctor",
        };
        var response = await _dynamoDb.ScanAsync(scanFilter);
        if (response.Count == 0)
        {
            return null;
        }

        var retVal = new List<AppointmentDto>();

        foreach (var item in response.Items)
        {
            var docItem = Document.FromAttributeMap(item);
            var itemJson = JsonSerializer.Deserialize<AppointmentDto>(docItem.ToJson());
            if (itemJson.Report != null)
            {
                continue;
            }
            if (itemJson != null)
            {
                retVal.Add(itemJson);
            }
        }
        return retVal!;
    }
}

