using IntegrationServer.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using System.Security.Cryptography;
using System.Text;

namespace IntegrationServer.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Early init of NLog to allow startup and exception logging, before host is built
            var pathToNLogConfig = "NLogConfigFile/nlog.config";
            NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(pathToNLogConfig);
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //HTTP Client Injection...
            builder.Services.AddScoped<IntegrationServices>();
            builder.Services.AddHttpClient();

            // NLog: Setup NLog for Dependency injection
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {

                var rsaKey = RSA.Create();
                string xmlKey = File.ReadAllText(builder.Configuration.GetSection("Jwt:PublicKeyPath").Value);
                rsaKey.FromXmlString(xmlKey);

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new RsaSecurityKey(rsaKey),
                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)),

                };
            }
            );

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
