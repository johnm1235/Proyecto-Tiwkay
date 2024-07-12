using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AiSensor : MonoBehaviour
{
    public float VisionAngle = 45;
    public Color VisionColor;
    public float MaxVisionDistance;
    public LayerMask mask;

    [Serializable]
    public class OnPlayerDetectClass : UnityEvent { }

    [FormerlySerializedAs("OnPlayerDetected")]
    [SerializeField]
    public OnPlayerDetectClass onPlayerDetected = new OnPlayerDetectClass();

    [Serializable]
    public class OnPlayerLostClass : UnityEvent { }

    [FormerlySerializedAs("OnPlayerLost")]
    [SerializeField]
    public OnPlayerLostClass onPlayerLost = new OnPlayerLostClass();

    private bool playerDetectedPreviously = false;

    private void Update()
    {
        Vector3 targetDirection = AiManager.instance.player.position - transform.position;
        float angle = Vector3.Angle(targetDirection, transform.forward);

        if (angle < VisionAngle)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, targetDirection, out hit, MaxVisionDistance, mask))
            {
                if (hit.collider != null && hit.collider.transform == AiManager.instance.player)
                {
                    Debug.DrawRay(transform.position, targetDirection, Color.red);

                    if (!playerDetectedPreviously)
                    {
                        onPlayerDetected?.Invoke();
                        playerDetectedPreviously = true;
                    }
                }
                else
                {
                    if (playerDetectedPreviously)
                    {
                        onPlayerLost?.Invoke();
                        playerDetectedPreviously = false;
                    }
                }
            }
            else
            {
                if (playerDetectedPreviously)
                {
                    onPlayerLost?.Invoke();
                    playerDetectedPreviously = false;
                }
            }
        }
        else
        {
            if (playerDetectedPreviously)
            {
                onPlayerLost?.Invoke();
                playerDetectedPreviously = false;
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(AiSensor))]
public class EnemyVisionSensor : Editor
{
    public void OnSceneGUI()
    {
        var ai = target as AiSensor;

        Vector3 startPoint = Mathf.Cos(-ai.VisionAngle * Mathf.Deg2Rad) * ai.transform.forward +
            Mathf.Sin(-ai.VisionAngle * Mathf.Deg2Rad) * ai.transform.right;

        Handles.color = ai.VisionColor;
        Handles.DrawSolidArc(ai.transform.position, Vector3.up, startPoint, ai.VisionAngle * 2f, ai.MaxVisionDistance);
    }
}
#endif

