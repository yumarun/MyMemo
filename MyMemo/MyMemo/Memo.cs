using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace MyMemo
{
    public class Memo
    {

        public MemoJson _memo;
        public int _memoId;

        string TemporaryTitle;
        List<string> TemporarySentences;

        
        public Memo(MemoJson memo)
        {
            _memo = memo;
            TemporaryTitle = _memo.Title;
            TemporarySentences = _memo.Sentences;
        }
        

        // Outputting title and all contents of this memo.
        public void PrintContents()
        {
            Console.WriteLine("----");
            Console.WriteLine($"Title: {_memo.Title}");
            Console.WriteLine($"MemoId: {_memoId}");
            for (var i = 0; i < _memo.Sentences.Count; i++)
            {
                Console.WriteLine($"{i}: {_memo.Sentences[i]}");
            }
            Console.WriteLine("----");
        }

        // User must call this function before calling below functions;
        public void Prepare()
        {
            TemporaryTitle = _memo.Title;
            TemporarySentences = _memo.Sentences;
        }

        // Changing title of this memo.
        public void ChangeTitle(string newTitle)
        {
            if (string.IsNullOrEmpty(newTitle))
            {
                Console.WriteLine("The app could not change the title. New Title must not be Null or Empty.");
                return;
            }

            if (int.TryParse(newTitle, out int tmp))
            {
                Console.WriteLine("The memo title must contain non-numrical letter.");
                return;
            }

            TemporaryTitle = newTitle;
            EngMemoProcessing();
            
        }

        // Adding a memo content.
        public void AddAContentToMemo(string sentence)
        {
            if (string.IsNullOrEmpty(sentence))
            {
                Console.WriteLine("The content must not be Null or Empty.");
                return;
            }

            TemporarySentences.Add(sentence);
        }
        
        
        public void ReflectChange()
        {
            _memo.Title = TemporaryTitle;
            _memo.Sentences = TemporarySentences;
            _memo.LastUpdatedTime = DateTime.Now;
            TemporaryTitle = _memo.Title;
            TemporarySentences = _memo.Sentences;

            MemoJson latestMemo = new MemoJson();
            latestMemo.Title = _memo.Title;
            latestMemo.LastUpdatedTime = _memo.LastUpdatedTime;
            latestMemo.Sentences = _memo.Sentences;
            latestMemo.FileName = $"{_memo.Title}.json";
            string json = JsonSerializer.Serialize(latestMemo);
            using (var writer = new StreamWriter($"{ConfigManager._configJson.LocalPass}/{latestMemo.Title}.json"))
            {
                writer.WriteLine(json);
            }

            //File.Delete($"{ConfigManager._configJson.LocalPass}");
        }
        

        private void EngMemoProcessing()
        {
            MemoManager.AddToQueue(_memoId);
            if (ConfigManager._configJson.IsAutoSave)
            {
                MemoManager.Save();
            }
        }
    }

    public class MemoJson
    {
        public MemoJson()
        {
            Title = "";
            LastUpdatedTime = DateTime.Now;
            FileName = "";
            Sentences = new List<string>();
        }

        public string Title { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public string FileName { get; set; }
        public List<string> Sentences { get; set; }
    }

    
}
