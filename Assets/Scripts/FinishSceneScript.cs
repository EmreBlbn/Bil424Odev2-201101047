using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishSceneScript : MonoBehaviour
{
    public TMPro.TMP_Text enemyKilledCount;
    public TMPro.TMP_Text gameplayCount;
    public TMPro.TMP_Text shootCount;
    public TMPro.TMP_Text shootHitCount;
    public TMPro.TMP_Text gameOver;
    public TMPro.TMP_Text farthest;
    public TMPro.TMP_Text closest;
    public TMPro.TMP_Text totalDamage;
    public Image healthBar;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        shootCount.text = PlayerPrefs.GetInt("ShootCount").ToString();

        shootHitCount.text = PlayerPrefs.GetInt("ShootHitCount").ToString();

        totalDamage.text = PlayerPrefs.GetFloat("TotalDamage").ToString();

        enemyKilledCount.text = PlayerPrefs.GetInt("EnemyKilledCount").ToString();

        farthest.text = PlayerPrefs.GetFloat("FarthestDistance").ToString();

        closest.text = PlayerPrefs.GetFloat("ClosestDistance").ToString();

        gameplayCount.text = ((int)(Time.realtimeSinceStartup - PlayerPrefs.GetFloat("PreviousTime"))).ToString();

        if (PlayerPrefs.GetInt("GameOver") == 0)
        {
            gameOver.text = "You lost";
            healthBar.transform.localScale = Vector3.zero;
            gameOver.color = Color.red;
        }
        else
        {
            gameOver.text = "You Win";
            gameOver.color = Color.green;
        }
    }


    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Home()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
