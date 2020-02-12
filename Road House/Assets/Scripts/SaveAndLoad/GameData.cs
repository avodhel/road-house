using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public float bestDistance;
    public int coin;

    public GameData(Game gameManager)
    {
        bestDistance = gameManager.bestDistance;
        coin = gameManager.collectedCoins;
    }
}
