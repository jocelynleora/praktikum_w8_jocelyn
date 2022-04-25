using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace praktikum_w8_jocelyn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string sqlConnection = "server=localhost; uid=root; pwd=;database=premier_league";
        public MySqlConnection sqlConnect = new MySqlConnection(sqlConnection);
        public MySqlCommand sqlCommand;
        public MySqlDataAdapter sqlAdapter;
        public string sqlQuery;
        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dtTeamHome = new DataTable();
            DataTable dtTeamLawan = new DataTable();
            sqlQuery = "select team_name as 'Nama Tim', team_id as 'ID TEAM' FROM TEAM";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtTeamHome);
            cbox_1.DataSource = dtTeamHome;
            cbox_1.DisplayMember = "Nama Tim";
            cbox_1.ValueMember = "Nama Tim";

            sqlQuery = "select team_name as 'Nama Tim', team_id as 'ID TEAM' FROM TEAM";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtTeamLawan);
            cbox_2.DataSource = dtTeamLawan;
            cbox_2.DisplayMember = "Nama Tim";
            cbox_2.ValueMember = "Nama Tim";
        }

        private void cbox_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTim = new DataTable();
                sqlQuery = "select t.team_name as 'namaTim' , p.player_name , m.manager_name, t.home_stadium, t.capacity from team t, manager m, player p WHERE t.manager_id = m.manager_id and t.captain_id = p.player_id and team_name = '" + cbox_1.SelectedValue + "'";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dtTim);
                lbl_manager1.Text = dtTim.Rows[0][2].ToString();
                lbl_captain1.Text = dtTim.Rows[0][1].ToString();
                lbl_namaStadium.Text = dtTim.Rows[0]["home_stadium"].ToString();
                lbl_kapasitas.Text = dtTim.Rows[0]["capacity"].ToString();
            }
            catch (Exception)
            {


            }
        }

        private void cbox_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTim = new DataTable();
                sqlQuery = "select t.team_name as 'namaTim' , p.player_name , m.manager_name, t.home_stadium, t.capacity from team t, manager m, player p WHERE t.manager_id = m.manager_id and t.captain_id = p.player_id and team_name = '" + cbox_2.SelectedValue + "'";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dtTim);
                lbl_manager2.Text = dtTim.Rows[0][2].ToString();
                lbl_captain2.Text = dtTim.Rows[0][1].ToString();
              
            }
            catch (Exception)
            {


            }
        }

        private void btn_Check_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dateScore = new DataTable();
                sqlQuery = "select date_format(match.match_date, '%e %M %Y'),concat(match.goal_home,' - ',match.goal_away)  from player p, dmatch d,team t, team t2, `match` where d.match_id = match.match_id and p.player_id = d.player_id and (((t.team_name = '" + cbox_1.SelectedValue.ToString() + "'and t2.team_name = '" + cbox_2.SelectedValue.ToString() + "')or (t2.team_name = '" + cbox_1.SelectedValue.ToString() + "' and t.team_name = '" + cbox_2.SelectedValue.ToString() + "')) and ((t.team_id = match.team_home and t2.team_id = match.team_away) or (t.team_id = match.team_away and t2.team_id = match.team_home) )); ";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dateScore);
                lbl_tanggal.Text = dateScore.Rows[0][0].ToString();
                lbl_skor.Text = dateScore.Rows[0][1].ToString();

                DataTable isiTabel = new DataTable();

                sqlQuery = "select d.minute as 'minute', if(p.team_id = match.team_home,p.player_name,' ') as 'Player Name 1',if(p.team_id = match.team_home,if(d.type = 'CY' ,'Yellow Card',if(d.type = 'CR','Red Card',if(d.type = 'GO','Goal',if(d.type = 'GP','Goal Penalty',if(d.type = 'GW','Own Goal','Penalty Miss'))))),' ') as 'Type 1',if(p.team_id = match.team_away,p.player_name,' ') as 'Player Name 2',if(p.team_id = match.team_away,if(d.type = 'CY' ,'Yellow Card',if(d.type = 'CR','Red Card',if(d.type = 'GO','Goal',if(d.type = 'GP','Goal Penalty',if(d.type = 'GW','Own Goal','Penalty Miss'))))),' ') as 'Type 2' from player p, dmatch d,team t, team t2, match where d.match_id = match.match_id and p.player_id = d.player_id and (((t.team_name = '" + cbox_1.SelectedValue.ToString() + "'and t2.team_name = '" + cbox_2.SelectedValue.ToString() + "')or (t2.team_name = '" + cbox_1.SelectedValue.ToString() + "' and t.team_name = '" + cbox_2.SelectedValue.ToString() + "')) and ((t.team_id = match.team_home and t2.team_id = match.team_away) or (t.team_id = match.team_away and t2.team_id = match.team_home) )) group by 1 order by 1; ";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(isiTabel);
                tabelData.DataSource = isiTabel;
            }
            catch (Exception)
            {

                
            }

        }
    }
}
