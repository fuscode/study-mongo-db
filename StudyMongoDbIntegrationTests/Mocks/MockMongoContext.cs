﻿using Domain.Core;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Infrastructure.Data.IoC;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace StudyMongoDbIntegrationTests.Mocks
{
    public class MockMongoContext : IMongoContext
    {
        private IContainer _container;
        private IClientSessionHandle _session;
        private IMongoClient _mongoClient;
        protected IMongoDatabase _database;

        public MockMongoContext()
        {

        }

        public async Task<int> SaveChangesAsync()
        {
            await ConfigureMongoAsync();

            using (_session = await _mongoClient.StartSessionAsync())
            {
                _session.StartTransaction();

                await _session.CommitTransactionAsync();
            }

            return 0;
        }

        private async Task ConfigureMongoAsync()
        {
            if (_mongoClient != null)
            {
                return;
            }

            _container = new ContainerBuilder()
              // Set the image for the container to "mongo"
              .WithImage("mongo")
              // Bind port 27017 of the container to a random port on the host.
              .WithPortBinding(27017, true)
              // Wait until the HTTP endpoint of the container is available.
              .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(r => r.ForPort(27017)))
              // Build the container configuration.
              .Build();

            // Inicia o contêiner
            await _container.StartAsync().ConfigureAwait(false);

            // Obtém a string de conexão para o MongoDB com base nas informações do contêiner
            // Obtém as informações de conexão do contêiner
            var host = _container.Hostname;
            var port = _container.GetMappedPublicPort(27017);

            // Cria a string de conexão para o MongoDB
            var connectionString = $"mongodb://{host}:{port}";

            // Cria uma instância do cliente MongoDB
            _mongoClient = new MongoClient(connectionString);

            // Obtém uma referência para o banco de dados
            _database = _mongoClient.GetDatabase("your-database-name");
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            ConfigureMongoAsync().Wait();

            return _database.GetCollection<T>(typeof(T).Name);
        }

        public void Dispose()
        {
            _session?.Dispose();
            _container?.DisposeAsync();
            GC.SuppressFinalize(this);
        }

    }
}