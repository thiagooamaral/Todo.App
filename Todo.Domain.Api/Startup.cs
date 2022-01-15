using Todo.Domain.Handlers;
using Todo.Domain.Respositories;
using Todo.Domain.Infra.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Infra.Repositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Todo.Domain.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // AddTransient:
            // Resolve a depedência sempre criando um novo item (nova instância)
            // Durante uma requisição, a cada vez que for chamado uma depedência, será criado um novo
            // Não é interessante para banco de dados por exemplo (lembrando que banco tem quantidade de conexões)

            // AddScoped:
            // Cria uma espécie de Singleton mas para cada requisição
            // A cada requisição, a instância do objeto é colocada na memória
            // Toda vez que precisar deste objeto (durante a requisição), ele estará disponível na memória
            // No fim da requisição, a instância é finalizada automaticamente

            // AddSingleton: 
            // Irá prover uma instância do objeto para a aplicação toda (irá manter na memória)
            // Como a API não mantem estado (precisamos sempre de autenticação), neste caso não é interessante

            //services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("connectionString")));

            services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddTransient<TodoHandler, TodoHandler>();

            services
               .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.Authority = "https://securetoken.google.com/project-8363852176002809647";
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidIssuer = "https://securetoken.google.com/project-8363852176002809647",
                       ValidateAudience = true,
                       ValidAudience = "project-8363852176002809647",
                       ValidateLifetime = true
                   };
               });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
