using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] GameObject fairy;
    [SerializeField]
    private PlayerController playerController;
    public string[] lines;
    public float speedText;
    public TextMeshProUGUI dialogText;
    public GameObject interfacePlayer;
    [SerializeField]
    private HintsManager hintsManager;
    [SerializeField]
    Camera cameraM;

    public int index;

    void Awake()
    {
        index = 0;
        StartDialog();
        playerController.isMove = false;
        cameraM = Camera.main;
    }

    void StartDialog()
    {
        dialogText.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            dialogText.text += c;
            yield return new WaitForSeconds(speedText);
        }
    }

    public void scipTextClick()
    {
        if (dialogText.text == lines[index])
        {
            NextLines();
        }
        else
        {
            StopAllCoroutines();
            dialogText.text = lines[index];
        }
    }

    public void NextLines()
    {
        if (index < lines.Length - 1)
        {
            index++;
            StartDialog();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            interfacePlayer.SetActive(true);
            playerController.isMove = true;
            hintsManager.index = 0;
            hintsManager.dialogText.text = hintsManager.lines[hintsManager.index];
            hintsManager.showTime = Time.time;
            fairy.SetActive(false);
            gameObject.SetActive(false);
            return;
        }
        else
        {
            interfacePlayer.SetActive(true);
            playerController.isMove = true;
            hintsManager.index = 0;
            hintsManager.dialogText.text = hintsManager.lines[hintsManager.index];
            hintsManager.showTime = Time.time;
            cameraM.orthographicSize = 5f;
            gameObject.SetActive(false);


        }
    }
}
