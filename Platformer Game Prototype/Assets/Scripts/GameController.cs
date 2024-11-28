﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("Player Data")]
    [SerializeField, Range(0, 10)] private int lifes;
    [SerializeField, Range(0, 5)] private int health;
    [SerializeField, Range(0, 99)] private int coins;
    [SerializeField, Range(0, 99)] private int orbs;

    [Header("UI GameObjects")]
    [SerializeField] private GameObject menu;

    [Header("UI Data")]
    [SerializeField] private Image[] currentLife = new Image[2];
    [SerializeField] private Image currentHealth;
    [SerializeField] private Image[] currentCoins = new Image[2];
    [SerializeField] private Image[] currentOrbs = new Image[2];

    [Header("Fonts")]
    [SerializeField] private List<Sprite> numberFonts = new List<Sprite>();

    private Image menuFillImage; // Cache for Menu Image

    private void Awake()
    {
        instance = this;
        menuFillImage = menu.GetComponentInChildren<Image>();
    }

    private void Update()
    {
        HandleMenuInput();
        UpdateUI();
    }

    public void UpdateUI()
    {
        // Update Life
        if (lifes >= 0 && lifes <= 10)
        {
            UpdateDigitUI(currentLife, lifes);
        }
        else
        {
            Debug.LogWarning("Life value is out of bounds!");
        }

        // Update Health
        if (health >= 0 && health <= 5)
        {
            currentHealth.sprite = numberFonts[health];
            menuFillImage.fillAmount = health / 5f; // Calculates the value directly as a ratio
        }
        else
        {
            Debug.LogWarning("Health value is out of bounds!");
        }

        // Update Coins
        UpdateDigitUI(currentCoins, coins);

        // Update Orbs
        UpdateDigitUI(currentOrbs, orbs);
    }

    private void UpdateDigitUI(Image[] digitImages, int value)
    {
        if (digitImages.Length >= 2)
        {
            int tens = value / 10;
            int units = value % 10;

            digitImages[0].sprite = numberFonts[tens];
            digitImages[1].sprite = numberFonts[units];
        }
        else
        {
            Debug.LogWarning("Digit image array is incomplete!");
        }
    }

    public void GetCoin()
    {
        coins = Mathf.Clamp(coins + 1, 0, 99); // Limit max value to 99
        UpdateUI();
    }

    public void HandleMenuInput()
    {
        // Toggle menu when pressing "Fire3" button
        if (Input.GetButtonDown("Fire3"))
        {

        }
    }
}