using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

public class PlayerShoot : NetworkBehaviour {


    public PlayerWeapon weapon;


    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;
    [SerializeField]
     private Transform gun;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Vector3 bulletOffset = new Vector3(0 , 0, 0.2439f);

    // Use this for initialization
    void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
            weapon.SetWeapon();
            Shoot();
        }

	}
    private void Shoot()
    {
        GameObject _bullet = Instantiate(weapon.bullet, gun.transform.position, gun.transform.rotation);
        Rigidbody bulletRb = _bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(_bullet.transform.forward * weapon.force * Time.fixedDeltaTime, ForceMode.Force);
        StartCoroutine("DestroyBullet", _bullet);
    }
    IEnumerator DestroyBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(weapon.timeToDestroy);
        Destroy(bullet);
    }
    
}
