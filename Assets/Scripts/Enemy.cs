using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public int power;

    public TextMeshProUGUI text;
    private void Awake()
    {
        text.SetText("Power : {0}", power);
    }
}
