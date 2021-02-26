using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageEtInterfaceCorrection
{
    interface IPainFeeler
    {
        int PainDuration { get; set; }
        void TestPain(int damages);
    }
}
