using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCounterScript : MonoBehaviour
{
    [SerializeField] private TMP_Text waveCounter;

    public void IncrementWaveCounter()
    {
        waveCounter++;
    }
}
