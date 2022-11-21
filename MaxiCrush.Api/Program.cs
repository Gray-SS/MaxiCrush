using MaxiCrush.Api.Common;
using MaxiCrush.Api.Common.Filters;
using MaxiCrush.Api.Common.Mapping;
using MaxiCrush.Api.Extensions;
using MaxiCrush.Application;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(x =>
{
    //x.Filters.Add<GlobalExceptionHandlerFilterAttribute>();
});

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["JWT:Issuer"],
    ValidAudience = builder.Configuration["JWT:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
};

builder.Services.AddSingleton(tokenValidationParameters);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.SaveToken = true;
    o.TokenValidationParameters = tokenValidationParameters;
});
builder.Services.AddAuthorization(x =>
{
    //Roles configuration
    x.AddPermissionPolicy(Permissions.Roles.Commands);
    x.AddPermissionPolicy(Permissions.Roles.Queries);
    
    //Users configuration
    x.AddPermissionPolicy(Permissions.Users.SetRole);
    x.AddPermissionPolicy(Permissions.Users.Commands);
    x.AddPermissionPolicy(Permissions.Users.Queries);

    //Permissions Configuration
    x.AddPermissionPolicy(Permissions.Permission.Queries);
    x.AddPermissionPolicy(Permissions.Permission.Commands);
});

builder.Services.AddApplication(builder.Configuration)
                .AddMapping()
                .AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
