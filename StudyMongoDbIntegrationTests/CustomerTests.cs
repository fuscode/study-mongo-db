using Domain.Customers;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;

namespace StudyMongoDbIntegrationTests
{
    [TestFixture]
    public class CustomerTests : BaseIntegrationTests
    {
        private ICustomerRepository _customerRepository;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _customerRepository = _serviceProvider.GetRequiredService<ICustomerRepository>();
        }

        [SetUp]
        public void SetUp()
        {
            //_customerRepository.de(FilterDefinition<BsonDocument>.Empty);
        }

        [Test]
        public async Task test1()
        {
            // Cria um documento para inserir
            var customer = Customer.Builder.Create("Nome", 18);

            // Insere o documento na coleção
            await _customerRepository.AddAsync(customer);

            // Recupera o documento da coleção
            var retrievedDocument = await _customerRepository.GetAllAsync();

            // Verifica se o documento foi recuperado corretamente
            Assert.NotNull(retrievedDocument);
        }
    }
}
