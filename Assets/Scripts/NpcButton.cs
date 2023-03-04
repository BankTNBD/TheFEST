using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcButton : MonoBehaviour
{
    private GameObject NPCs;
    public void NpcButtonClick()
    {
        NPCs.GetComponent<TalkWithNPC>().NextLine();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "NPCs")
        {
            NPCs = GameObject.Find("/NPCs"+other.name);
        }
    }
}
