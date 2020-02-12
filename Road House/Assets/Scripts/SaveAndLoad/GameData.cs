using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public float bestDistance;

    public GameData(Game gameData)
    {
        bestDistance = gameData.bestDistance;
    }
}
