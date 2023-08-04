using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject gun1;
    public GameObject gun2;
    public GameObject gun3;
    public Mesh mesh1;
    public Mesh mesh2;
    public Mesh mesh3;

    public TMPro.TMP_Text nameText;
    public TMPro.TMP_Text currentBataryText;
    public TMPro.TMP_Text temperatureText;


    public GameManager gameManager;

    public GameObject[] zombies;

    public Camera[] weaponCameras;

    private GameObject[] guns;
    private Mesh[] gunMeshes;
    private readonly int[] weaponCapacities = new int[] { 5000, 10000, 15000 };

    private int rotateAmount = 200;
    private int currentBattery;
    private float startTime = -1;
    private float startTime2;
    private float temperature = 20f;
    private float cd = 0;
    private float eld;


    // Start is called before the first frame update
    private void Start()
    {
        SetSelectedPlayer();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    private void SetSelectedPlayer()
    {
        int indexCharacter = PlayerPrefs.GetInt("IndexCharacter");

        GameObject[] characterList = new GameObject[3];

        for (int i = 0; i < 3; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
            characterList[i].SetActive(false);
        }

        if (characterList[indexCharacter])
            characterList[indexCharacter].SetActive(true);

        // Get Entered Name
        string name = PlayerPrefs.GetString("Name");
        nameText.text = name;

        // Get Selected Weapon
        guns = new GameObject[] { gun1, gun2, gun3 };
        gunMeshes = new Mesh[] { mesh1, mesh2, mesh3 };

        int indexGun = PlayerPrefs.GetInt("IndexGun");
        guns[indexCharacter].GetComponent<MeshFilter>().sharedMesh = gunMeshes[indexGun];
        currentBattery = weaponCapacities[indexGun];
    }

    private void Update()
    {
        currentBataryText.text = currentBattery.ToString();
        temperatureText.text = temperature.ToString("0.00");
        RotatePlayer();
        if (temperature == 20)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startTime = Time.time;
            }
            if (Input.GetMouseButtonUp(0) || (startTime != -1 && Time.time - startTime >= 5.0f))
            {
                if (Time.time - startTime > 5.0f)
                {
                    eld = 5.0f;
                }
                else
                {
                    eld = Time.time - startTime;
                }
                Shoot();

                startTime = -1;
            }
        }
        else if (Time.time - startTime2 >= cd / 2f)
        {
            temperature = 20;
        }
        else
        {
            temperature -= (Time.time - startTime2) / cd / 6.2f;
        }

    }

    private void RotatePlayer()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if (mouseDelta.x < 0)
        {
            transform.Rotate(0, -rotateAmount * Time.deltaTime, 0);
        }
        else if (mouseDelta.x > 0)
        {
            transform.Rotate(0, rotateAmount * Time.deltaTime, 0);
        }
    }

    private void Shoot()
    {
        temperature += eld * 10f;
        cd = (float)(Math.Pow(2, temperature / 10.0f) / Math.Pow(2, temperature / 20.0f));
        startTime2 = Time.time;

        float range = weaponCameras[PlayerPrefs.GetInt("IndexGun")].farClipPlane;
        float le = (float)Math.Pow(2, eld) * 100f;
        currentBattery -= (int)le;

        if (currentBattery < 0)
        {
            return;
        }

        PlayerPrefs.SetInt("ShootCount", PlayerPrefs.GetInt("ShootCount") + 1);
        Ray ray = weaponCameras[PlayerPrefs.GetInt("IndexGun")].ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.transform.gameObject.layer == 6)
            {

                Vector2 dist = new Vector2(transform.position.x - hit.transform.position.x,
                                    transform.position.z - hit.transform.position.z);
                float distance = (float)Math.Sqrt(dist.x * dist.x + dist.y * dist.y);
                float uer = le * (float)Math.Log(distance / 50, 20);
                float damage = le - uer;

                gameManager.isShot(hit.transform.gameObject, damage, distance);

            }
        }
    }


}
