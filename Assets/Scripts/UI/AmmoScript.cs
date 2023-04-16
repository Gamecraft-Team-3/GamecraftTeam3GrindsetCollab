using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoScript : MonoBehaviour
{
    [SerializeField] private TMP_Text ammo;

    public void SetAmmo(int ammo)
    {
        this.ammo.text = ammo.ToString();
    }
}
