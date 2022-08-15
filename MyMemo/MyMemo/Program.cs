using System.Text.Json;

namespace MyMemo
{
    internal class Program
    {


        static void Main(string[] args)
        {
            Prepare();
            Start();


            string line;
            while ((line = Console.ReadLine()) != "quit")
            {
                string[] inputs = line.Split();

                // Handleing inputs.
                HandleInput(inputs);
                Console.WriteLine("--");
                Console.WriteLine("idx queue");
                foreach (var t1 in MemoManager._changedMemoIdx)
                {
                    Console.WriteLine(t1);
                }
                Console.WriteLine("title queue");
                foreach (var t2 in MemoManager._changedMemoTitle)
                {
                    Console.WriteLine(t2);
                }
                Console.WriteLine("--");
                

                Console.WriteLine();
                Console.Write(">>> ");
            }

            //var tmp = new MemoJson();
            //tmp.LastUpdatedTime = DateTime.Now;
            //tmp.Sentences = new List<string>();
            //tmp.Sentences.Add("a"); tmp.Sentences.Add("bbbb");
            //var options = new JsonSerializerOptions { WriteIndented = true };
            //string jsonString = JsonSerializer.Serialize(tmp, options);
            //Console.WriteLine(jsonString);



            Console.ReadLine();
        }

        static void Prepare()
        {
            ConfigManager.GetConfigFromJson();
            MemoManager.Prepare();
        }

        static void Start()
        {
            Console.WriteLine("Hello.");
            Console.Write(">>> ");

        }

        static void HandleInput(string[] inputs)
        {
            var command = ConfigManager._configJson.Commands;
            if (inputs[0] == command.DisplayMemoContent) // DisplayMemoContent.
            {
                if (inputs.Length != 2)
                {
                    Console.WriteLine("This command need 2 args.");
                }
                else
                {
                    int idx;
                    if (int.TryParse(inputs[1], out idx)) 
                    {
                        if (idx < MemoManager._memos.Count)
                        {
                            MemoManager._memos[idx].PrintContents();
                        }
                        else
                        {
                            Console.WriteLine("The index is out of length.");
                        }
                    }
                    else
                    {
                        if (MemoManager._title_Idx.TryGetValue(inputs[1], out idx))
                        {
                            if (idx < MemoManager._memos.Count)
                            {
                                MemoManager._memos[idx].PrintContents();
                            }
                            else
                            {
                                Console.WriteLine("The index is out of length.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("The arg in not in Titles.");
                        }

                    }
                }
            }
            else if (inputs[0] == command.ChangeMemoTitle)
            {
                if (inputs.Length != 3)
                {
                    Console.WriteLine("This command need 3 args.");
                }
                else
                {
                    var idx = Utilities.GetMemoIndexFromIndexOrTitleString(inputs[1]);
                    if (idx != -1)
                    {
                        MemoManager._memos[idx].ChangeTitle(inputs[2]);
                    }
                }
            }
            else if (inputs[0] == command.DisplayMemoList)
            {
                if (inputs.Length != 1)
                {
                    Console.WriteLine("This command need 1 arg.");
                }
                else
                {
                    MemoManager.DisplayMemoList();
                }
            }
        }
    }
}