using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
public class PlayerConfig: ScriptableObject
{
    [SerializeField] private MovementStateConfig _movementConfig;
    
    public MovementStateConfig MovementConfig => _movementConfig;
}