using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour
{

    public GameObject gate;
    private bool _isDoorOpened = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OpenGate()
    {
        if (!_isDoorOpened)
        {
            Destroy(gate);
            _isDoorOpened = true;
        }
    }
}
