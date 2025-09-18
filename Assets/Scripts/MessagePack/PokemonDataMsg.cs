
using System.Collections.Generic;
using MessagePack;

namespace MsgData
{
    [MessagePackObject]
    public class CharaData
    {
        [Key("id")]
        public int id;
        [Key("name")]
        public string name;
        [Key("order")]
        public int order;
        [Key("height")]
        public int height;
        [Key("weight")]
        public int weight;

        [Key("abilities")]
        public List<Abilities> abilities;
        [Key("types")]
        public List<Types> types;
        [Key("stats")]
        public List<Stats> stats;
        [Key("species")]
        public Species species;
        [Key("sprites")]
        public Sprites sprites;
    }

    [MessagePackObject]
    public class Abilities
    {
        [Key("slot")]
        public int slot;
        [Key("ability")]
        public Ability ability;
        [Key("is_hidden")]
        public bool is_hidden;
    }

    [MessagePackObject]
    public class Ability
    {
        [Key("name")]
        public string name;
        [Key("url")]
        public string url;
    }

    [MessagePackObject]
    public class Types
    {
        [Key("slot")]
        public int slot;
        [Key("type")]
        public Type type;
    }

    [MessagePackObject]
    public class Type
    {
        [Key("name")]
        public string name;
        [Key("url")]
        public string url;
    }

    [MessagePackObject]
    public class Stats
    {
        [Key("base_stat")]
        public int base_stat;
        [Key("effort")]
        public int effort;
        [Key("stat")]
        public Stat stat;
    }

    [MessagePackObject]
    public class Stat
    {
        [Key("name")]
        public string name;
        [Key("url")]
        public string url;
    }

    //--------------------------------------------

    [MessagePackObject]
    public class AbilityStatus
    {
        [Key("names")]
        public List<Names> names;
        [Key("flavor_text_entries")]
        public List<FlavorTextEntries> flavor_text_entries;
    }

    [MessagePackObject]
    public class FlavorTextEntries
    {
        [Key("flavor_text")]
        public string flavor_text;
        [Key("language")]
        public Language language;
    }

    [MessagePackObject]
    public class Names
    {
        [Key("name")]
        public string name;
        [Key("language")]
        public Language language;
    }

    [MessagePackObject]
    public class Language
    {
        [Key("name")]
        public string name;
        [Key("url")]
        public string url;
    }

    //--------------------------------------------

    [MessagePackObject]
    public class Species
    {
        [Key("name")]
        public string name;
        [Key("url")]
        public string url;
    }

    [MessagePackObject]
    public class CharaNames
    {
        [Key("names")]
        public List<Names> names;
    }

    //--------------------------------------------

    [MessagePackObject]
    public class Sprites
    {
        [Key("back_default")]
        public string back_default;
        [Key("back_female")]
        public string back_female;
        [Key("back_shiny")]
        public string back_shiny;
        [Key("back_shiny_female")]
        public string back_shiny_female;
        [Key("front_default")]
        public string front_default;
        [Key("front_female")]
        public string front_female;
        [Key("front_shiny")]
        public string front_shiny;
        [Key("front_shiny_female")]
        public string front_shiny_female;
    }
}
