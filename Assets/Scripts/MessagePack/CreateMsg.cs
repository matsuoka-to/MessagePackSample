
using UnityEditor;
using UnityEngine;

using System.IO;
using System.Collections.Generic;
using System;

using MessagePack;
using MsgData;

public class CreateMsg : EditorWindow
{
    int max = 1000;

    [MenuItem("Assets/MessagePack")]
    public static void CreateData()
    {
        EditorWindow.GetWindow(typeof(CreateMsg), false, "MessagePack Data");
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
            for (var j = 0; j < max; j++)
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
        filepath = Path.Combine(filepath, "message.dat");

        Debug.LogErrorFormat("save start");

        var startData = DateTime.Now;

        var bytes = MessagePackSerializer.Serialize(data);
        File.WriteAllBytes(filepath, bytes);

        AssetDatabase.Refresh();

        var endTime = DateTime.Now - startData;
        Debug.LogErrorFormat($"save end : {endTime.TotalMilliseconds}");
    }

    private void LoadJsonData()
    {
        var filepath = Application.dataPath;
        filepath = Path.Combine(filepath, "../Document");
        filepath = Path.Combine(filepath, "message.dat");
        if (!File.Exists(filepath))
        {
            Debug.LogErrorFormat("file error");
            return;
        }

        Debug.LogErrorFormat("load start");

        var startData = DateTime.Now;

        var bytes = File.ReadAllBytes(filepath);
        var data = MessagePackSerializer.Deserialize<JsonAllData>(bytes);

        AssetDatabase.Refresh();

        var endTime = DateTime.Now - startData;
        Debug.LogErrorFormat($"load end : {endTime.TotalMilliseconds} | {data.players.Count} | {data.enemies.Count}");
    }

}
