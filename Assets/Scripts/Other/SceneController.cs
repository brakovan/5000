using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    public int sceneIndex;
    private void Awake()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (sceneIndex == 1)
        {
            LoadSceneByIndex(2);
        }
        if (sceneIndex == 2 && playerController.isHavePalka == true && GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            LoadSceneByIndex(3);
        }
    }

    public void ExitGame()
    {
        // «акрывает игру, если она запущена не в редакторе Unity
        Application.Quit();

        // ¬ыводит сообщение в консоль (полезно дл€ отладки)
        Debug.Log("Game is exiting");
    }
}
