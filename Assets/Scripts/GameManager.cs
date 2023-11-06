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

    bool isLose;
    

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        if (SceneManager.GetActiveScene().name != ("MainMenu"))
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }

    }

    private void Start()
    {
        isWin = false;
        isLose = false;
        isGameOver = false;
        Time.timeScale = 1;


        if (AudioManager.Instance.x == true)
        {
            if (SceneManager.GetActiveScene().name == ("MainMenu"))
            {
                AudioManager.Instance.PlayMusic("MainMenu");
            }

            if (SceneManager.GetActiveScene().name == ("Level 1") || SceneManager.GetActiveScene().name == ("Level 2") || SceneManager.GetActiveScene().name == ("Level 3"))
            {
                AudioManager.Instance.PlayMusic("LevelGame");
            }
            AudioManager.Instance.x = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().name != ("MainMenu"))
        {

            if (Objective == victoryCondition)
            {
                if (!isWin)
                    AudioManager.Instance.PlaySFX("Win");
                isWin = true;

                UnlockNewLevel();
            }

            if (!UIManager.Instance.isPaused)
            {
                if (isGameOver)
                {
                    Time.timeScale = 0; 
                    if(!isLose)
                    AudioManager.Instance.PlaySFX("Lose");

                    isLose = true;
                }
                else
                {
                    Time.timeScale = 1;
                }
            }
        }

    }


    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
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
        isLose = false;

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


