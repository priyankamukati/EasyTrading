using Microsoft.EntityFrameworkCore;
using TradingPlatform.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(
                o => o.UseNpgsql(builder.Configuration.GetConnectionString("DB_CONNECTION_STRING")
            ));
 

/* builder.Services.AddDbContext<DataContext>(
    o => o.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"))
);
 */
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000",
                          "http://localhost:3001",
                                              "http://www.contoso.com").AllowAnyHeader()
                                                  .AllowAnyMethod(); ;
                      });
});

builder.Services.AddCognitoIdentity();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["AWSCognito:Authority"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});


app.UseAuthentication();
app.UseAuthorization();


app.UseHttpsRedirection();


app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);

app.Run();