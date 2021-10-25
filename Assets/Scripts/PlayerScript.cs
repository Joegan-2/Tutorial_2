using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    public Text lives;
    
    public Text YouWin;

    private int gameOver = 0;

    private int scoreValue = 0;

    private int livesValue = 3;

    public float jumpforce;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        YouWin.text = "";
        lives.text = livesValue.ToString();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

        if (gameOver == 1)
        {
            livesValue = 3;
            lives.text = livesValue.ToString();
        }
    }

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement* speed));
    }

private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
               if (scoreValue == 4)
            {
                transform.position = new Vector2(114.8f, 0.08f);
                livesValue = 3;
                lives.text = livesValue.ToString();
            }
             if (scoreValue == 8)
            {
                YouWin.text = "You Won! Game by Joseph Donnelly.";
                gameOver = 1;
            }
                if (gameOver == 1)
            {
                musicSource.Stop();
                musicSource.clip = musicClipTwo;
                musicSource.Play();
                musicSource.loop = true;
            }
        }

               if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
                if (livesValue == 0)
            {        
                    YouWin.text = "You Lost! Game by Joseph Donnelly.";
                    Destroy(this);
        }
             
        }
    }

private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
    }
}