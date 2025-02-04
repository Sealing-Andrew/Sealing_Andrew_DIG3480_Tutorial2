﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;
    public TextMeshProUGUI lifeText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
     public AudioClip coin;
     public AudioClip musicClipTwo;
     public GameObject Sprite;
     

public AudioSource musicSource;
private bool winState;

    private int scoreValue = 0;
    private int lifeValue;
    

    // Start is called before the first frame update
    void Start()
    {
        
    
        rd2d = GetComponent<Rigidbody2D>();
     lifeValue = 3;
   SetCountText();
        score.text = scoreValue.ToString();
           winState = false;
        winTextObject.SetActive(false);
     loseTextObject.SetActive(false);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.tag == "Enemy")
        {
           lifeValue -= 1;
           
            Destroy(collision.collider.gameObject);
        }
        
        if (scoreValue >= 4)
	 {
		winTextObject.SetActive(true);
	 }
     if(lifeValue <= 0)
		{
		loseTextObject.SetActive(true);
		}
 SetCountText();
   
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
    }
    void SetCountText()
	{
		score.text = "Count: " + scoreValue.ToString();
       
		if (scoreValue >= 4) 
		{
                    // Set the text value of your 'winText'
                    winTextObject.SetActive(true);
        }
        {
            			lifeText.text = "Lives: " + lifeValue.ToString();
                        if (lifeValue ==0)
                        {
                           loseTextObject.SetActive(true); 
                        }


        }
       
    }
     void PlayWin()
    {
        if(winState == false)
        {
            winState = true;
            musicSource.clip = coin;
            musicSource.Play();
        }
    }
     void Update()
    {
        if (scoreValue >= 4)
        {
            PlayWin();
        }
         if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (scoreValue == 4)
        {
                 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);      
        }
        
    }

}