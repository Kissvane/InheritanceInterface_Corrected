using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageEtInterfaceCorrection
{
    class Robot : Character
    {
        public Robot(string name) : base(name, 10, 100, 50, 20, 200, color: (ConsoleColor)4) { }

        public override int RollDice()
        {
            return 50;
        }

        public override void CanAttackReset()
        {
            base.CanAttackReset();
            Attack = RoundToInt(Attack * 1.5f);
            MyLog("L'attaque de "+Name+" augmente à "+ Attack+".");
        }

        
    }
}
