using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunPickup : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject pickupMesh;
    [SerializeField] private MeshFilter pickupRender;

    [Header("Components")]
    [SerializeField] private Collider pickupCollider;
    
    [Header("Fields")]
    [SerializeField] private List<GunInfo> gunOptions;
    [SerializeField] private float spawnTime;
    private GunInfo _currentGun;

    private void Start()
    {
        StartCoroutine(RespawnPickup(0.1f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerCombat>().SetCurrentWeapon(_currentGun);

            StartCoroutine(RespawnPickup(spawnTime));
        }    
    }

    private IEnumerator RespawnPickup(float time)
    {
        pickupMesh.SetActive(false);
        pickupCollider.enabled = false;
        pickupRender.gameObject.SetActive(false);
        
        int rng = Random.Range(0, gunOptions.Count);
        _currentGun = gunOptions[rng];
        
        pickupRender.mesh = _currentGun.pickupMesh;
        
        if (_currentGun.gunName == GunInfo.GunName.Random)
        {
            _currentGun.RandomizeWeapon();
        }

        yield return new WaitForSeconds(time);
        
        pickupMesh.SetActive(true);
        pickupCollider.enabled = true;
        pickupRender.gameObject.SetActive(true);

        // Other Guns Here.
    }
}
