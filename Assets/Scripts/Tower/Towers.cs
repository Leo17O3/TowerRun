using UnityEngine;

public class Towers : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private int _towersCount;
    private void Awake()
    {
        for (int i = 0; i < _towersCount; i++)
        {
            Instantiate(_tower, transform);
        }
    }
}
