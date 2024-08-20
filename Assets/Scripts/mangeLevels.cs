using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class mangeLevels : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            StartCoroutine(HandleCollision(collision.gameObject));
        }
    }

    IEnumerator HandleCollision(GameObject player)
    {
        player.GetComponent<Rigidbody2D>().isKinematic = true;
        Animator animator = player.GetComponent<Animator>();
        animator.SetTrigger("isOnHole");
        yield return new WaitForSeconds(2f);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
