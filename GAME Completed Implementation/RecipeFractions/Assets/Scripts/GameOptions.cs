using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOptions : MonoBehaviour {

    //game vars
    private bool levelHeaderShown;
    public static int level;
    public static bool timing;
    public static float timer;

    //GameObjects
    private GameObject LevelHeaderText;
    private GameObject TimeTxt;

    // Use this for initialization
    void Start () {
        level = 1;
        levelHeaderShown = false;

        LevelHeaderText = GameObject.Find("Level2");
        LevelHeaderText.SetActive(false);
        TimeTxt = GameObject.Find("TimeTxt");
    }
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(Level2HeaderText());

        if (timing)
        {
            timer += Time.deltaTime;
            TimeTxt.GetComponent<Text>().text = "Time: " + Mathf.Round(timer).ToString();
        }
	}

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        timing = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    private IEnumerator Level2HeaderText()
    {
        if (level == 2 && !levelHeaderShown)
        {
            yield return new WaitForSeconds(1.5f);
            LevelHeaderText.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            DeliveryMan.spawnDeliveryMan = true;
            yield return new WaitForSeconds(3.5f);
            LevelHeaderText.SetActive(false);
            levelHeaderShown = true;
        }
    }
}
