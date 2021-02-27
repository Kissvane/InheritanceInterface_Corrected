using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageEtInterfaceCorrection
{
    public class GhoulSerializer : CharacterSerializer
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Ghoul).IsAssignableFrom(objectType);
        }

        public override void WriteCharacterProperties(JsonWriter writer, object value, JsonSerializer serializer)
        {
            base.WriteCharacterProperties(writer, value, serializer);
            IPainFeeler pain = value as IPainFeeler;
            //PAIN DURATION
            writer.WritePropertyName("PainDuration");
            serializer.Serialize(writer, pain.PainDuration);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            ObjectWriting(writer, value, serializer, "GHOUL");
        }
    }

    [JsonConverter(typeof(GhoulSerializer))]
    class Ghoul : Undead, IScavenger, IPainFeeler
    {
        public Ghoul(string name) : base(name, 80, 80, 120, 30, 250, (ConsoleColor)2)
        {
            PainDuration = -1;
        }

        public int PainDuration { get; set; }

        public void EatBody()
        {
            CurrentLife += random.Next(50, 101);
            CurrentLife = Math.Min(CurrentLife, MaxLife);
            MyLog(Name + " mange un corps. Sa vie est maintenant à " + CurrentLife + ".");
        }

        public override void CanAttackReset()
        {
            if (PainDuration == -1)
                CanAttack = true;
            else
            {
                MyLog(Name+" a mal et ne peut pas attaquer durant ce round.");
                PainDuration--;
            }
        }

        public void TestPain(int _damages)
        {
            //can be affected by pain
            if (_damages > CurrentLife && CurrentLife > 0)
            {
                int painProbability = RoundToInt( ((_damages - CurrentLife) * 2f * 100f) / (CurrentLife + _damages));
                MyLog(Name+" Pain proba " + painProbability + "%.");
                int painTest = random.Next(0, 101);
                if (painTest < painProbability)
                {
                    PainDuration = random.Next(-1, 2);
                    if (CanAttack)
                    {
                        if (PainDuration != -1)
                        {
                            MyLog(Name + " a mal et ne peut pas attaquer durant ce round et les " + (PainDuration + 1) + " suivants.");
                        }
                        else
                        {
                            MyLog(Name + " a mal et ne peut pas attaquer durant ce round.");
                        }
                        CanAttack = false;
                    }
                }
            }
        }

        public override void TakeDamages(int _damages)
        {
            base.TakeDamages(_damages);
            TestPain(_damages);
        }
    }
}
