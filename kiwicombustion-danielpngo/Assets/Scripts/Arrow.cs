using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{
    public Transform endPoint; // Reference to the end point GameObject
    private RectTransform pointerRectTransform;
    [SerializeField] Transform myTransfrom;

    private void Awake()
    {
        pointerRectTransform = transform.Find("Arrow").GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector3 toPosition = endPoint.position;
        Vector3 fromPosition = myTransfrom.transform.position;
        fromPosition.z = 0f;
        Vector3 direction = (toPosition - fromPosition).normalized;
        float angle = UtilsClass.GetAngleFromVectorFloat(direction);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
