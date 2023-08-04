using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMPro.TMP_Text enemyKilledCount;

    public GameObject[] zombies;

    public Camera[] cameras;

    public GameObject[] sprites;

    public GameObject player;

    public float[] healths;

    private void Start()
    {
        PlayerPrefs.SetInt("ShootCount", 0);
        PlayerPrefs.SetInt("ShootHitCount", 0);
        PlayerPrefs.SetInt("EnemyKilledCount", 0);
        PlayerPrefs.SetFloat("PreviousTime", Time.realtimeSinceStartup);
        healths = new float[zombies.Length];
        for (int i = 0; i < zombies.Length; i++)
        {
            switch (zombies[i].tag)
            {
                case "Zombie1":
                    healths[i] = 1000; break;
                case "Zombie2":
                    healths[i] = 1500; break;
                case "Zombie3":
                    healths[i] = 2500; break;
            }
        }
    }

    private void Update()
    {
        Radar();
    }

    public void isShot(GameObject zombie, float damage, float distance)
    {
        PlayerPrefs.SetInt("ShootHitCount", PlayerPrefs.GetInt("ShootHitCount") + 1);

        PlayerPrefs.SetFloat("TotalDamage", PlayerPrefs.GetFloat("TotalDamage") + damage);

        if (PlayerPrefs.GetFloat("ClosestDistance") > distance || PlayerPrefs.GetFloat("ClosestDistance") == 0)
        {
            PlayerPrefs.SetFloat("ClosestDistance", distance);
        }

        if (PlayerPrefs.GetFloat("FarthestDistance") < distance)
        {
            PlayerPrefs.SetFloat("FarthestDistance", distance);
        }

        int index = -1;
        for (int i = 0; i < zombies.Length; i++)
        {
            if (zombies[i].Equals(zombie))
            {
                index = i;
                i = zombies.Length;
            }
        }

        int count = Int32.Parse(enemyKilledCount.text);
        Debug.Log(healths[index]);
        healths[index] -= damage;
        Debug.Log(healths[index]);

        if (healths[index] < 0)
        {
            enemyKilledCount.text = (count + 1).ToString();
            PlayerPrefs.SetInt("EnemyKilledCount", count + 1);
            zombie.SetActive(false);
        }

        if (count == 2)
        {
            PlayerPrefs.SetInt("GameOver", 1);
            SceneManager.LoadScene("FinishScene");
        }
    }

    public void Radar()
    {
        foreach (GameObject image in sprites)
        {
            image.transform.position = new Vector3(0, 0, 0);
        }
        int index = 0;

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cameras[PlayerPrefs.GetInt("Equipment")]);
        for (int i = 0; i < zombies.Length; i++)
        {
            if (GeometryUtility.TestPlanesAABB(planes, zombies[i].GetComponent<MeshRenderer>().bounds) && zombies[i].activeSelf)
            {
                Vector2 dist = new Vector2(player.transform.position.x - zombies[i].transform.position.x,
                                    player.transform.position.z - zombies[i].transform.position.z);
                //Vector3 newPosition = new Vector3(dist.x * 3f + 1066, dist.y * 3f - 44f + 60, 0);
                Vector3 newPosition = new Vector3(dist.x, dist.y, 0);
                newPosition = Quaternion.AngleAxis(player.transform.eulerAngles.y, Vector3.up) * newPosition;
                newPosition = new(newPosition.x * 3f + 1066, newPosition.y * 3f -44f + 60, 0);

                sprites[index].transform.position = newPosition;
                index++;
            }
        }
    }
}
