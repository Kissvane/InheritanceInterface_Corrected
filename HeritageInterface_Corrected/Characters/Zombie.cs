using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageEtInterfaceCorrection
{
    public class ZombieSerializer : CharacterSerializer
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Zombie).IsAssignableFrom(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            ObjectWriting(writer, value, serializer, "ZOMBIE");
        }
    }

    [JsonConverter(typeof(ZombieSerializer))]
    class Zombie : Undead, IScavenger
    {
        public Zombie(string name) : base(name, 100, 0, 20, 50, 1000, (ConsoleColor)7) { }

        public override void Defend(int _attackValue, int _damage, Character _attacker)
        {
            MyLog(Name + " encaisse le coup.");
            //on calcule les dégâts finaux
            int finalDamages = (int)(_attackValue * _damage / 100f);
            if (_attacker.HolyDamages)
            {
                finalDamages *= 2;
            }
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
