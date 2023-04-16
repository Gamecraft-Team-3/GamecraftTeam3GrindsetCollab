using System;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;

        //[Header("Objects")] 
        //[Header("Components")] 
        //[Header("Fields")] 
            
        [Header("Values")] 
        [SerializeField] private int score;
        [SerializeField] private int health, maxHealth;
        [SerializeField] private HealthbarScript healthbar;
        
        private void Awake()
        {
            Instance = this;
        }

        public void AddScore(int scoreToAdd, float multiplier)
        {
            score += Mathf.RoundToInt((float)scoreToAdd * multiplier);
            
            // Hook the multiplier to the Score ui element for effects.
        }

        public int GetScore()
        {
            return score;
        }

        public void Damage(int damage)
        {
            health -= damage;

            if (health < 0)
                health = 0;

            if (health == 0)
                Debug.Log("Player Died");

            healthbar.UpdateHealth(health);
        }

        public int GetHealthPercentage()
        {
            return Mathf.RoundToInt((float)health / (float)maxHealth);
        }
    }
}
