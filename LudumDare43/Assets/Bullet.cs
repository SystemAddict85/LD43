using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    private float bulletSpeed = 30f;
    [SerializeField]
    private LayerMask layersToHit;

    private string[] layerNames;

    private void Awake()
    {
        //    layerNames = new string[layersToHit.Length];
        //    for(int i = 0; i < layersToHit.Length; ++i)
        //    {
        //        layerNames[i] = layerNames.ToString();
        //    }
    }

    private void Start()
    {
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        var dir = -transform.up * bulletSpeed * Time.deltaTime;
        transform.position += dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.IsTouchingLayers(layersToHit.value))//(layerNames[0].layerNames.Contains(collision.gameObject.layer.ToString())){            
            if (collision.gameObject.tag == "Player")
            {
                collision.GetComponent<Player>().OnHealthUpdate(-3);
                Destroy(this.gameObject);
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                collision.GetComponent<AnimalStats>().TakeDamage(1);
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
    }
}

