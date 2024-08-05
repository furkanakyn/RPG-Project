using UnityEngine;

public class ActionScheduler : MonoBehaviour
{
    private IAction currentAction;

    public void StartAction(IAction action)
    {
        if (currentAction == action)
        {
            return;
        }

        if (currentAction != null)
        {
            currentAction.Cancel();
            Debug.Log($"{currentAction.GetType().Name} action cancelled.");
        }

        currentAction = action;
        Debug.Log($"{action.GetType().Name} action started.");
    }

}
