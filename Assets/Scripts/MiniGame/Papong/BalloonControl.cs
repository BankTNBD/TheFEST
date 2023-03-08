using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BalloonControl : MonoBehaviour
{
    public GameObject[] Waypoints;
    public int selectedWaypoint;
    public float moveSpeed;
    public int scores;
    public int bullets;
    public GameObject endPanel;
    public Text bulletText;
    public GameObject parentObj;
    public TMP_Text scoresPanel;
    private int emptyChild = 0;
    private int childIndex = 0;
    private GameObject balloonTrig;
    private bool isEnd = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (selectedWaypoint > 0)
            {
                selectedWaypoint--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (selectedWaypoint < Waypoints.Length-1)
            {
                selectedWaypoint++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bullets > 0)
            {

                bullets--;
                if (balloonTrig != null)
                {
                    Destroy(balloonTrig);
                    scores++;
                }
            }

        }
        if (bullets == 0 && !isEnd)
        {
            for (int i = 0; i < parentObj.transform.childCount; i++)
            {
                if (parentObj.transform.GetChild(i).childCount == 0)
                {
                    emptyChild++;
                }
            }
            scores += emptyChild * 5;
            scoresPanel.text = scores + " แต้ม";
            endPanel.SetActive(true);
            isEnd = true;
        }
        if (bullets > 0)
        {
            
            MoveToNextWaypoint();
        }
        bulletText.text = "ลูกดอกคงเหลือ:" + bullets;
    }

    private void MoveToNextWaypoint()
    {
        Vector2 targetDirection = ((Vector2)Waypoints[selectedWaypoint].transform.GetChild(childIndex).position - (Vector2)transform.position).normalized;

        transform.Translate(targetDirection * moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, Waypoints[selectedWaypoint].transform.GetChild(childIndex).position) < 0.1f)
        {
            childIndex++;

            if (childIndex >= Waypoints[selectedWaypoint].transform.childCount)
            {
                childIndex = 0;
            }
            /*
            if (currentWaypointIndex == startWaypoint)
            {
                currentWaypointIndex++;
            }
            else
            {
                currentWaypointIndex = startWaypoint;
            }*/
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obj")
        {
            balloonTrig = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Obj")
        {
            balloonTrig = null;
        }
    }
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }
    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            Debug.Log(operation.progress);

            yield return null;
        }
    }
}
