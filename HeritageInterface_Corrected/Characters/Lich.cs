using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageEtInterfaceCorrection
{
    class Lich : Undead
    {
        public Lich(string name) : base(name, 75, 125, 80, 50, 125, (ConsoleColor)3, unholyDamages_:true) { }
    }
}
