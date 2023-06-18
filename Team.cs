public class Team {
  public int side { get; set; }
  public List<Line>? lines { get; set; }
  public Color color { get; set; }
  public Line focus { get; set; }
  public int focusIndex = 0;

  public Team(int side, Color color) {
    this.InitAttributes(side, color);
  }

  public void InitAttributes(int side, Color color) {
    this.side = side;
    this.color = color;
    this.InitLines();
  }

  public void InitLines() {
    this.lines = new List<Line>();
    if (this.side == 1) {
      this.InitLeftSide();
    } else {
      this.InitRightSide();
    }
    this.SetFocus();
  }

  public void InitLeftSide() {
    List<int> xList = this.LeftSideX();
    this.lines.Add(new Line(xList[0], 1, this.color));
    this.lines.Add(new Line(xList[1], 3, this.color));
    this.lines.Add(new Line(xList[2], 5, this.color));
    this.lines.Add(new Line(xList[3], 3, this.color));
  }

  public void InitRightSide() {
    List<int> xList = this.RightSideX();
    this.lines.Add(new Line(xList[0], 1, this.color));
    this.lines.Add(new Line(xList[1], 3, this.color));
    this.lines.Add(new Line(xList[2], 5, this.color));
    this.lines.Add(new Line(xList[3], 3, this.color));
  }

  public void AddGoal() {
  }

  public void DrawTeam(PaintEventArgs e) {
    for (int i = 0; i < this.lines.Count; i++) {
      this.lines[i].PaintLine(e);
    } 
  }

  public List<int> LeftSideX() {
    List<int> leftSideX = new List<int>();
    leftSideX.Add(80);
    leftSideX.Add(220);
    leftSideX.Add(550);
    leftSideX.Add(890);
    return leftSideX;
  }

  public List<int> RightSideX() {
    List<int> rightSideX = new List<int>();
    rightSideX.Add(1190);
    rightSideX.Add(1040);
    rightSideX.Add(720);
    rightSideX.Add(380);
    return rightSideX;
  }

  public void PaintTeam(PaintEventArgs e) {
    for (int i = 0; i < this.lines.Count; i ++) {
      this.lines[i].PaintLine(e);
    }
    Brush brush = new SolidBrush(this.color);
    e.Graphics.FillEllipse(brush, new Rectangle(this.focus.x - 5, 10, 10, 10));
  }

  public void SetFocus() {  
    this.focus = this.lines[this.focusIndex];
  }

  public void SetFocusOnFront() {
    if (this.focusIndex != this.lines.Count - 1) {
      this.focusIndex += 1;
      this.SetFocus();
    }
  }

  public void SetFocusOnBehind() {
    if (this.focusIndex != 0) {
      this.focusIndex -= 1;
      this.SetFocus();
    }
  }

  public List<Player> AllPlayers () {
    List<Player> players = new List<Player>();
    foreach (Line line in this.lines) {
      foreach (Player player in line.players) {
        players.Add(player);
      }
    }
    return players;
  }

  public bool HasBall() {
    foreach (Line line in this.lines) {
      if (line.HasBall()) return true;
    }
    return false;
  }
}