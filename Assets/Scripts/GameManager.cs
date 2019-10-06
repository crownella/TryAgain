using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject eyePodPrefab;
    public GameObject spawnPoint;
    GameObject pip = null;
    public int currentLevel = 1;
    public int maxLevels;
    public bool levelFinished;
    public bool gameOVer = false;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Respawn");
    }

    // Update is called once per frame
    void Update()
    {
        if (pip == null && !gameOVer)
        {
            pip = Instantiate(eyePodPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }

        
    }
}
