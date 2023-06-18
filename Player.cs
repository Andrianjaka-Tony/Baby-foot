public class Player {

  public int x { get; set; }
  public int y { get; set; }
  public int width { get; set; }
  public int height { get; set; }
  public bool hasBall = false;

  public Player(int x, int y) {
    this.x = x;
    this.y = y;
    this.initSize();
  }

  public void initSize() {
    this.width = 20;
    this.height = 20;
  }

  public int xCenter() {
    return this.x - this.width / 2;
  }

  public int yCenter() {
    return this.y - this.height / 2;
  }

  public void PaintPlayer(PaintEventArgs e, Color color) {
    Brush brush = new SolidBrush(color);
    e.Graphics.FillEllipse(brush, new Rectangle(this.x, this.y, this.width, this.height));
  }
}