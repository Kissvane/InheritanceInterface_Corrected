using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageEtInterfaceCorrection
{
    public class RobotSerializer : CharacterSerializer
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Robot).IsAssignableFrom(objectType);
        }

        public override void WriteCharacterProperties(JsonWriter writer, object value, JsonSerializer serializer)
        {
            base.WriteCharacterProperties(writer, value, serializer);
            Robot robot = value as Robot;
            //ATTACK
            writer.WritePropertyName("Attack");
            serializer.Serialize(writer, robot.Attack);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            ObjectWriting(writer, value, serializer, "ROBOT");
        }
    }

    [JsonConverter(typeof(RobotSerializer))]
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
