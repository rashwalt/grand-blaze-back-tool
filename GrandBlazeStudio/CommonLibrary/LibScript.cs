using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPython.Hosting;
using IronPython.Modules;
using Microsoft.Scripting;
using CommonLibrary.Character;
using CommonLibrary.Script;
using Microsoft.Scripting.Hosting;

namespace CommonLibrary
{
    public class LibScript
    {
        private ScriptScope scope;
        private List<LibPlayer> CharaList;

        //public ScUtil Util = new ScUtil();
        //public ScGet Get = new ScGet();

        private bool IsMoving;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="InCharaList">キャラリスト</param>
        public LibScript(List<LibPlayer> InCharaList)
        {
            CharaList = InCharaList;
        }

        /// <summary>
        /// スクリプト実行
        /// </summary>
        /// <param name="EntryNo">エントリー番号</param>
        /// <param name="PartyNo">パーティ番号</param>
        /// <param name="MarkID">マークID</param>
        /// <param name="IsAreaMoving">エリア移動設定</param>
        /// <param name="ScriptData">スクリプト</param>
        /// <returns>使用スクリプト表示内容</returns>
        public string Exec(int EntryNo, int PartyNo, int MarkID, ref bool IsAreaMoving, string ScriptData)
        {
            if (ScriptData.Length == 0) { return ""; }

            ScUtil.PutString = new StringBuilder();
            ScGet.EntryNo = EntryNo;
            ScGet.PartyNo = PartyNo;
            ScGet.CharaList = CharaList;
            ScGet.MarkID = MarkID;
            ScCheck.EntryNo = EntryNo;
            ScCheck.PartyNo = PartyNo;
            ScCheck.CharaList = CharaList;
            ScCheck.MarkID = MarkID;
            IsMoving = IsAreaMoving;

            ScopeSet(ref ScriptData);

            ScriptEngine engine = Python.CreateEngine();
            ScriptSource source = engine.CreateScriptSourceFromString(ScriptData);
            scope = engine.CreateScope();
            scope.SetVariable("party_no", PartyNo);
            scope.SetVariable("mark_id", MarkID);

            source.Execute(scope);

            IsAreaMoving = IsMoving;

            return ScUtil.PutString.ToString().Replace("\n", "<br />");
        }

        /// <summary>
        /// アイテム使用スクリプト実行
        /// </summary>
        /// <param name="EntryNo">エントリー番号</param>
        /// <param name="MarkID">マークID</param>
        /// <param name="ScriptData">スクリプト</param>
        /// <returns>使用スクリプト表示内容</returns>
        public string UseExec(int EntryNo, int MarkID, string ScriptData)
        {
            if (ScriptData.Length == 0) { return ""; }

            ScUtil.PutString = new StringBuilder();
            ScGet.EntryNo = EntryNo;
            ScGet.PartyNo = 0;
            ScGet.MarkID = MarkID;
            ScGet.CharaList = CharaList;
            ScCheck.EntryNo = EntryNo;
            ScCheck.PartyNo = 0;
            ScCheck.CharaList = CharaList;
            ScCheck.MarkID = MarkID;

            ScopeSet(ref ScriptData);

            ScriptEngine engine = Python.CreateEngine();
            ScriptSource source = engine.CreateScriptSourceFromString(ScriptData);
            scope = engine.CreateScope();
            scope.SetVariable("entry_no", EntryNo);
            scope.SetVariable("mark_id", MarkID);

            source.Execute(scope);

            return ScUtil.PutString.ToString();
        }

        private void ScopeSet(ref string Script)
        {
            // スコープ設定（関数）
            StringBuilder Scopes = new StringBuilder();

            Scopes.AppendLine("import clr");
            Scopes.AppendLine("clr.AddReference(\"CommonLibrary\")");
            Scopes.AppendLine("from CommonLibrary.Script import *");
            Scopes.AppendLine(Script);

            Script = Scopes.ToString();

        }
    }
}
