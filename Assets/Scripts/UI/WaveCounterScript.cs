using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour
{
    [SerializeField] private TMP_Text waveCounter;

    public void IncrementWaveCounter()
    {
        waveCounter++;
    }
}
