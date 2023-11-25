using UnityEngine;

public class JumpBooster : MonoBehaviour
{
    [SerializeField] private float _additiveForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerTowerJumper towerJumper))
        {
            towerJumper.IncreaseJumpForce(_additiveForce);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerTowerJumper towerJumper))
        {
            towerJumper.ReduceJumpForce(_additiveForce);
        }
    }
}
