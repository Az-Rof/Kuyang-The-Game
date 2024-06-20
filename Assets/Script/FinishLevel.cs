using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinishLevel : MonoBehaviour
{
    [SerializeField]
    string sceneName;

    private int level;
    private bool isKidnapped;
    public GameObject levelfinish;

    private void Start()
    {
        // Extract the level number from the scene name
        string sceneName = SceneManager.GetActiveScene().name;
        level = int.Parse(sceneName.Replace("Level ", ""));
        AudioManager.Instance.LsfxSource.Stop();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        GameObject babyObject = GameObject.FindGameObjectWithTag("Baby");
        if (babyObject == null) return; // If there's no baby object, exit the method

        BabyScript babyScript = babyObject.GetComponent<BabyScript>();
        if (babyScript == null) return; // If the baby object doesn't have a Baby_script component, exit the method

        if (isKidnapped = babyScript.isKidnapped)
        {
            isKidnapped = true;
        }

        if (other != null && other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            if (isKidnapped)
            {
                Time.timeScale = 0f;
                levelfinish.SetActive(true);
                AudioManager.Instance.LsfxSource.Stop();
            }
        }
    }
    public void NextLevel()
    {
        SceneManager.LoadScene("Level " + (++level));
        Time.timeScale = 1f;
    }

}