using UnityEngine;

public class RegisterPlayer : MonoBehaviour
{

	// Use this for initialization
	void Start () {
        string _ID = "Collider" + gameObject.transform.parent.name;
        transform.name = _ID;
    }
	

}
