using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthbarScript : MonoBehaviour
{
    [SerializeField] private Image heart1;
    [SerializeField] private Image heart2;
    [SerializeField] private Image heart3;
    [SerializeField] private Sprite heartAlive;
    [SerializeField] private Sprite heartDead;

    public void UpdateHealth(int health)
    {
        switch(health)
        {
            case 2:
                {
                    heart3.sprite = heartDead;
                }
            case 1:
                {
                    heart2.sprite = heartDead;
                }
            case 0:
                {
                    heart1.sprite = heartDead;
                }
        }
    }
}
