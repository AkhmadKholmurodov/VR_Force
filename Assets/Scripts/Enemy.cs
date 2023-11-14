using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SimpleShoot shooter;

    // Start is called before the first frame update
    void Start()
    {
        SetupRagdoll(true);
    }

    Vector3 GetTarget()
    {
        return ((Camera.main.transform.position - shooter.barrelLocation.position) / 3) + new Vector3(0, 0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = Vector3.ProjectOnPlane((Camera.main.transform.position - transform.position), Vector3.up).normalized;
    }

    void SetupRagdoll(bool value)
    {
        foreach (var item in GetComponentsInChildren<Rigidbody>())
        {
            item.isKinematic = value;
        }
    }

    void Dead(Vector3 hitpoint)
    {
        GetComponent<Animator>().enabled = false;
        SetupRagdoll(false);

        foreach (var item in Physics.OverlapSphere(hitpoint,0.5f))
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb)
                rb.AddExplosionForce(1000, hitpoint, 0.5f);
        }

        this.enabled = false;
    }

    void Shoot()
    {
        shooter.barrelLocation.forward = GetTarget().normalized;
        shooter.shotPower = GetTarget().magnitude;
        shooter.Shoot();
    }
}
