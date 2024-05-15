using UnityEngine;

public interface IEnemyMovable
{
    Rigidbody RB { get; set; }

    void MoveEnemy(Vector2 velocity);
}
