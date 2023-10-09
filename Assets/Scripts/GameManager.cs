using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerMovement player;

    public Health playerHealth;

    public int Objective,victoryCondition = 3;


    public bool isGameOver;
    public bool isWin;
    // Start is called before the first frame update
    [HideInInspector]
    public Vector3 respawnPoint;
    

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    private void Start()
    {
        isWin = false;
        isGameOver = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null && SceneManager.GetActiveScene().name == ("Level 1"))
        {

        }

        if(Objective == victoryCondition)
        {
            isWin = true;
            AudioManager.Instance.PlaySFX("Win");
        }

        if (!UIManager.Instance.isPaused)
        {
            if (isGameOver)
            {
                Time.timeScale = 0;
                AudioManager.Instance.PlaySFX("Lose");
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }



    public float GetPercent(float value , float max)
    {
        return (((value * 100) / max) / 100);
    }

    public void RestartToChekcpoint()
    {
        //UIManager.Instance.panelLose.SetActive(false);
        isGameOver = false;

        player.transform.position = respawnPoint;
        //player.RB.velocity = Vector2.zero;
       
        StartCoroutine(WaitSecond());

        Rest();
    }

    public void Rest()
    {
        playerHealth.currentHealthPoints = playerHealth.maxHealthPoints;
    }

    IEnumerator WaitSecond()
    {
        player.RB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        player.RB.AddForce( new Vector2(0, 1) * 50);
        yield return new WaitForSeconds(0.7f);

        player.RB.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

}


