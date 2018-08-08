using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

public class PlayerShoot : NetworkBehaviour {

    private const string PLAYER_TAG = "Player";


    public PlayerWeapon weapon;


    [SerializeField]
    private GameObject spawnBullet;

    [SerializeField]
    private LayerMask mask;


    // Use this for initialization
    void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

	}
    [Client]
    private void Shoot()
    {


        RaycastHit _hit;
        if(Physics.Raycast(spawnBullet.transform.position, spawnBullet.transform.forward, out _hit,weapon.range, mask))
        {
            if(_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name, weapon.damage);
            }
        }
    }

    [Command]
    public void CmdPlayerShot(string _playerID, int _damage)
    {
        Debug.Log(_playerID + " has been shot.");

        PlayerManager _player = GameManager.GetPlayer(_playerID);
        _player.TakeDamage(_damage);

    }

}
