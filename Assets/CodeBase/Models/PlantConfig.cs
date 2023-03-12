using UnityEngine;

[CreateAssetMenu(fileName = "Plants", menuName = "Gardening-time/Configs/Plant", order = 1)]
public class PlantConfig : ScriptableObject
{
    //TODO: add plant model
    public PlantType type;
    public int cost;
    public int rewardCost;
    public float grownTime;
    public float floweringTime;
    public Sprite[] sprites;
    public GameObject prefab;
}