using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NAI
{
    public partial class frmMain : Form
    {

        int oldLength = 0;
        bool loading = false;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            rtbLineNum.Height = this.Height - 130;
            rtbCode.Height = this.Height - 130;
            rtbCode.Width = (this.Width + 60) / 2;

            dgvReference.Width = this.Width - rtbCode.Width - rtbCode.Location.X - 35;
            dgvReference.Height = (this.Height - 120) / 2;
            dgvReference.Location = new System.Drawing.Point(rtbCode.Location.X + rtbCode.Width + 5, dgvReference.Location.Y);

            dgvError.Height = this.Height - dgvReference.Height - 130 - 5;
            dgvError.Width = dgvReference.Width;
            dgvError.Location = new System.Drawing.Point(dgvReference.Location.X, dgvReference.Location.Y+ dgvReference.Height + 5);

        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            // save to temp file
            string path = "_compile_.NoTouch";
            string fullFile = rtbCode.Text;
            fullFile = fullFile.Replace("\n", "\r\n");

            StreamWriter output = new StreamWriter(path);
            output.Write(fullFile);
            output.Close();

            // load again
            rtbCode.Clear();
            Compiler compile = new Compiler(rtbCode, path);

            /*
            dgvError.DataSource = curCode.errorList;
            DataGridViewColumn colErrorMsg = dgvError.Columns[1];
            colErrorMsg.Width = 450;*/

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            loading = true;
            string path;
            OpenFileDialog file = new OpenFileDialog();

            if (file.ShowDialog() == DialogResult.OK)
            {
                rtbCode.Clear();

                path = file.FileName;
                Code curCode = new Code(rtbCode, path);
                dgvError.DataSource = curCode.errorList;
                DataGridViewColumn colErrorMsg = dgvError.Columns[1];
                colErrorMsg.Width = 450;

            }
            loading = false;

            rtbCode_TextChanged(null, null);
        }

        private void rtbCode_TextChanged(object sender, EventArgs e)
        {
            if (loading)
            {
                return;
            }

            if (rtbCode.Lines.Length != oldLength)
            {
                int startChar = rtbCode.GetCharIndexFromPosition(new Point(0, 0));
                int topLineNum = rtbCode.GetLineFromCharIndex(startChar);
                int endChar = rtbCode.GetCharIndexFromPosition(new Point(0, this.rtbCode.Height));
                int botLineNum = rtbCode.GetLineFromCharIndex(endChar);
                populateLineNum(topLineNum, botLineNum);
                oldLength = rtbCode.Lines.Length;
            }


            // lblLCD.Text = rtbCode.SelectionStart + ", " + rtbCode.GetLineFromCharIndex(rtbCode.SelectionStart);
            int curIndex = rtbCode.SelectionStart;
            if (rtbCode.Lines.Length > 0)
            {

                int firstChar = rtbCode.GetFirstCharIndexOfCurrentLine();
                int curLineNum = rtbCode.GetLineFromCharIndex(firstChar);
                int lastChar = firstChar + rtbCode.Lines[curLineNum].Length;
                string curLine = rtbCode.Lines[curLineNum];

                string curWord = "";

                int startIndex = 0;
                int endIndex = 0;
                int index = curIndex;

                while (index - firstChar - 2 > -1 && !(curLine[index - firstChar - 1].Equals('/') && curLine[index - firstChar - 2].Equals('/')))
                {
                    index--;
                }

                bool isComment = false;
                if (index - firstChar - 2 > -1)  // is a comment
                {
                    startIndex = index - 2;
                    endIndex = lastChar - 1;
                    isComment = true;
                }
                else  // not a comment
                {
                    index = curIndex;
                    while (index - firstChar - 1 > -1 && !curLine[index - firstChar - 1].Equals(' ') && !curLine[index - firstChar - 1].Equals(',') && !curLine[index - firstChar - 1].Equals('\t'))
                    {
                        // curWord = curLine[index - firstChar - 1] + curWord;
                        index--;
                    }

                    startIndex = index;

                    index = curIndex;
                    while ((index < lastChar && !curLine[index - firstChar].Equals('\t') && !curLine[index - firstChar].Equals(' ') && !curLine[index - firstChar].Equals(',')) && !(index < lastChar - 1 && curLine[index - firstChar].Equals('/') && curLine[index - firstChar + 1].Equals('/')))
                    {
                        // curWord = curWord + curLine[index - firstChar];
                        index++;
                    }
                    endIndex = index - 1;
                }
                curWord = rtbCode.Lines[curLineNum].Substring(startIndex - firstChar, endIndex - startIndex + 1);
                // lblLCD.Text = startIndex + " - " + endIndex + ": " + curWord;

                int colorCode = -1;

                if (isComment)
                {
                    lblLCD.Text = curWord + "is comment";
                    colorCode = 5;
                }
                else if (LineOfCode.checkConstant(curWord) >= 0)
                {
                    lblLCD.Text = curWord + " is a constant";
                    colorCode = 3;
                } else if (LineOfCode.checkDirective(curWord))
                {
                    lblLCD.Text = curWord + " is a directive";
                    colorCode = 7;
                }
                else if (LineOfCode.checkIsInstruction(curWord) > -1)
                {
                    lblLCD.Text = curWord + " is an instruction";
                    colorCode = 1;
                }
                else if (LineOfCode.checkReg(curWord) > -1)
                {
                    lblLCD.Text = curWord + " is a register";
                    colorCode = 2;
                }
                else if (LineOfCode.checkAddress(curWord))
                {
                    lblLCD.Text = curWord + " is an address";
                    colorCode = 4;
                }
                else if (curWord.Length > 0 && curWord[curWord.Length - 1].Equals(':') && LineOfCode.checkLabel(curWord.Substring(0, curWord.Length - 1)))
                {
                    lblLCD.Text = curWord + " is a label";
                    colorCode = 0;
                } else
                {
                    lblLCD.Text = curWord + " is nothing";
                    colorCode = -1;
                }

                Color thisColor = Color.SlateGray;

                thisColor = GlobalVars.getColorFromIndex(colorCode);

                rtbCode.SelectionStart = startIndex;
                rtbCode.SelectionLength = endIndex - startIndex + 1;
                rtbCode.SelectionColor = thisColor;

                lblLCD.Text = rtbCode.SelectionLength + " , " + thisColor.ToString() + " , " + colorCode;

                rtbCode.SelectionStart = curIndex;
                rtbCode.SelectionLength = 0;

            }
            else
            {
                lblLCD.Text = "" + " - " + curIndex;
            }
        }

        private void rtbCode_VScroll(object sender, EventArgs e)
        {

            int startChar = rtbCode.GetCharIndexFromPosition(new Point(0, 0));
            int topLineNum = rtbCode.GetLineFromCharIndex(startChar);
            int endChar = rtbCode.GetCharIndexFromPosition(new Point(0, this.rtbCode.Height));
            int botLineNum = rtbCode.GetLineFromCharIndex(endChar);
            lblLCD.Text = topLineNum + ", " + botLineNum;

            populateLineNum(topLineNum, botLineNum);
        }

        private void populateLineNum(int start, int end)
        {
            rtbLineNum.Clear();
            int counter = 0;
            for (int i = start; i <= end; i++)
            {
                counter++;
                if (counter % 2 == 1)
                {
                    rtbLineNum.AppendTextBackColor(((i + 1) + "").PadRight(4) + "\r\n", GlobalVars.COLOR_LINENUM, GlobalVars.COLOR_LN1);
                } else
                {
                    rtbLineNum.AppendTextBackColor(((i + 1) + "").PadRight(4) + "\r\n", GlobalVars.COLOR_LINENUM, GlobalVars.COLOR_LN2);
                }
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string path;
            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "Text files (*.txt)|*.txt|Nios Assembly files (*.s)|*.s|Good files (*.good)|*.good|All files (*.*)|*.*";
            file.FilterIndex = 2;
            file.RestoreDirectory = true;

            if (file.ShowDialog() == DialogResult.OK)
            {
                if (file != null)
                {
                    path = file.FileName;
                    string fullFile = rtbCode.Text;
                    fullFile = fullFile.Replace("\n", "\r\n");

                    StreamWriter output = new StreamWriter(path);
                    output.Write(fullFile);
                    output.Close();

                }

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

            loading = true;

            // save to temp file
            string path = "_temp_.NoTouch";
            string fullFile = rtbCode.Text;
            fullFile = fullFile.Replace("\n", "\r\n");

            StreamWriter output = new StreamWriter(path);
            output.Write(fullFile);
            output.Close();

            // load again
            rtbCode.Clear();
            Code curCode = new Code(rtbCode, path);
            dgvError.DataSource = curCode.errorList;
            DataGridViewColumn colErrorMsg = dgvError.Columns[1];
            colErrorMsg.Width = 450;

            loading = false;

            rtbCode_TextChanged(null, null);

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadInstructions();
        }

        private void btnMulti_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming Soon");
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;

            DialogResult result = MessageBox.Show("Do you want to save current item?", "New File", buttons);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                btnSave_Click(sender, e);
                rtbCode.Clear();
            } else if (result == System.Windows.Forms.DialogResult.No)
            {
                rtbCode.Clear();
            } else
            {
                // do nothing
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnLoad_Click(sender, e);
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            btnSave_Click(sender, e);
        }

        private void editorSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!EditorOptions.running)
            {
                EditorOptions eo = new EditorOptions();
                eo.Show();
            } else
            {
                EditorOptions.instance.BringToFront();
            }
        }

        private void simulatorSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming soon");
        }

        private void descriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Free stuff");
        }

        private void LoadInstructions()
        {

            LineOfCode.GetInstruction("Instructions.csv");

            DataTable helpTable = new DataTable();
            DataColumn instr = new DataColumn("Name", System.Type.GetType("System.String"));
            DataColumn opcode = new DataColumn("Opcode", System.Type.GetType("System.String"));
            DataColumn opr1 = new DataColumn("Opr 1", System.Type.GetType("System.String"));
            DataColumn opr2 = new DataColumn("Opr 2", System.Type.GetType("System.String"));
            DataColumn opr3 = new DataColumn("Opr 3", System.Type.GetType("System.String"));
            DataColumn descrip = new DataColumn("Description", System.Type.GetType("System.String"));
            DataColumn exp = new DataColumn("Example", System.Type.GetType("System.String"));

            helpTable.Columns.Add(instr);
            helpTable.Columns.Add(opcode);
            helpTable.Columns.Add(opr1);
            helpTable.Columns.Add(opr2);
            helpTable.Columns.Add(opr3);
            helpTable.Columns.Add(descrip);
            helpTable.Columns.Add(exp);

            for (int i = 0; i < LineOfCode.numInstr + LineOfCode.numDir; i++)
            {
                Instruction temp = LineOfCode.instrSet[i];
                helpTable.Rows.Add(temp.name, temp.opcode, temp.opr[0], temp.opr[1], temp.opr[2], temp.descrip, temp.example);
            }

            dgvReference.DataSource = helpTable;
            DataGridViewColumn colName = dgvReference.Columns[0];
            DataGridViewColumn colOpcode = dgvReference.Columns[1];
            DataGridViewColumn colOpr1 = dgvReference.Columns[2];
            DataGridViewColumn colOpr2 = dgvReference.Columns[3];
            DataGridViewColumn colOpr3 = dgvReference.Columns[4];
            DataGridViewColumn colDescrip = dgvReference.Columns[5];
            DataGridViewColumn colExample = dgvReference.Columns[6];

            colName.Width = 60;
            colOpcode.Width = 80;
            colOpr1.Width = 50;
            colOpr2.Width = 50;
            colOpr3.Width = 50;

        }
    }
}
