using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_manager : MonoBehaviour
{
    void Update()
    {
        // Check if either the main Enter key or the numeric keypad Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return)) { 
            SceneManager.LoadScene(0);
        }
    }
}
