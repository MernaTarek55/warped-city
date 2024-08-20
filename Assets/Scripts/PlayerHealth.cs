using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public static int health = 7; // Static health variable shared across scripts
    public int maxHealth = 7;
    public Sprite[] heartSprites; // Array of sprites for different health states
    public Image healthImage;     // The Image component representing the health

    void Start()
    {
        health = maxHealth; // Initialize health to maximum at the start
        UpdateHealthBar();
    }

    void Update()
    {
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        // Ensure health is within the valid range
        health = Mathf.Clamp(health, 0, maxHealth);

        // Calculate which sprite to use based on the player's health
        int spriteIndex = Mathf.Clamp(health - 1, 0, heartSprites.Length - 1);
        healthImage.sprite = heartSprites[spriteIndex];
    }
}
