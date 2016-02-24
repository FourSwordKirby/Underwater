using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {
    private static GameManager instance;

    public static List<Player> Players;
    public static CameraControls Camera;
    public static Stage stage;
    public static GameObject[] hit_boxes;

    private static List<GameObject> spawnPoints;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }

        Camera = GameObject.FindObjectOfType<CameraControls>();
        if (Camera == null)
        {
            Debug.Log("Cannot find camera on the current scene.");
        }

        Players = new List<Player>(GameObject.FindObjectsOfType<Player>());
        if (Players == null)
        {
            Debug.Log("Cannot find players on the current scene.");
        }

        stage = GameObject.FindObjectOfType<Stage>();
        if (Players == null)
        {
            Debug.Log("Cannot locate stage");
        }
        spawnPoints = stage.spawnPoints;

        hit_boxes = GameObject.FindObjectsOfType<GameObject>().Where(x => x.GetComponent<Collider2D>() != null).ToArray();
    }

    void Start()
    {
        //Initialize some physics.
        Physics2D.gravity = new Vector2(0.0f, -10.0f);
    }

    /*public static void LoadScene(string sceneName, bool persistPlayer = true)
    {
        if (persistPlayer)
            DontDestroyOnLoad(Player);
        else
        {
            Destroy(Player);
            _player = null;
        }

        Application.LoadLevel(sceneName);

        FindPlayer();
        FindCamera();
    }*/


    void Update()
    {
        /*ButtonCheck*/
        if (Controls.attackInputDown(Players[0]))
            Debug.Log("P1 ATTACK");
        if (Controls.specialInputDown(Players[0]))
            Debug.Log("P1 SPECIAL");
        if (Controls.jumpInputDown(Players[0]))
            Debug.Log("P1 JUMP");
        if (Controls.shieldInputDown(Players[0]))
            Debug.Log("P1 SHIELD");
        if (Controls.enhanceInputDown(Players[0]))
            Debug.Log("P1 ENHANCE");
        if (Controls.superInputDown(Players[0]))
            Debug.Log("P1 SUPER");
        if (Controls.pauseInputDown(Players[0]))
            Debug.Log("PAUSE");
    }

    public static void PlayerDeath()
    {
        Camera.Shake();
    }


    //Returns an open position in the specified direction that is at most maxDistance away
    public static Vector2 getOpenLocation(Parameters.InputDirection direction, Vector2 startingPosition, float maxDistance = 1.0f)
    {
        Vector2 newPosition = startingPosition;
        Vector2 increment = new Vector2(0, 0);
        float incrementDistance = 0.1f;
        float currentDistance = 0.0f;

        increment = Parameters.getVector(direction) * incrementDistance;
        while (pointCollides(newPosition))
        {
            currentDistance += incrementDistance;
            newPosition += increment;

            if(currentDistance > maxDistance)
                return startingPosition;
        }
        return newPosition + (10f * currentDistance * increment);
    }

    //Checks if there are any collision boxes over the specified point
    private static bool pointCollides(Vector2 point)
    {
        return System.Array.Exists(hit_boxes, (GameObject hitbox) => hitbox.GetComponent<Collider2D>().bounds.Contains(point));
    }

    public static Vector3 GetRespawnPosition()
    {
        //We will make this pick a location specified by the stage later
        return stage.spawnPoints[Random.Range(0, stage.spawnPoints.Count)].transform.position;
    }
}
