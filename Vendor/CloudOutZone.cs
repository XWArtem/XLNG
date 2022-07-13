using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CloudOutZone : MonoBehaviour
{
    [SerializeField] private GameObject[] clouds;
    [SerializeField] private BoxCollider2D[] cloudsColliders = new BoxCollider2D[3];

    private void Awake()
    {
            for (int i = 0; i < clouds.Length; i++) 
        {
            var tempCloud = clouds[i];
            cloudsColliders[i] = tempCloud.GetComponent<BoxCollider2D>();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Array.Exists(cloudsColliders, x => x == other))
        {
            other.gameObject.GetComponent<RectTransform>().anchoredPosition = 
                new Vector3(-1920f, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
        }
    }
}
