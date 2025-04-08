using Project2Action;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    [HideInInspector] public ActionGameInput actionInput;
    private ActionGameInput.PlayerActions playerActions;

    void Awake()
    {
        actionInput = new ActionGameInput();
        playerActions = actionInput.Player;
    }

    void OnDestroy()
    {
        actionInput.Dispose();
    }

    void OnEnable()
    {
        playerActions.Enable();
    }

    void OnDisable()
    {
        playerActions.Disable();
    }
}
