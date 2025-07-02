
using DockerNewPsg.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DockerNewPsg.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _users = database.GetCollection<User>(settings.Value.CollectionName);
        }

        public async Task<List<User>> GetAsync() => await _users.Find(_ => true).ToListAsync();

        public async Task CreateAsync(User user) => await _users.InsertOneAsync(user);
    }
}
