using System.Collections;
using UnityEngine;

public class RifleController : MonoBehaviour
{

    public bool canShoot = true;
    private Animator anim;

    private bool rifleEquipped = false;

    [SerializeField]
    private float shootingDelay = 1f;
    [SerializeField]
    private Vector3 rifleOffset = new Vector2(-0.74f, .5f);
    private bool readyToShoot = true;

    public static int TotalAmmo = 1000;

    [SerializeField]
    private LayerMask layersToHit;
    [SerializeField]
    private float rifleRange = 20f;

    public static bool IsActivePlayerCarryingGun
    {
        get
        {
            var rifleController = ActivePlayerController.ActivePlayer.GetComponentInChildren<RifleController>();
            return rifleController != null && rifleController.rifleEquipped;
        }
    }

    private void Awake()
    {
        anim = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (IsActivePlayerCarryingGun && rifleEquipped && canShoot && readyToShoot && TotalAmmo > 0 && Input.GetButtonDown("Shoot"))
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
        // actual animation
        anim.SetTrigger("shootRifle");

        // for smoke
        GetComponentInChildren<Animator>().SetTrigger("shoot");
        AudioManager.PlaySFX("rifleShot", .25f, 0f);
        var hit = Physics2D.Raycast(transform.position, -transform.up, rifleRange, layersToHit.value);
        Debug.DrawRay(transform.position, -transform.up, Color.red);
        if (hit)
        {
            Debug.Log("pew: hit " + hit.transform.name);
            if (hit.transform.tag == "Player")
            {
                hit.transform.GetComponent<Player>().OnHealthUpdate(-3);
                var blood = Instantiate(Resources.Load("Prefabs/BloodSpurt"), hit.point, transform.rotation) as GameObject;
                Destroy(blood.gameObject, 1f);
            }
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                hit.transform.GetComponent<AnimalStats>().TakeDamage(1);
                var blood = Instantiate(Resources.Load("Prefabs/BloodSpurt"), hit.point, Quaternion.identity, hit.collider.transform) as GameObject;
                Destroy(blood.gameObject, 1f);
            }
        }

        //var bullet = Instantiate(Resources.Load("Prefabs/Bullet"), transform.position, transform.rotation, transform.root) as GameObject; 

        --TotalAmmo;
    }

    IEnumerator WaitToShoot()
    {
        readyToShoot = false;
        yield return new WaitForSeconds(shootingDelay);
        readyToShoot = true;
    }


}
