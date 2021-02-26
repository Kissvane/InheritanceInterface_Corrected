using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageEtInterfaceCorrection
{
    class Warrior : LivingBeing
    {
        public Warrior(string name) : base(name, 100, 100, 50, 100, 200, (ConsoleColor)5) { }

        public override void TestPain(int _damages)
        {
            //can be affected by pain
            if (_damages > CurrentLife && CurrentLife > 0)
            {
                int painProbability = RoundToInt(((_damages - CurrentLife) * 2f *100f) / (CurrentLife + _damages));
                MyLog(Name+" Pain proba " + painProbability + "%.");
                int painTest = random.Next(0, 101);
                if (painTest < painProbability)
                {
                    //can only be affected by pain during this round
                    CanAttack = false;
                }
            }
        }
    }
}
