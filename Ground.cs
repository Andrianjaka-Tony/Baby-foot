public class Ground : Form {
  public GroundPanel gPanel { get; set; }
  public bool wKey { get; set; }
  public bool sKey { get; set; }
  public bool aKey { get; set; }
  public bool dKey { get; set; }
  public bool upKey { get; set; }
  public bool downKey { get; set; }
  public bool leftKey { get; set; }
  public bool rightKey { get; set; }
  public bool cKey { get; set; }
  public bool bKey { get; set; }
  public bool jKey { get; set; }
  public bool lKey { get; set; }
  public bool fKey { get; set; }
  public bool vKey { get; set; }
  public bool iKey { get; set; }
  public bool kKey { get; set; }
  public bool eKey { get; set; }
  public bool rKey { get; set; }
  public bool yKey { get; set; }
  public bool uKey { get; set; }
  public bool zKey { get; set; }
  public bool hKey { get; set; }


  public Ground() {
    this.InitForm();
    this.AddPanel();
  }

  public void AddPanel() {
    this.gPanel = new GroundPanel(this);
    this.gPanel.Dock = DockStyle.Fill;
    if (this.gPanel.start) {
      this.Controls.Add(gPanel);
    }
  }

  public void InitForm() {
    this.InitSize();
    this.Text = "Ground";
    this.FormBorderStyle = FormBorderStyle.FixedSingle;
    this.MaximizeBox = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.AddListener();
  }

  public void InitSize() {
    this.Width = 1280;
    this.Height = 720;
  }

  public void AddListener() {
    this.KeyDown += new KeyEventHandler(KeyDownListener);
    this.KeyUp += new KeyEventHandler(KeyUpListener);
  }

  public void UpdateKey() {
    this.MovePlayerLineFocus();
    this.UpdatePlayerFocus();
    this.Pass();
  }

  public void MovePlayerLineFocus() {
    if (wKey) this.gPanel.gamer1.team.focus.moveOn(this.gPanel);
    if (sKey) this.gPanel.gamer1.team.focus.moveDown(this.gPanel);
    if (upKey) this.gPanel.gamer2.team.focus.moveOn(this.gPanel);
    if (downKey) this.gPanel.gamer2.team.focus.moveDown(this.gPanel);
  }

  public void UpdatePlayerFocus() {
    if (aKey) this.gPanel.gamer1.team.SetFocusOnBehind();
    if (dKey) this.gPanel.gamer1.team.SetFocusOnFront();
    if (leftKey) this.gPanel.gamer2.team.SetFocusOnFront();
    if (rightKey) this.gPanel.gamer2.team.SetFocusOnBehind();
  }

  public void Pass() {
    if (this.cKey && this.gPanel.gamer1.team.focus.HasBall()) this.gPanel.ball.Pass(3, this.gPanel);
    if (this.bKey && this.gPanel.gamer1.team.focus.HasBall()) this.gPanel.ball.Pass(4, this.gPanel);
    if (this.jKey && this.gPanel.gamer2.team.focus.HasBall()) this.gPanel.ball.Pass(3, this.gPanel);
    if (this.lKey && this.gPanel.gamer2.team.focus.HasBall()) this.gPanel.ball.Pass(4, this.gPanel);
    if (this.fKey && this.gPanel.gamer1.team.focus.HasBall()) this.gPanel.ball.Pass(1, this.gPanel);
    if (this.vKey && this.gPanel.gamer1.team.focus.HasBall()) this.gPanel.ball.Pass(2, this.gPanel);
    if (this.iKey && this.gPanel.gamer2.team.focus.HasBall()) this.gPanel.ball.Pass(1, this.gPanel);
    if (this.kKey && this.gPanel.gamer2.team.focus.HasBall()) this.gPanel.ball.Pass(2, this.gPanel);
    if (this.zKey) this.gPanel.gamer1.team.lines[0].AddGoal();
    if (this.hKey) this.gPanel.gamer2.team.lines[0].AddGoal();
    this.PassAndSHoot();
  }

  public void PassAndSHoot() {
    if (this.eKey && this.gPanel.gamer1.team.focus.HasBall()) {
      this.gPanel.ball.Pass(1, this.gPanel);
      System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
      timer.Interval = 1000;
      timer.Tick += G1Shoot;
      timer.Start();
    }
    if (this.rKey && this.gPanel.gamer1.team.focus.HasBall()) {
      this.gPanel.ball.Pass(2, this.gPanel);
      System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
      timer.Interval = 1000;
      timer.Tick += G1Shoot;
      timer.Start();
    }
    if (this.yKey && this.gPanel.gamer2.team.focus.HasBall()) {
      this.gPanel.ball.Pass(1, this.gPanel);
      System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
      timer.Interval = 1000;
      timer.Tick += G2Shoot;
      timer.Start();
    }
    if (this.uKey && this.gPanel.gamer2.team.focus.HasBall()) {
      this.gPanel.ball.Pass(2, this.gPanel);
      System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
      timer.Interval = 1000;
      timer.Tick += G2Shoot;
      timer.Start();
    }
  }

  public void G1Shoot(object sender, EventArgs e) {
    this.gPanel.ball.Pass(4, this.gPanel);
    System.Windows.Forms.Timer timer = (System.Windows.Forms.Timer) sender;
    timer.Stop();
  }

  public void G2Shoot(object sender, EventArgs e) {
    this.gPanel.ball.Pass(3, this.gPanel);
    System.Windows.Forms.Timer timer = (System.Windows.Forms.Timer) sender;
    timer.Stop();
  }

  public void KeyDownListener(object sender, KeyEventArgs e) {
    if (e.KeyCode == Keys.W) this.wKey = true;
    if (e.KeyCode == Keys.S) this.sKey = true;
    if (e.KeyCode == Keys.A) this.aKey = true;
    if (e.KeyCode == Keys.D) this.dKey = true;
    if (e.KeyCode == Keys.Up) this.upKey = true;
    if (e.KeyCode == Keys.Down) this.downKey = true;
    if (e.KeyCode == Keys.Left) this.leftKey = true;
    if (e.KeyCode == Keys.Right) this.rightKey = true;
    if (e.KeyCode == Keys.C) this.cKey = true;
    if (e.KeyCode == Keys.B) this.bKey = true;
    if (e.KeyCode == Keys.J) this.jKey = true;
    if (e.KeyCode == Keys.L) this.lKey = true;    
    if (e.KeyCode == Keys.F) this.fKey = true;
    if (e.KeyCode == Keys.V) this.vKey = true;
    if (e.KeyCode == Keys.I) this.iKey = true;
    if (e.KeyCode == Keys.K) this.kKey = true;
    if (e.KeyCode == Keys.E) this.eKey = true;
    if (e.KeyCode == Keys.R) this.rKey = true;
    if (e.KeyCode == Keys.Y) this.yKey = true;
    if (e.KeyCode == Keys.U) this.uKey = true;
    if (e.KeyCode == Keys.Z) this.zKey = true;
    if (e.KeyCode == Keys.H) this.hKey = true;
    this.UpdateKey();
  }

  private void KeyUpListener(object sender, KeyEventArgs e) {
    if (e.KeyCode == Keys.W) this.wKey = false;
    if (e.KeyCode == Keys.S) this.sKey = false;
    if (e.KeyCode == Keys.A) this.aKey = false;
    if (e.KeyCode == Keys.D) this.dKey = false;
    if (e.KeyCode == Keys.Up) this.upKey = false;
    if (e.KeyCode == Keys.Down) this.downKey = false;
    if (e.KeyCode == Keys.Left) this.leftKey = false;
    if (e.KeyCode == Keys.Right) this.rightKey = false;
    if (e.KeyCode == Keys.C) this.cKey = false;
    if (e.KeyCode == Keys.B) this.bKey = false;
    if (e.KeyCode == Keys.J) this.jKey = false;
    if (e.KeyCode == Keys.L) this.lKey = false;    
    if (e.KeyCode == Keys.F) this.fKey = false;
    if (e.KeyCode == Keys.V) this.vKey = false;
    if (e.KeyCode == Keys.I) this.iKey = false;
    if (e.KeyCode == Keys.K) this.kKey = false;
    if (e.KeyCode == Keys.E) this.eKey = false;
    if (e.KeyCode == Keys.R) this.rKey = false;
    if (e.KeyCode == Keys.Y) this.yKey = false;
    if (e.KeyCode == Keys.U) this.uKey = false;
    if (e.KeyCode == Keys.Z) this.zKey = false;
    if (e.KeyCode == Keys.H) this.hKey = false;
  }
}