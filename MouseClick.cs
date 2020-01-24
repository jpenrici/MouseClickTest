using System;
using System.Drawing;
using System.Windows.Forms;

namespace MouseClickTest
{
    class MouseClick : Form
    {
        private const string RESOURCE = "Images";
        private const int TOTALIMAGES = 6;

        private const int WIDTH = 600;  // largura do display
        private const int LENGTH = 500; // altura do display
        private const int BORDER = 10;  // Borda do painel

        private Panel mousePanel;
        private Image img;
        private int imgNumber;
        private int imgX, imgY;
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
            mousePanel.MouseClick += new MouseEventHandler(Position);
            mousePanel.Paint += new PaintEventHandler(ImgPaint);
            mpWidth = mousePanel.Width;
            mpHeight = mousePanel.Height;

            imgNumber = 1;
            img = GetImage(1);

            ClientSize = new Size(WIDTH, LENGTH);
            Controls.AddRange(new Control[] { mousePanel });
            Text = "Mouse Click";
        }

        private Image GetImage(int imgNumber)
        {
            string imgName = RESOURCE + "\\target_" + imgNumber.ToString() + ".png";
            return Image.FromFile(imgName);
        }

        private void ImgPaint(object sender, PaintEventArgs e)
        {
            Random rnd = new Random();
            imgX = rnd.Next(img.Width, mpWidth - img.Width);
            imgY = rnd.Next(img.Height, mpHeight - img.Height);
            Point imagePoint = new Point(imgX, imgY);
            e.Graphics.DrawImage(img, imagePoint);
        }

        private void Position(Object sender, MouseEventArgs e)
        {
            Console.WriteLine("Mouse Position: ({0} , {1})", e.X, e.Y);
            Console.WriteLine("Colision: {0}", Colision(e.X, e.Y));
            if (Colision(e.X, e.Y))
            {
                UpdatePanel();
            }
        }

        private bool Colision(int mouseX, int mouseY)
        {
            return (mouseX >= imgX && mouseY >= imgY && mouseX <= (imgX + img.Width) && mouseY <= (imgY + img.Height));
        }

        private void UpdatePanel()
        {
            img.Dispose();

            imgNumber++;
            if (imgNumber > TOTALIMAGES)
            {
                imgNumber = 1;
            }
            img = GetImage(imgNumber);

            mousePanel.Invalidate();
        }
    }
}
