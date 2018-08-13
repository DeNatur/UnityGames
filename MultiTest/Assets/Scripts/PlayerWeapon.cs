using UnityEngine;

[System.Serializable]
public class PlayerWeapon {

    //public GameObject bullet;
    public string name = "Glock";


    public int damage = 10;
    public float range = 100f;

    public float fireRate = 0f;

    public GameObject graphics;
    //public float force = 50000f;
    //public float timeToDestroy = 2f;
    //public void SetWeapon()
    //{
    //    if (name == "Glock")
    //        bullet = (GameObject)Resources.Load("prefabs/bullet", typeof(GameObject));
    //}

}
