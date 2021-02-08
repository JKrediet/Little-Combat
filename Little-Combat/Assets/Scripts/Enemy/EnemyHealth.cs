using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 6;
    public float health;

    private void Start()
    {
        health = maxHealth;
    }
    public void GiveDamage(float _damageTaken)
    {
        health = Mathf.Clamp(health - _damageTaken, 0, maxHealth);
        if (health == 0)
        {
            //enemy died
        }
    }
}
