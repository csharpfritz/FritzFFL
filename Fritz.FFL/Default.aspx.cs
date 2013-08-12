using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fritz.FFL.Data;

namespace Fritz.FFL
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
                GetAverageScores();

        }

        public IQueryable<ProPlayer> GetPlayers()
        {
            var repo = new PlayerRepository();
            return repo.Get(x => true).AsQueryable();

        }

        public IEnumerable<FantasyTeam> GetFantasyTeams()
        {
            return FantasyTeam.Teams.OrderBy(t => t.Id);
        }

        public IEnumerable<ProPlayer> GetTeamPlayers([Control("fflTeam")]int? fantasyOwnerId)
        {

            if (!(fantasyOwnerId.HasValue)) return new ProPlayer[] {};

            var repo = new PlayerRepository();
            return repo.Get(x => x.FantasyTeamId == fantasyOwnerId);
        }

        public void SavePlayerChanges(ProPlayer updated)
        {
            Debug.WriteLine(updated);
            var repo = new PlayerRepository();
            repo.Update(updated);

            GetAverageScores();

        }

        protected void playerGrid_ItemUpdated(object sender, Telerik.Web.UI.GridUpdatedEventArgs e)
        {

        }

        protected void playerGrid_BatchEditCommand(object sender, Telerik.Web.UI.GridBatchEditingEventArgs e)
        {

        }

        protected void fflTeam_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            GetTeamPlayers(Convert.ToInt32( e.Value));
            teamPlayers.DataBind();
        }
  
        private void GetTeamPlayers(int teamId)
        {
            var repo = new PlayerRepository();
            var players = repo.Get(x => x.FantasyTeamId == teamId);

            lblAvgPts.Text = (players.Sum(p => p.ProjectedPoints) / 16M).ToString("0.0");

            teamPlayers.DataSource = players;
        }

        protected void teamPlayers_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            if (string.IsNullOrEmpty( fflTeam.SelectedValue ))
                return;

            GetTeamPlayers(Convert.ToInt32(fflTeam.SelectedValue));
        }

        public void GetAverageScores()
        {
            
            var repo = new PlayerRepository();
            var takenPlayers = repo.Get(p => p.FantasyTeamId != null);

            var agg = takenPlayers.GroupBy(p => p.FantasyOwner).Select(p => new GraphData(p.Key, p.Sum(d => d.ProjectedPoints) / 16M)).OrderByDescending(p => p.AvgPts).ToList();
            avgChart.DataSource = agg;
            avgChart.DataBind();

        }


    }

    public class GraphData
    {

        public GraphData(string n, decimal pts)
        {
            Name = n;
            AvgPts = pts;
        }

        public string Name { get; set; }
        public decimal AvgPts { get; set; }
    }
}