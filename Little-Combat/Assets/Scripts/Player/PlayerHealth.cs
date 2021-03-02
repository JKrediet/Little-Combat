using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 6;
    public float health;

    private void Start()
    {
        health = maxHealth;
    }
    public void GiveDamage(float _damageTaken)
    {
        if (!GetComponent<PlayerMovement>().shieldMoving)
        {
            _damageTaken = 0;
        }
        if (health != 0)
        {
            health = Mathf.Clamp(health - _damageTaken, 0, maxHealth);
            if (health == 0)
            {
                FindObjectOfType<PlayerMovement>().isDead = true;
                FindObjectOfType<AnimationController>().Death();
            }
        }
    }
    public void GiveHealth(int _healthRestored)
    {
        health = Mathf.Clamp(health + _healthRestored, 0, maxHealth);
    }
}
