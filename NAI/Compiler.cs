using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace NAI
{
    class Compiler
    {

        private List<LineOfCode> AllCode;
        private List<Word> AllWords;

        public List<String> haveLabel;
        public List<String> useLabel;

        int status;
        // -1 = syntax error

        public Compiler(RichTextBox rtbCode, string path)
        {
            status = 0;

            AllCode = new List<LineOfCode>();
            AllWords = new List<Word>();
            haveLabel = new List<String>();
            useLabel = new List<String>();

            /*errorList = new DataTable();
            DataColumn colLineNum = new DataColumn("Line #", System.Type.GetType("System.Int32"));
            DataColumn colError = new DataColumn("Error Message", System.Type.GetType("System.String"));
            errorList.Columns.Add(colLineNum);
            errorList.Columns.Add(colError);*/

            StreamReader input = new StreamReader(path);

            string temp;
            int counter = 0;
            while ((temp = input.ReadLine()) != null)
            {
                counter++;

                LineOfCode loc = new LineOfCode(temp);
                loc.lineNum = counter;
                AllCode.Add(loc);
            }

            initialProcess();
        }

        private void initialProcess()
        {

            int counter = 0;
            int pcCounter = 0;
            while (counter < AllCode.Count)
            {
                if (!AllCode[counter].ErrMsg.Equals(""))
                {
                    status = -1;
                    return;
                }

                if (!AllCode[counter].thisLine[1].Equals(""))   // a legit line of stuff
                {
                    Word[] temp = Word.GetWord(AllCode[counter], pcCounter, this);
                    for (int i = 0; i < temp.Length; i++)
                    {
                        AllWords.Add(temp[i]);
                        pcCounter++;
                    }
                }
                else if (AllCode[counter].thisLine[0].Equals(""))  // only a comment, screw this line
                {
                    // do nothing
                } else  // is a label then, cool stuff
                {
                    int counter2 = counter + 1;
                    bool keepGoing = true;
                    while (counter2 < AllCode.Count && keepGoing)
                    {
                        if (!AllCode[counter2].thisLine[1].Equals("") && AllCode[counter2].thisLine[0].Equals("")) // next line is good
                        {
                            AllCode[counter2].thisLine[0] = AllCode[counter].thisLine[0];
                            keepGoing = false;
                        }
                    }
                    counter = counter2 - 1;
                }

                counter++;
            }

        }

    }
}
