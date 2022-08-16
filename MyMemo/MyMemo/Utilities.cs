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

        public static int CheckOrGetMemoIndexFromIndexOrTitleString(string input)
        {
            int idx;
            if (int.TryParse(input, out idx))
            {
                if (idx < MemoManager._memos.Count)
                {
                    return idx;
                }
                else
                {
                    Console.WriteLine("This index is out of range.");
                    return -1;
                }
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
