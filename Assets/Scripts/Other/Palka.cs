using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palka : MonoBehaviour
{
    [SerializeField]
    private HintsManager hintsManager;

    [SerializeField]
    private LayerMask playerLayer;

    [SerializeField]
    private float pickupRadius = 5f;


    private void Update()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, pickupRadius, playerLayer);

        if (hitPlayer != null && Input.GetKeyDown(KeyCode.Q))
        {
            PlayerController playerController = hitPlayer.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.isHavePalka = true;
                playerController.UpdatePlayerAnimations();
                hintsManager.index = 1;
                hintsManager.dialogText.text = hintsManager.lines[hintsManager.index];
                hintsManager.showTime = Time.time;
                Destroy(gameObject);
            }
        }

    }

  

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}


