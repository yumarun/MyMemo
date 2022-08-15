using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyMemo
{
    public static class Utilities
    {
        
        
        // ConfigJsonの中身を変えてこれを呼べばjsonが生成される.
        public static void GenerateConfigJson()
        {
            var configJson = new ConfigJson();
            configJson.Commands = new Command();
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(configJson, options);
            Console.WriteLine(jsonString);
        }

        public static int GetMemoIndexFromIndexOrTitleString(string input)
        {
            int idx;
            if (int.TryParse(input, out idx))
            {
                return idx;
            }
            else
            {
                if (MemoManager._title_Idx.TryGetValue(input, out idx))
                {
                    return MemoManager._title_Idx[input];
                }
                else
                {
                    Console.WriteLine("This title does't exist.");
                    return -1;
                }
            }
        }

    }
}
