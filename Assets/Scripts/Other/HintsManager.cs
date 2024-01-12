using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintsManager : MonoBehaviour
{
    public string[] lines;
    public TextMeshProUGUI dialogText;
    public float showTime;
    public float hintTime;

    public int index = -1;

    void Awake()
    {
        dialogText.text = null;
    }

    private void Update()
    {
        if (index >= 0 && Time.time > showTime + hintTime)
        {
            dialogText.text = null;
            index = -1;
        }
    }
    public void NextHint()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogText.text = lines[index];
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
