using System.Data.SqlClient;

public class GroundPanel : Panel {
  public Ball? ball { get; set; }
  private System.Windows.Forms.Timer timer { get; set; }
  public int fps = 1000;
  public Brush ballBrush = new SolidBrush(Color.Black);
  Brush line = new SolidBrush(Color.FromArgb(51, 51, 51));
  public Gamer gamer1 { get; set; }
  public Gamer gamer2 { get; set; }
  public int gameMode { get; set; }
  public double jeton = 100;
  public double fond = 1000;
  public int maxScore { get; set; }
  public bool start { get; set; }
  public Ground parent { get; set; }
  public bool stop = false;

  public GroundPanel(Ground parent) {
    this.parent = parent;
    this.InitPanel();
  }

  protected override void OnPaint(PaintEventArgs e) {
    base.OnPaint(e);
    this.DrawGoals(e);
    this.DrawGround(e);
    this.PaintGamers(e);
    this.PaintScore(e);
    if (this.ball != null) {
      this.ball.PaintBall(e, ballBrush);
    }
  }

  public void PaintScore(PaintEventArgs e) {
    String text = this.gamer1.score + " - " + this.gamer2.score;
    Font font = new Font("Poppins", 10);
    Brush brush = new SolidBrush(Color.Black);
    PointF point = new PointF(40, 2);
    e.Graphics.DrawString(text, font, brush, point);
  }

  public void PaintGamers(PaintEventArgs e) {
    this.gamer1.PaintGamerTeam(e);
    this.gamer2.PaintGamerTeam(e);
  }

  public void DrawGround(PaintEventArgs e) {
    this.DrawHorizontalLines(e);
    this.DrawVerticalLines(e);
  }

  public void DrawHorizontalLines(PaintEventArgs e) {
    e.Graphics.FillRectangle(this.line, new Rectangle(40, 20, 1190, 1));
    e.Graphics.FillRectangle(this.line, new Rectangle(40, 659, 1190, 1));
  }

  public void DrawVerticalLines(PaintEventArgs e) {
    e.Graphics.FillRectangle(this.line, new Rectangle(40, 20, 1, 639));
    e.Graphics.FillRectangle(this.line, new Rectangle(1230, 20, 1, 639));
  }

  public void DrawGoals(PaintEventArgs e) {
    int yPosition = this.Height / 2 - 100;

    e.Graphics.FillRectangle(this.line, new Rectangle(30, yPosition, 10, 200));
    e.Graphics.FillRectangle(this.line, new Rectangle(1230, yPosition, 10, 200));
  }

  public void InitTimer() {
    this.timer = new System.Windows.Forms.Timer();
    this.timer.Interval = 1000 / this.fps;
    this.timer.Tick += Timer_tick;
    this.timer.Start();
  }

  public void Timer_tick(Object sender, EventArgs e) {
    if (sender != null && this.ball != null) {
      this.ball.Update();
      this.ball.Collision(this);
      this.ball.Goal(this);
      this.EndGame();
    }
    this.Invalidate();
    Refresh();
  }

  public void InitPanel() {
    this.DoubleBuffered = true;
    this.BackColor = Color.FromArgb(255, 255, 255);
    this.ball = new Ball();
    this.InitTimer();
    this.gamer1 = new Gamer(1, 1);
    this.gamer2 = new Gamer(2, 2);
    this.Paris();
    this.Start();
  }

  public void LosePay() {
    this.gameMode = 1;
    this.maxScore = 3;
  }

  public void Paris() {
    this.gameMode = 2;
    this.maxScore = 3;
  }

  public void Diff() {
    this.gameMode = 3;
    this.maxScore = 5;
  }

  public void LosePayBegin() {
    this.start = this.gamer1.findMoney() >= this.jeton && this.gamer2.findMoney() >= this.jeton;
  }

  public void ParisBegin() {
    this.start = this.gamer1.findMoney() >= this.fond + this.jeton && this.gamer2.findMoney() >= this.fond + this.jeton;
  }

  public void DiffBegin() {
    this.start = this.gamer1.findMoney() >= this.fond * 5 + this.jeton && this.gamer2.findMoney() >= this.fond * 5 + this.jeton;
  }

  public void Start() {
    if (this.gameMode == 1) LosePayBegin();
    if (this.gameMode == 2) ParisBegin();
    if (this.gameMode == 3) DiffBegin();
  }

  public void LosePayEnd() {
    if (this.gamer1.score == 3 || this.gamer2.score == 3) {
      string match = "insert into match (idGamer1, idGamer2, idGameMode, scoreGamer1, scoreGamer2) values ('" + this.gamer1.idGamer + "', '" + this.gamer2.idGamer + "', '" + this.gameMode + "', '" + this.gamer1.score + "', '" + this.gamer2.score + "')";
      Connect connectMatch = new Connect();
      SqlConnection connection1 = connectMatch.getConnection();
      SqlCommand command1 = new SqlCommand(match, connection1);
      command1.ExecuteNonQuery();

      string query = "";
      if (this.gamer1.score == 3) {
        query = "update gamer set money = (select money from gamer where idGamer = '" + this.gamer2.idGamer + "') - " + this.jeton + " where idGamer = '" + this.gamer2.idGamer + "'";
      } else {
        query = "update gamer set money = (select money from gamer where idGamer = '" + this.gamer1.idGamer + "') - " + this.jeton + " where idGamer = '" + this.gamer1.idGamer + "'";
      }
      this.UpdateCashRegister();
      Connect connect = new Connect();
      SqlConnection connection = connect.getConnection();
      SqlCommand command = new SqlCommand(query, connection);
      command.ExecuteNonQuery();
      this.stop = true;
    }
  }

  public void ParisEnd() {
    if (this.gamer1.score == 3 || this.gamer2.score == 3) {
      string match = "insert into match (idGamer1, idGamer2, idGameMode, scoreGamer1, scoreGamer2) values ('" + this.gamer1.idGamer + "', '" + this.gamer2.idGamer + "', '" + this.gameMode + "', '" + this.gamer1.score + "', '" + this.gamer2.score + "')";
      Connect connectMatch = new Connect();
      SqlConnection connection1 = connectMatch.getConnection();
      SqlCommand command1 = new SqlCommand(match, connection1);
      command1.ExecuteNonQuery();

      string query1 = "";
      string query2 = "";
      if (this.gamer1.score == 3) {
        query1 = "update gamer set money = " + (this.gamer1.findMoney() + this.fond) + " where idGamer = '" + this.gamer1.idGamer + "'";
        query2 = "update gamer set money = " + (this.gamer2.findMoney() - (this.fond + this.jeton)) + " where idGamer = '" + this.gamer2.idGamer + "'";
      } else {
        query1 = "update gamer set money = " + (this.gamer2.findMoney() + this.fond) + " where idGamer = '" + this.gamer2.idGamer + "'";
        query2 = "update gamer set money = " + (this.gamer1.findMoney() - (this.fond + this.jeton)) + " where idGamer = '" + this.gamer1.idGamer + "'";
      }
      this.UpdateCashRegister();

      Connect connect = new Connect();
      SqlConnection conn1 = connect.getConnection();
      SqlCommand comm1 = new SqlCommand(query1, conn1);
      comm1.ExecuteNonQuery();

      SqlConnection conn2 = connect.getConnection();
      SqlCommand comm2 = new SqlCommand(query2, conn1);
      comm2.ExecuteNonQuery();
      this.stop = true;
    }
  }

  public void End() {
    if (this.gameMode == 1) LosePayEnd();
    if (this.gameMode == 2) ParisEnd();
  }

  public void EndGame() {
    this.End();
    if (this.stop) {
      this.parent.Close();
    }
  }

  public void UpdateCashRegister() {
    string query = "update cashRegister set money = (select top 1 money from cashRegister) + " + this.jeton;
    Connect connect = new Connect();
    SqlConnection connection = connect.getConnection();
    SqlCommand command = new SqlCommand(query, connection);
    command.ExecuteNonQuery();
  }

  public void ResetPlayer() {
    foreach (Player player in this.gamer1.team.AllPlayers()) {
      player.hasBall = false;
    }
    foreach (Player player in this.gamer2.team.AllPlayers()) {
      player.hasBall = false;
    }
  }
}