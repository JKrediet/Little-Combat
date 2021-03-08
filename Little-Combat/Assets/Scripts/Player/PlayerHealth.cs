using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 6;
    public float health;

    public Slider slider;

    private void Start()
    {
        health = maxHealth;
        if (slider != null)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }
    }
    public void GiveDamage(float _damageTaken)
    {
        FindObjectOfType<AnimationController>().IsTakingDamage();
        FindObjectOfType<PlayerMovement>().isTakingDamage = true;
        if (health != 0)
        {
            health = Mathf.Clamp(health - _damageTaken, 0, maxHealth);
            slider.value = health;
            if (health == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                FindObjectOfType<PlayerMovement>().isDead = true;
                FindObjectOfType<AnimationController>().Death();
            }
        }
    }
    public void GiveHealth(int _healthRestored)
    {
        health = Mathf.Clamp(health + _healthRestored, 0, maxHealth);
    }
    private void Update()
    {
        HealthRegen();
        if (Input.GetKeyDown(KeyCode.G))
        {
            GiveDamage(1);
        }
    }
    public void HealthRegen()
    {
        if(health < maxHealth)
        {
            health = Mathf.Clamp(health += 0.1f * Time.deltaTime, 0, maxHealth);
            slider.value = health;
        }
    }
}
