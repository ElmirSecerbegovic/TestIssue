using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Npgsql;
using System.Threading.Tasks;
using WebApplication1.security;

namespace WebApplication1
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            BuildAppSettingsProvider();
        }

        public void ConfigureServices(IServiceCollection services)
        {

            NpgsqlConnection.GlobalTypeMapper.UseJsonNet();

            services.AddResponseCompression();

            services.AddControllers().AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new DefaultNamingStrategy()
                };

            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IAuthorizationHandler, authorization_handler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Auth", policy => policy.Requirements.Add(new authorization_requirement()));
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        private void BuildAppSettingsProvider()
        {
            AppSettings.connection_string = Configuration.GetSection("connection_strings:db").Value;
        }
    }
}
