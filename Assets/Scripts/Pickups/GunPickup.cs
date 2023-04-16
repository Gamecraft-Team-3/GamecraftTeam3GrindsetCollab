using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunPickup : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private MeshFilter pickupRender;
    [SerializeField] private MeshRenderer pickupRenderColor;

    [Header("Components")]
    [SerializeField] private Collider pickupCollider;
    
    [Header("Fields")]
    [SerializeField] private List<GunInfo> gunOptions;
    [SerializeField] private float spawnTime;
    [SerializeField] private float spinSpeed, hoverSpeed, hoverMagnitude;
    [SerializeField] private Vector3 basePosition;
    private GunInfo _currentGun;

    private void Start()
    {
        StartCoroutine(RespawnPickup(0.1f));
        basePosition = transform.position;
    }

    private void Update()
    {
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
        transform.position = basePosition + new Vector3(0, Mathf.Sin(Time.time * hoverSpeed) * hoverMagnitude, 0);
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
        pickupCollider.enabled = false;
        pickupRender.gameObject.SetActive(false);
        
        int rng = Random.Range(0, gunOptions.Count);
        _currentGun = gunOptions[rng];
        
        pickupRender.mesh = _currentGun.pickupMesh;
        // pickupRenderColor.material = Instantiate(pickupRenderColor.material) as Material;
        pickupRenderColor.material.SetColor("_Color", _currentGun.color);

        pickupRender.transform.localScale = new Vector3(_currentGun.scale, _currentGun.scale, _currentGun.scale);
        
        if (_currentGun.gunName == GunInfo.GunName.Random)
        {
            _currentGun.RandomizeWeapon();
        }

        yield return new WaitForSeconds(time);
        
        pickupCollider.enabled = true;
        pickupRender.gameObject.SetActive(true);

        // Other Guns Here.
    }
}
