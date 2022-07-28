using Amazon;
using Amazon.DynamoDBv2;
using FastEndpoints;
using FastEndpoints.Swagger;
using HospitalApi.Contracts.Responses;
using HospitalApi.Repositories;
using HospitalApi.Repositories.Interfaces;
using HospitalApi.Services;
using HospitalApi.Services.Interfaces;
using HospitalApi.Validation;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();

builder.Services.AddSingleton<IAmazonDynamoDB>(_ => new AmazonDynamoDBClient(RegionEndpoint.EUCentral1));

builder.Services.AddSingleton<IPatientRepository>(provider =>
    new PatientRepository(provider.GetRequiredService<IAmazonDynamoDB>(), config.GetValue<string>("Database:TableName")));
builder.Services.AddSingleton<IAccountRepository>(provider =>
    new AccountRepository(provider.GetRequiredService<IAmazonDynamoDB>(), config.GetValue<string>("Database:AccountTable")));
builder.Services.AddSingleton<IDoctorRepository>(provider =>
    new DoctorRepository(provider.GetRequiredService<IAmazonDynamoDB>(), config.GetValue<string>("Database:DoctorsTable")));
builder.Services.AddSingleton<IAppointmentRepository>(provider =>
    new AppointmentRepository(provider.GetRequiredService<IAmazonDynamoDB>(), config.GetValue<string>("Database:AppointmentsTable")));

builder.Services.AddSingleton<IPatientService, PatientService>();
builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<IDoctorService, DoctorService>();
builder.Services.AddSingleton<IAppointmentService, AppointmentService>();

var app = builder.Build();

app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseFastEndpoints(x =>
{
    x.ErrorResponseBuilder = (failures, _) =>
    {
        return new ValidationFailureResponse
        {
            Errors = failures.Select(y => y.ErrorMessage).ToList()
        };
    };
});

app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());

app.Run();
