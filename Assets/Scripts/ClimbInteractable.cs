using System;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteractable : XRDirectInteractor
{
    public static event Action<string> ClimbHandActivated, ClimbHandDeactivated;

    private string _controllerName;

    protected override void Start()
    {
        base.Start();
        _controllerName = gameObject.name;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (args.interactableObject.transform.gameObject.CompareTag("Climbable"))
        {
            ClimbHandActivated?.Invoke(_controllerName);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        ClimbHandDeactivated?.Invoke(_controllerName);
    }
}