using System;
using System.Drawing;
using System.Windows.Forms;

namespace MouseClickTest
{
    class MouseClick : Form
    {
        private const int WIDTH = 600;  // largura do display
        private const int LENGTH = 500; // altura do display
        private const int BORDER = 10;  // Borda do painel
        private readonly string IMAGEPATH = AppDomain.CurrentDomain.BaseDirectory + "\\Images\\target.png";

        private Panel mousePanel;
        private Image imageBmp;
        private int imgX, imgY;
        private readonly int imgWidth;
        private readonly int imgHeight;
        private readonly int mpWidth;
        private readonly int mpHeight;

        [STAThread]
        static void Main()
        {
            Application.Run(new MouseClick());
        }

        public MouseClick()
        {
            mousePanel = new Panel
            {
                Anchor = ((AnchorStyles.Top | AnchorStyles.Left)
                | AnchorStyles.Right),
                BackColor = SystemColors.ControlDarkDark,
                Location = new Point(BORDER, BORDER),
                Size = new Size(WIDTH - 2 * BORDER, LENGTH - 2 * BORDER)
            };
            mousePanel.MouseClick += new MouseEventHandler(MarkPosition);
            mousePanel.Paint += new PaintEventHandler(BmpPaint);
            mpWidth = mousePanel.Width;
            mpHeight = mousePanel.Height;

            imageBmp = Image.FromFile(IMAGEPATH);
            imgWidth = imageBmp.Width;
            imgHeight = imageBmp.Height;

            ClientSize = new Size(WIDTH, LENGTH);
            Controls.AddRange(new Control[] { mousePanel });
            Text = "Mouse Click";
        }

        private void BmpPaint(object sender, PaintEventArgs e)
        {
            Random rnd = new Random();
            imgX = rnd.Next(imgWidth, mpWidth - imgWidth);
            imgY = rnd.Next(imgHeight, mpHeight - imgHeight);
            Point imagePoint = new Point(imgX, imgY);
            e.Graphics.DrawImage(imageBmp, imagePoint);
        }

        private void MarkPosition(Object sender, MouseEventArgs e)
        {
            Console.WriteLine("Mouse Position: ({0} , {1})", e.X, e.Y);
            Console.WriteLine("Colision: {0}", Colision(e.X, e.Y));
            if (Colision(e.X, e.Y))
            {
                ClearPanel();
            }
        }

        private bool Colision(int mouseX, int mouseY)
        {
            return (mouseX >= imgX && mouseY >= imgY && mouseX <= (imgX + imgWidth) && mouseY <= (imgY + imgHeight));
        }

        private void ClearPanel()
        {
            imageBmp.Dispose();
            imageBmp = imageBmp = Image.FromFile(IMAGEPATH);
            mousePanel.Invalidate();
        }
    }
}
