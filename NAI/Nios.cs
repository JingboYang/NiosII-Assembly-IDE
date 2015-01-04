using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NAI
{
    class Nios : Form
    {

        public List<Word> AllWords;

        public int[] reg = new int[32];
        public int[] oldreg = new int[32];
        public int[] memory = new int[32768]; // this is 128kB memory, probably enough

        public int pc;

        public bool stop;

        public Thread niosRun;

        public DataTable registerData;

        public Nios(List<Word> words)
        {
            registerData = new DataTable();
            DataColumn colReg = new DataColumn("Reg #", System.Type.GetType("System.Int32"));
            DataColumn colRegVal = new DataColumn("Value", System.Type.GetType("System.String"));
            registerData.Columns.Add(colReg);
            registerData.Columns.Add(colRegVal);

            AllWords = words;
            reset();
        }

        public void run()
        {
            niosRun = new Thread(new ThreadStart(run_nios));
            niosRun.Start();
        }

        private void run_nios()
        {
            int status = 0;
            while (!stop && status == 0)
            {
                status = ExecuteWord();
                UpdateMemoryMap();
                UpdateRegisterDGV();
            }
        }

        public void reset()
        {
            for (int i = 0; i < reg.Length; i++)
            {
                reg[i] = 0;
                oldreg[i] = 0;
            }

            for (int i = 0; i < memory.Length; i++)
            {
                memory[i] = 0;
            }

            pc = 0;

            for (int i = 0; i < 32; i++)
            {
                registerData.Rows.Add(i, reg[i] + "");
            }
            Emulator.instance.dgvReg.DataSource = registerData;

            UpdateMemoryMap();
            UpdateRegisterDGV();
        }

        private int ExecuteWord()
        {
            try
            {
                Word curword = AllWords[pc];

                switch (curword.instr)
                {

                    case "add":
                        reg[curword.opr1] = reg[curword.opr2] + reg[curword.opr3];
                        pc++;
                        break;
                    case "addi":
                        // MessageBox.Show(curword.opr3 + "");
                        reg[curword.opr1] = reg[curword.opr2] + curword.opr3;
                        pc++;
                        break;






                    default:
                        pc++;
                        break;
                }
            }
            catch (Exception e)
            {
                return -1;
            }
            return 0;
        }

        private void UpdateMemoryMap()
        {

        }

        private void UpdateRegisterDGV()
        {
            // registerData.Rows.Clear();
            CheckForIllegalCrossThreadCalls = false;
            Task updateDGV = Task.Run(() =>
            {

                for (int i = 0; i < 32; i++)
                {
                    // registerData.Rows.Add(i, reg[i] + "");
                    if (reg[i] != oldreg[i])
                    {
                        Emulator.instance.dgvReg.Rows[i].Cells[1].Value = reg[i];
                        oldreg[i] = reg[i];
                    }
                }

                // Emulator.instance.dgvReg.DataSource = registerData;
                // DataGridViewColumn colReg = Emulator.instance.dgvReg.Columns[0];
                //  DataGridViewColumn colRegVal = Emulator.instance.dgvReg.Columns[1];

                // colReg.Width = 30;
                // colRegVal.Width = 60;

                Emulator.instance.dgvReg.Refresh();
            }
            );

            updateDGV.Wait();
        }

    }
}
