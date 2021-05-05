using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0f;

    [SerializeField]
    private int powerupID; // 1 = Triple Shot 2 = Speed

    [SerializeField]
    private AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y < -4)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(audioClip,transform.position);

            Destroy(this.gameObject);

            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                if(powerupID == 1)
                {
                    player.ActivateTripleShot();
                }
                else if(powerupID == 2)
                {
                    player.ActivateSpeed();
                }
                else if(powerupID == 3)
                {
                    player.ActivateShield();
                }
            }
        }
    }
}
