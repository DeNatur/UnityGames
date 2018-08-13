using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour {

    private const string PLAYER_TAG = "Player";



    
    [SerializeField]
    private GameObject spawnBullet;


    [SerializeField]
    private LayerMask mask;

    private PlayerWeapon currentWeapon;
    
    private WeaponManager weaponManager;

    // Use this for initialization
    void Start () {

        //weapon.SetWeapon();
        weaponManager = GetComponent<WeaponManager>();
	}
	
	// Update is called once per frame
	void Update () {

        currentWeapon = weaponManager.GetCurrentWeapon();

        if(currentWeapon.fireRate <=0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shoot",0f,1f/currentWeapon.fireRate);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }
        }
		

	}

    //Is called on the server when a player shoots
    [Command]
    void CmdOnShoot()
    {
        RpcDoShootEffect();    
    }

    //Is called on all clients when we need to do a shoot effect
    [ClientRpc]
    void RpcDoShootEffect()
    {
        weaponManager.GetCurrentGraphics().muzzleFlash.Play();
    }
    //Is Called on the server when we hit something
    //Takes in the hit point and the normal of the surface
    [Command]
    void CmdOnHit(Vector3 _pos, Vector3 _normal)
    {
        RpcDoHitEffect(_pos, _normal);
    }

    //Is called ona all clients
    //Here we can spawn in effects
    [ClientRpc]
    void RpcDoHitEffect(Vector3 _pos, Vector3 _normal)
    {
        GameObject _hitEffect = Instantiate(weaponManager.GetCurrentGraphics().hitEffectPrefab, _pos, Quaternion.LookRotation(_normal));
        Destroy(_hitEffect, 2f);
    }

    [Client]
    private void Shoot()
    {
        //GameObject bullet = Instantiate(weapon.bullet, spawnBullet.transform.position, spawnBullet.transform.rotation);
        //Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        //bulletRb.AddForce(bullet.transform.forward * weapon.force * Time.fixedDeltaTime, ForceMode.Force);
        //StartCoroutine("WaitToDestroy", bullet);
        if(!isLocalPlayer)
        {
            return;
        }
        //We are shooting, call the OnShoot method on the server
        CmdOnShoot();

        RaycastHit _hit;
        if (Physics.Raycast(spawnBullet.transform.position, spawnBullet.transform.forward, out _hit, currentWeapon.range, mask))
        {
            if (_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name, currentWeapon.damage);
            }

            //We hit something, call the OnHit method on the server
            CmdOnHit(_hit.point, _hit.normal);
        }
    }

    [Command]
    public void CmdPlayerShot(string _playerID, int _damage)
    {
        Debug.Log(_playerID + " has been shot.");

        PlayerManager _player = GameManager.GetPlayer(_playerID);
        _player.RpcTakeDamage(_damage);

    }

    //IEnumerator WaitToDestroy(GameObject _bullet)
    //{
    //    yield return new WaitForSeconds(weapon.timeToDestroy);
    //    Destroy(_bullet);
    //}
}
