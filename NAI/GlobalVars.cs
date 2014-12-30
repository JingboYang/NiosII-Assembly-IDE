using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace NAI
{
    class GlobalVars
    {

        public static Color COLOR_LABEL;
        public static Color COLOR_INSTR;
        public static Color COLOR_DIR;
        public static Color COLOR_REG;
        public static Color COLOR_CONST;
        public static Color COLOR_ADDR;
        public static Color COLOR_COMM;
        public static Color COLOR_ERR;
        public static Color COLOR_LINENUM;
        public static Color COLOR_LN1;
        public static Color COLOR_LN2;
        public static Color COLOR_DEFAULT = Color.SlateGray;

        public static int FONT_SIZE;
        /// public static FontFamily FONT_FMLY;
        // public static FontStyle FONT_STYLE;
        public static Font FONT;

        // public static string PATH_INSTR;

        public GlobalVars()
        {
            COLOR_LABEL = Color.Fuchsia;
            COLOR_INSTR = Color.Blue;
            COLOR_DIR = Color.Indigo;
            COLOR_REG = Color.Black;
            COLOR_CONST = Color.BlueViolet;
            COLOR_ADDR = Color.Coral;
            COLOR_COMM = Color.Green;
            COLOR_ERR = Color.Red;
            COLOR_LINENUM = Color.DarkOrchid;
            COLOR_LN1 = Color.LightCyan;
            COLOR_LN2 = Color.LightGray;

            FONT_SIZE = 10;
            // FONT_FMLY = FontFamily.GenericMonospace;
            // FONT_STYLE = FontStyle.Regular;

            FONT = new Font(FontFamily.GenericMonospace, FONT_SIZE, FontStyle.Regular);

            saveProperties();
        }

        public static void saveProperties()
        {
            StreamWriter output = new StreamWriter("Properties.dat");

            output.WriteLine("COLOR_LABEL%" + getARGBStringFromColor(COLOR_LABEL));
            output.WriteLine("COLOR_INSTR%" + getARGBStringFromColor(COLOR_INSTR));
            output.WriteLine("COLOR_DIR%" + getARGBStringFromColor(COLOR_DIR));
            output.WriteLine("COLOR_REG%" + getARGBStringFromColor(COLOR_REG));
            output.WriteLine("COLOR_CONST%" + getARGBStringFromColor(COLOR_CONST));
            output.WriteLine("COLOR_ADDR%" + getARGBStringFromColor(COLOR_ADDR));
            output.WriteLine("COLOR_COMM%" + getARGBStringFromColor(COLOR_COMM));
            output.WriteLine("COLOR_ERR%" + getARGBStringFromColor(COLOR_ERR));
            output.WriteLine("COLOR_LINENUM%" + getARGBStringFromColor(COLOR_LINENUM));
            output.WriteLine("COLOR_LN1%" + getARGBStringFromColor(COLOR_LN1));
            output.WriteLine("COLOR_LN2%" + getARGBStringFromColor(COLOR_LN2));
            output.WriteLine("COLOR_DEFAULT%" + getARGBStringFromColor(COLOR_DEFAULT));

            output.WriteLine("FONT_SIZE%" + FONT_SIZE);
            // output.WriteLine("FONT_FAMILY%" + FONT_FMLY);
            // output.WriteLine("FONT_STYLE%" + FONT_STYLE);
            // output.WriteLine("FONT%" + FONT.ToString());

            output.Close();

            FONT = new Font(FontFamily.GenericMonospace, FONT_SIZE, FontStyle.Regular);
        }

        public static void loadProperties()
        {

            try
            {
                StreamReader input = new StreamReader("Properties.dat");

                string temp;
                string[] temps;

                temp = input.ReadLine();
                temps = temp.Split('%');
                COLOR_LABEL = Color.FromArgb(Convert.ToInt32(temps[1]), Convert.ToInt32(temps[2]), Convert.ToInt32(temps[3]), Convert.ToInt32(temps[4]));

                temp = input.ReadLine();
                temps = temp.Split('%');
                COLOR_INSTR = Color.FromArgb(Convert.ToInt32(temps[1]), Convert.ToInt32(temps[2]), Convert.ToInt32(temps[3]), Convert.ToInt32(temps[4]));

                temp = input.ReadLine();
                temps = temp.Split('%');
                COLOR_DIR = Color.FromArgb(Convert.ToInt32(temps[1]), Convert.ToInt32(temps[2]), Convert.ToInt32(temps[3]), Convert.ToInt32(temps[4]));

                temp = input.ReadLine();
                temps = temp.Split('%');
                COLOR_REG = Color.FromArgb(Convert.ToInt32(temps[1]), Convert.ToInt32(temps[2]), Convert.ToInt32(temps[3]), Convert.ToInt32(temps[4]));

                temp = input.ReadLine();
                temps = temp.Split('%');
                COLOR_CONST = Color.FromArgb(Convert.ToInt32(temps[1]), Convert.ToInt32(temps[2]), Convert.ToInt32(temps[3]), Convert.ToInt32(temps[4]));

                temp = input.ReadLine();
                temps = temp.Split('%');
                COLOR_ADDR = Color.FromArgb(Convert.ToInt32(temps[1]), Convert.ToInt32(temps[2]), Convert.ToInt32(temps[3]), Convert.ToInt32(temps[4]));

                temp = input.ReadLine();
                temps = temp.Split('%');
                COLOR_COMM = Color.FromArgb(Convert.ToInt32(temps[1]), Convert.ToInt32(temps[2]), Convert.ToInt32(temps[3]), Convert.ToInt32(temps[4]));

                temp = input.ReadLine();
                temps = temp.Split('%');
                COLOR_ERR = Color.FromArgb(Convert.ToInt32(temps[1]), Convert.ToInt32(temps[2]), Convert.ToInt32(temps[3]), Convert.ToInt32(temps[4]));

                temp = input.ReadLine();
                temps = temp.Split('%');
                COLOR_LINENUM = Color.FromArgb(Convert.ToInt32(temps[1]), Convert.ToInt32(temps[2]), Convert.ToInt32(temps[3]), Convert.ToInt32(temps[4]));

                temp = input.ReadLine();
                temps = temp.Split('%');
                COLOR_LN1 = Color.FromArgb(Convert.ToInt32(temps[1]), Convert.ToInt32(temps[2]), Convert.ToInt32(temps[3]), Convert.ToInt32(temps[4]));

                temp = input.ReadLine();
                temps = temp.Split('%');
                COLOR_LN2 = Color.FromArgb(Convert.ToInt32(temps[1]), Convert.ToInt32(temps[2]), Convert.ToInt32(temps[3]), Convert.ToInt32(temps[4]));

                temp = input.ReadLine();
                temps = temp.Split('%');
                COLOR_DEFAULT = Color.FromArgb(Convert.ToInt32(temps[1]), Convert.ToInt32(temps[2]), Convert.ToInt32(temps[3]), Convert.ToInt32(temps[4]));

                temp = input.ReadLine();
                temps = temp.Split('%');
                FONT_SIZE = Convert.ToInt32(temps[1].Trim());

                /*temp = input.ReadLine();
                temps = temp.Split('%');
                FONT_FMLY = new FontFamily(temps[1].Trim());*/

                /*temp = input.ReadLine();
                temps = temp.Split('%');
                FONT_STYLE = new FontStyle(temps[1].Trim());*/

                /*temp = input.ReadLine();
                temps = temp.Split('%');
                FONT = new Font(temps[1].Trim());*/

                FONT = new Font(FontFamily.GenericMonospace, FONT_SIZE, FontStyle.Regular);

                input.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading settings: " + e.ToString());
            }
        }

        public static Color getColorFromIndex(int index)
        {
            Color thisColor;

            switch (index)
            {
                case 0:
                    thisColor = GlobalVars.COLOR_LABEL;
                    break;
                case 1:
                    thisColor = GlobalVars.COLOR_INSTR;
                    break;
                case 2:
                    thisColor = GlobalVars.COLOR_REG;
                    break;
                case 3:
                    thisColor = GlobalVars.COLOR_CONST;
                    break;
                case 4:
                    thisColor = GlobalVars.COLOR_ADDR;
                    break;
                case 5:
                    thisColor = GlobalVars.COLOR_COMM;
                    break;
                case 6:
                    thisColor = GlobalVars.COLOR_ERR;
                    break;
                case 7:
                    thisColor = GlobalVars.COLOR_DIR;
                    break;
                default:
                    thisColor = GlobalVars.COLOR_DEFAULT;
                    break;
            }

            return thisColor;
        }

        public static string getARGBStringFromColor(Color temp)
        {
            string result;

            result = temp.A + "%" + temp.R + "%" + temp.G + "%" + temp.B;

            return result;

        }

        public static string getPrettyARGBStringFromColor(Color temp)
        {
            string result;

            result = "A = " + temp.A + ", R = " + temp.R + ", G = " + temp.G + ", B = " + temp.B;

            return result;

        }

        public static Color getColorFromPrettyString(string temp)
        {
            string[] delim = { ",", " ", "=", "A", "B", "R", "G" };
            string[] temps = temp.Split(delim, StringSplitOptions.RemoveEmptyEntries);

            if (temps.Length != 4)
            {
                return COLOR_DEFAULT;
            } else
            {
                return Color.FromArgb(Convert.ToInt32(temps[0]), Convert.ToInt32(temps[1]), Convert.ToInt32(temps[2]), Convert.ToInt32(temps[3]));
            }

        }
    }
}
