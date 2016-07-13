using UnityEngine;
using System.Collections;

public class TeleportLogic : MonoBehaviour
{

    public GameObject ExitGate;

    public void TeleportToExitGate()
    {
        var player = PlayerController.Instance.gameObject;
        var velocity = player.GetComponent<Rigidbody2D>().velocity;
        var locationHelperObj = ExitGate.transform.FindChild("LocationHelper");
        
        player.transform.position = locationHelperObj.position;
        var Vmagnitude = velocity.magnitude;
        player.GetComponent<Rigidbody2D>().velocity = ExitGate.transform.up * Vmagnitude;
        PlayerController.Instance.TeleportAudioSource.Play();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("TeleportLogic::OnTriggerEnter2D");
        TeleportToExitGate();
    }
}
