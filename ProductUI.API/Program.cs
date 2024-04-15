using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using ProductUI.API.AutoMapperConfigProfile;
using ProductUI.API.Contextes;
using ProductUI.API.Services;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace ProductUI.API
{
    public class Program
    {
        

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Early init of NLog to allow startup and exception logging, before host is built
            var pathToNLogConfig = "NLogConfigFile/nlog.config";
            NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(pathToNLogConfig);
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init NLog main...");

            // Add services to the container.
            // Add AutoMapper...
            //builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // NLog: Setup NLog for Dependency injection
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();


            builder.Services.AddDbContext<ProductAPIDbContext>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("TestDBConnection")));

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
