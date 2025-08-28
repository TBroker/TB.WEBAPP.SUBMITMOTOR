using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Globalization;
using System.Text;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Features.UploadFile;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystem.Agents;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Compulsories;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Masters;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Payments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Policies;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Quotations;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Voluntaries;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CreateFiles;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Agents;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Payments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Premiums;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Reports;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Rewards;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Verifies;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Notifications;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Payments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Utilities;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Verifies;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Services;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Services.Notifications;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Services.Payments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Agents;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Compulsories;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Masters;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Payments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Policies;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Quotations;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Voluntaries;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CreateFiles;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Data.Agents;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Data.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Data.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Data.Payments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Data.Premiums;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Data.Reports;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Data.Rewards;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Data.Verifies;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Verifies;
using TB.WEBAPP.SUBMITMOTOR.INFRASTRUCTURE.Configurations;
using TB.WEBAPP.SUBMITMOTOR.INFRASTRUCTURE.ExternalServices;
using TB.WEBAPP.SUBMITMOTOR.INFRASTRUCTURE.Services;
using TB.WEBAPP.SUBMITMOTOR.SHARED.Helpers;

var cultureInfo = new CultureInfo("en-GB");

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

// Add limit request body
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 200 * 1024 * 1024; // 200MB
});

// configure strongly typed settings object
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSetting"));
builder.Services.Configure<CoreSystemSetting>(builder.Configuration.GetSection("CoreSystemSetting"));
builder.Services.Configure<CreateFileSetting>(builder.Configuration.GetSection("CreateFileSetting"));
builder.Services.Configure<DataSetting>(builder.Configuration.GetSection("DataWebSiteSetting"));
builder.Services.Configure<PaymentKBankSetting>(builder.Configuration.GetSection("PaymentKBankSetting"));
builder.Services.Configure<PaymentSetting>(builder.Configuration.GetSection("PaymentSetting"));
builder.Services.Configure<ServiceSetting>(builder.Configuration.GetSection("ServiceSetting"));

// Register application services
builder.Services.AddScoped<IAgentTokenUseCase, AgentTokenUseCase>();
builder.Services.AddScoped<IAgentUseCase, AgentUseCase>();
builder.Services.AddScoped<IApiClientService, ApiClientService>();
builder.Services.AddScoped<ICompulsoryUseCase, CompulsoryUseCase>();
builder.Services.AddScoped<ICreateFileUseCase, CreateFileUseCase>();
builder.Services.AddScoped<IInstallmentUseCase, InstallmentUseCase>();
builder.Services.AddScoped<IJwtReaderService, JwtReaderService>();
builder.Services.AddScoped<IMasterUseCase, MasterUseCase>();
builder.Services.AddScoped<IMotorUseCase, MotorUseCase>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IPaymentCoreSystemUseCase, PaymentCoreSystemUseCase>();
builder.Services.AddScoped<IPaymentDataUseCase, PaymentDataUseCase>();
builder.Services.AddScoped<IPaymentInstallmentUseCase, PaymentInstallmentUseCase>();
builder.Services.AddScoped<IPaymentKBankService, PaymentKBankService>();
builder.Services.AddScoped<IPolicyUseCase, PolicyUseCase>();
builder.Services.AddScoped<IPremiumUseCase, PremiumUseCase>();
builder.Services.AddScoped<IQuotationUseCase, QuotationUseCase>();
builder.Services.AddScoped<IReportUseCase, ReportUseCase>();
builder.Services.AddScoped<IRewardUesCase, RewardUseCase>();
builder.Services.AddScoped<IServiceVerifyUseCase, ServiceVerifyUseCase>();
builder.Services.AddScoped<ISubmitMotorUseCase, SubmitMotorUseCase>();
builder.Services.AddScoped<IUtilityHelper, UtilityHelper>();
builder.Services.AddScoped<IVerifyUseCase, VerifyUseCase>();
builder.Services.AddScoped<IVoluntaryUseCase, VoluntaryUseCase>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(UploadFileCommand).Assembly);
});

builder.Services.AddControllersWithViews();

// Add services to the container.
var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
HttpClientFactory.RegisterHttpClients(builder.Services, environmentName);

var app = builder.Build();


// เพิ่ม MIME type สำหรับ .ftl
var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".ftl"] = "text/plain";

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");    
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Append("x-frame-options", "SAMEORIGIN");
    context.Response.Cookies.Append("token", Guid.NewGuid().ToString(), new CookieOptions
    {
        HttpOnly = true,
        Secure = true,
    });
    await next();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();
