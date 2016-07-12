using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public enum PlayerMode
    {
        Elastic,
        Hard
    }

    public Sprite ElasticSprite;
    public Sprite HardSprite;
    public PhysicsMaterial2D BouncyMaterial;

    public PlayerMode Mode;

    private float _elasticMass = 2.0f;
    private float _hardMass = 20.0f;

    // Use this for initialization
    void Start ()
    {
        Time.timeScale = 0.75f;
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(0))
	    {
	        SwitchMode();
            UpdatePlayerMode();
	    }

        
	}

    public void Init()
    {
        Mode = PlayerMode.Elastic;

    }

    public void SwitchMode()
    {
        Mode = Mode == PlayerMode.Elastic ? PlayerMode.Hard : PlayerMode.Elastic;
    }

    public void UpdatePlayerMode()
    {
        switch (Mode)
        {
            case PlayerMode.Elastic:
                gameObject.GetComponent<SpriteRenderer>().sprite = ElasticSprite;
                gameObject.GetComponent<Rigidbody2D>().mass = _elasticMass;
                gameObject.GetComponent<CircleCollider2D>().sharedMaterial = BouncyMaterial;
                break;
            case PlayerMode.Hard:
                gameObject.GetComponent<SpriteRenderer>().sprite = HardSprite;
                gameObject.GetComponent<Rigidbody2D>().mass = _hardMass;
                gameObject.GetComponent<CircleCollider2D>().sharedMaterial = null;
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Surface" && Mode == PlayerMode.Hard)
        {
            if (col.gameObject.GetComponent<SurfaceConfig>().Destroyable)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), col.gameObject.GetComponent<BoxCollider2D>());
                Destroy(col.gameObject);
                gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity * 0.8f;
            }
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        Debug.Log("OnCollisionStay2D");
        if (col.gameObject.tag == "Button" && Mode == PlayerMode.Hard)
        {
            col.gameObject.GetComponent<ButtonController>().OpenGate();
            Destroy(col.gameObject);
        }
    }
}
