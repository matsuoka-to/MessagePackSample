using System;
using System.Collections.Generic;

namespace JsonData
{
    [Serializable]
    public class CharaData
    {
        public int id;
        public string name;
        public int order;
        public int height;
        public int weight;

        public List<Abilities> abilities;
        public List<Types> types;
        public List<Stats> stats;
        public Species species;
        public Sprites sprites;
    }

    [Serializable]
    public class Abilities
    {
        public int slot;
        public Ability ability;
        public bool is_hidden;
    }

    [Serializable]
    public class Ability
    {
        public string name;
        public string url;
    }

    [Serializable]
    public class Types
    {
        public int slot;
        public Type type;
    }

    [Serializable]
    public class Type
    {
        public string name;
        public string url;
    }

    [Serializable]
    public class Stats
    {
        public int base_stat;
        public int effort;
        public Stat stat;
    }

    [Serializable]
    public class Stat
    {
        public string name;
        public string url;
    }

    //--------------------------------------------

    [Serializable]
    public class AbilityStatus
    {
        public List<Names> names;
        public List<FlavorTextEntries> flavor_text_entries;
    }

    [Serializable]
    public class FlavorTextEntries
    {
        public string flavor_text;
        public Language language;
    }

    [Serializable]
    public class Names
    {
        public string name;
        public Language language;
    }

    [Serializable]
    public class Language
    {
        public string name;
        public string url;
    }

    //--------------------------------------------

    [Serializable]
    public class Species
    {
        public string name;
        public string url;
    }

    [Serializable]
    public class CharaNames
    {
        public List<Names> names;
    }

    //--------------------------------------------

    [Serializable]
    public class Sprites
    {
        public string back_default;
        public string back_female;
        public string back_shiny;
        public string back_shiny_female;
        public string front_default;
        public string front_female;
        public string front_shiny;
        public string front_shiny_female;
    }
}
