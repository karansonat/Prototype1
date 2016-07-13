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

    private int collectedStarCount = 0;


    public PlayerMode Mode;

    private float _elasticMass = 2.0f;
    private float _hardMass = 20.0f;

	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(0))
	    {
	        if (GameController.Instance.isGameStarted)
	        {
                SwitchMode();
                UpdatePlayerMode();
	            return;
	        }
            GameController.Instance.isGameStarted = true;
	        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            UIController.Instance.HideTapToStartDialogue();
	    }
	}

    public void Init()
    {
        Mode = PlayerMode.Elastic;
        UpdatePlayerMode();
        collectedStarCount = 0;
        UIController.Instance.SetStars(collectedStarCount);
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        transform.position = new Vector3(0, 4.722f, 0);
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
                //TODO: Ignore Collision doesn't work in same frame. Fix it.
                Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), col.gameObject.GetComponent<BoxCollider2D>());
                Destroy(col.gameObject);
                gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity * 0.8f;
            }
        }else if (col.gameObject.tag == "EndSurface")
        {
            //TODO: End game screen should implement here.
            GameController.Instance.Retry();
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Button" && Mode == PlayerMode.Hard)
        {
            col.gameObject.GetComponent<ButtonController>().OpenGate();
            Destroy(col.gameObject);
        }
    }
}
