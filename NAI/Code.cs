using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Data;

namespace NAI
{
    class Code
    {

        private string filename;
        private RichTextBox rtbCode;

        private ArrayList AllCode;
        public DataTable errorList;

        public Code()
        {
            // do nothing
        }

        public Code(RichTextBox rtbCode_, string filename_)
        {
            rtbCode = rtbCode_;
            filename = filename_;

            AllCode = new ArrayList();

            errorList = new DataTable();
            DataColumn colLineNum = new DataColumn("Line #", System.Type.GetType("System.Int32"));
            DataColumn colError = new DataColumn("Error Message", System.Type.GetType("System.String"));
            errorList.Columns.Add(colLineNum);
            errorList.Columns.Add(colError);

            StreamReader input = new StreamReader(filename);

            string temp;
            int counter = 0;
            while ((temp = input.ReadLine())!= null)
            {

                counter++;

                LineOfCode loc = new LineOfCode(temp);
                loc.lineNum = counter;
                AllCode.Add(loc);

                if (loc.ErrMsg.Equals("") == false)
                {
                    errorList.Rows.Add(loc.lineNum, loc.ErrMsg);
                }

                for (int i = 0; i < 6; i++)
                {

                    Color thisColor = Color.Cyan;

                    thisColor = GlobalVars.getColorFromIndex(loc.thisLineProp[i]);

                    rtbCode.AppendText(loc.thisLine[i], thisColor);  // Label
                }

                
            }

            input.Close();

        }   

    }
}
