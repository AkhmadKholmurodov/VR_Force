using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShoot : MonoBehaviour
{
    public int maxammo = 10;
    private int currentammo;

    public TMPro.TextMeshPro text;
    public GameObject line;
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;
    public Transform barrelLocation;
    public Transform casingExitLocation;

    public AudioSource source;
    public AudioClip fire;
    public AudioClip reload;
    public AudioClip noammo; 


    public float shotPower = 100f;

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        Reload();
    }

    void Reload()
    {
        currentammo = maxammo;
        source.PlayOneShot(reload);
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger,OVRInput.Controller.RTouch))
        {
            if (currentammo > 0)
                GetComponent<Animator>().SetTrigger("Fire");
            else
                source.PlayOneShot(noammo);
        }

        if (Vector3.Angle(transform.up, Vector3.up) > 100 && currentammo < maxammo)
            Reload();

        text.text = currentammo.ToString();
    }

    public void Shoot()
    {
        //  GameObject bullet;
        //  bullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
        // bullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

        currentammo--;
        source.PlayOneShot(fire);

        GameObject tempFlash;
        if(bulletPrefab)
            Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower,ForceMode.VelocityChange);
       tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

        RaycastHit hitInfo;
        bool hasHit = Physics.Raycast(barrelLocation.position, barrelLocation.forward, out hitInfo, 100);

        if (hasHit)
            hitInfo.collider.SendMessageUpwards("Dead", hitInfo.point, SendMessageOptions.DontRequireReceiver);

        if(line)
        {
            GameObject liner = Instantiate(line);
            liner.GetComponent<LineRenderer>().SetPositions(new Vector3[] { barrelLocation.position , barrelLocation.position + barrelLocation.forward * 100 });

            Destroy(liner, 0.5f);
        }

       // Destroy(tempFlash, 0.5f);
        //  Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation).GetComponent<Rigidbody>().AddForce(casingExitLocation.right * 100f);
       
    }

    void CasingRelease()
    {
         GameObject casing;
        casing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        casing.GetComponent<Rigidbody>().AddExplosionForce(550f, (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        casing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(10f, 1000f)), ForceMode.Impulse);
    }


}
