using System.Data.SqlClient;

public class Gamer {
  public int idGamer { get; set; } 
  public int side { get; set; }
  public int score = 0;
  public Team? team { get; set; }

  public Gamer(int idGamer, int side) {
    this.InitAttributes(idGamer, side);
  }

  public void PaintGamerTeam(PaintEventArgs e) {
    this.team.PaintTeam(e);
  }

  public void InitAttributes(int idGamer, int side) {
    this.idGamer = idGamer;
    this.side = side;
    this.team = new Team(this.side, this.GetColor());
  }

  public Color GetColor() {
    if (this.side == 1) {
      return Color.FromArgb(87, 95, 207);
    } 
    return Color.FromArgb(239, 87, 119);
  }

  public double findMoney() {
    double reponse = 0;
    Connect connect = new Connect();
    SqlConnection connection = connect.getConnection();
    string query = "SELECT money FROM gamer WHERE idGamer = '" + this.idGamer + "'";
    SqlCommand command = new SqlCommand(query, connection);
    SqlDataReader reader = command.ExecuteReader();
    int money = reader.GetOrdinal("money");
    while (reader.Read()) {
      reponse = reader.GetDouble(money);
    }
    reader.Close();
    connection.Close();

    return reponse;
  }
}