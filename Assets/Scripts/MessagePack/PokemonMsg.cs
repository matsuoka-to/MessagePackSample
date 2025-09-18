using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MsgData;
using MessagePack;

public class PokemonMsg : MonoBehaviour
{
    [SerializeField]
    Button[] buttons;

    [SerializeField]
    Text[] texts;

    [SerializeField]
    Image image;

    [SerializeField]
    GameObject ContentRoot;

    [SerializeField]
    Abilitity abilitityPrefab;

    enum ButtonType
    {
        Left,
        Right,
        Enter
    };

    enum TextType
    {
        Select,
        Name,
        Type,
        Hp,
        Attack,
        Defense,
        SpecailAttack,
        SpecailDefense,
        Speed,
        Height,
        Weight
    };

    private const int max = 1000;
    private int select;
    private bool isSend;
    private List<Abilitity> abilitityList;
    private List<Coroutine> coroutines;

    private void Start()
    {
        select = 1;
        isSend = false;
        abilitityList = new List<Abilitity>();
        coroutines = new List<Coroutine>();

        for (var i = 0; i < buttons.Length; i++)
        {
            var id = i;
            buttons[i].onClick.AddListener(() => OnButtonCallBack(id));
        }
    }

    private void OnButtonCallBack(int id)
    {
        if(isSend)
        {
            return;
        }

        switch((ButtonType)id)
        {
            case ButtonType.Left:
            {
                select--;
                select = Mathf.Clamp(select, 1, max);
            }
            break;

            case ButtonType.Right:
            {
                select++;
                select = Mathf.Clamp(select, 1, max);
            }
            break;

            case ButtonType.Enter:
            {
                isSend = true;
                coroutines.Clear();
                buttons[(int)ButtonType.Enter].enabled = false;
                StartCoroutine(NetworkSend());
            }
            break;
        }

        texts[(int)TextType.Select].text = select.ToString();
    }

    IEnumerator NetworkSend()
    {
        var url = string.Format($"https://pokeapi.co/api/v2/pokemon/{select}");
        var request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if(abilitityList.Count != 0)
        {
            for(var i = abilitityList.Count - 1; i >= 0; i--)
            {
                GameObject.Destroy(abilitityList[i].gameObject);
                abilitityList.RemoveAt(i);
            }
        }

        var json = request.downloadHandler.text;
        if(!string.IsNullOrEmpty(json))
        {
            Debug.LogErrorFormat("load start");

            var startData = DateTime.Now;

            var text = MessagePackSerializer.ConvertFromJson(json);
            var data = MessagePackSerializer.Deserialize<CharaData>(text);

            var co0 = StartCoroutine(SpriteSend(data.sprites.front_default));
            coroutines.Add(co0);
            var co1 = StartCoroutine(NameSend(data.species.url));
            coroutines.Add(co1);

            for (var i = 0; i < data.abilities.Count; i++)
            {
                var abilitieUrl = data.abilities[i].ability.url;
                var co2 = StartCoroutine(AbilitySend(abilitieUrl));
                coroutines.Add(co2);
            }

            texts[(int)TextType.Type].text = data.types[0].type.name.ToString();
            texts[(int)TextType.Hp].text = data.stats.FirstOrDefault(x => x.stat.name == "hp").base_stat.ToString();
            texts[(int)TextType.Attack].text = data.stats.FirstOrDefault(x => x.stat.name == "attack").base_stat.ToString();
            texts[(int)TextType.Defense].text = data.stats.FirstOrDefault(x => x.stat.name == "defense").base_stat.ToString();
            texts[(int)TextType.SpecailAttack].text = data.stats.FirstOrDefault(x => x.stat.name == "special-attack").base_stat.ToString();
            texts[(int)TextType.SpecailDefense].text = data.stats.FirstOrDefault(x => x.stat.name == "special-defense").base_stat.ToString();
            texts[(int)TextType.Speed].text = data.stats.FirstOrDefault(x => x.stat.name == "speed").base_stat.ToString();
            texts[(int)TextType.Height].text = data.height.ToString();
            texts[(int)TextType.Weight].text = data.weight.ToString();

            for(var i = coroutines.Count - 1; i >= 0; i--)
            {
                yield return coroutines[i];
                coroutines.RemoveAt(i);
            }

            var endTime = DateTime.Now - startData;
            Debug.LogErrorFormat($"load end : {endTime.TotalMilliseconds}");
        }

        buttons[(int)ButtonType.Enter].enabled = true;
        isSend = false;
    }

    /// <summary>
    /// 画像取得
    /// </summary>
    IEnumerator SpriteSend(string url)
    {
        var request = UnityWebRequestTexture.GetTexture(url);

        yield return request.SendWebRequest();

        var tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
        image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
    }

    /// <summary>
    /// 名前取得
    /// </summary>
    IEnumerator NameSend(string url)
    {
        var request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        var json = request.downloadHandler.text;
        if (!string.IsNullOrEmpty(json))
        {
            Debug.LogErrorFormat("name start");

            var startData = DateTime.Now;
            var text = MessagePackSerializer.ConvertFromJson(json);
            var data = MessagePackSerializer.Deserialize<CharaNames>(text);
            var name = data.names.FirstOrDefault(x => x.language.name == "ja");
            texts[(int)TextType.Name].text = name.name.ToString();

            var endTime = DateTime.Now - startData;
            Debug.LogErrorFormat($"name end : {endTime.TotalMilliseconds}");
        }
    }

    /// <summary>
    /// アビリティー取得
    /// </summary>
    IEnumerator AbilitySend(string url)
    {
        var request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        var json = request.downloadHandler.text;
        if (!string.IsNullOrEmpty(json))
        {
            Debug.LogErrorFormat("ability start");

            var startData = DateTime.Now;
            var text = MessagePackSerializer.ConvertFromJson(json);
            var data = MessagePackSerializer.Deserialize<AbilityStatus>(text);

            var name = data.names.FirstOrDefault(x => x.language.name == "ja");
            var info = data.flavor_text_entries.FirstOrDefault(x => x.language.name == "ja");

            var obj = Abilitity.Instantiate(abilitityPrefab);
            obj.SetInfo(name.name, info.flavor_text);
            obj.transform.SetParent(ContentRoot.transform);

            abilitityList.Add(obj);

            var endTime = DateTime.Now - startData;
            Debug.LogErrorFormat($"ability end : {endTime.TotalMilliseconds}");
        }
    }

}
