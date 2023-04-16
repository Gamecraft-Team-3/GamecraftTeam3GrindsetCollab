using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    
    [Header("Values")]
    [SerializeField] private float scoreMultiplier = 1;

    [Header("Fields")] 
    [SerializeField] private int refractionCount;
    [SerializeField] private float travelSpeed = 100f;
    [SerializeField] private float refractAngle = 15;
    [SerializeField] private float colliderDelay = 0.05f;
    [SerializeField] private int bounceCount = 3;
    [SerializeField] private int scoreForKill = 100;

    [Header("Objects")]
    [SerializeField] private Collider collider;
    [SerializeField] private Transform clone;
    private Vector3 _direction;
    private Rigidbody _body;

    private void Start()
    {
        Setup(transform.forward);
        Destroy(gameObject, 5.0f);
    }

    public void Setup(Vector3 direction)
    {
        _body = GetComponent<Rigidbody>();
        ShootInDirection(direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            scoreMultiplier += 1;
            ShootInDirection(Vector3.Reflect(_direction, collision.contacts[0].normal));
            if (bounceCount > 0) { bounceCount--; }
            else { Destroy(gameObject); }
        }
        else if (collision.gameObject.CompareTag("Glass"))
        {
            scoreMultiplier += 1;
            Debug.Log("Refract");
            Refract();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            scoreMultiplier *= 2;
            Debug.Log("Enemy Collision");
            collision.gameObject.GetComponent<EnemyBehavior>().DestroySelf();
            
            PlayerManager.Instance.AddScore(scoreForKill, scoreMultiplier);
                
            Refract();
        }
    }

    private void ShootInDirection(Vector3 newDirection)
    {
        _direction = newDirection.normalized;
        _body.velocity = Vector3.zero;
        _body.angularVelocity = Vector3.zero;
        _body.AddForce(_direction * travelSpeed, ForceMode.Impulse);
        transform.forward = _direction;
    }

    private void Refract()
    {
        // Transform refraction1 = Instantiate(clone, transform.position, transform.rotation);
        // Transform refraction2 = Instantiate(clone, transform.position, transform.rotation);
        // ShootInDirection(_direction);
        // collider.enabled = false;
        // refraction1.GetComponent<LaserScript>().collider.enabled = false;
        // refraction2.GetComponent<LaserScript>().collider.enabled = false;
        // StartCoroutine(EnableColliderAfterDelay(colliderDelay));
        // StartCoroutine(refraction1.GetComponent<LaserScript>().EnableColliderAfterDelay(colliderDelay));
        // StartCoroutine(refraction2.GetComponent<LaserScript>().EnableColliderAfterDelay(colliderDelay));
        // refraction1.Rotate(0, refractAngle, 0);
        // refraction2.Rotate(0, -refractAngle, 0);
        
        collider.enabled = false;
        ShootInDirection(_direction);
        StartCoroutine(EnableColliderAfterDelay(colliderDelay));

        for (int i = 1; i < refractionCount+1; i++)
        {
            float angle = (refractAngle / 2) - (refractAngle / i);

            if (angle == 0)
                continue;
            
            Transform refraction = Instantiate(clone, transform.position, transform.rotation);
            LaserScript refractionLaser = refraction.GetComponent<LaserScript>();

            refraction.Rotate(new Vector3(0, angle, 0));
            refractionLaser.collider.enabled = false;
            StartCoroutine(refractionLaser.EnableColliderAfterDelay(colliderDelay));
        }
    }

    private IEnumerator EnableColliderAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("re-enable");
        collider.enabled = true;
    }
}
