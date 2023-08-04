using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CharacterSelection : MonoBehaviour
{
    private GameObject[] characterList;
    private int indexCharacter = 0;
    private int indexGun = 0;

    public TMPro.TMP_InputField nameText;

    public GameObject gun1;
    public GameObject gun2;
    public GameObject gun3;
    public Mesh mesh1;
    public Mesh mesh2;
    public Mesh mesh3;

    public TMPro.TMP_Dropdown diffDropdown;
    public TMPro.TMP_Dropdown equipmentDropdown;

    public TMPro.TMP_Text weaponType;


    private GameObject[] guns;
    private Mesh[] gunMeshes;
    private string[] gunNames = new string[] {"Q Laser Gun", "W Laser Gun", "E Laser Gun" };


    private void Start()
    {
        // Get Selected Character
        indexCharacter = PlayerPrefs.GetInt("IndexCharacter");

        characterList = new GameObject[transform.childCount];

        for (int i = 0; i < characterList.Length; i++)
            characterList[i] = transform.GetChild(i).gameObject;

        foreach (GameObject character in characterList)
            character.SetActive(false);

        if (characterList[indexCharacter])
            characterList[indexCharacter].SetActive(true);

        // Get Entered Name
        nameText.text = PlayerPrefs.GetString("Name");

        // Get Selected Weapon
        guns = new GameObject[] { gun1, gun2, gun3 };
        gunMeshes = new Mesh[] { mesh1, mesh2, mesh3 };

        indexGun = PlayerPrefs.GetInt("IndexGun");
        foreach (GameObject gun in guns)
            gun.GetComponent<MeshFilter>().sharedMesh = gunMeshes[indexGun];
        weaponType.text = gunNames[indexGun];

        // Get Difficulty
        int diffIndex = PlayerPrefs.GetInt("Difficulty");
        diffDropdown.value = diffIndex;

        // Get Equipment
        int equipmentIndex = PlayerPrefs.GetInt("Equipment");
        equipmentDropdown.value = equipmentIndex;
    }

    public void ToggleLeft()
    {
        Toggle((indexCharacter - 1 + characterList.Length) % characterList.Length);
    }

    public void ToggleRight()
    {
        Toggle((indexCharacter + 1) % characterList.Length);
    }

    private void Toggle(int value)
    {
        characterList[indexCharacter].SetActive(false);

        indexCharacter = value;

        characterList[indexCharacter].SetActive(true);

        PlayerPrefs.SetInt("IndexCharacter", indexCharacter);
    }

    public void ToggleLeftWeapon()
    {
        indexGun = (indexGun + 2) % 3;
        ToggleWeapon();
    }

    public void ToggleRightWeapon()
    {
        indexGun = (indexGun + 1) % 3;
        ToggleWeapon();
    }

    private void ToggleWeapon()
    {
        foreach (GameObject gun in guns)
            gun.GetComponent<MeshFilter>().sharedMesh = gunMeshes[indexGun];
        PlayerPrefs.SetInt("IndexGun", indexGun);
        weaponType.text = gunNames[indexGun];

    }

    public void NameEntered()
    {
        PlayerPrefs.SetString("Name", nameText.text);
    }

    public void DifficultySelected()
    {
        PlayerPrefs.SetInt("Difficulty", diffDropdown.value);
    }

    public void EquipmentSelected()
    {
        PlayerPrefs.SetInt("Equipment", equipmentDropdown.value);
    }

    public void Confirm()
    {
        SceneManager.LoadScene("GameScene");
    }


}
