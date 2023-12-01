using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private PlayerTower _playerTower;

    private void OnValidate()
    {
        if (_playerTower == null)
        {
            _playerTower = FindObjectOfType<PlayerTower>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerTower playerTower))
        {
            int humansCount = (int)(transform.localScale.y / 1.5);

            Human[] humans = playerTower.GetHumans(humansCount);

            if (humans == null)
            {
                return;
            }

            Human[] upHumans = null;

            if (humans.Length > 0)
            {
                upHumans = humans[humans.Length - 1].GetUpHumans();
            }

            for (int i = 0; i < upHumans.Length; i++)
            {
                Vector3 newPosition = new Vector3(upHumans[i].transform.position.x, upHumans[i].transform.position.y - upHumans[i].transform.localScale.y * humansCount, upHumans[i].transform.position.z);

                upHumans[i].transform.position = newPosition;
            }

            if (humans[humans.Length - 1] != null)
            {
                Vector3 newPosition = new Vector3(humans[humans.Length - 1].transform.position.x, humans[humans.Length - 1].transform.position.y - humans[humans.Length - 1].transform.localScale.y * humansCount, humans[humans.Length - 1].transform.position.z);

                humans[humans.Length - 1].transform.position = newPosition;
            }

            for (int i = 0; i < humans.Length; i++)
            {
                _playerTower.RemoveHuman(humans[i]);
                Destroy(humans[i].gameObject);
            }
        }
    }
}
