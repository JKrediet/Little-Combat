using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 6;
    public float health;

    public GameObject deathScreen;

    public RectTransform fillBar;

    public Slider slider;

    bool isDead;

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
        if (!isDead)
        {
            FindObjectOfType<AnimationController>().IsTakingDamage();
            FindObjectOfType<PlayerMovement>().isTakingDamage = true;
            if (health != 0)
            {
                health = Mathf.Clamp(health - _damageTaken, 0, maxHealth);
                slider.value = health;

                if (health <= 0)
                {
                    deathScreen.SetActive(true);
                    FindObjectOfType<PlayerMovement>().isDead = true;
                    FindObjectOfType<AnimationController>().Death();
                    isDead = true;

                    for (int i = 0; i < GetComponent<PauseMenu>().disableOnPause.Length; i++)
                    {
                        GetComponent<PauseMenu>().disableOnPause[i].enabled = false;
                    }

                    GetComponent<PauseMenu>().enabled = false;
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                }
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

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
