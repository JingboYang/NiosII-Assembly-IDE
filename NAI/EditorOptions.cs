using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NAI
{
    public partial class EditorOptions : Form
    {

        public static bool running;
        public static EditorOptions instance;

        public EditorOptions()
        {
            InitializeComponent();
        }

        private void EditorOptions_Load(object sender, EventArgs e)
        {
            running = true;
            instance = this;

            GlobalVars.loadProperties();

            cmbType.Items.Add("Label");
            cmbType.Items.Add("Instruction");
            cmbType.Items.Add("Directory");
            cmbType.Items.Add("Register");
            cmbType.Items.Add("Constant");
            cmbType.Items.Add("Address");
            cmbType.Items.Add("Comment");
            cmbType.Items.Add("Error");
            cmbType.Items.Add("Line Number");
            cmbType.Items.Add("Line # Back 1");
            cmbType.Items.Add("Line # Back 2");
            cmbType.Items.Add("Other");

            tbFontSize.Text = GlobalVars.FONT_SIZE + "";

        }

        private void EditorOptions_Closed(object sender, EventArgs e)
        {
            running = false;
            instance = null;
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            DialogResult result = cd.ShowDialog();
            if (result == DialogResult.OK)
            {
                lblColor.Text = GlobalVars.getPrettyARGBStringFromColor(cd.Color);
            }

        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (cmbType.Text)
            {
                case "Label":
                    lblColor.Text = GlobalVars.getPrettyARGBStringFromColor(GlobalVars.COLOR_LABEL);
                    break;
                case "Instruction":
                    lblColor.Text = GlobalVars.getPrettyARGBStringFromColor(GlobalVars.COLOR_INSTR);
                    break;
                case "Directory":
                    lblColor.Text = GlobalVars.getPrettyARGBStringFromColor(GlobalVars.COLOR_DIR);
                    break;
                case "Register":
                    lblColor.Text = GlobalVars.getPrettyARGBStringFromColor(GlobalVars.COLOR_REG);
                    break;
                case "Constant":
                    lblColor.Text = GlobalVars.getPrettyARGBStringFromColor(GlobalVars.COLOR_CONST);
                    break;
                case "Address":
                    lblColor.Text = GlobalVars.getPrettyARGBStringFromColor(GlobalVars.COLOR_ADDR);
                    break;
                case "Comment":
                    lblColor.Text = GlobalVars.getPrettyARGBStringFromColor(GlobalVars.COLOR_COMM);
                    break;
                case "Error":
                    lblColor.Text = GlobalVars.getPrettyARGBStringFromColor(GlobalVars.COLOR_ERR);
                    break;
                case "Line Number":
                    lblColor.Text = GlobalVars.getPrettyARGBStringFromColor(GlobalVars.COLOR_LINENUM);
                    break;
                case "Line # Back 1":
                    lblColor.Text = GlobalVars.getPrettyARGBStringFromColor(GlobalVars.COLOR_LN1);
                    break;
                case "Line # Back 2":
                    lblColor.Text = GlobalVars.getPrettyARGBStringFromColor(GlobalVars.COLOR_LN2);
                    break;
                case "Other":
                    lblColor.Text = GlobalVars.getPrettyARGBStringFromColor(GlobalVars.COLOR_DEFAULT);
                    break;
                default:
                    lblColor.Text = "Please select";
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            Color selected = GlobalVars.getColorFromPrettyString(lblColor.Text);

            switch (cmbType.Text)
            {
                case "Label":
                    GlobalVars.COLOR_LABEL = selected;
                    break;
                case "Instruction":
                    GlobalVars.COLOR_INSTR = selected;
                    break;
                case "Directory":
                    GlobalVars.COLOR_DIR = selected;
                    break;
                case "Register":
                    GlobalVars.COLOR_REG = selected;
                    break;
                case "Constant":
                    GlobalVars.COLOR_CONST = selected;
                    break;
                case "Address":
                    GlobalVars.COLOR_ADDR = selected;
                    break;
                case "Comment":
                    GlobalVars.COLOR_COMM = selected;
                    break;
                case "Error":
                    GlobalVars.COLOR_ERR = selected;
                    break;
                case "Line Number":
                    GlobalVars.COLOR_LINENUM = selected;
                    break;
                case "Line # Back 1":
                    GlobalVars.COLOR_LN1 = selected;
                    break;
                case "Line # Back 2":
                    GlobalVars.COLOR_LN2 = selected;
                    break;
                case "Other":
                    GlobalVars.COLOR_DEFAULT = selected;
                    break;
            }

            try
            {
                GlobalVars.FONT_SIZE = Convert.ToInt32(tbFontSize.Text.Trim());
            }catch (Exception e1)
            {
                MessageBox.Show(e.ToString());
            }

            GlobalVars.saveProperties();

        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            GlobalVars lol = new GlobalVars();
            GlobalVars.saveProperties();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
