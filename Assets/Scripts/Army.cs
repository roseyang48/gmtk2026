using UnityEngine;

[System.Serializable]
public class Army
{
    [SerializeField]
    public int peasantCount = 0;

    [SerializeField]
    public int infantryCount = 0;

    [SerializeField]
    public int rangedCount = 0;

    [SerializeField]
    public int cavalryCount = 0;

    public int armySize()
    {
        return peasantCount + infantryCount + rangedCount + cavalryCount;
    }
}
