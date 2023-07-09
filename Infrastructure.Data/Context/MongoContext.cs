using Infrastructure.Data.Repositories;
using MongoDB.Driver;

namespace Infrastructure.Data.Context
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoClient MongoClient { get; set; }

        public MongoContext()
        {

            // Every command will be stored and it'll be processed at SaveChanges
        }

        public async Task<int> SaveChangesAsync()
        {
            ConfigureMongo();

            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();

                await Session.CommitTransactionAsync();
            }

            return 0;
        }

        private void ConfigureMongo()
        {
            if (MongoClient != null)
            {
                return;
            }

            // Configure mongo (You can inject the config, just to simplify)
            MongoClient = new MongoClient("MongoSettings:Connection");

            Database = MongoClient.GetDatabase("MongoSettings:DatabaseName");
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            ConfigureMongo();

            return Database.GetCollection<T>(typeof(T).Name);
        }

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}