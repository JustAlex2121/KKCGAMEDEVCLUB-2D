using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    private InputAction menuAction;
    public GameObject menuCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuCanvas.SetActive(false);
        menuAction = InputSystem.actions.FindAction("Menu");
    }

    // Update is called once per frame
    public void MenuButton()
    {
        menuCanvas.SetActive(!menuCanvas.activeSelf);
    }
}
