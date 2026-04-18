using UnityEngine;
using UnityEngine.UI; 

public class Credits : MonoBehaviour

{
    public float scrollSpeed = 40f;
    private RectTransform rectTransform;
{
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.anchoredPosition +- Vector2(0, scrollSpeed * Time.deltaTime);
    }
}
