using UnityEngine;
using UnityEngine.UI;

public class Abilitity : MonoBehaviour
{
    [SerializeField]
    Text title;

    [SerializeField]
    Text info;

    public void SetInfo(string title, string info)
    {
        this.title.text = title;
        this.info.text = info;
    }

}
