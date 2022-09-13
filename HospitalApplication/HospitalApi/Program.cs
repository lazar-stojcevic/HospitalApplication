using Amazon;
using Amazon.DynamoDBv2;
using FastEndpoints;
using FastEndpoints.Security;
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
builder.Services.AddAuthenticationJWTBearer(builder.Configuration.GetSection("Secret:Token").Value);
builder.Services.AddSwaggerDoc();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IAmazonDynamoDB>(_ => new AmazonDynamoDBClient(RegionEndpoint.EUCentral1));

builder.Services.AddSingleton<IPatientRepository>(provider =>
    new PatientRepository(provider.GetRequiredService<IAmazonDynamoDB>(), config.GetValue<string>("Database:TableName")));
builder.Services.AddSingleton<IAccountRepository>(provider =>
    new AccountRepository(provider.GetRequiredService<IAmazonDynamoDB>(), config.GetValue<string>("Database:AccountTable")));
builder.Services.AddSingleton<IDoctorRepository>(provider =>
    new DoctorRepository(provider.GetRequiredService<IAmazonDynamoDB>(), config.GetValue<string>("Database:DoctorsTable")));
builder.Services.AddSingleton<IAppointmentRepository>(provider =>
    new AppointmentRepository(provider.GetRequiredService<IAmazonDynamoDB>(), config.GetValue<string>("Database:AppointmentsTable")));
builder.Services.AddSingleton<IAccountantRepository>(provider =>
    new AccountantRepository(provider.GetRequiredService<IAmazonDynamoDB>(), config.GetValue<string>("Database:AccountantsTable")));
builder.Services.AddSingleton<IAdminRepository>(provider =>
    new AdminRepository(provider.GetRequiredService<IAmazonDynamoDB>(), config.GetValue<string>("Database:AdminsTable")));

builder.Services.AddSingleton<IPatientService, PatientService>();
builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<IDoctorService, DoctorService>();
builder.Services.AddSingleton<IAppointmentService, AppointmentService>();
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
builder.Services.AddSingleton<IAnonymizationService, AnonymizationService>();
builder.Services.AddSingleton<IAccountantService, AccountantService>();
builder.Services.AddSingleton<IAdminService, AdminService>();

builder.Services.AddCors(o => o.AddDefaultPolicy(b => b
                                                        .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                                                            .AllowAnyHeader()
                                                                .AllowAnyMethod()
));

var app = builder.Build();

app.UseMiddleware<ValidationExceptionMiddleware>();

app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());

app.UseAuthentication();
app.UseCors(opt => opt.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost"));
app.UseRouting();
app.UseAuthorization();

app.UseFastEndpoints(x =>
{
    x.Errors.ResponseBuilder = (failures, _) =>
    {
        return new ValidationFailureResponse
        {
            Errors = failures.Select(y => y.ErrorMessage).ToList()
        };
    };
});

app.Run();
