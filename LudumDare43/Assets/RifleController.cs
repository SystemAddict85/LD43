using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleController : MonoBehaviour {

    public bool canShoot = true;
    private Animator anim;

    private bool rifleEquipped = false;

    [SerializeField]
    private float shootingDelay = 1f;
    [SerializeField]
    private Vector3 rifleOffset = new Vector2(-0.74f, .5f);
    private bool readyToShoot = true;

    public static int TotalAmmo = 1000;


    private void Awake()
    {
        anim = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (rifleEquipped && canShoot && readyToShoot && TotalAmmo > 0 && Input.GetButtonDown("Shoot"))
        {
            Shoot();    
        }
    }

    public void ToggleRifle(bool enabled)
    {
        rifleEquipped = enabled;
        anim.SetBool("rifleEquipped", enabled);
    }

    void Shoot()
    {
        StartCoroutine(WaitToShoot());
        anim.SetTrigger("shootRifle");
        var bullet = Instantiate(Resources.Load("Prefabs/Bullet"), transform.position, transform.rotation, transform.root) as GameObject; 
        Debug.Log("pew");
        --TotalAmmo;
    }

    IEnumerator WaitToShoot()
    {
        readyToShoot = false;
        yield return new WaitForSeconds(shootingDelay);
        readyToShoot = true;
    }


}
