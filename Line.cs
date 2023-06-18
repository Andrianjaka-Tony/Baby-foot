public class Line {
  public List<Player>? players { get; set; }
  public int nbPlayers { get; set; }
  public int x { get; set; }
  public int y = 20;
  public int width = 1;
  public int height = 639;
  public Color color { get; set; }
  public int speed = 10;

  public Line(int x, int nbPlayers, Color color) {
    this.InitAttributes(x, nbPlayers, color);
    this.InitPlayers();
  }

  public void InitAttributes(int x, int nbPlayers, Color color) {
    this.x = x;
    this.nbPlayers = nbPlayers;
    this.color = color;
  }

  public void InitPlayers() {
    this.players = new List<Player>();
    this.AddPlayer();
  }

  public void AddPlayer() {
    int gap = this.height / (this.nbPlayers + 1);
    int begin = 20 + gap;
    for (int i = 0; i < this.nbPlayers; i++) {
      Player player = new Player(this.x - 10, begin - 10);
      begin += gap;
      this.players.Add(player);
    }
  }

  public void AddGoal() {
    this.nbPlayers += 1;
    this.InitPlayers();
  }

  public void PaintPlayers(PaintEventArgs e) {
    for (int i = 0; i < this.players.Count; i++) {
      this.players[i].PaintPlayer(e, this.color);
    }
  }

  public void PaintLine(PaintEventArgs e) {
    Brush brush = new SolidBrush(this.color);
    e.Graphics.FillRectangle(brush, new Rectangle(this.x, this.y, this.width, this.height));
    this.PaintPlayers(e);
  }

  public int maxTop() {
    return this.players[0].y;
  }

  public int maxBottom() {
    return this.players[this.players.Count - 1].y + 10;
  }

  public void moveOn(GroundPanel gPanel) {
    if (this.maxTop() >= 20) {
      foreach (Player player in this.players) {
        player.y -= this.speed;
        if (player.hasBall) gPanel.ball.Adjust(player);
      }
    }
  }

  public void moveDown(GroundPanel gPanel) {
    if (this.maxBottom() <= 640) {
      foreach (Player player in this.players) {
        player.y += this.speed;
        if (player.hasBall) gPanel.ball.Adjust(player);
      }
    }
  }

  public bool HasBall() {
    foreach (Player player in this.players) {
      if (player.hasBall) return true;
    }
    return false;
  }
}