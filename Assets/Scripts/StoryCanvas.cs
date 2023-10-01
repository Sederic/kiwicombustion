using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryCanvas : MonoBehaviour
{
    [SerializeField] List<TMP_Text> storyTexts = new List<TMP_Text>();
    int index = 0;

    public void NextText()
    {
        Debug.Log("Button clicked");
        storyTexts[index].gameObject.SetActive(true);
        index++;
   
    }
}
