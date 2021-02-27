using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageEtInterfaceCorrection
{
    public class UndeadSerializer : CharacterSerializer
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Undead).IsAssignableFrom(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            ObjectWriting(writer, value, serializer, "UNDEAD");
        }
    }

    [JsonConverter(typeof(UndeadSerializer))]
    class Undead : Character
    {
        public Undead(string name, int attack, int defense, int initiative, int damages, int maxLife, ConsoleColor _color,bool unholyDamages_ = false) : base(name, attack, defense, initiative, damages, maxLife, cursed : true, unholyDamages:unholyDamages_, color:_color) { }
    }
}
