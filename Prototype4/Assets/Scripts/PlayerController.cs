using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject focalPoint;
    public GameObject powerUpIndecator;
    private Rigidbody playerRb;
    public float speed;
    public float powerUpStrength = 5;
    public bool hasPowerUp;


    // Start is called before the first frame update
    void Start()
    {

        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -5){
            transform.position = new Vector3(0,0,0);
        }
        float forwardInput = Input.GetAxis("Vertical");
        float forwardInput2 = Input.GetAxis("Horizontal");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        playerRb.AddForce(focalPoint.transform.right * speed * forwardInput2);
        powerUpIndecator.transform.position = transform.position+ new Vector3(0,-0.4f,0);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("PowerUp")){
            hasPowerUp = true;
            Destroy(other.gameObject);
            powerUpIndecator.SetActive(true);
            StartCoroutine(PowerUpCountdown());
            
        }
        if(powerUpStrength < 20){
            powerUpStrength++;
        }
    }

    IEnumerator PowerUpCountdown()
    {
        yield return new WaitForSeconds(10);
        powerUpIndecator.SetActive(false);
        hasPowerUp = false;
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Enemy")&& hasPowerUp){
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (other.gameObject.transform.position - transform.position);
            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength,ForceMode.Impulse);
        }
    }

}
