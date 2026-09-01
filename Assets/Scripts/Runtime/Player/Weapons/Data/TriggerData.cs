using UnityEngine;

[CreateAssetMenu(fileName = "TriggerData", menuName = "ScriptableObjects/Weapons/TriggerData", order = 1)]
public class TriggerData : ScriptableObject
{
    public bool automatic = false;
    public int burstCount = 1;
}
