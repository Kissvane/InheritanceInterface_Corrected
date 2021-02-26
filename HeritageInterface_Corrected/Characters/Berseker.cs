using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageEtInterfaceCorrection
{
    class Berseker : LivingBeing
    {
        public Berseker(string name) : base(name, 100, 100, 80,20, 300, (ConsoleColor)1) { }

        public override void TakeDamages(int _damages)
        {
            base.TakeDamages(_damages);
            if (CurrentLife > 0)
            {
                Damages += _damages;
                MyLog("Les dégâts de " + Name + " augmente à " + Damages + ".");
            }
        }

        public override void TestPain(int _damages)
        {
            //can be affected by pain
            if (_damages > CurrentLife && CurrentLife > 0)
            {
                int painProbability = RoundToInt(((_damages - CurrentLife) * 2f * 100f) / (CurrentLife + _damages));
                MyLog(Name + " Pain proba " + painProbability + "%.");
                int painTest = random.Next(0, 101);
                if (painTest < painProbability)
                {
                    MyLog(Name + " ignore sa douleur.");
                }
            }
        }
    }
}
