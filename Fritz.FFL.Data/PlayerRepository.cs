using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace Fritz.FFL.Data
{

    public class PlayerRepository : IPlayerRepository
    {
          
        public void Add(ProPlayer newPlayer)
        {
              
            var coll = GetCollection<ProPlayer>();

            coll.Insert(newPlayer);

        }

        public void Update(ProPlayer player)
        {
            var coll = GetCollection<ProPlayer>();
            var thisPlayer = coll.FindOneByIdAs<ProPlayer>(player.Id);
            thisPlayer.FantasyTeamId = player.FantasyTeamId;
            coll.Save(thisPlayer);
        }

        public IEnumerable<ProPlayer> Get(Func<ProPlayer, bool> where)
        {
            
            var coll = GetCollection<ProPlayer>();

            return coll.FindAllAs<ProPlayer>().Where(where);

        }
    
        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <returns></returns>
        private MongoCollection<T> GetCollection<T>()
        {

            var server = MongoServer.Create("mongodb://localhost:27017");
            var db = server.GetDatabase("ffl");
            return db.GetCollection<T>(typeof(T).Name);

        }
    


    }
  
}