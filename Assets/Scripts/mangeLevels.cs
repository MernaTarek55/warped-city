using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class mangeLevels : MonoBehaviour
{
    public bool isfinalLevel = false;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            StartCoroutine(HandleCollision(collision.gameObject));
        }
    }

    IEnumerator HandleCollision(GameObject player)
    {
        //player.GetComponent<Player_Movement>().enabled = false;
        player.GetComponent<Animator>().enabled = false;
        player.GetComponent<Rigidbody2D>().isKinematic = true;
        player.GetComponent<Rigidbody2D>().velocity = new(0, 0);
        //player.transform.position = new(98.6f, 25, 0);
        for (int i = 0; i < 600; i++)
        {
            player.transform.Rotate(0, 0, 0.6f);
            player.transform.localScale /= 1.001f;
            yield return new WaitForSeconds(0.003f);
        }
        //audioManager.PlaySFX(audioManager.Win);
        if (!isfinalLevel)
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene("EndScene");
        }
        
    }
}
