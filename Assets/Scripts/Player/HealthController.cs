using System.Collections;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] healthParticle;

    public int initialHealth;
    public int health;

    private void Start()
    {
        health = PlayerPrefs.GetInt("Health");
        for (int i = 0; i < health; i++)
        {
            healthParticle[i].SetActive(true);
        }
    }

    public void HealthReduce()
    {
        health--;
        PlayerPrefs.SetInt("Health", health);
        healthParticle[health].SetActive(false);
    }

    public void HealthIcrease()
    {
        health++;
        healthParticle[health - 1].SetActive(true);
    }
}

