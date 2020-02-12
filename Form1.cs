
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        private readonly Timer _renderLoop = new Timer();
        private readonly BoardRenderer _boardRenderer = new BoardRenderer();
        private readonly Game _game = new Game();

        public Form1()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Tetris";
            this.WindowState = FormWindowState.Maximized;
            this.KeyDown += Form1_KeyDown;

            _renderLoop.Interval = 16; //40fps
            _renderLoop.Enabled = true;
            _renderLoop.Tick += _renderLoop_Tick;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            _game.HandleKey(e.KeyCode);
        }

        private void _renderLoop_Tick(object sender, EventArgs e)
        {
            try
            {
                var currentContext = BufferedGraphicsManager.Current;
                using var myBuffer = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
                var g = myBuffer.Graphics;

                _boardRenderer.RenderBackground(g, this.ClientSize.Width, this.ClientSize.Height);
                _boardRenderer.RenderBlocks(g, this.ClientSize.Width, this.ClientSize.Height, _game);
                _boardRenderer.RenderPreview(g, this.ClientSize.Width, this.ClientSize.Height, _game);
                _boardRenderer.RenderScore(g, this.ClientSize.Width, this.ClientSize.Height, _game);

                myBuffer.Render();
            }
            catch (System.ObjectDisposedException)
            {
                // App closing - this.CreateGraphics() will fail
            }

        }
    }
}
