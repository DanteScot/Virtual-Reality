using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    public int health;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image fillImage;
    [SerializeField] private Text gameOver;
    [SerializeField] private Image damageImage;
    private Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    private float flashSpeed = 5f;
    private float barValueDamage;
    private Image healthBarBackground;
    private bool damaged;

    void Start()
    {
        barValueDamage = healthBar.maxValue / health;
        healthBarBackground = healthBar.GetComponentInChildren<Image>();
    }

    void Update()
    {
        if(health <= 0)
        {
            Death();
        }
        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    
    }

    public void Hurt(int damage)
    {
        damaged = true;
        health -= damage;
        healthBar.value -= barValueDamage;
    }

    public void Death()
    {
        fillImage.enabled = false;
        gameOver.enabled = true;
        Time.timeScale = 0;
        healthBarBackground.color = Color.red;
    }
}
