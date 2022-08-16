# MyMemo
メモ管理用アプリ。

## 使用法
1. releaseから最新バージョンをダウンロード.
2. MyMemo.exeが置いてあるディレクトリのconfig.jsonを開き, LocalPathを書き換える(この場所にメモが保存される)
3. MyMemo.exeを開く

#### コマンド一覧 (コマンドの記述名は, config.jsonから変更可能)
- DisplayCommands ... コマンド一覧を表示.
  - ex) cmd
- Quit ... アプリの終了.
  - ex) quit
- Save ... 変更をセーブ.
  - ex) save
- DisplayMemoList ... メモ一覧を表示.
  - ex) ls
- CreateMemo ... 新規メモを作成.
  - ex) create [メモ名]
- DisplayMemoContent ... 既存メモの中身を閲覧.
  - ex) cat [メモ名 or メモの通し番号]
- DeleteMemo ... 既存メモを削除.
  - ex) remove [メモ名]
- AddLineToMemo ... 既存メモに1行追加
  - ex) w [メモ名 or メモの通し番号] [メモ内容]
- DeleteLineFromMemo ... 既存メモから消したい1行を削除
  - ex) d [メモ名 or メモの通し番号] [メモの消したい行の行番号]
- ChangeMemoTitle ... 既存メモのタイトルを変更
  - ex) chttl [新しいタイトル]


## 開発
- Visual Studio 2022
- net6.0
