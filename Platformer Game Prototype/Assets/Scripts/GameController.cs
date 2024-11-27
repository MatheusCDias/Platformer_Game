using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private int life;
    private int coins;

    public int Life
    {
        get { return life; }
        set { life = value; }
    }

    public int Coins
    {
        get { return coins; }
        set { coins = value; }
    }

    private void Awake()
    {
        instance = this;
    }

    public void GetCoin()
    {
        coins++;
    }
}
