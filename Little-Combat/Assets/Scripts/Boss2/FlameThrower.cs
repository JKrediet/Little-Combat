using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    public bool isTurnedOn;
    public Transform boss;
    private Quaternion baseRotation;
    public GameObject muzzleFlash, cannonlocation;
    public Rigidbody stroomBall;
    public float stroomBallSpeed;

    public AudioSource source;

    private void Start()
    {
        baseRotation = transform.rotation;
    }
    private void Update()
    {
        if(isTurnedOn)
        {
            transform.LookAt(boss);
        }
        else
        {
            transform.rotation = baseRotation;
        }
    }
    public void BothOn()
    {
        GameObject muzzle = Instantiate(muzzleFlash, muzzleFlash.transform.position, Quaternion.identity);
        Destroy(muzzle, 1);
        Invoke("ShootCannon", 1);
    }
    private void ShootCannon()
    {
        source.Play();

        Rigidbody ball = Instantiate(stroomBall, transform.position, transform.rotation);
        ball.velocity = ball.transform.forward * stroomBallSpeed;
    }
}
