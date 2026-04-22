using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class Credits : MonoBehaviour

{
    public RectTransform PanelObject;
    [SerializeField] float scrollSpeed = 50f; // Speed of the scroll
    [SerializeField] float stopYPositionx; // Y position where scrolling stops
    public string menuSceneName = "MainMenu"; // Scene to load when finished

    private void Start()
    {
        stopYPositionx = PanelObject.rect.height;
           
    }
    void Update()
    {
        // Move the container upward
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);

        // Check if credits have finished scrolling
        if (transform.localPosition.y >= stopYPositionx)
        {
            SceneManager.LoadScene(menuSceneName);
        }
    }
}