using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextController : MonoBehaviour
{
    [SerializeField] List<GameObject> texts = new List<GameObject>();
    private int index = 0;

    private void OnMouseUpAsButton()
    {
        if (index < texts.Count)
        {
            texts[index].SetActive(false);
            index++;
        }
    }
}
