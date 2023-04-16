using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        [SerializeField] private ScoreScript scoreUI;

        [Header("Objects")] 
        [SerializeField] private GameObject audioPlayer;

        [Header("Audio Clips")] 
        [SerializeField] private List<AudioClip> scoreAudioClips;

        private void Awake()
        {
            Instance = this;
        }

        public void AddScore(int scoreToAdd, int multiplier)
        {
            score += Mathf.RoundToInt((float)scoreToAdd * multiplier);

            // Hook the multiplier to the Score ui element for effects.
            scoreUI.SetScore(this.GetScore());

            GameObject audioInstance = Instantiate(audioPlayer);
            AudioSource source = audioInstance.GetComponent<AudioSource>();
            
            source.clip = scoreAudioClips[multiplier];
            source.enabled = true;
            source.Play();
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
            {
                SceneManager.LoadScene("DeathScreen");
            }

            healthbar.UpdateHealth(health);
        }

        public int GetHealthPercentage()
        {
            return Mathf.RoundToInt((float)health / (float)maxHealth);
        }

        public int GetHealth()
        {
            return health;
        }
    }
}
