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

    public GameObject menu;
    private bool menuCondition = false;

    public List<Sprite> fonts = new List<Sprite>();

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
            menuCondition = !menuCondition;
        }
    }
}
