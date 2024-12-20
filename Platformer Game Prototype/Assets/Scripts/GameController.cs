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

    [Header("Items")]
    [SerializeField, Range(0, 5)] private int potions;
    [SerializeField, Range(0, 5)] private int antidote;
    [SerializeField, Range(0, 5)] private int meat;
    [SerializeField, Range(0, 5)] private int apple;

    [Header("UI GameObjects")]
    private Image healthFillImage;
    private Image menuFillPanel;
    private bool openPanel = false;

    [Header("UI Data")]
    [SerializeField] private Image[] currentLife = new Image[2];
    [SerializeField] private Image currentHealth;
    [SerializeField] private Image[] currentCoins = new Image[2];
    [SerializeField] private Image[] currentOrbs = new Image[2];
    [SerializeField] private Image itemFont;

    [Header("Fonts")]
    [SerializeField] private List<Sprite> numberFonts = new List<Sprite>();


    private void Awake()
    {
        instance = this;
        healthFillImage = GameObject.Find("Health Bar").GetComponent<Image>();
        menuFillPanel = GameObject.Find("Black Panel").GetComponent<Image>();
        menuFillPanel.enabled = openPanel;
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
            healthFillImage.fillAmount = health / 5f; // Calculates the value directly as a ratio
        }
        else
        {
            Debug.LogWarning("Health value is out of bounds!");
        }

        // Update Coins
        UpdateDigitUI(currentCoins, coins);

        // Update Orbs
        UpdateDigitUI(currentOrbs, orbs);

        // Update Potions
        UpdateItem(potions, 5);
    }

    private void UpdateItem(int itemAmount, int maxItems)
    {
        if (itemAmount >= 0 && itemAmount <= maxItems)
        {
            itemFont.sprite = numberFonts[itemAmount];
        }
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

    public void HandleLifes()
    {
        lifes--;
        
        if (lifes <= 0)
        {
            lifes = 0;
            // Game Over
        }
        else
        {
            // Revive
        }

        UpdateUI();
    }

    public void HandleHealth(int health)
    {
        this.health = health;
        UpdateUI();
    }

    public void GetCoin()
    {
        coins = Mathf.Clamp(coins + 1, 0, 99); // Limit max value to 99
        UpdateUI();
    }

    public void GetOrb()
    {
        orbs = Mathf.Clamp(orbs + 1, 0, 99); // Limit max value to 99
        UpdateUI();
    }

    public void GetItem(int itemNumber)
    {
        switch (itemNumber)
        {
            case 0: // Health Potion
                potions = Mathf.Clamp(potions + 1, 0, 5); // Limit max value to 5
                break;
            case 1: // Antidote Potion
                antidote = Mathf.Clamp(antidote + 1, 0, 5); // Limit max value to 5
                break;
            case 2: // Meat
                meat = Mathf.Clamp(meat + 1, 0, 5); // Limit max value to 5
                break;
            case 3: // Apple
                apple = Mathf.Clamp(apple + 1, 0, 5); // Limit max value to 5
                break;
        }

        UpdateUI();
    }

    public void HandleMenuInput()
    {
        // Toggle menu when pressing "Fire3" button
        if (Input.GetButtonDown("Fire3"))
        {
            openPanel = !openPanel;
            menuFillPanel.enabled = openPanel;
        }
    }
}
