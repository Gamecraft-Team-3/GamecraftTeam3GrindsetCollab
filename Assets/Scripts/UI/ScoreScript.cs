using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] private TMP_Text score;

    public void SetScore(int score)
    {
        this.score.text = score.ToString();
    }
}
