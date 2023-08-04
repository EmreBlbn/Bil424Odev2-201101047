using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyNavMesh : MonoBehaviour
{
    public Transform player;
    public float speed;

    private void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = player.position;
        int diff = PlayerPrefs.GetInt("Difficulty");
        switch (diff)
        {
            case 0:
                agent.speed = speed * 0.75f; break;
            case 1:
                agent.speed = speed; break;
            case 2:
                agent.speed = speed * 1.25f; break;
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 1f)
        {
            PlayerPrefs.SetInt("GameOver", 0);
            SceneManager.LoadScene("FinishScene");
        }
    }
}