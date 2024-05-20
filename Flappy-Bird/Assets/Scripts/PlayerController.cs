using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class NewBehaviourScript : MonoBehaviour
{
    int score = 0;
    [SerializeField] private GameObject scoreText;
    [SerializeField] float velocity = 10f;
    Rigidbody2D rb;
    Touch touch;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
            rb.velocity = Vector2.up * velocity;
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        score++;
        scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
}
