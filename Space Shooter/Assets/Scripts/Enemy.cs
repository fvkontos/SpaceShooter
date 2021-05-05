using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 4.0f;

    private Player player;

    private Animator animator;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);

        if(transform.position.y < - 4f)
        {
            float randomx = Random.Range(-9.1f,9.1f); 
            transform.position = new Vector3(randomx, 7f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);

            animator.SetTrigger("OnEnemyDestruction");
            speed = 0;

            if(audioSource != null)
            {
                audioSource.Play();
            }

            Destroy(this.gameObject,2.4f);

            if(player != null)
            {
                player.AddScore();
            }
        }
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();

                animator.SetTrigger("OnEnemyDestruction");
                speed = 0;

                if (audioSource != null)
                {
                    audioSource.Play();
                }

                Destroy(this.gameObject, 2.4f);
            }
        }
    }
}
