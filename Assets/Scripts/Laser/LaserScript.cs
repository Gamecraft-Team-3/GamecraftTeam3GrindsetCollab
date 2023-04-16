using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    
    [Header("Values")]
    [SerializeField] private int scoreMultiplier = 1;

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
    [SerializeField] AudioSource source;
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
            IncreaseScoreMultiplier(false);
            ShootInDirection(Vector3.Reflect(_direction, collision.contacts[0].normal));
            if (bounceCount > 0) { bounceCount--; }
            else { Destroy(gameObject); }
        }
        else if (collision.gameObject.CompareTag("Glass"))
        {
            IncreaseScoreMultiplier(false);
            Debug.Log("Refract");
            Refract();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            //source.Play();
            Debug.Log("Enemy Collision");
            collision.gameObject.GetComponent<EnemyBehavior>().DestroySelf();
            
            PlayerManager.Instance.AddScore(scoreForKill, scoreMultiplier);

            IncreaseScoreMultiplier(true);

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

    private void IncreaseScoreMultiplier(bool multiply)
    {
        if (multiply) { scoreMultiplier *= 2; }
        else { scoreMultiplier += 1; }

        // switch(scoreMultiplier)
        // {
        //     case 1:
        //         source.clip = clip1;
        //         break;
        //         case 2: source.clip = clip2; break;
        //         case 3: source.clip = clip3;
        //         break;
        //         case 4: source.clip = clip4;
        //         break;
        //         case 5: source.clip = clip5;
        //         break;
        //         case 6: source.clip = clip6;    
        //         break;
        //         case 7: source.clip = clip7;
        //         break;  
        //         case 8: source.clip = clip8;
        //         break;  
        //         case 9: source.clip = clip9;
        //         break;
        //         case 10: source.clip = clip10;
        //         break;
        //         default: source.clip = clip10; break;
        // }
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
