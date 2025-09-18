
using System.Collections.Generic;
using MessagePack;

namespace MsgData
{
    [MessagePackObject]
    public class JsonAllData
    {
        [Key("players")]
        public List<Player> players;
        [Key("enemies")]
        public List<Enemy> enemies;
    }

    [MessagePackObject]
    public class Player
    {
        [Key("id")]
        public long id;
        [Key("name")]
        public string name;
        [Key("hp")]
        public long hp;
        [Key("hpMax")]
        public long hpMax;
        [Key("mp")]
        public long mp;
        [Key("mpMax")]
        public long mpMax;
        [Key("skills")]
        public List<Skill> skills;
    }

    [MessagePackObject]
    public class Enemy
    {
        [Key("id")]
        public long id;
        [Key("name")]
        public string name;
        [Key("hp")]
        public long hp;
        [Key("hpMax")]
        public long hpMax;
        [Key("mp")]
        public long mp;
        [Key("mpMax")]
        public long mpMax;
        [Key("skills")]
        public List<Skill> skills;
    }

    [MessagePackObject]
    public class Skill
    {
        [Key("id")]
        public long id;
        [Key("name")]
        public string name;
        [Key("attack")]
        public long attack;
        [Key("defense")]
        public long defense;
        [Key("heal")]
        public long heal;
    }
}
