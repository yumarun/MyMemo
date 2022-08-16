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
                foreach (var t2 in MemoManager._memosToBeDeletedLater)
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
                    return;
                }

                int idx = Utilities.CheckOrGetMemoIndexFromIndexOrTitleString(inputs[1]);
                if (idx != -1)
                {
                    MemoManager._memos[idx].PrintContents();
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
                    var idx = Utilities.CheckOrGetMemoIndexFromIndexOrTitleString(inputs[1]);
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
            else if (inputs[0] == command.DisplayCommands)
            {
                if (inputs.Length != 1)
                {
                    Console.WriteLine("This command need 1 arg.");
                }
                else
                {
                    ConfigManager.DisplayCommands();
                }
            }
            else if (inputs[0] == command.CreateMemo)
            {
                if (inputs.Length != 2)
                {
                    Console.WriteLine("This command need 2 args.");
                }
                else
                {
                    MemoManager.CreateNewMemo(inputs[1]);
                }
            }
            else if (inputs[0] == command.DeleteMemo)
            {
                if (inputs.Length != 2)
                {
                    Console.WriteLine("This command need 2 args.");
                    return;
                }
                
                if (!MemoManager._title_Idx.ContainsKey(inputs[1]))
                {
                    Console.WriteLine("This title doesn't exist.");
                }
                else
                {
                    Console.Write("Do you really delete this memo ? (Answer \"yes\" or \"no\".) >>> ");
                    var yesNo = Console.ReadLine();
                    if (yesNo != null)
                    {
                        yesNo = yesNo.ToLower();
                        if (yesNo == "yes")
                        {
                            MemoManager.DeleteMemo(inputs[1]);
                            Console.WriteLine($"Memo: {inputs[1]} was deleted.");
                        }
                    }

                }
                
            }
            else if (inputs[0] == command.AddLineToMemo)
            {
                if (inputs.Length != 3)
                {
                    Console.WriteLine("This command need 3 args.");
                    return;
                }


                var idx = Utilities.CheckOrGetMemoIndexFromIndexOrTitleString(inputs[1]);
                if (idx != -1)
                {
                    MemoManager._memos[idx].AddAContentToMemo(inputs[2]);
                }

            }
            else if (inputs[0] == command.DeleteLineFromMemo)
            {
                if (inputs.Length != 3)
                {
                    Console.WriteLine("This command need 3 args.");
                    return;
                }

                var idx = Utilities.CheckOrGetMemoIndexFromIndexOrTitleString(inputs[1]);
                if (idx != -1)
                {
                    int contentIdx;
                    if (int.TryParse(inputs[2], out contentIdx))
                    {
                        MemoManager._memos[idx].DeleteAContentFromMemo(contentIdx);
                    }
                    else
                    {
                        Console.WriteLine("Input No.2 must be int.");
                        return;
                    }

                }
            }
            else
            {
                Console.WriteLine("This command doesn't exist.");
            }
        }
    }
}