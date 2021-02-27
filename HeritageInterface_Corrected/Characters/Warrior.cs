using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace HeritageEtInterfaceCorrection
{
    public class WarriorSerializer : BeingSerializer
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Warrior).IsAssignableFrom(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            ObjectWriting(writer, value, serializer, "WARRIOR");
        }
    }

    [JsonConverter(typeof(WarriorSerializer))]
    class Warrior : LivingBeing
    {
        public Warrior(string name) : base(name, 100, 100, 50, 100, 200, (ConsoleColor)5) { }

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
                    //can only be affected by pain during this round
                    CanAttack = false;
                }
            }
        }
    }
}
