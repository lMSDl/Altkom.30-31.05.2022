using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Models;
using Services.Bogus;
using Services.Bogus.Fakers;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var signingKey = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IWebService<WebUser>, WebService<WebUser>>();
builder.Services.AddTransient<EntityFaker<WebUser>, WebUserFaker>();

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.ExpireTimeSpan = TimeSpan.FromSeconds(30);
//        options.LoginPath = "/login";
//        options.AccessDeniedPath = "/bye";
//    });
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(signingKey),
            RequireExpirationTime = true
    };
    });
builder.Services.AddAuthorization();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

//app.Map("/WebUsers", webUsersApp =>
//{
//    webUsersApp.UseRouting();
//    webUsersApp.UseEndpoints(enpoints =>
//    {
//        enpoints.MapGet("/", async context => await context.Response.WriteAsJsonAsync(await context.RequestServices.GetService<IWebService<WebUser>>()!.ReadAsync()));
//        enpoints.MapGet("/{id:int}", async context =>
//        {
//            if (context.Request.RouteValues.TryGetValue("id", out var id))
//            {
//                var entity = await context.RequestServices.GetService<IWebService<WebUser>>()!.ReadAsync(int.Parse(id.ToString()));
//                if (entity != null)
//                    await context.Response.WriteAsJsonAsync(entity);
//            }
//        });
//        enpoints.MapDelete("/{id:int}", async context =>
//        {
//            if (context.Request.RouteValues.TryGetValue("id", out var id))
//            {
//                await context.RequestServices.GetService<IWebService<WebUser>>()!.DeleteAsync(int.Parse(id.ToString()));
//                context.Response.StatusCode = StatusCodes.Status204NoContent;
//            }
//        });
//    });

//});

app.MapGet("/WebUsers", async context => await context.Response.WriteAsJsonAsync(await context.RequestServices.GetService<IWebService<WebUser>>()!.ReadAsync())).RequireAuthorization();
app.MapGet("/WebUsers/{id:int}", async context =>
{
    if (context.Request.RouteValues.TryGetValue("id", out var id))
    {
        var entity = await context.RequestServices.GetService<IWebService<WebUser>>()!.ReadAsync(int.Parse(id.ToString()));
        if (entity != null)
            await context.Response.WriteAsJsonAsync(entity);
    }
});
app.MapDelete("/WebUsers/{id:int}", [Authorize(Roles = "Delete")] async (context) =>
{
    if (context.Request.RouteValues.TryGetValue("id", out var id))
    {
        await context.RequestServices.GetService<IWebService<WebUser>>()!.DeleteAsync(int.Parse(id.ToString()));
        context.Response.StatusCode = StatusCodes.Status204NoContent;
    }
});

app.MapGet("/login", async context =>
{
    if (context.Request.Query.TryGetValue("username", out var username) &&
        context.Request.Query.TryGetValue("password", out var password))
    {
        var users = await context.RequestServices.GetService<IWebService<WebUser>>()!.ReadAsync();
        var user = users
                    .Where(x => x.UserName == username)
                    .SingleOrDefault(x => x.Password == password);


        if (user == null)
            user = users.First();
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                //new Claim(ClaimTypes.Role, "Delete"),
                new Claim(ClaimTypes.Role, "Read")
            };

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor(); 

        tokenDescriptor.Subject = new ClaimsIdentity(claims);
        tokenDescriptor.Expires = DateTime.Now.AddSeconds(30);
        tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256);
        var token = tokenHandler.CreateToken(tokenDescriptor);

        await context.Response.WriteAsync(tokenHandler.WriteToken(token));
    }
});

//app.MapGet("/login", async context =>
//{
//    if (context.Request.Query.TryGetValue("username", out var username) &&
//        context.Request.Query.TryGetValue("password", out var password))
//    {
//        var users = await context.RequestServices.GetService<IWebService<WebUser>>()!.ReadAsync();
//        var user = users
//                    .Where(x => x.UserName == username)
//                    .SingleOrDefault(x => x.Password == password);


//        if (user == null)
//            user = users.First();
//        var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.Name, user.UserName),
//                new Claim(ClaimTypes.Email, user.Email)
//            };

//        var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
//        var claimPrincipal = new ClaimsPrincipal(claimIdentity);
//        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal);
//    }
//});

app.MapGet("/Bye", () => "Bye!");
app.MapGet("/Hello", async context => await context.Response.WriteAsync($"Hello {context.User.Identity.Name}!"));

app.MapGet("/", () => "Hello World!");

app.Run();
