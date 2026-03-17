using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using DataAcessLayer.Concrete;
using DataAcessLayer.Abstract;
using BussinessLayer.Abstract;
using BussinessLayer.Concrete;
using BussinessLayer.Settings;
using DataAcessLayer.SeedData;

// Load .env file
DotNetEnv.Env.Load();

// Fix for Npgsql DateTime issue
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// ==================== Kestrel - Büyük dosya yükleme desteği ====================
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 500 * 1024 * 1024; // 500MB
});

// ==================== JWT Settings ====================
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.SectionName));

// ==================== Email Settings ====================
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection(EmailSettings.SectionName));

// ==================== Google Analytics Settings ====================
builder.Services.Configure<GoogleAnalyticsSettings>(builder.Configuration.GetSection(GoogleAnalyticsSettings.SectionName));

var jwtSettings = builder.Configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()!;

// ==================== Authentication ====================
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
        ClockSkew = TimeSpan.Zero
    };

    // Cookie'den token okuma
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            // accessToken cookie'sinden JWT'yi oku
            if (context.Request.Cookies.ContainsKey("accessToken"))
            {
                context.Token = context.Request.Cookies["accessToken"];
            }
            return Task.CompletedTask;
        }
    };
});

// ==================== Authorization ====================
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User", "Admin"));
});

// ==================== DB Context ====================
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(connectionString));

// ==================== Dependency Injection ====================
// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddSingleton<IImageService>(sp =>
    new ImageService(builder.Environment.ContentRootPath));
builder.Services.AddScoped<IListingService, ListingService>();
builder.Services.AddScoped<IQuoteService, QuoteService>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();
builder.Services.AddScoped<ISiteContentService, SiteContentService>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

// ==================== CORS ====================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "http://localhost:3000",
            "http://localhost:5173",
            "https://erdemden.com",
            "https://www.erdemden.com",
            "https://testerdemden.d1-tech.com"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();  // Cookie göndermek için gerekli
    });
});

// ==================== Response Compression ====================
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

// ==================== Controllers & Swagger ====================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Erdemden API", Version = "v1" });
});

var app = builder.Build();

// ==================== Database Migration & Seed ====================
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Context>();
    await context.Database.MigrateAsync();
    await SeedDatabase.InitializeAsync(context);
}

// ==================== Middleware Pipeline ====================

// Global Exception Handler - Stack trace sızıntısını önle
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var response = new { success = false, message = "Sunucu hatası oluştu. Lütfen daha sonra tekrar deneyin." };
        await context.Response.WriteAsJsonAsync(response);
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseResponseCompression();

// Uploads klasörünü static files olarak sun (görseller)
var uploadsPath = Path.Combine(app.Environment.ContentRootPath, "uploads");
Directory.CreateDirectory(uploadsPath);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(uploadsPath),
    RequestPath = "/uploads",
    OnPrepareResponse = ctx =>
    {
        // Görseller için 30 gün cache
        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=2592000");
    }
});

app.UseCors("AllowFrontend");

app.UseAuthentication();  // JWT doğrulama
app.UseAuthorization();   // Yetkilendirme

app.MapControllers();

app.Run();
