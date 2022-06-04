using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Old_stuff_exchange.Helper;
using Old_stuff_exchange.Repository.Implement;
using Old_stuff_exchange.Repository.Interface;
using Old_stuff_exchange.Service;
using old_stuff_exchange_v2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddDbContext<AppDbContext>();
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("Configs/Firebase/old-stuff-exchange-admindsk.json")
            });

            #region Dependency injection
            services.AddTransient<IUserRepository<User>, UserRepository>();
            services.AddTransient<UserService>();
            services.AddTransient<IRoleRepository<Role>, RoleRepository>();
            services.AddTransient<RoleService>();
            services.AddTransient<IProductRepository<Product>, ProductRepository>();
            services.AddTransient<ProductService>();
            services.AddTransient<IPostRepository<Post>, PostRepository>();
            services.AddTransient<PostService>();
            services.AddTransient<ICategoryRepository<Category>, CategoryRepository>();
            services.AddTransient<CategoryService>();
            services.AddTransient<IBuildingRepository<Building>, BuildingRepository>();
            services.AddTransient<BuildingService>();
            services.AddTransient<IJwtHelper, JwtHelper>();

            // remove when finish app
            services.AddTransient<DatabaseService>();
            #endregion

            var secretKey = Configuration["JWT:Key"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        //tu cap token
                        ValidateIssuer = false,
                        ValidateAudience = false,

                        //signingkey
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                        ClockSkew = TimeSpan.Zero
                    };
                });
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "old_stuff_exchange_v2", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "old_stuff_exchange_v2 v1"));
            

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
