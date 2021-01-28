using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float speed, gravity, jumpForce;

    //privates/protected
    protected Transform Player;
    private CharacterController enemyController;
    private Vector3 movement;
    private float downForce;

    //attacks
    public float attackDisntance, damage;
    private bool isAttacking;

    private void Start()
    {
        enemyController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (Player)
        {
            if(!isAttacking)
            {
                GoToTarget();
            }
        }
        Gravity();
    }
    private void OnTriggerEnter(Collider _enter)
    {
        if (_enter.tag == "Player")
        {
            Player = _enter.transform;
        }
    }
    private void OnTriggerExit(Collider _exit)
    {
        if (_exit.tag == "Player")
        {
            Player = null;
        }
    }
    protected virtual void GoToTarget()
    {
        //give direction
        Vector3 lookAt = new Vector3(Player.position.x, 1.1f, Player.position.z);
        transform.LookAt(lookAt);
        //give movement
        movement = transform.forward.normalized;
        movement *= speed;
        //move
        enemyController.Move(movement * Time.deltaTime);

        //check if target is in range (attackDistance)
        float dist = Vector3.Distance(transform.position, Player.position);
        if (dist < 3)
        {
            StartCoroutine(Attack());
        }
    }
    public void Gravity()
    {
        if (enemyController.isGrounded)
        {
  
        }
        else
        {
            downForce -= gravity * Time.deltaTime;
            enemyController.Move(new Vector3(0, downForce, 0) * Time.deltaTime);
        }
    }
    IEnumerator Attack()
    {
        isAttacking = true;
        print("rawr");
        yield return new WaitForSeconds(1);
        isAttacking = false;
    }
}
