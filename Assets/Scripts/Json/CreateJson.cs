
using UnityEditor;
using UnityEngine;

using System.IO;
using System.Collections.Generic;
using System;

using JsonData;

public class CreateJson : EditorWindow
{
    int max = 1000;

    [MenuItem("Assets/Json")]
    public static void CreateData()
    {
        EditorWindow.GetWindow(typeof(CreateJson), false, "Json Data");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Write"))
        {
            CreateJsonData();
        }
        if (GUILayout.Button("Load"))
        {
            LoadJsonData();
        }
    }

    private void CreateJsonData()
    {
        var data = new JsonAllData();
        data.players = new List<Player>();
        data.enemies = new List<Enemy>();

        for (var i = 0; i < max; i++)
        {
            var player = new Player();
            player.id = i;
            player.name = string.Format($"player{i}");
            player.hp = 100;
            player.hpMax = 100;
            player.mp = 100;
            player.mpMax = 100;
            player.skills = new List<Skill>();
            for(var j = 0; j < max; j++)
            {
                var skill = new Skill();
                skill.id = i;
                skill.name = string.Format($"skill{i}");
                skill.attack = 100;
                skill.defense = 100;
                skill.heal = 0;
                player.skills.Add(skill);
            }
            data.players.Add(player);
        }

        for (var i = 0; i < max; i++)
        {
            var enemy = new Enemy();
            enemy.id = i;
            enemy.name = string.Format($"enemy{i}");
            enemy.hp = 100;
            enemy.hpMax = 100;
            enemy.mp = 100;
            enemy.mpMax = 100;
            enemy.skills = new List<Skill>();
            for (var j = 0; j < max; j++)
            {
                var skill = new Skill();
                skill.id = i;
                skill.name = string.Format($"skill{i}");
                skill.attack = 100;
                skill.defense = 100;
                skill.heal = 0;
                enemy.skills.Add(skill);
            }
            data.enemies.Add(enemy);
        }

        var filepath = Application.dataPath;
        filepath = Path.Combine(filepath, "../Document");
        if (!Directory.Exists(filepath))
        {
            Directory.CreateDirectory(filepath);
        }
        filepath = Path.Combine(filepath, "json.dat");

        Debug.LogErrorFormat("save start");

        var startData = DateTime.Now;

        var json = JsonUtility.ToJson(data);            // jsonとして変換
        var wr = new StreamWriter(filepath, false);     // ファイル書き込み指定
        wr.WriteLine(json);                             // json変換した情報を書き込み
        wr.Close();                                     // ファイル閉じる

        AssetDatabase.Refresh();

        var endTime = DateTime.Now - startData;
        Debug.LogErrorFormat($"save end : {endTime.TotalMilliseconds}");
    }

    private void LoadJsonData()
    {
        var filepath = Application.dataPath;
        filepath = Path.Combine(filepath, "../Document");
        filepath = Path.Combine(filepath, "json.dat");
        if(!File.Exists(filepath))
        {
            Debug.LogErrorFormat("file error");
            return;
        }

        Debug.LogErrorFormat("load start");

        var startData = DateTime.Now;

        var rd = new StreamReader(filepath);                // ファイル読み込み指定
        string json = rd.ReadToEnd();                       // ファイル内容全て読み込む
        rd.Close();                                         // ファイル閉じる

        var data = JsonUtility.FromJson<JsonAllData>(json); // jsonファイルを型に戻して返す

        AssetDatabase.Refresh();

        var endTime = DateTime.Now - startData;
        Debug.LogErrorFormat($"load end : {endTime.TotalMilliseconds} | {data.players.Count} | {data.enemies.Count}");
    }

}
