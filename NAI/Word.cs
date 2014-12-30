using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAI
{
    class Word
    {
        

        int type; // 0 = actual code, 2 = directive, 3 = .data

        int position;

        string label;
        string instr;
        int[] opr;  // stores the register #
        String immedSTR;  // store address, label, and constants
        string comment;

        public Word(LineOfCode loc, int position_)
        {
            opr = new int[3];
            if (loc.instrIndex < LineOfCode.numInstr)
            {
                type = 0;
            } else if (loc.thisLine[1].Equals(".data"))
            {
                type = 3;
            } else
            {
                type = 2;
            }

            position = position_;

            label = loc.thisLine[0];
            instr = loc.thisLine[1];

            for (int i = 0; i < LineOfCode.instrSet[loc.instrIndex].numOpr; i++)
            {
                if (LineOfCode.instrSet[loc.instrIndex].opr[i].Equals("reg"))
                {
                    opr[i] = LineOfCode.checkIsInstruction(loc.thisLine[2 + i]);
                } else
                {
                    immedSTR = loc.thisLine[2 + i]; 
                }
            }

            comment = loc.thisLine[5];

        }

        public static Word[] GetWord(LineOfCode loc, int number, Compiler comp)
        {
            
            if (loc.thisLine[1].Equals("bgt"))
            {
                loc.thisLine[1] = "blt";
                string temp = loc.thisLine[3];
                loc.thisLine[3] = loc.thisLine[4];
                loc.thisLine[4] = temp; 
            }
            /*else if (loc.thisLine[1].Equals("bgtu"))
            {
                loc.thisLine[1] = "bltu";
                string temp = loc.thisLine[3];
                loc.thisLine[3] = loc.thisLine[4];
                loc.thisLine[4] = temp; 
            }*/
            else if (loc.thisLine[1].Equals("ble"))
            {
                loc.thisLine[1] = "bge";
                string temp = loc.thisLine[3];
                loc.thisLine[3] = loc.thisLine[4];
                loc.thisLine[4] = temp;
            }
            /*else if (loc.thisLine[1].Equals("bleu"))
            {
                loc.thisLine[1] = "bgeu";
                string temp = loc.thisLine[3];
                loc.thisLine[3] = loc.thisLine[4];
                loc.thisLine[4] = temp; 
            }*/
            else if (loc.thisLine[1].Equals("cmpgt"))
            {
                loc.thisLine[1] = "cmplt";
                string temp = loc.thisLine[3];
                loc.thisLine[3] = loc.thisLine[4];
                loc.thisLine[4] = temp;
            }
            /*else if (loc.thisLine[1].Equals("cmpgti"))
            {
                loc.thisLine[1] = "cmplti";
                string temp = loc.thisLine[3];
                loc.thisLine[3] = loc.thisLine[4];
                loc.thisLine[4] = temp; 
            }*/
            else if (loc.thisLine[1].Equals("cmpgtu"))
            {
                loc.thisLine[1] = "cmpltu";
                string temp = loc.thisLine[3];
                loc.thisLine[3] = loc.thisLine[4];
                loc.thisLine[4] = temp;
            }
            else if (loc.thisLine[1].Equals("cmpgtui"))
            {
                loc.thisLine[1] = "cmpltui";
                string temp = loc.thisLine[3];
                loc.thisLine[3] = loc.thisLine[4];
                loc.thisLine[4] = temp;
            }
            else if (loc.thisLine[1].Equals("cmple"))
            {
                loc.thisLine[1] = "cmpge";
                string temp = loc.thisLine[3];
                loc.thisLine[3] = loc.thisLine[4];
                loc.thisLine[4] = temp;
            }
            else if (loc.thisLine[1].Equals("cmplei"))
            {
                loc.thisLine[1] = "cmpgei";
                string temp = loc.thisLine[3];
                loc.thisLine[3] = loc.thisLine[4];
                loc.thisLine[4] = temp;
            }
            else if (loc.thisLine[1].Equals("cmpleui"))
            {
                loc.thisLine[1] = "cmpgeui";
                string temp = loc.thisLine[3];
                loc.thisLine[3] = loc.thisLine[4];
                loc.thisLine[4] = temp;
            }
            else if (loc.thisLine[1].Equals("mov"))
            {
                loc.thisLine[1] = "add";
                loc.thisLine[4] = "r0";
            }
            else if (loc.thisLine[1].Equals("movi"))
            {

            }
            else if (loc.thisLine[1].Equals("movhi"))
            {

            }
            else if (loc.thisLine[1].Equals("movia"))
            {

            }
            else if (loc.thisLine[1].Equals("movui"))
            {

            }
            else if (loc.thisLine[1].Equals("subi"))
            {

            }
            else if (loc.thisLine[1].Equals("bgt"))
            {

            }
            else if (loc.thisLine[1].Equals("bgt"))
            {

            }

            return null;

        }

    }
}
