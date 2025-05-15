using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR;
using System.Collections;
using UnityEngine.XR.Management;

public class XRAnchorSnap : MonoBehaviour
{
    [SerializeField] private Transform anchorPoint;
    [SerializeField] private XROrigin xrOrigin;

    private IEnumerator Start()
    {
        if (xrOrigin == null || anchorPoint == null)
        {
            Debug.LogError("Anchor или XR Origin не назначены!");
            yield break;
        }

        yield return new WaitUntil(() => XRSubsystemHelpers.IsXrReady());
        yield return new WaitForSeconds(0.1f); 

        AlignRigToAnchor();
    }

    private void AlignRigToAnchor()
    {
        Transform head = xrOrigin.Camera.transform;

        Vector3 headOffset = head.position - xrOrigin.transform.position;
        Vector3 targetPosition = anchorPoint.position - new Vector3(headOffset.x, 0f, headOffset.z);
        targetPosition.y = xrOrigin.transform.position.y;
        xrOrigin.transform.position = targetPosition;

        Vector3 currentHeadForward = new Vector3(head.forward.x, 0f, head.forward.z).normalized;
        Vector3 targetForward = new Vector3(anchorPoint.forward.x, 0f, anchorPoint.forward.z).normalized;

        float angleDifference = Vector3.SignedAngle(currentHeadForward, targetForward, Vector3.up);

        xrOrigin.transform.RotateAround(head.position, Vector3.up, angleDifference);
    }


}

public static class XRSubsystemHelpers
{
    public static bool IsXrReady()
    {
        return XRGeneralSettings.Instance != null
            && XRGeneralSettings.Instance.Manager != null
            && XRGeneralSettings.Instance.Manager.isInitializationComplete;
    }
}
