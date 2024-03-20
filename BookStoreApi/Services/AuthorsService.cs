using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services
{
    public class AuthorsService
    {
        private readonly IMongoCollection<Author> _authorsCollection;

        public AuthorsService(
            IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _authorsCollection = mongoDatabase.GetCollection<Author>(
                bookStoreDatabaseSettings.Value.AuthorsCollectionName);
        }

        public async Task<List<Author>> GetAsync() =>
            await _authorsCollection.Find(_ => true).ToListAsync();

        public async Task<Author?> GetAsync(string id) =>
            await _authorsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Author newBook) =>
            await _authorsCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, Author updatedBook) =>
            await _authorsCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _authorsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
