using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 6;
    private int health;

    private void Start()
    {
        health = maxHealth;
    }
    public void GiveDamage(int _damageTaken)
    {
        health = Mathf.Clamp(health - _damageTaken, 0, maxHealth);
        if (health == 0)
        {
            //player died
        }
    }
    public void GiveHealth(int _healthRestored)
    {
        health = Mathf.Clamp(health + _healthRestored, 0, maxHealth);
    }
}
