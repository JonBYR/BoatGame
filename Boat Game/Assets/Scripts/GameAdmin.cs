using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class GameAdmin : MonoBehaviour
{
    [SerializeField] InputActionReference reset;
    [SerializeField] InputActionReference exit;
    // Start is called before the first frame update
    private void OnEnable() //this function is required for inputs that have an action type of button
    {
        reset.action.started += GameReset; //call this function
        exit.action.started += LeaveGame;
    }
    private void OnDisable()
    {
        reset.action.started -= GameReset; //ensure that function is not called repeatedly
        exit.action.started -= LeaveGame;
    }
    void GameReset(InputAction.CallbackContext context)
    {
        Debug.Log("Resetting");
        SceneManager.LoadScene("MainScene"); //reset the scene if boat is on island or capsized
    }
    void LeaveGame(InputAction.CallbackContext context)
    {
        Debug.Log("Quitting");
        Application.Quit(); //Pressing escape will cause application to quit
    }
}
