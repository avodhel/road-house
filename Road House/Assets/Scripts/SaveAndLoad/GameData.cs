using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public float bestDistance;
    public int coin;
    public CarModel selectedCar;
    public List<bool> lockList = new List<bool>();

    public GameData(Game gameManager)
    {
        bestDistance = gameManager.bestDistance;
        coin = gameManager.collectedCoins;
        selectedCar = gameManager.selectedCar;
        lockList = gameManager.lockList;
    }
}
