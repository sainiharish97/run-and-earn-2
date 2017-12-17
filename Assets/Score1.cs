using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score1 : MonoBehaviour {

    public Text ScoreText;
    static int score = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        score++;
        ScoreText.text = "Score " + score;
    }
}
