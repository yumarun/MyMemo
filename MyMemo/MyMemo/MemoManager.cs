﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyMemo
{
    public static class MemoManager
    {
        public static List<Memo> _memos = new List<Memo>();

        // ↓ 後でprivateに
        public static Queue<int> _changedMemoIdx = new Queue<int>();

        // ↓ 後でprivateに
        public static Queue<string> _changedMemoTitle = new Queue<string>();

        public static Dictionary<string, int> _title_Idx = new Dictionary<string, int>();


        public static void Prepare()
        {
            GetMemoFromJson();
            MakeDictionary();
        }
        private static void GetMemoFromJson()
        {
            var memoPass = ConfigManager._configJson.LocalPass;
            string[] memoFiles = Directory.GetFiles(memoPass);
            Console.WriteLine(memoFiles[0]);
            Console.WriteLine(Path.GetFileName(memoFiles[0]));

            for (var i = 0; i < memoFiles.Length; i++)
            {
                using (var reader = new StreamReader(memoFiles[i]))
                {
                    MemoJson tmpJson = JsonSerializer.Deserialize<MemoJson>(reader.ReadToEnd());
                    Memo tmpMemo = new Memo(tmpJson);
                    tmpMemo._memoId = i;
                    _memos.Add(tmpMemo);
                }
            }
        }

        private static void MakeDictionary()
        {
            for (var i = 0; i < _memos.Count; i++)
            {
                _title_Idx.Add(_memos[i]._memo.Title, i);
            }
        }

        public static void Save()
        {
            // App save changes.
            while (_changedMemoIdx.Count != 0)
            {
                var idx = _changedMemoIdx.Dequeue();
                _memos[idx].ReflectChange();
            }

            // Delete old json.
            while (_changedMemoTitle.Count != 0)
            {
                var title = _changedMemoTitle.Dequeue();
                File.Delete($"{ConfigManager._configJson.LocalPass}/{title}.json");
            }
        }

        public static void AddToQueue(int idx)
        {
            _changedMemoIdx.Enqueue(idx);
        }

        
        
    }
}
