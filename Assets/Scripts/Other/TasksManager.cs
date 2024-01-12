using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TasksManager : MonoBehaviour
{
    public string[] lines;
    public TextMeshProUGUI dialogText;

    public int index;

    void Awake()
    {
        index = 0;
        dialogText.text = lines[index];

    }

    public void NextTask()
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
