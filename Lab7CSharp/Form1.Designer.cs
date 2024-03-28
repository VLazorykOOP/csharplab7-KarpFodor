using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Lab7CSharp
{
    partial class Form1 : Form
    {
        private CircularPanel rotatingPanel;
        private Panel movingPanel;
        private Timer timerRotation;
        private Timer timerMovement;
        private Random random;

        private float rotationAngle = 0.0f;
        private int rotatingPanelRadius = 120;

        private void InitializeTimers()
        {
            timerRotation = new Timer();
            timerRotation.Interval = 50;
            timerRotation.Tick += timerRotation_Tick;

            timerMovement = new Timer();
            timerMovement.Interval = 50;
            timerMovement.Tick += timerMovement_Tick;

            random = new Random();
        }

        private void InitializeRotatingPanel()
        {
            rotatingPanel = new CircularPanel();
            rotatingPanel.Size = new Size(100, 100);
            rotatingPanel.Location = new Point((ClientSize.Width - rotatingPanel.Width) / 2, (ClientSize.Height - rotatingPanel.Height) / 2);
            rotatingPanel.BackColor = Color.Transparent; // Set background to transparent

            Controls.Add(rotatingPanel);
        }

        private void InitializeMovingPanel()
        {
            movingPanel = new Panel();
            movingPanel.Size = new Size(50, 50);
            movingPanel.BackColor = GetRandomColor();

            Controls.Add(movingPanel);
        }

        private void timerRotation_Tick(object sender, EventArgs e)
        {
            rotationAngle += 0.1f;

            // Розраховуємо нові координати панелі для переміщення відносно обертального кола
            int x = rotatingPanel.Location.X + rotatingPanel.Width / 2 + (int)(rotatingPanelRadius * Math.Cos(rotationAngle));
            int y = rotatingPanel.Location.Y + rotatingPanel.Height / 2 + (int)(rotatingPanelRadius * Math.Sin(rotationAngle));
            movingPanel.Location = new Point(x - movingPanel.Width / 2, y - movingPanel.Height / 2);

            rotatingPanel.BackColor = GetRandomColor();
            movingPanel.BackColor = GetRandomColor();

            Invalidate();
        }

        private void timerMovement_Tick(object sender, EventArgs e)
        {
            // Переміщення панелі обертання на випадкову відстань відносно поточного положення форми
            rotatingPanel.Location = new Point(
                 (rotatingPanel.Location.X + random.Next(2 * 5 + 1) - 5 + ClientSize.Width) % ClientSize.Width,
                 (rotatingPanel.Location.Y + random.Next(2 * 5 + 1) - 5 + ClientSize.Height) % ClientSize.Height
             );
        }

        private Color GetRandomColor()
        {
            return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }

        private void InitializeComponent()
        {

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 500);

            this.Text = "Task1";
            this.ResumeLayout(false);
            this.PerformLayout();

            InitializeTimers();
            InitializeRotatingPanel();
            InitializeMovingPanel();

            timerRotation.Start();
            timerMovement.Start();
        }

    }

    public class CircularPanel : Panel
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddEllipse(0, 0, this.Width, this.Height);
            this.Region = new Region(graphicsPath);
        }
    }
}