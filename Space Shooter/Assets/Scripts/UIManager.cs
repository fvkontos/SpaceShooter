using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text gameOverText;
    [SerializeField]
    private Text restartText;

    [SerializeField]
    private Sprite[] liveSprites;

    [SerializeField]
    private Image livesImage;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score : 0";
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score : " + score;
    }

    public void UpdateLives(int lives)
    {
        livesImage.sprite = liveSprites[lives];
        
        if(lives <=0)
        {
            if(gameManager !=null)
            {
                gameManager.GameOver();
            }
            gameOverText.gameObject.SetActive(true);
            restartText.gameObject.SetActive(true);

            StartCoroutine(GameOverEffect());
        }
    }

    IEnumerator GameOverEffect()
    {
        while (true)
        {
            gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
