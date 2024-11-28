using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField, Range(0, 10)]
    private int lifes;
    [SerializeField, Range(0, 5)]
    private int health;
    [SerializeField]
    private int coins;
    [SerializeField]
    private List<Sprite> fonts = new List<Sprite>();

    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject uIPlayerData;
    private bool menuCondition = false;

    [SerializeField]
    private Image currentHealth;

    public int Lifes
    {
        get { return lifes; }
        set { lifes = value; }
    }

    public int Coins
    {
        get { return coins; }
        set { coins = value; }
    }

    private void Awake()
    {
        instance = this;
        menu.SetActive(menuCondition);
    }

    // Update is called once per frame
    void Update()
    {
        MenuMode();
        healthUIController();
    }

    public void healthUIController()
    {
        switch (health)
        {
            case 0:
                currentHealth.sprite = fonts[0];
                menu.GetComponentInChildren<Image>().fillAmount = 0;
                break;
            case 1:
                currentHealth.sprite = fonts[1];
                menu.GetComponentInChildren<Image>().fillAmount = .2f;
                break;
            case 2:
                currentHealth.sprite = fonts[2];
                menu.GetComponentInChildren<Image>().fillAmount = .4f;
                break;
            case 3:
                currentHealth.sprite = fonts[3];
                menu.GetComponentInChildren<Image>().fillAmount = .6f;
                break;
            case 4:
                currentHealth.sprite = fonts[4];
                menu.GetComponentInChildren<Image>().fillAmount = .8f;
                break;
            case 5:
                currentHealth.sprite = fonts[5];
                menu.GetComponentInChildren<Image>().fillAmount = 1;
                break;
        }
    }

    public void GetCoin()
    {
        coins++;
    }

    public void MenuMode()
    {
        // Open Menu
        if (Input.GetButtonDown("Fire3"))
        {
            menu.SetActive(!menuCondition);
            uIPlayerData.SetActive(menuCondition);
            menuCondition = !menuCondition;
        }
    }
}
