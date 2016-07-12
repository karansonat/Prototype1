using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoSingleton<PlayerController> {

    public enum PlayerMode
    {
        Elastic,
        Hard
    }

    public Sprite ElasticSprite;
    public Sprite HardSprite;
    public PhysicsMaterial2D BouncyMaterial;

    private bool isGameStarted = false;
    private int collectedStarCount = 0;

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
	        if (isGameStarted)
	        {
                SwitchMode();
                UpdatePlayerMode();
	            return;
	        }
	        isGameStarted = true;
	        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
	        var txt = GameObject.Find("txtTapToStart");
            Destroy(txt);
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

    public void CollectStar()
    {
        collectedStarCount++;
        UIController.Instance.SetStars(collectedStarCount);
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
        }else if (col.gameObject.tag == "EndSurface")
        {
            SceneManager.LoadScene(Application.loadedLevelName);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Button" && Mode == PlayerMode.Hard)
        {
            Debug.Log("OpenGate");
            col.gameObject.GetComponent<ButtonController>().OpenGate();
            Destroy(col.gameObject);
        }
    }

    void OnTriggerEnter2d(Collider2D col)
    {
        Debug.Log("OnTriggerEnter2d Player");
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
