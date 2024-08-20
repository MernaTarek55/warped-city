using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_manager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) { 
            SceneManager.LoadScene("MainMenu");
        }
    }
}
