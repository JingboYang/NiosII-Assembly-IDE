using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAI
{
    class Word
    {

        public int memAddr;
        public string instr;
        public int opr1;
        public int opr2;
        public int opr3;

        public int shift;
        public int reg;

        public Word(int memAddr_, string instr_, string opr1_, string opr2_, string opr3_)
        {
            memAddr = memAddr_;
            instr = instr_;

            if (opr1_.Contains('('))
            {
                getShiftAndReg(opr1_);
            }
            else
            {
                int temp = LineOfCode.checkReg(opr1_);
                if (temp == -3)
                {
                    opr1 = Convert.ToInt32(opr1_);
                }
                else if (temp == -2)
                {
                    opr1 = -1;
                }
                else
                {
                    opr1 = temp;
                }
            }

            if (opr2_.Contains('('))
            {
                getShiftAndReg(opr2_);
            }
            else
            {
                int temp = LineOfCode.checkReg(opr2_);
                if (temp == -3)
                {
                    opr2 = Convert.ToInt32(opr2_);
                }
                else if (temp == -2)
                {
                    opr2 = -1;
                }
                else
                {
                    opr2 = temp;
                }
            }

            if (opr3_.Contains('('))
            {
                getShiftAndReg(opr3_);
            }
            else
            {
                int temp = LineOfCode.checkReg(opr3_);
                if (temp == -3)
                {
                    opr3 = Convert.ToInt32(opr3_);
                }
                else if (temp == -2)
                {
                    opr3 = -1;
                }
                else
                {
                    opr3 = temp;
                }
            }


        }

        public void getShiftAndReg(string temp)
        {
            string[] delim = { "(", ")", " " };
            string[] temps = temp.Split(delim, StringSplitOptions.RemoveEmptyEntries);

            shift = Convert.ToInt32(temps[0]);
            reg = LineOfCode.checkReg(temps[1]);
        }

        public override string ToString()
        {
            return memAddr + ", " + instr + ", " + opr1 + ", " + opr2 + ", " + opr3;
        }

    }
}
