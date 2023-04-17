using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class HandController : MonoBehaviour
{
    private ActionBasedController controller;
    public Hand hand;
    
    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<ActionBasedController>();
    }

    // Update is called once per frame
    private void Update()
    {
        hand.SetGrip(controller.selectAction.action.ReadValue<float>());
        hand.SetTrigger(controller.activateAction.action.ReadValue<float>());
    }
}
