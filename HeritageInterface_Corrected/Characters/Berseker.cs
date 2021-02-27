using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeritageEtInterfaceCorrection
{
    public class BerserkerSerializer : BeingSerializer
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Berseker).IsAssignableFrom(objectType);
        }

        public override void WriteCharacterProperties(JsonWriter writer, object value, JsonSerializer serializer)
        {
            base.WriteCharacterProperties(writer, value, serializer);
            Berseker berseker = value as Berseker;
            //DAMAGES
            writer.WritePropertyName("Damages");
            serializer.Serialize(writer, berseker.Damages);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            ObjectWriting(writer, value, serializer, "BERSEKER");
        }
    }

    [JsonConverter(typeof(BerserkerSerializer))]
    class Berseker : LivingBeing
    {
        public override void ConstructionHelper(List<JProperty> properties)
        {
            base.ConstructionHelper(properties);
            PainDuration = -1;
            Damages = (int)properties[8].Value;
        }

        public Berseker(string name) : base(name, 100, 100, 80, 20, 300, (ConsoleColor)1) { }

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
