using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    [SerializeField] private float travelSpeed = 100f;
    [SerializeField] private int bounceCount = 3;
    [SerializeField] private Transform clone;
    [SerializeField] private float refractAngle = 15;
    [SerializeField] private Collider collider;
    [SerializeField] private float colliderDelay = 0.05f;
    [SerializeField] private int scoreForKill = 100;
    [SerializeField] private float scoreMultiplier = 1;

    private Vector3 direction;
    private Rigidbody body;

    private void Start()
    {
        Setup(transform.forward);
        Destroy(gameObject, 5.0f);
    }

    public void Setup(Vector3 direction)
    {
        body = GetComponent<Rigidbody>();
        ShootInDirection(direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            scoreMultiplier += 1;
            ShootInDirection(Vector3.Reflect(direction, collision.contacts[0].normal));
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
            Destroy(collision.gameObject);
            
            GameObject.FindWithTag("Player").GetComponent<PlayerManager>().AddScore(5, scoreMultiplier);
                
            Refract();
        }
    }

    private void ShootInDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        body.AddForce(direction * travelSpeed, ForceMode.Impulse);
        transform.forward = direction;
    }

    private void Refract()
    {
        Transform refraction1 = Instantiate(clone, transform.position, transform.rotation);
        Transform refraction2 = Instantiate(clone, transform.position, transform.rotation);
        ShootInDirection(direction);
        collider.enabled = false;
        refraction1.GetComponent<LaserScript>().collider.enabled = false;
        refraction2.GetComponent<LaserScript>().collider.enabled = false;
        StartCoroutine(EnableColliderAfterDelay(colliderDelay));
        StartCoroutine(refraction1.GetComponent<LaserScript>().EnableColliderAfterDelay(colliderDelay));
        StartCoroutine(refraction2.GetComponent<LaserScript>().EnableColliderAfterDelay(colliderDelay));
        refraction1.Rotate(0, refractAngle, 0);
        refraction2.Rotate(0, -refractAngle, 0);
    }

    private IEnumerator EnableColliderAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("re-enable");
        collider.enabled = true;
    }
}
