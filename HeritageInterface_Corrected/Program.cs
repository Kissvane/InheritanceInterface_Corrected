using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeritageEtInterfaceCorrection
{
    class Program
    {
        static FightManager fightManager;

        static void Main(string[] args)
        {
            List<Character> characters = new List<Character>
            {
                new Berseker("Berseker"),
                new Watcher("Gardien"),
                new Ghoul("Goule"),
                new Lich("Liche"),
                new Robot("Robot"),
                new Warrior("Guerrier"),
                new Zombie("Zombie")
            };

            fightManager = new FightManager(characters);
            fightManager.StartCombat(true);
        }

        static void SaveFight()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(fightManager, settings);
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "FightSave.json");
            Console.WriteLine("combat save to : " + filePath);
            File.WriteAllText(filePath, json);
        }

        static void LoadFight()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "FightSave.json");
            string jsonLoadedData = File.ReadAllText(filePath);
            fightManager = JsonConvert.DeserializeObject<FightManager>(jsonLoadedData, settings);
            Console.Clear();
            Console.WriteLine("combat loaded from : " + filePath);
            Console.WriteLine(fightManager.charactersList[4].Attack);
            fightManager.CombatReStart();
        }

        static void SaveFight2()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(fightManager, settings);
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "FightSave.json");
            Console.WriteLine("combat save to : " + filePath);
            File.WriteAllText(filePath, json);
        }

        static void LoadFight2()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "FightSave.json");
            string jsonLoadedData = File.ReadAllText(filePath);
            Console.Clear();
            fightManager = JsonConvert.DeserializeObject<FightManager>(jsonLoadedData, settings);
            Console.WriteLine("combat loaded from : " + filePath);
            fightManager.CombatReStart();
        }

        public static void waitInput()
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.S:
                    SaveFight2();
                    break;
                case ConsoleKey.L:
                    LoadFight2();
                    break;
                default:
                    fightManager.continueFight = true;
                    break;
            }
        }
    }
}
