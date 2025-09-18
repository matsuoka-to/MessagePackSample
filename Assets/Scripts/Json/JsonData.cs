
using System;
using System.Collections.Generic;

namespace JsonData
{
    [Serializable]
    public class JsonAllData
    {
        public List<Player> players;
        public List<Enemy> enemies;
    }

    [Serializable]
    public class Player
    {
        public long id;
        public string name;
        public long hp;
        public long hpMax;
        public long mp;
        public long mpMax;
        public List<Skill> skills;
    }

    [Serializable]
    public class Enemy
    {
        public long id;
        public string name;
        public long hp;
        public long hpMax;
        public long mp;
        public long mpMax;
        public List<Skill> skills;
    }

    [Serializable]
    public class Skill
    {
        public long id;
        public string name;
        public long attack;
        public long defense;
        public long heal;
    }
}

