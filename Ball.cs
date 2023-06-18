public class Ball {
  public int x { get; set; }
  public int y { get; set; }
  public int width { get; set; }
  public int height { get; set; }
  public int xSpeed = 5;
  public int ySpeed = 5;
  public int xDirection = -1;
  public int yDirection = 1;

  public Ball() {
    this.InitSize();
    this.InitPosition();
  }

  public void PaintBall(PaintEventArgs e, Brush painter) {
    e.Graphics.FillEllipse(painter, new Rectangle(this.x, this.y, this.width, this.height));
  }

  public void InitSize() {
    this.width = 15;
    this.height = 15;
  }

  public int xCenter() {
    return this.x - this.width / 2;
  }

  public int yCenter() {
    return this.y - this.height / 2;
  }

  public double DistanceBetweenPlayer(Player player) {
    int a = this.xCenter() - player.xCenter();
    int b = this.yCenter() - player.yCenter();
    return Math.Sqrt((a * a) + (b * b));
  }

  public bool IsPlayerCollision(Player player) {
    return DistanceBetweenPlayer(player) <= this.width / 2 + player.width / 2;
  }

  public void Adjust(Player player) {
    this.x = player.x + (player.width - this.width) / 2;
    this.y = player.y + (player.height - this.height) / 2;
  }

  public void Collision(GroundPanel gPanel) {
    foreach (Player player in gPanel.gamer1.team.AllPlayers()) {
      if (IsPlayerCollision(player)) {
        this.SetAttributesOnCollision(gPanel, player);
      }
    }
    foreach (Player player in gPanel.gamer2.team.AllPlayers()) {
      if (IsPlayerCollision(player)) {
        this.SetAttributesOnCollision(gPanel, player);
      }
    }
  }

  public void SetAttributesOnCollision(GroundPanel gPanel, Player player) {
    gPanel.ResetPlayer();
    this.Stop();
    this.Adjust(player);
    player.hasBall = true;
  }

  public void Stop() {
    this.xSpeed = 0;
    this.ySpeed = 0;
  }

  public void InitPosition() {
    int screenWidth = 1280;
    int screenHeight = 720;
    this.x = (screenWidth / 2 - this.width - this.width / 2);
    this.y = (screenHeight / 2 - this.height - this.height / 2);
  }

  public void SetDirection() {
    if (this.x <= 40 || this.x >= 1230 - this.width) {
      this.xDirection *= -1;
    }
    if (this.y <= 20 || this.y >= 659 - this.width) {
      this.yDirection *= -1;
    }
  }

  public void Goal(GroundPanel gPanel) {
    if (this.x <= 40 && this.y >= 240 && this.y <= 460 - this.height * 2) { 
      gPanel.gamer2.score += 1;
    }
    if (this.x >= 1217 && this.y >= 240 && this.y <= 460 - this.height * 2) {
      gPanel.gamer1.score += 1;
    }
  }

  public void Update() {
    this.y += (this.yDirection * this.ySpeed);
    this.x += (this.xDirection * this.xSpeed);
    this.SetDirection();
  }

  public void Pass(int direction, GroundPanel gPanel) {
    gPanel.ResetPlayer();
    if (direction == 1) this.GoTop();
    if (direction == 2) this.GoBottom();
    if (direction == 3) this.GoLeft();
    if (direction == 4) this.GoRight();
    this.Update();
  }

  public void GoTop() {
    this.ySpeed = 5;
    this.yDirection = -1;
    this.y -= 25;
  }

  public void GoBottom() {
    this.ySpeed = 5;
    this.yDirection = 1;
    this.y += 25;
  }

  public void GoLeft() {
    this.xSpeed = 5;
    this.xDirection = -1;
    this.x -= 25;
  }

  public void GoRight() {
    this.xSpeed = 5;
    this.xDirection = 1;
    this.x += 25;
  }
}