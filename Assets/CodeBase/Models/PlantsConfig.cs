using UnityEngine;

[CreateAssetMenu(fileName = "Plants", menuName = "Gardening-time/Configs", order = 1)]
public class PlantsConfig : ScriptableObject
{
    public PlantType Type;
    public int Cost;
    public int RewardCost;
    public float GrownTime;
    public float FloweringTime;
    public Sprite[] Sprites;
}