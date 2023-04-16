using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class LaserTrigger : MonoBehaviour
{
    [SerializeField] private int damage;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerManager>().Damage(damage);
            Destroy(transform.parent.gameObject);
        }
    }
}
