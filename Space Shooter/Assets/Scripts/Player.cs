using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    public float speed = 2.5f;

    private float speedMultiplier = 2.0f;

    [SerializeField]
    private GameObject laserPrefab = null;
    [SerializeField]
    private GameObject tripleShotPrefab = null;
    [SerializeField]
    private GameObject shieldObject;
    [SerializeField]
    private GameObject rightEngine;
    [SerializeField]
    private GameObject leftEngine;

    private int lives = 3;
    private int score = 0;

    private float fireRate = 0.5f;
    private float lastFired = -0.5f;

    private SpawnManager spawnManager;
    private UIManager uiManager;

    private bool tripleShotActive = false;
    private bool speedActive = false;
    private bool shieldActive = false;

    private AudioSource audioSource;



    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if(spawnManager == null)
        {
            Debug.LogError("Η μεταβλητή spawnManager είναι null");
        }

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerFire();

    }

   void PlayerFire()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Fire")) && Time.time > lastFired + fireRate)
        {
            if(tripleShotActive == true)
            {
                Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            }

            //Instantiate(laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            if(audioSource != null)
            {
                audioSource.Play();
            }
       
            lastFired = Time.time;
        }
    }

   void PlayerMovement()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        float verticalInput = CrossPlatformInputManager.GetAxis("Vertical");

        float newSpeed = speed;
        if(speedActive == true)
        {
            newSpeed = speed * speedMultiplier;
        }

        transform.Translate(new Vector3(1, 0, 0) * horizontalInput * newSpeed * Time.deltaTime);
        transform.Translate(new Vector3(0, 1, 0) * verticalInput * newSpeed * Time.deltaTime);

        if (transform.position.x >= 9.1f)
        {
            transform.position = new Vector3(9.1f, transform.position.y, 0);
        }
        else if (transform.position.x <= -9.1f)
        {
            transform.position = new Vector3(-9.1f, transform.position.y, 0);
        }
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }
    }

   public void Damage()
    {
        if(shieldActive == true)
        {
            shieldActive = false;
            shieldObject.SetActive(false);
            return;
        }

        lives--;

        if(lives==2)
        {
            rightEngine.SetActive(true);
        }
        else if(lives==1)
        {
            leftEngine.SetActive(true);
        }

        uiManager.UpdateLives(lives);

        if(lives <= 0)
        {
            Destroy(this.gameObject);

            spawnManager.PlayerDied();
        }
    }

   public void ActivateTripleShot()
   {
        tripleShotActive = true;

        StartCoroutine(DeactivateTripleShot());
   }

   IEnumerator DeactivateTripleShot()
   {
        yield return new WaitForSeconds(5.0f);
        tripleShotActive = false;
   }
    public void ActivateSpeed()
    {
        speedActive = true;

        StartCoroutine(DeactivateSpeed());
    }

    IEnumerator DeactivateSpeed()
    {
        yield return new WaitForSeconds(5.0f);
        speedActive = false;
   
    }

    public void ActivateShield()
    {
        shieldActive = true;
        shieldObject.SetActive(true);

        StartCoroutine(DeactivateShield());
    }

    IEnumerator DeactivateShield()
    {
        yield return new WaitForSeconds(5.0f);
        shieldActive = false;
        shieldObject.SetActive(false);
    }

    public void AddScore()
    {
        score++;

        if(uiManager != null)
        {
            uiManager.UpdateScore(score);
        }
    }
}
