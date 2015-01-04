using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAI
{
    public class Instruction
    {

        public string line;
        public string name;
        public string opcode;
        public int numOpr;

        public string[] opr;
        public string descrip;
        public string example;

        public Instruction()
        {
            // do nothing
        }

        public Instruction(string line_)
        {

            opr = new string[3];

            line = line_;
            string[] temps = line_.Split(',');
            name = temps[0];
            opcode = temps[1].ToUpper();
            numOpr = Convert.ToInt32(temps[2]);

            for (int i = 0; i < numOpr; i++)
            {
                opr[i] = temps[3 + i];
            }
            for (int i = numOpr; i < 3; i++)
            {
                opr[i] = "";
            }

            descrip = temps[3+numOpr].Replace("%", "");
            example = temps[4+numOpr].Replace("%", "");

        }

    }
}
