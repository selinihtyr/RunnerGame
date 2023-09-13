using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{
    public int score;
    public int heart = 3;
    CharController charController;


    public GameObject DeathPanel;
    public TextMeshProUGUI deathText;
    public GameObject WinPanel;
    public TextMeshProUGUI winText;


    public TextMeshProUGUI starText;


    public TextMeshProUGUI doorText1;
    public GameObject door1;
    public TextMeshProUGUI doorText2;
    public GameObject door2;
    public TextMeshProUGUI doorText3;
    public GameObject door3;
    
    
    void Start()
    {
        DeathPanel.SetActive(false);
        WinPanel.SetActive(false);
    }

    void Update()
    {
        starText.text = score.ToString();
        RestartGame();
    }

    public void DestroyDoor()
    {
        if(score >= 15)
        {
            Destroy(door1);
            Destroy(doorText1);
        }
    }
    public void DestroyDoor1()
    {
        if(score >= 25)
        {
            Destroy(door2);
            Destroy(doorText2);
        }
    }
    public void DestroyDoor2()
    {
        if(score >= 40)
        {
            Destroy(door3);
            Destroy(doorText3);
        }
    }

    public void WinGame() //oyunu bitirdiği zaman gelen mesaj
    {
        WinPanel.SetActive(true);
        winText.text = "Congratulations!! You Succeeded";
    }

    public void DefeatGame() //kapıda öldüğü zaman gelen mesaj
    {
        DeathPanel.SetActive(true);
        deathText.text = "Your Died!! Try Again";
        if(Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void RestartGame() //engellere çarparak ölünce
    {
        if(score < 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void StartGame() //butona eklenecek olan mesaj
    {
        SceneManager.LoadScene(1);
        Debug.Log("Game started");
    }

    public void HeartScore()
    {
        heart--;
    }

    public void AddScore()
    {
        score++;
        starText.text = score.ToString();
    }

    public void SubtractScore()
    {
        score --;
        starText.text = score.ToString();
    }
}