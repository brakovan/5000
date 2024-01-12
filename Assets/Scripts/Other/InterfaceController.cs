using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InterfaceController : MonoBehaviour
{
    private GameObject player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (player != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");

        }
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            gameObject.SetActive(false);
        }
        else
            gameObject.SetActive(true);

        if (player.GetComponent<PlayerController>().isHavePalka)
            transform.GetChild(1).gameObject.SetActive(true);
        else
            transform.GetChild(1).gameObject.SetActive(false);

    }
}
