using System;
using System.Linq;
using Fritz.FFL.Data;
using NUnit.Framework;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace Fritz.FFL.Test
{

    [TestFixture]
    public class AddPlayer
    {

        [Test]
        public void CleanPlayerShouldAdd()
        {
            
            // Arrange
            var line = "1	Drew Brees	NO	7	 	34	12	 436.7";
            var repo = Mock.Create<IPlayerRepository>();
            repo.Arrange(r => r.Add(Arg.Matches<ProPlayer>(p => p.Rank == 1 && 
                p.Name == "Drew Brees" && 
                p.Pos == "QB" && 
                p.NflTeam == "NO" &&
                p.ByeWeek == 7 && 
                p.Age == 34 && 
                p.Exp == 12 && 
                p.ProjectedPoints == 436.7M))).OccursOnce();

            // Act
            var sut = new ImportHandler(repo);
            sut.AddPlayer(line, "QB");

            // Assert
            repo.Assert();

        }

        [Test]
        public void ShouldHandleRookie()
        {

            // Arrange
            var line = "29	E.J. Manuel 	BUF	12	 	23	R	 187.1";
            var repo = Mock.Create<IPlayerRepository>();
            repo.Arrange(r => r.Add(Arg.Matches<ProPlayer>(p => 
                p.Exp == 0))).OccursOnce();

            // Act
            var sut = new ImportHandler(repo);
            sut.AddPlayer(line, "QB");

            // Assert
            repo.Assert();

        }

        [Test]
        public void DefenseShouldAdd()
        {

            // Arrange
            var line = "1	Seattle Defense	SEA	12	 145.0";
            var repo = Mock.Create<IPlayerRepository>();
            repo.Arrange(r => r.Add(Arg.Matches<ProPlayer>(p => p.Rank == 1 &&
                p.Name == "Seattle Defense" &&
                p.NflTeam == "SEA" &&
                p.Pos == "DT" &&
                p.ByeWeek == 12 &&
                p.ProjectedPoints == 145.0M))).OccursOnce();

            // Act
            var sut = new ImportHandler(repo);
            sut.AddPlayer(line, "DT");

            // Assert
            repo.Assert();

        }

    }

}
