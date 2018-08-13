using UnityEngine;

public class PlayerUI : MonoBehaviour {

    [SerializeField]
    RectTransform thrusterFuelFill;

    private Player controller;

    public void SetController(Player _controller)
    {
        controller = _controller;
    }
    private void Update()
    {
        SetFuelAmount(controller.GetThrusterFuelAmount());
    }

    void SetFuelAmount(float _amount)
    {
        thrusterFuelFill.localScale = new Vector3(1f, _amount, 1f);
    }
}
