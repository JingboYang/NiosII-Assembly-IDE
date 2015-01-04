using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Forms;
using System.Collections;

namespace NAI
{
    public class LineOfCode
    {
        private static bool gotInstr;
        public static Instruction[] instrSet;
        
        public static int numInstr;
        public static int numDir;

        public int instrIndex;

        public int lineNum;
      
        string oldLine;

        public string[] thisLine;  //0 = label, 1 = instruction, 2 = op1, 3 = op2, 4 = op3, 5 = comment
        public int[] thisLineProp; //0 = label, 1 = instruction, 2 = register, 3 = number, 4 = address, 
                                   // 5 = comment, 6 = Error, 7 = directive

        public string ErrMsg;

        public LineOfCode(string line_)
        {
            if (!gotInstr)
            {
                gotInstr = true;
                GetInstruction("Instructions.csv");
            }

            lineNum = 0;

            oldLine = line_;
            thisLine = new string[6];
            thisLineProp = new int[6];

            if (oldLine.Trim().Equals(""))
            {
                ErrMsg = "";
                for (int i = 0; i < 6; i++)
                {
                    thisLine[i] = "";
                    thisLineProp[i] = 0;
                }
                thisLine[5] = "\r\n";
            }
            else
            {
                ErrMsg = decipher();

                if (ErrMsg.Equals(""))
                {
                    postProcessLine();
                }
                else
                {
                    // MessageBox.Show(oldLine + "   " + ErrMsg);
                    thisLine[0] = oldLine;
                    thisLineProp[0] = 6;

                    for (int i = 1; i < 6; i++)
                    {
                        thisLine[i] = "";
                        thisLineProp[i] = 6;
                    }
                    thisLine[5] = "\r\n";
                }
            }

        }

        private string decipher()
        {

            string[] delim = { "//", "#" };
            string[] temps2 = oldLine.Split(delim, StringSplitOptions.RemoveEmptyEntries);

            if (oldLine.Contains("//") || oldLine.Contains("#"))
            {
                if (temps2.Length == 1)
                {
                    thisLine[5] = oldLine.Trim();
                    thisLineProp[5] = 5;
                    return "";
                }
            }
            if (temps2.Length > 1)
            {
                thisLine[5] = temps2[1].Trim();
                thisLineProp[5] = 5;
            }

            string[] delimeters = { " ", ",", "\t" };
            string[] temps = temps2[0].Split(delimeters, StringSplitOptions.RemoveEmptyEntries);

            if (temps.Length != 0)
            {
                instrIndex = checkIsInstruction(temps[0]);
                if (instrIndex != -1)
                // ---------- First Thing is Instruction -----------------
                {
                    int instrNum = instrIndex;

                    thisLine[0] = null;
                    thisLineProp[0] = 0;
                    thisLine[1] = temps[0];

                    if (instrNum >= numInstr)
                    {
                        thisLineProp[1] = 7;    // is directive
                    }
                    else
                    {
                        thisLineProp[1] = 1;    // is instruction
                    }

                    // ---- special cases ----
                    //      .section .exceptions, "ax"
                    //      .section .reset, "ax"

                    if (temps[0].Equals(".section") && temps.Length == 3)
                    {
                        if (temps[1].Equals(".exceptions") || temps[1].Equals(".reset"))
                        {
                            if (temps[2].Equals('"' + "ax" + '"'))
                            {
                                return "";
                            }
                            return "Expect 'ax' (double quote), found " + temps[2] + "'";
                        }
                        return "Expect expection or reset, found '" + temps[1] + "'";
                    }


                    // --- check length of line ---
                    if (temps.Length > instrSet[instrNum].numOpr + 1)
                    {
                        return "Too many operands";
                    } else if (temps.Length < instrSet[instrNum].numOpr + 1)
                    {
                        return "Too few operands";
                    }

                    for (int i = 0; i < instrSet[instrNum].numOpr; i++)
                    {
                        if (instrSet[instrNum].opr[i].Equals("reg"))
                        {

                            int temp = checkReg(temps[i + 1]);
                            if (temp > -1)
                            {
                                thisLine[i + 2] = temps[i + 1];
                                thisLineProp[i + 2] = 2;
                            }
                            else
                            {
                                return "Expect register, found '" + temps[i + 1] + "'";
                            }
                        }
                        else if (instrSet[instrNum].opr[i].Equals("const"))
                        {

                            int validConst = checkConstantLabel(temps[i + 1].Trim());

                            if (validConst == -1)
                            {
                                return "Invalid number, found '" + temps[i + 1] + "'";
                            }
                            else if (validConst == -2)
                            {
                                return "Invalid label, found '" + temps[i + 1] + "'";
                            }

                            thisLine[i + 2] = temps[i + 1].Trim();
                            thisLineProp[i + 2] = 3;

                        }
                        else if (instrSet[instrNum].opr[i].Equals("lbl"))
                        {
                            if (checkLabel(temps[i + 1]))
                            {
                                thisLine[i + 2] = temps[i + 1];
                                thisLineProp[i + 2] = 0;
                            }
                            else
                            {
                                return "Invalid label, found '" + temps[i + 1] + "'";
                            }

                        }
                        else if (instrSet[instrNum].opr[i].Equals("addr"))
                        {

                            if (checkAddress(temps[i + 1]))
                            {
                                thisLine[i + 2] = temps[i + 1];
                                thisLineProp[i + 2] = 4;
                            } else
                            {
                                return "Invalid address, found '" + temps[i + 1] + "'";
                            }
                        }

                    }

                }
                else
                {
                    if (temps[0].ElementAt(temps[0].Length - 1) == ':' && temps.Length > 1)
                    {
                        instrIndex = checkIsInstruction(temps[1]);
                        if (instrIndex != -1)
                        {
                            int instrNum = instrIndex;

                            thisLine[0] = temps[0];
                            thisLineProp[0] = 0;
                            thisLine[1] = temps[1];

                            if (instrNum >= numInstr)
                            {
                                thisLineProp[1] = 7;
                            }
                            else
                            {
                                thisLineProp[1] = 1;
                            }

                            if (temps.Length > instrSet[instrNum].numOpr + 2)
                            {
                                return "Too many operands";
                            }
                            else if (temps.Length < instrSet[instrNum].numOpr + 2)
                            {
                                return "Too few operands";
                            }

                            for (int i = 0; i < instrSet[instrNum].numOpr; i++)
                            {

                                if (instrSet[instrNum].opr[i].Equals("reg"))
                                {
                                    int temp = checkReg(temps[i + 2]);

                                    if (temp > -1)
                                    {
                                        thisLine[i + 2] = temps[i + 2];
                                        thisLineProp[i + 2] = 2;
                                    }
                                    else
                                    {
                                        return "Expect Register, found '" + temps[i + 2] + "'";
                                    }
                                }
                                else if (instrSet[instrNum].opr[i].Equals("const"))
                                {
                                    int validConst = checkConstantLabel(temps[i + 2].Trim());

                                    if (validConst == -1)
                                    {
                                        return "Invalid number, found '" + temps[i + 2] + "'";
                                    }
                                    else if (validConst == -2)
                                    {
                                        return "Invalid label, found '" + temps[i + 2] + "'";
                                    }

                                    thisLine[i + 2] = temps[i + 2].Trim();
                                    thisLineProp[i + 2] = 3;

                                }
                                else if (instrSet[instrNum].opr[i].Equals("lbl"))
                                {
                                    if (checkLabel(temps[i + 2]))
                                    {
                                        thisLine[i + 2] = temps[i + 2];
                                        thisLineProp[i + 2] = 0;
                                    }
                                    else
                                    {
                                        return "Invalid label, found '" + temps[i + 2] + "'";
                                    }
                                }
                                else if (instrSet[instrNum].opr[i].Equals("addr"))
                                {
                                    
                                    thisLine[i + 2] = temps[i + 2];
                                    thisLineProp[i + 2] = 4;
                                }

                            }

                        }
                        else
                        {
                            return "Expect instruction， found " + temps[1] + "'";
                        }
                    }
                    else if (temps[0].ElementAt(temps[0].Length - 1) == ':')
                    {
                        thisLine[0] = temps[0];
                        thisLineProp[0] = 0;
                    }
                    else
                    {
                        return "Expect ':'， found " + temps[0];
                    }
                }
            }

            return "";

        }

        public void postProcessLine()
        {
            bool onlyComment = true;
            for (int i = 0; i < 5; i++)
            {
                if (thisLine[i] != null)
                {
                    onlyComment = false;
                    break;
                }
            }

            if (onlyComment)
            {
                for (int i = 0; i < 5; i++)
                {
                    thisLine[i] = "";
                }
                thisLine[5] = oldLine + "\r\n";
            }
            else
            {
                if (thisLine[0] == null)    // Label
                {
                    thisLine[0] = ("").PadRight(10);
                }
                else
                {
                    thisLine[0] = thisLine[0].PadRight(10);
                }

                if (thisLine[1] == null)    // Instruction
                {
                    thisLine[1] = "";
                }
                else
                {
                    thisLine[1] = thisLine[1].PadRight(8);
                }

                int emptyOperand = 0;
                for (int i = 2; i < 5; i++)
                {
                    if (thisLine[i] == null)
                    {
                        thisLine[i] = "";
                        emptyOperand++;
                    }
                    else
                    {

                        if (i != 4 && thisLine[i + 1] != null)
                        {
                            thisLine[i] = (thisLine[i] + ",").PadRight(5);
                        }
                        else if (thisLine[i + 1] != null)
                        {
                            thisLine[i] = (thisLine[i]).PadRight(5);
                        }
                        else
                        {
                            thisLine[i] = (thisLine[i]).PadRight(5);
                        }
                    }
                }


                if (thisLine[5] == null)
                {
                    thisLine[5] = "\r\n";
                }
                else
                {
                    thisLine[5] = ("// " + thisLine[5]);
                    for (int i = 0; i < emptyOperand; i++)
                    {
                        thisLine[5] = "    " + thisLine[5];
                    }
                    thisLine[5] = thisLine[5] + "\r\n";
                }

            }

        }

        public static int checkIsInstruction(string str1)
        {
            for (int i = 0; i < instrSet.Length; i++)
            {
                if (str1.Equals(instrSet[i].name))
                {
                    return i;
                }
            }

            return -1;
        }

        public static int checkConstantLabel(string str1)    // 2 = binary, 10 = decimal, 16 = hex, -1 = number fail, -2 = invalid label
        {
            if (str1.Length == 0)
            {
                return -1;
            }

            if (str1.ElementAt(0) > 47 && str1.ElementAt(0) < 58)   // is a number
            {
                if (str1.Length != 1)   // length more than 1, need to check
                {
                    if (str1.ElementAt(1) == 'x' || str1.ElementAt(1) == 'X')   // is hex
                    {
                        if (str1.ElementAt(0) != '0')
                        {
                            return -1;
                        }
                        else
                        {
                            string temp = str1.Substring(2);
                            for (int i = 0; i < temp.Length; i++)
                            {
                                if ((temp.ElementAt(i) > 47 && temp.ElementAt(i) < 58) ||
                                        (temp.ElementAt(i) > 64 && temp.ElementAt(i) < 71) ||
                                        (temp.ElementAt(i) > 96 && temp.ElementAt(i) < 103))
                                {
                                    // do nothing
                                }
                                else
                                {
                                    /*MessageBox.Show("1");
                                    MessageBox.Show(temp);*/
                                    return -1;
                                }
                            }
                            return 16;
                        }

                    }
                    else if (str1.ElementAt(1) == 'b' || str1.ElementAt(1) == 'B')    // is binary
                    {

                        if (str1.ElementAt(0) != '0')
                        {
                            // MessageBox.Show("2");
                            return -1;
                        }
                        else
                        {
                            string temp = str1.Substring(2);
                            try
                            {
                                int temp2 = Convert.ToInt32(temp);
                                return 2;
                            }
                            catch (Exception e)
                            {
                                // MessageBox.Show("3");
                                return -1;
                            }
                        }

                    }
                    else    // is decimal
                    {
                        try
                        {
                            int temp = Convert.ToInt32(str1);
                            return 10;
                        }
                        catch (Exception e)
                        {
                            // MessageBox.Show("4");
                            return -1;
                        }

                    }
                }
                else // length is 1, has to be decimal
                {
                    return 10;
                }

            }
            else  // end is a number
            {
                if (checkLabel(str1))
                {
                    return 999;
                }
                else
                {
                    return -2;
                }
            }
        }

        public static int checkConstant(string str1)    // 1 = label, 2 = binary, 10 = decimal, 16 = hex, -1 = number fail, -2 = invalid label
        {
            if (str1.Length == 0)
            {
                return -1;
            }

            if (str1.ElementAt(0) > 47 && str1.ElementAt(0) < 58)   // is a number
            {
                if (str1.Length != 1)   // length more than 1, need to check
                {
                    if (str1.ElementAt(1) == 'x' || str1.ElementAt(1) == 'X')   // is hex
                    {
                        if (str1.ElementAt(0) != '0')
                        {
                            return -1;
                        }
                        else
                        {
                            string temp = str1.Substring(2);
                            for (int i = 0; i < temp.Length; i++)
                            {
                                if ((temp.ElementAt(i) > 47 && temp.ElementAt(i) < 58) ||
                                        (temp.ElementAt(i) > 64 && temp.ElementAt(i) < 71) ||
                                        (temp.ElementAt(i) > 96 && temp.ElementAt(i) < 103))
                                {
                                    // do nothing
                                }
                                else
                                {
                                    return -1;
                                }
                            }
                            return 1;
                        }

                    }
                    else if (str1.ElementAt(1) == 'b' || str1.ElementAt(1) == 'B')    // is binary
                    {

                        if (str1.ElementAt(0) != '0')
                        {
                            return -1;
                        }
                        else
                        {
                            string temp = str1.Substring(2);
                            try
                            {
                                int temp2 = Convert.ToInt32(temp);
                                return 1;
                            }
                            catch (Exception e)
                            {
                                return -1;
                            }
                        }

                    }
                    else    // is decimal
                    {
                        try
                        {
                            int temp = Convert.ToInt32(str1);
                            return 1;
                        }
                        catch (Exception e)
                        {
                            return -1;
                        }

                    }
                }
                else // length is 1, has to be decimal
                {
                    return 1;
                }

            }
            else  // end is a number
            {
                /*if (checkLabel(str1))
                {
                    return 1;
                }
                else
                {
                    return -2;
                }*/
                return -2;
            }
        }

        public static bool checkLabel(string label)
        {
            for (int i = 0; i < label.Length; i++)
            {
                if ((label.ElementAt(i) > 47 && label.ElementAt(i) < 58) ||
                        (label.ElementAt(i) > 64 && label.ElementAt(i) < 91) ||
                        (label.ElementAt(i) > 96 && label.ElementAt(i) < 123) ||
                        (label.ElementAt(i) == 95))
                {
                    // do nothing
                }
                else
                {
                    // MessageBox.Show("'" + label.ElementAt(i) + "'");
                    return false;
                }
            }
            return true;
        }

        public static bool checkAddress(string str)
        {

            int countL = str.Length - str.Replace("(", "").Length;
            int countR = str.Length - str.Replace(")", "").Length;

            if (countL != countR)
            {
                return false;
            }

            string[] delim = { "(", ")" };
            string[] temp = str.Split(delim, StringSplitOptions.RemoveEmptyEntries);

            if (temp.Length != 2)
            {
                return false;
            }

            if (checkReg(temp[1]) < 0)
            {
                return false;
            }

            try
            {
                int temp2 = Convert.ToInt32(temp[0]);
                return true;
            }catch (Exception e)
            {
                return checkLabel(temp[0]);
            }

        }

        public static string[] getLabelFromAddress(string str)
        {

            string[] delim = { "(", ")" };
            string[] temp = str.Split(delim, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                int temp2 = Convert.ToInt32(temp[0]);
                temp[0] = null;
                return temp;
            }
            catch (Exception e)
            {
                return temp;
            }

        }

        public static bool checkDirective(string str)
        {
            int temp = checkIsInstruction(str);

            if (temp > numInstr - 1)
            {
                return true;
            }
            return false;
        }

        public static bool checkFilename(string str)
        {

            if (str.ElementAt(0) == '"' && str.ElementAt(str.Length - 1) == '"' && str.Substring(1,str.Length-2).Contains('"') == false)
            {
                return true;
            }

            return false;
        }

        public static int checkReg(string stuff)
        {
            if (stuff.Length == 0)
            {
                return -2;
            }

            if (stuff.ElementAt(0) == 'r')
            {

                string temp = stuff.Substring(1).Trim();

                try
                {
                    if (Convert.ToInt32(Convert.ToInt32(temp)) < 32 && Convert.ToInt32(temp) > -1)
                    {
                        return Convert.ToInt32(temp);
                    }
                }
                catch (Exception e)
                {
                    return -1;
                }
            }
            else
            {
                string temp = stuff.Trim();

                switch (temp)
                {
                    case "at": return 1;
                    case "et": return 24;
                    case "bt": return 25;
                    case "gp": return 26;
                    case "sp": return 27;
                    case "fp": return 28;
                    case "ea": return 29;
                    case "ba": return 30;
                    case "ra": return 31;
                }

            }

            // MessageBox.Show("Here");
            return -3;
        }

        public static void GetInstruction(string instrFile)
        {
            StreamReader input = new StreamReader(instrFile);

            string temp = input.ReadLine();
            string[] temps = temp.Split(',');

            numInstr = Convert.ToInt32(temps[0]);
            numDir = Convert.ToInt32(temps[1]);
            int instrSize = numInstr + numDir;

            instrSet = new Instruction[instrSize];

            for (int i = 0; i < instrSize; i++)
            {
                temp = input.ReadLine();
                instrSet[i] = new Instruction(temp);
            }

            input.Close();

        }

    }
}
