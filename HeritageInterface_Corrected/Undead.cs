using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageEtInterfaceCorrection
{
    class Undead : Character
    {
        public Undead(string name, int attack, int defense, int initiative, int damages, int maxLife, ConsoleColor _color,bool unholyDamages_ = false) : base(name, attack, defense, initiative, damages, maxLife, cursed : true, unholyDamages:unholyDamages_, color:_color) { }


    }
}
