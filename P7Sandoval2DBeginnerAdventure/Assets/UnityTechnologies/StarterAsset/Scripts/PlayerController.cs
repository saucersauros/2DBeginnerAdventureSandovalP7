using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    public float speed = 3.0f;


    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;

    public float timeInvincible = 2.0f;
    bool isInvisible;
    float damageCooldown;


    void Start()
    {
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
    }
 

    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();

        if(isInvisible)
        {
            damageCooldown -= Time.deltaTime;
            if(damageCooldown < 0 )
            {
                isInvisible = false;
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }
    public void ChangeHealth(int amount)
    {
        if (amount  < 0)
        {
            if (isInvisible)
            {
                return;
            }
            isInvisible  = true;
            damageCooldown = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UiHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }
}