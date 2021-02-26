using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageEtInterfaceCorrection
{
    class Watcher : LivingBeing
    {
        public Watcher(string name) : base (name, 50, 150, 50, 50, 150, (ConsoleColor)6, true, true) { }

        public override void Counter(int _CounterBonus, Character Attacker)
        {
            _CounterBonus *= 2;
            base.Counter(_CounterBonus, Attacker);
        }
    }
}
