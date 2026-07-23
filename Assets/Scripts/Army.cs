using UnityEngine;

public class Army
{
    public int peasantCount = 0;

    public int infantryCount = 0;

    public int rangedCount = 0;

    public int cavalryCount = 0;

    public int armySize()
    {
        return peasantCount + infantryCount + rangedCount + cavalryCount;
    }
}
