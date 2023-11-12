using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTower : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private List<Human> _humans;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Human human) && IsFirstHuman(human) && human.transform.position.y <= _humans[0].transform.position.y)
        {
            Human[] downHumans = human.GetDownHumans();

            for (int i = 0; i < downHumans.Length; i++)
            {
                _humans.Add(downHumans[i]);
                _tower.RemoveHuman(downHumans[i]);

                BoxCollider boxCollider = downHumans[i].GetComponent<BoxCollider>();
                boxCollider.isTrigger = false;
                Vector3 newPosition = transform.position;

                for (int j = 0; j < _humans.Count - downHumans.Length; j++)
                {
                    Vector3 n = new Vector3(_humans[j].transform.position.x, _humans[j].transform.position.y + boxCollider.size.y, _humans[j].transform.position.z);
                    _humans[j].transform.position = n;
                }

                downHumans[i].transform.position = newPosition;

                downHumans[i].transform.SetParent(transform);
            }
        }
    }

    private bool IsFirstHuman(Human human)
    {
        float[] humansPositionsY = new float[_humans.Count];

        for (int i = 0; i < _humans.Count; i++)
        {
            humansPositionsY[i] = _humans[i].transform.position.y;
        }

        if (human.transform.position.y <= humansPositionsY.Min())
        {
            return true;
        }

        return false;
    }
}
