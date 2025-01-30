using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using STIVE.CL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();

builder.Services.AddScoped<ApiService>();

// Configuration de l'authentification JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://your-auth-server"; // L'URL de ton serveur d'autorisation
        options.Audience = "YourAPIName";               // L'identifiant de ton API
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,             // Valide le fournisseur d'émission
            ValidIssuer = "https://your-auth-server",  // L'ISSUER
            ValidateAudience = true,           // Valide l'audience
            ValidAudience = "YourAPIName",     // Audience attendu
            ValidateLifetime = true,           // Valide la date d'expiration
            ClockSkew = TimeSpan.Zero          // Pas de décalage d'horloge
        };
    });

builder.Services.AddAuthorization(); // Ajoute la gestion des autorisations

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Ajoute l'authentification ici
app.UseAuthorization();

app.MapRazorPages();

app.Run();