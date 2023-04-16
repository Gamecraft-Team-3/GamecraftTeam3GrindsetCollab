using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveCounterScript : MonoBehaviour
{
    [SerializeField] private TMP_Text waveText;

    public void SetWaveCounter(int wave)
    {
        waveText.text = wave.ToString();
    }
}
