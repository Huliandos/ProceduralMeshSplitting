using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitNinjaController : MonoBehaviour
{
    [SerializeField]
    GameObject[] objectsToHide;

    [SerializeField]
    GameObject[] objectsToExpose;

    [SerializeField]
    GameObject[] prefabsToInstantiate;

    [SerializeField]
    Text scoreText, livesText;

    bool minigameInitialized;

    float timePassed, timeSinceLastFrame, timeBetweenSpawns = 2f;

    int score, lives = 3;

    // Update is called once per frame
    void Update()
    {
        if (minigameInitialized) {
            if (timePassed > timeBetweenSpawns) {
                Vector3 position = new Vector3(Random.Range(-4f, 4f), 10, Random.Range(9f, 17f));
                float size = Random.Range(.5f, 1);
                Vector3 eulerRotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

                int itemNumber = Random.Range(0, prefabsToInstantiate.Length);

                GameObject go = Instantiate(prefabsToInstantiate[itemNumber], position, Quaternion.Euler(eulerRotation));
                go.transform.localScale *= size;
                go.name = "fruit";

                timePassed -= timeBetweenSpawns;
            }

            timePassed += Time.time - timeSinceLastFrame;

            timeSinceLastFrame = Time.time;
        }
    }

    public void startMinigame() {
        foreach (GameObject obj in objectsToHide)
        {
            foreach (MeshRenderer meshRend in obj.GetComponentsInChildren<MeshRenderer>())
            {
                meshRend.enabled = false;
                if(meshRend.GetComponent<Collider>())
                    meshRend.GetComponent<Collider>().enabled = false;
            }
            foreach (Canvas canvas in obj.GetComponentsInChildren<Canvas>())
            {
                canvas.enabled = false;
            }
        }

        foreach (GameObject obj in objectsToExpose)
            obj.SetActive(true);

        lives = 3;
        livesText.text = "Lives:" + System.Environment.NewLine +"OOO";
        score = 0;
        scoreText.text = "Score:" + System.Environment.NewLine + "0";

        timeSinceLastFrame = Time.time;

        minigameInitialized = true;
    }

    public void endMinigame() {
        foreach (GameObject obj in objectsToHide)
        {
            foreach (MeshRenderer meshRend in obj.GetComponentsInChildren<MeshRenderer>())
            {
                meshRend.enabled = true;
                if (meshRend.GetComponent<Collider>())
                    meshRend.GetComponent<Collider>().enabled = true;
            }
            foreach (Canvas canvas in obj.GetComponentsInChildren<Canvas>())
            {
                canvas.enabled = true;
            }
        }

        foreach (GameObject obj in objectsToExpose)
            obj.SetActive(false);

        minigameInitialized = false;
    }

    public void looseLive() {
        lives--;

        livesText.text = livesText.text.Substring(0, livesText.text.Length-1);

        if (lives == 0)
            endMinigame();
    }

    public void increaseScore(GameObject go) {

        if (!go.name.Contains("(Clone)"))
        {
            looseLive();
        }
        else
        {
            score += 25;

            scoreText.text = "Score:" + System.Environment.NewLine + score;
        }
        
        Destroy(go);
    }
}
