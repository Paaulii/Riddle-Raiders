using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Small Player" || collision.name == "Big Player")
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("attack");
            HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
            healthManager.health -= 1;

            if (healthManager.health <= 0)
            {
              
            }
        }
    }
}
