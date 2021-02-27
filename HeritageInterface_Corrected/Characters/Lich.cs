using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageEtInterfaceCorrection
{
    public class LichSerializer : CharacterSerializer
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Lich).IsAssignableFrom(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            ObjectWriting(writer, value, serializer, "LICH");
        }
    }

    [JsonConverter(typeof(LichSerializer))]
    class Lich : Undead
    {
        public Lich(string name) : base(name, 75, 125, 80, 50, 125, (ConsoleColor)3, unholyDamages_:true) { }
    }
}
