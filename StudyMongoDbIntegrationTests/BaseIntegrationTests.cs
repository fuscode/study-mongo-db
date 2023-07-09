using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Infrastructure.Data.IoC;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using StudyMongoDbIntegrationTests.Mocks;

namespace StudyMongoDbIntegrationTests
{
    [TestFixture]
    public class BaseIntegrationTests
    {
        //private IContainer _container;
        //private IMongoClient _mongoClient;
        //protected IMongoDatabase _database;
        protected IServiceProvider _serviceProvider;

        [OneTimeSetUp]
        public async Task BaseOneTimeSetUp()
        {
            //_container = new ContainerBuilder()
            //  // Set the image for the container to "mongo"
            //  .WithImage("mongo")
            //  // Bind port 27017 of the container to a random port on the host.
            //  .WithPortBinding(27017, true)
            //  // Wait until the HTTP endpoint of the container is available.
            //  .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(r => r.ForPort(27017)))
            //  // Build the container configuration.
            //  .Build();

            //// Inicia o contêiner
            //await _container.StartAsync().ConfigureAwait(false);

            //// Obtém a string de conexão para o MongoDB com base nas informações do contêiner
            //// Obtém as informações de conexão do contêiner
            //var host = _container.Hostname;
            //var port = _container.GetMappedPublicPort(27017);

            //// Cria a string de conexão para o MongoDB
            //var connectionString = $"mongodb://{host}:{port}";

            //// Cria uma instância do cliente MongoDB
            //_mongoClient = new MongoClient(connectionString);

            //// Obtém uma referência para o banco de dados
            //_database = _mongoClient.GetDatabase("your-database-name");

            IServiceCollection services = new ServiceCollection();
            services.ConfigureDataServices();
            services.AddScoped<IMongoContext, MockMongoContext>();
            _serviceProvider = services.BuildServiceProvider();
        }

        [OneTimeTearDown]
        public async Task BaseOneTimeTearDown()
        {
            // Encerra o contêiner Docker
            //await _container.DisposeAsync();
        }
    }
}
