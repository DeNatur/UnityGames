using UnityEngine;

[System.Serializable]
public class PlayerWeapon {

    public GameObject bullet;
    public string name = "Glock";

    public float damage = 10f;
    public float force = 50000f;
    public float timeToDestroy = 0.5f;
    public void SetWeapon()
    {
        if (name == "Glock")
            bullet = (GameObject)Resources.Load("prefabs/bullet", typeof(GameObject));
    }

}
