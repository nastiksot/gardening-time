using UnityEngine;

[CreateAssetMenu(fileName = "Plants", menuName = "Gardening-time/Configs", order = 1)]
public class PlantsConfig : ScriptableObject
{
    public PlantType type;
    public int cost;
    public int rewardCost;
    public float grownTime;
    public float floweringTime;
    public Sprite[] sprites;
    public GameObject prefab;
}