using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Fritz.FFL.Data
{

    public class ProPlayer 
    {

        [BsonId()]
        public ObjectId Id { get; set; }

        [BsonElement("r")]
        public int Rank { get; set; }

        [BsonElement("p")]
        public string Pos { get; set; }

        public string Name { get; set; }

        [BsonElement("nfl")]
        public string NflTeam { get; set; }

        [BsonElement("bye")]
        public int ByeWeek { get; set; }

        [BsonElement("i")]
        public string Injury { get; set; }
        public int Age { get; set; }
        public int Exp { get; set; }
        
        [BsonElement("pts")]
        public decimal ProjectedPoints { get; set; }

        [BsonElement("ffl")]
        public int? FantasyTeamId { get; set; }

        [BsonIgnore]
        public FantasyTeam FantasyTeam
        {
            get
            {
                if (FantasyTeamId.HasValue)
            {
                return FantasyTeam.Teams.First(t => t.Id == FantasyTeamId);
            }
                return null;
            }
            set
            {
                FantasyTeamId = value.Id;
            }
        }

        [BsonIgnore]
        public string FantasyOwner
        {
            get
            {
                return this.FantasyTeam == null ? "" : this.FantasyTeam.Owner;
            }
        }

        public static class Position
        {
            public const string Quarterback = "QB";
            public const string Runningback = "RB";
            public const string WideReceiver = "WR";
            public const string TightEnd = "TE";
            public const string Kicker = "K";
            public const string Defense = "DT";
        }

    }

}
