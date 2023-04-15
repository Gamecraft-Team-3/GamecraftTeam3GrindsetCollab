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
    }
}
