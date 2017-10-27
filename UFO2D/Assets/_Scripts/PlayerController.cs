using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rbody;
    public int speed;
    private int scorePoint;
    private float timeleft;
    public Text countText, winText, bonusText;
    private Boolean gameover;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        scorePoint = 0;
        RefreshScorePoint();
        winText.text = "";
        bonusText.text = "";
        gameover = false;
        timeleft = 3;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameover)
        {
            bonusText.text = ((int)timeleft).ToString();
            timeleft -= Time.deltaTime;
            if (timeleft < 0)
            {
                Application.Quit();
            }
        }
	}

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rbody.AddForce(movement * speed, ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherGameobject = other.gameObject;
        if (otherGameobject.CompareTag("PickUp"))
        {
            otherGameobject.SetActive(false);
            scorePoint++;
            RefreshScorePoint();
            checkWinning();
        }
    }

    private void RefreshScorePoint()
    {
        countText.text = "Score : " + scorePoint.ToString();
    }

    private void checkWinning()
    {
        if (scorePoint == 9)
        {
            winText.text = "Win \n;)";
            gameover = true;
            rbody.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }
}
