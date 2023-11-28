using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace appdevfinal.Classes
{
    public class DBConnection
    {
        private IMongoDatabase _database;

        public DBConnection()
        {
            try
            {
                var connectionString = "mongodb://admin:test123@ac-nnq7dcj-shard-00-00.4jhaesp.mongodb.net:27017,ac-nnq7dcj-shard-00-01.4jhaesp.mongodb.net:27017,ac-nnq7dcj-shard-00-02.4jhaesp.mongodb.net:27017/?ssl=true&replicaSet=atlas-e18t97-shard-0&authSource=admin&retryWrites=true&w=majority";

                var mongoClient = new MongoClient(connectionString);

                _database = mongoClient.GetDatabase("homefinder");
                Console.WriteLine("Connection to the database is successful.");
            }
            catch (MongoException ex)
            {
                Console.WriteLine($"Error during database connection: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error during database connection: {ex}");
                throw;
            }
        }

        public IMongoDatabase GetDatabase()
        {
            if( _database == null )
            {
                throw new InvalidOperationException("Database is not initialized. Make sure the MongoDB Database is Connected to your application");
            }

            return _database;
        }

        public async Task<bool> AuthenticateUser(string username, string password)
        {
            try
            {
                var database = GetDatabase();
                var adminsCollection = database.GetCollection<Admins>("admins");
                var user = await adminsCollection.Find(u => u.Username == username).FirstOrDefaultAsync();

                if (user != null)
                {
                    return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during user authentication: {ex.Message}");
                return false;
            }
        }
    }
}
