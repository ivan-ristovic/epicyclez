using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Epicyclez.Common;
using Newtonsoft.Json;

namespace Epicyclez
{
    public partial class MainWindow : Form
    {
        private Painter p = new Painter();


        public MainWindow()
        {
            this.InitializeComponent();

            this.p.Skip = 8;
            this.p.TryLoadData("data.json");
        }


        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            this.p.Paint(e.Graphics, this.Width, this.Height);
        }

        private void TmrTick_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void TbStep_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
