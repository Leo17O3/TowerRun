using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Human : MonoBehaviour
{
    private float _fixationPoint;

    public float FixationPoint => _fixationPoint;

    private void Start()
    {
        _fixationPoint = GetComponent<BoxCollider>().size.y;
    }

    public Human[] GetDownHumans()
    {
        Human[] humans = FindObjectsOfType<Human>();
        List<Human> _downHumans = new List<Human>();

        for (int i = 0; i < humans.Length; i++)
        {
            if (humans[i].transform.position.x == transform.position.x && humans[i].transform.position.z == transform.position.z && humans[i].transform.position.y < transform.position.y)
            {
                _downHumans.Add(humans[i]);
            }
        }

        return _downHumans.ToArray();
    }
}
