using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NAI
{
    public partial class Emulator : Form
    {

        public static bool running;
        public static Emulator instance;

        public Code code;
        private Compiler compiler;

        private Nios nios;

        public Emulator()
        {
            InitializeComponent();
        }

        private void Emulator_Load(object sender, EventArgs e)
        {
            running = true;
            instance = this;
            compiler = new Compiler(code);
            dgvCompiler.DataSource = compiler.errorList;

            nios = new Nios(compiler.AllWords);

            CheckForIllegalCrossThreadCalls = false;
        }

        private void Emulator_Closed(object sender, EventArgs e)
        {
            running = false;
            instance = null;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (nios != null)
            {
                nios.pc = 0;
                nios.stop = false;
                nios.run();
            }
            else
            {
                nios = new Nios(compiler.AllWords);
                nios.stop = false;
                nios.run();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            nios.reset();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            nios.stop = true;
        }

        private void dgvCode_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
