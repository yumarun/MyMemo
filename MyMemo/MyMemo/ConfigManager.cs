using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace MyMemo
{

    public class ConfigJson
    {
        public string LocalPass { get; set; }
        public bool IsDisplayingMemoListEveryTime { get; set; }
        public bool IsAutoSave { get; set; }

        public Command Commands { get; set; }

    }

    public class Command
    {
        public string Quit { get; set; }
        public string Save { get; set; }
        public string DisplayCommands { get; set; }
        public string DisplayMemoList { get; set; }
        public string CreateMemo { get; set; }
        public string DeleteMemo { get; set; }
        public string AddLineToMemo { get; set; }
        public string DeleteLineFromMemo { get; set; }
        public string DisplayMemoContent { get; set; }
        public string ChangeMemoTitle { get; set; }
    }

    internal static class ConfigManager
    {
        public static ConfigJson _configJson;

        public static void GetConfigFromJson()
        {
            // _configJsonにconfig.jsonの内容をぶち込む
            using (var sr = new StreamReader("config.json"))
            {
                string tmp = sr.ReadToEnd();
                _configJson = JsonSerializer.Deserialize<ConfigJson>(tmp);
            }


        }

        public static void DisplayCommands()
        {
            var option = new JsonSerializerOptions();
            option.WriteIndented = true;
            string json = JsonSerializer.Serialize(_configJson.Commands, option);

            Console.WriteLine("---Command list---------");
            Console.WriteLine(json);
            Console.WriteLine("------------------------");
        }
    }
}
