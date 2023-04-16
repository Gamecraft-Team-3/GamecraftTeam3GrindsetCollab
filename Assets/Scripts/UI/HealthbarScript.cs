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
        /*Debug.Log("HIT");
        switch(health)
        {
            case 2:
                {
                    heart3.sprite = heartDead;
                    break;
                }
            case 1:
                {
                    heart2.sprite = heartDead;
                    break;
                }
            default:
                {
                    heart1.sprite = heartDead;
                    break;
                }
        }*/
    }
}
