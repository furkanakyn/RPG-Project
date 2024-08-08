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
        
            if (currentAction != null)
            {
                currentAction.Cancel();
                Debug.Log($"{currentAction.GetType().Name} action cancelled.");
            }
        }

        currentAction = action;

        if (action != null)
        {
            Debug.Log($"{action.GetType().Name} action started.");
        }
    }

    public void CancelCurrentAction()
    {
        if (currentAction != null)
        {
            StartAction(null);
        }
    }
}