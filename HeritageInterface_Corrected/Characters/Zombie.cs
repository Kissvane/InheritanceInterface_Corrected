using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageEtInterfaceCorrection
{
    class Zombie : Undead, IScavenger
    {
        public Zombie(string name) : base(name, 100, 0, 20, 50, 1000, (ConsoleColor)7) { }

        public override void Defend(int _attackValue, int _damage, Character _attacker, bool canBeCountered)
        {
            MyLog(Name + " encaisse le coup.");
            //on calcule les dégâts finaux
            int finalDamages = (int)(_attackValue * _damage / 100f);
            TakeDamages(finalDamages);
        }

        public void EatBody()
        {
            CurrentLife += random.Next(50, 101);
            CurrentLife = Math.Min(CurrentLife, MaxLife);
            MyLog(Name + " mange un corps. Sa vie est maintenant à " + CurrentLife + ".");
        }
    }
}
