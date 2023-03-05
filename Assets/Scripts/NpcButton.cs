using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NpcButton : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    private string[] dialogue;
    private int index;

    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;

    private int sceneIndex;

    private GameObject NPCs;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
        /*
        if (dialogueText.text.Length >= dialogue[index].Length - 1)
        {
            contButton.SetActive(true);
        }*/
        if (Input.GetKeyDown(KeyCode.Space) && contButton.activeInHierarchy)
        {
            NextLine();
        }
        
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    private IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {

            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        contButton.SetActive(true);

    }

    public void NextLine()
    {
        contButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
            LoadScene(sceneIndex);

        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "NPCs")
        {
            NPCs = GameObject.Find(other.name);
            dialogue = NPCs.GetComponent<TalkWithNPC>().dialogue;
            sceneIndex = NPCs.GetComponent<TalkWithNPC>().sceneIndex;
            playerIsClose = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "NPCs")
        {
            NPCs = null;
            dialogue = null;
            playerIsClose = false;
            index = 0;
            zeroText();
            contButton.SetActive(false);

        }
    }
    public void NpcButtonClick()
    {
        NextLine();
    }
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }
    private IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            Debug.Log(operation.progress);

            yield return null;
        }
    }
}
