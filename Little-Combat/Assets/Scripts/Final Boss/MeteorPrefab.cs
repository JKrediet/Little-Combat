using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorPrefab : MonoBehaviour
{
    public Rigidbody meteor;

    private bool dissolveOn, dissolveOff;
    private Material mat;
    public float speed = 1;
    private float amount;

    void Start()
    {
        //mat = GetComponent<Renderer>().material;
        //amount = -1;
        //mat.SetFloat("_CutoffHeight", amount);
        //DissolveOn();

        Rigidbody met = Instantiate(meteor, transform.position + transform.up * 50, transform.rotation);
        met.GetComponent<Meteor>().originObject = transform;
        met.velocity = -transform.up * 10;
    }
    //private void Update()
    //{
    //    if (dissolveOn)
    //    {
    //        if (amount < 1)
    //        {
    //            amount += speed * Time.deltaTime;
    //            mat.SetFloat("_CutoffHeight", amount);
    //        }
    //        else
    //        {
    //            dissolveOn = false;
    //        }
    //    }
    //    if (dissolveOff)
    //    {
    //        if (amount > -1)
    //        {
    //            amount -= speed * Time.deltaTime;
    //            mat.SetFloat("_CutoffHeight", amount);
    //        }
    //        else
    //        {
    //            dissolveOff = false;
    //            Destroy(gameObject);
    //        }
    //    }
    //}
    //public void DissolveOn()
    //{
    //    dissolveOff = false;
    //    dissolveOn = true;
    //}
    //public void DissolveOff()
    //{
    //    dissolveOn = false;
    //    dissolveOff = true;
    //}
}
