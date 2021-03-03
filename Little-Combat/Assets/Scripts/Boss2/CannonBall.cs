using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public GameObject Inpactparticle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boss")
        {
            FindObjectOfType<Boss2>().TakeDamageToShield();
            GameObject hit = Instantiate(Inpactparticle, transform.position, Quaternion.identity);
            Destroy(hit, 0.2f);
            Destroy(gameObject, 1);
        }
    }
}
