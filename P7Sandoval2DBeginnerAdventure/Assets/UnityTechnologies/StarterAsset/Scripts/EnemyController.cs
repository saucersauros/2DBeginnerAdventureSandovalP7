using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyController : MonoBehaviour
{
    public AudioClip oof;
    public ParticleSystem smokeEffect;
    public AudioClip Dance;
    AudioSource audioSource;
    // Public variables
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    bool broken = true;

    // Private variables
    Rigidbody2D rigidbody2d;
    Animator animator;
    float timer;
    int direction = 1;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = changeTime;

    }

    void Update()
    {
        timer -= Time.deltaTime;


        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }


    // FixedUpdate has the same call rate as the physics system
    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }
        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y = position.y + speed * direction * Time.deltaTime;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        }
        else
        {
            position.x = position.x + speed * direction * Time.deltaTime;
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }


        rigidbody2d.MovePosition(position);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();


        if (player != null)
        {
            player.ChangeHealth(-1);
            player.PlaySound(oof);
        }
    }
    
    public async Task Fix()
    {
        EnemyController enemy = gameObject.GetComponent<EnemyController>();
        AudioSource music = gameObject.GetComponent<AudioSource>();
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
        enemy.PlaySound(Dance);
        await Task.Delay(500);
        music.Stop();
        smokeEffect.Stop();
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }


}