using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;


public class ButtonGrab : XRGrabInteractable
{
    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        // Check if the appropriate grab button is held:
        // Right hand: A (Button.One) or B (Button.Two)
        // Left hand: X (Button.One) or Y (Button.Two)
        bool isHoldingGrabButton =
            OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch) ||
            OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.RTouch) ||
            OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.LTouch) ||
            OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.LTouch);

        return isHoldingGrabButton && base.IsSelectableBy(interactor);
    }
}
