using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerTower playerTower))
        {
            int humansCount = (int)(transform.localScale.y / 1.5);

            Human[] humans = playerTower.GetHumans(humansCount);

            Human[] upHumans = humans[humans.Length - 1].GetUpHumans();

            for (int i = 0; i < upHumans.Length; i++)
            {
                Vector3 newPosition = new Vector3(upHumans[i].transform.position.x, upHumans[i].transform.position.y - upHumans[i].transform.localScale.y * humansCount, upHumans[i].transform.position.z);

                upHumans[i].transform.position = newPosition;
            }

            for (int i = 0; i < humans.Length; i++)
            {
                Destroy(humans[i].gameObject);
            }
        }
    }
}
