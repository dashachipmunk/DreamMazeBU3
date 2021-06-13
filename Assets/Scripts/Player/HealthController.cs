using System.Collections;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] healthParticle;

    public int health;

    private void Awake()
    {
        health = PlayerPrefs.GetInt("Health");
    }

    private void Start()
    {
        for (int i = 0; i < health; i++)
        {
            healthParticle[i].SetActive(true);
        }
    }

    private void Update()
    {
        PlayerPrefs.SetInt("Health", health);
    }

    public void HealthReduce()
    {
        health--;
        healthParticle[health].SetActive(false);
    }

    public void HealthIcrease()
    {
        health++;
        healthParticle[health - 1].SetActive(true);
    }
}

