using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace NAI
{
    class Compiler
    {
        public List<LineOfCode> AllCode;
        public List<Word> AllWords;

        public List<Tuple<string, int, int>> haveLabel;    // <label, memory location, line number>
        public List<Tuple<string, int, int>> useLabel;      // <label, line number, operand #>

        public DataTable errorList;

        public Code code;

        public Compiler(Code code_)
        {

            haveLabel = new List<Tuple<string, int, int>>();
            useLabel = new List<Tuple<string, int, int>>();

            code = code_;
            AllCode = code.AllCode;
            AllWords = new List<Word>();

            errorList = new DataTable();
            DataColumn colLineNum = new DataColumn("Line #", System.Type.GetType("System.Int32"));
            DataColumn colError = new DataColumn("Error Message", System.Type.GetType("System.String"));
            errorList.Columns.Add(colLineNum);
            errorList.Columns.Add(colError);

            collectLabelsAndConvertConst();
            Tuple<string, int, int, int> result = matchLabels();

            if (result == null)
            {
                convertToWord();
            }
            else
            {
                if (result.Item4 == -1)
                {
                    errorList.Rows.Add(result.Item2, "Label '" + result.Item1 + "' is repeated");
                }
                else if (result.Item4 == -2)
                {
                    errorList.Rows.Add(result.Item2, "Label '" + result.Item1 + "' cannot be found");
                }
            }

        }

        private void collectLabelsAndConvertConst()
        {

            int counter = 0;
            int memCounter = 0;
            while (counter < AllCode.Count)
            {

                if (!AllCode[counter].thisLine[0].Trim().Equals(""))   // has a label
                {
                    string thething = AllCode[counter].thisLine[0].Trim();
                    haveLabel.Add(Tuple.Create(thething.Substring(0,thething.Length - 1), memCounter, counter));
                }

                if (!AllCode[counter].thisLine[1].Equals("") && !AllCode[counter].thisLine[1].Contains('.'))  // is an instruction and not a directive
                {
                    memCounter++;

                    Instruction curInstr = LineOfCode.instrSet[AllCode[counter].instrIndex];

                    for (int i = 0; i < curInstr.numOpr; i++)
                    {
                        if (curInstr.opr[i].Equals("lbl"))
                        {
                            useLabel.Add(Tuple.Create(AllCode[counter].thisLine[i + 2].Trim(), counter, i + 2));
                        }
                        else if (curInstr.opr[i].Equals("const"))
                        {
                            int result = LineOfCode.checkConstantLabel(AllCode[counter].thisLine[i + 2].Trim());
                            if (result == 999)
                            {
                                useLabel.Add(Tuple.Create(AllCode[counter].thisLine[i + 2].Trim(), counter, i + 2));
                            }
                            else
                            {
                                /*MessageBox.Show(AllCode[counter].thisLine[i + 2]);
                                MessageBox.Show(result + "");*/
                                AllCode[counter].thisLine[i + 2] = Convert.ToInt32(AllCode[counter].thisLine[i + 2].Trim(), result) + "";
                            }
                        }
                        else if (curInstr.opr[i].Equals("addr"))
                        {
                            string[] perhapsLabel = LineOfCode.getLabelFromAddress(AllCode[counter].thisLine[i + 2].Trim());
                            if (perhapsLabel[0] != null)
                            {
                                useLabel.Add(Tuple.Create(perhapsLabel[0].Trim(), counter, i + 2));
                            }
                        }
                    }


                }
                counter++;
            }

        }

        private Tuple<string, int, int, int> matchLabels()   // -1 = good, non-negative = line number
        {

            foreach (Tuple<string, int, int> l1 in haveLabel)
            {
                foreach (Tuple<string, int, int> l2 in haveLabel)
                {

                    if (l1.Item1.Equals(l2.Item1) && l1.Item3 != l2.Item3)
                    {
                        return Tuple.Create(l1.Item1, l1.Item2, l1.Item3, -1);
                    }

                }
            }

            foreach (Tuple<string, int, int> need in useLabel)
            {
                bool found = false;
                foreach (Tuple<string, int, int> have in haveLabel)
                {

                    if (need.Item1.Equals(have.Item1))
                    {
                        Instruction curInstr = LineOfCode.instrSet[AllCode[need.Item2].instrIndex];

                        if (curInstr.opr[need.Item3 - 2].Equals("addr"))
                        {
                            string[] temp = LineOfCode.getLabelFromAddress(AllCode[need.Item2].thisLine[need.Item3].Trim());
                            AllCode[need.Item2].thisLine[need.Item3] = have.Item2 + "(" + temp[1] + ")";
                        }
                        else
                        {
                            AllCode[need.Item2].thisLine[need.Item3] = have.Item2 + "";
                        }
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    return Tuple.Create(need.Item1, need.Item2, need.Item3, -2); ;
                }
            }

            return null;
        }

        private void convertToWord()
        {

            int counter = 0;
            int memCounter = 0;
            while (counter < AllCode.Count)
            {

                if (!AllCode[counter].thisLine[1].Equals("") && !AllCode[counter].thisLine[1].Contains('.'))  // is an instruction and not a directive
                {
                    string[] replace = {",", " "};
                    Word word = new Word(memCounter, 
                        AllCode[counter].thisLine[1].Replace(',', ' ').Trim(), 
                        AllCode[counter].thisLine[2].Replace(',', ' ').Trim(), 
                        AllCode[counter].thisLine[3].Replace(',', ' ').Trim(), 
                        AllCode[counter].thisLine[4].Replace(',', ' ').Trim());
                    memCounter++;
                    AllWords.Add(word);
                    // MessageBox.Show(word.ToString());
                }
                counter++;
            }

        }
    }
}