using System;

namespace Fritz.FFL.Data
{
    public class ImportHandler
    {

        private IPlayerRepository _Repo;

        public ImportHandler(IPlayerRepository repo)
        {
            this._Repo = repo;
        }

        public void AddPlayer(string playerRecord, string pos)
        {
              
            var details = playerRecord.Split('\t');

            ProPlayer newPlayer;
            if (!pos.Equals(ProPlayer.Position.Defense, StringComparison.InvariantCultureIgnoreCase))
            {
                newPlayer = new ProPlayer()
                {
                    Rank = Convert.ToInt32(details[0]),
                    Name = details[1].Trim(),
                    ByeWeek = Convert.ToInt32(details[3]),
                    Pos = pos,
                    NflTeam = details[2].Trim(),
                    Age = Convert.ToInt32(details[5]),
                    Exp = details[6] == "R" ? 0 : Convert.ToInt32(details[6]),
                    ProjectedPoints = Convert.ToDecimal(details[7])
                };
            } else
            {
                newPlayer = new ProPlayer
                {
                    Rank = Convert.ToInt32(details[0]),
                    Name = details[1].Trim(),
                    NflTeam = details[2].Trim(),
                    Pos = pos,
                    ByeWeek = Convert.ToInt32(details[3]),
                    ProjectedPoints = Convert.ToDecimal(details[4])
                };
            }

            _Repo.Add(newPlayer);
            Console.Out.WriteLine("Added player '{0}' successfully", newPlayer.Name);

        }

    }

}