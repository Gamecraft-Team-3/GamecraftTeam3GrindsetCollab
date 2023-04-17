using System;
using System.Collections.Generic;
using TMPro;
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
        [SerializeField] private TextMeshProUGUI scoreMultiplierText;

        [Header("Values")] 
        [SerializeField] private float scoreAlphaSpeed;

        [Header("Objects")] 
        [SerializeField] private GameObject audioPlayer;

        [Header("Audio Clips")] 
        [SerializeField] private List<AudioClip> scoreAudioClips;
        [SerializeField] private AudioClip hitAudio;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            scoreMultiplierText.alpha = Mathf.Lerp(scoreMultiplierText.alpha, 0, scoreAlphaSpeed * Time.deltaTime);
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

            scoreMultiplierText.text = $"+{scoreToAdd} x{multiplier}";
            scoreMultiplierText.alpha = 1.0f;
            
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
                PlayerPrefs.SetInt("Score", score);
                SceneManager.LoadScene("DeathScreen");
            }

            healthbar.UpdateHealth(health);
            
            GameObject audioInstance = Instantiate(audioPlayer);
            AudioSource source = audioInstance.GetComponent<AudioSource>();

            source.clip = hitAudio;
            source.volume = 0.1f;
            source.enabled = true;
            source.Play();
            
            Destroy(audioInstance, 5f);
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
