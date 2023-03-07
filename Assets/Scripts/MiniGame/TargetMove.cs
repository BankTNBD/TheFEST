using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
public class TargetMove : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 2f;
    public int score;
    public int maxBullets;
    public Text scoreText;
    public Text bulletsText;
    public GameObject winPanel;
    public GameObject winButton;
    public TMP_Text winText;
    public bool isWin = false;
    private int bullets;
    private int currentWaypointIndex = 0;
    private int startWaypoint = 0;
    private GameObject trigObject;

    private void Start()
    {
        winPanel.SetActive(false);
        winButton.SetActive(false);
        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoints found!");
            return;
        }

        transform.position = waypoints[0].position;
        bullets = maxBullets;
    }

    private void Update()
    {
        EventSystem.current.SetSelectedGameObject(null, null);
        scoreText.text = "" + score;
        bulletsText.text = "กระสุน: " + bullets;
        if (waypoints.Length == 0)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (startWaypoint >= 2)
            {
                startWaypoint -= 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (startWaypoint < waypoints.Length-2)
            {
                startWaypoint += 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isWin)
        {
            if (bullets > 0)
            {
                bullets--;
                if (trigObject != null)
                {
                    Destroy(trigObject);
                    score++;
                }
            }

        }
        else if (isWin)
        {
            Win();
        }
        if (bullets == 0 && score >= 6)
        {
            winText.text = "ชนะ";
            isWin = true;
        }
        else if (bullets == 0)
        {
            winText.text = "แพ้";
            isWin = true;
        }
        if (!isWin)
        {
            MoveToNextWaypoint();
        }
        Debug.Log("dsdfsd");
    }

    private void MoveToNextWaypoint()
    {
        Vector2 targetDirection = ((Vector2)waypoints[currentWaypointIndex].position - (Vector2)transform.position).normalized;

        transform.Translate(targetDirection * moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            if (currentWaypointIndex == startWaypoint)
            {
                currentWaypointIndex++;
            }
            else
            {
                currentWaypointIndex = startWaypoint;
            }
            /*
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }*/
        }
    }
    public void MoveUp()
    {
        if (startWaypoint >= 2)
        {
            startWaypoint -= 2;
        }
    }

    public void MoveDown()
    {
        if (startWaypoint < waypoints.Length - 2)
        {
            startWaypoint += 2;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obj")
        {
            Debug.Log("hskdka");
            trigObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Obj")
        {
            trigObject = null;
        }
    }
    private void Win()
    {
        winPanel.SetActive(true);
        winButton.SetActive(true);
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

    public void Shoot()
    {
        if (bullets > 0 && !isWin)
        {
            bullets--;
            if (trigObject != null)
            {
                Destroy(trigObject);
                score++;
            }
        }
    }
}
