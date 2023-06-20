using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Mongo
{
    public static class Extensions
    {
        /// <summary>
        /// Estender o container de Serviços do .NET Core para Utilizar o MongoDB
        /// </summary>
        public static void AddMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            //Adicionar uma configuração ao Container de Serviços, puxando dados do 
            //appsettings.json (MongoOptions é uma classe que representa as configurações)
            services.Configure<MongoOptions>(configuration.GetSection("mongo"));

            //Instanciar uma classe MongoClient (Singleton)
            services.AddSingleton<MongoClient>(c =>
            {
                var options = c.GetService<IOptions<MongoOptions>>();
                return new MongoClient(options.Value.ConnectionString); 
            });

            //Instanciar uma classe MongoDatabase
            services.AddScoped<IMongoDatabase>(c =>
            {
                var client = c.GetService<MongoClient>();
                var options = c.GetService<IOptions<MongoOptions>>();
                return client.GetDatabase(options.Value.Database);
            });

            //Seeder do MongoDB
            services.AddScoped<IDatabaseSeeder, MongoSeeder>();

            //Concenções do MongoDB
            services.AddScoped<IDatabaseInitializer, MongoInitializer>();

            Console.WriteLine("Registrado Mongo");
        }
    }
}
