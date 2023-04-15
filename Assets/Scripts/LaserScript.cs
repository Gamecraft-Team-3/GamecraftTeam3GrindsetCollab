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
    private float colliderDelay = 0.1f;
    private Vector3 direction;
    private Rigidbody body;

    private void Start()
    {
        Setup(transform.forward);
    }

    public void Setup(Vector3 direction)
    {
        body = GetComponent<Rigidbody>();
        ShootInDirection(direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            ShootInDirection(Vector3.Reflect(direction, collision.contacts[0].normal));
            if (bounceCount > 0) { bounceCount--; }
            else { Destroy(gameObject); }
        }
        else if (collision.gameObject.tag == "Glass")
        {
            Debug.Log("Refract");
            Refract();
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Collision");
            Destroy(collision.gameObject);
            Refract();
        }

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Wall")
    //    {
    //        Debug.Log("bounce");
    //        var collisionPoint = other.ClosestPoint(transform.position);
    //        var collisionNormal = transform.position - collisionPoint;
    //        ShootInDirection(Vector3.Reflect(direction, collisionNormal));
    //        if (bounceCount > 0) { bounceCount--; }
    //        else { Destroy(gameObject); }
    //    }
    //    if (other.gameObject.tag == "Glass")
    //    {
    //        Debug.Log("refract");
    //        Transform refraction1 = Instantiate(clone, transform.position, transform.rotation);
    //        Transform refraction2 = Instantiate(clone, transform.position, transform.rotation);
    //        refraction1.GetComponent<LaserScript>().collider.enabled = false;
    //        refraction2.GetComponent<LaserScript>().collider.enabled = false;
    //        refraction1.GetComponent<LaserScript>().StartCoroutine(EnableColliderAfterDelay(colliderDelay));
    //        refraction2.GetComponent<LaserScript>().StartCoroutine(EnableColliderAfterDelay(colliderDelay));
    //        refraction1.Rotate(0, refractAngle, 0);
    //        refraction2.Rotate(0, -refractAngle, 0);
    //    }
    //}

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
