using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCameraContainer : MonoBehaviour
{
    public Transform target;
    public float moveSmoothSpeed = 5f;
    public float lookSmoothSpeed = 5f;
    public float minDistance = 0.1f;

    public Transform PatientPosition;

    // public void MoveToTarget(WaypointSerialized waypointSerialized, int waypointIndex)
    // {
    //     if (waypointSerialized.waypoints.Count <= waypointIndex)
    //     {
    //         LookAtDoctor(waypointSerialized.sequencePhase);
    //         return;
    //     }
    //
    //     target = waypointSerialized.waypoints[waypointIndex];
    //     if (target == null) return;
    //
    //     StartCoroutine(initiateMovement());
    //
    //     IEnumerator initiateMovement()
    //     {
    //         float distance = Vector3.Distance(transform.position, target.position);
    //         while (distance > minDistance)
    //         {
    //             Vector3 targetPosition = target.position;
    //             transform.position = Vector3.Lerp(transform.position, targetPosition, moveSmoothSpeed * Time.deltaTime);
    //             distance = Vector3.Distance(transform.position, target.position);
    //             if (distance < minDistance)
    //             {
    //                 Debug.Log("waslt");
    //                 waypointIndex++;
    //                 MoveToTarget(waypointSerialized, waypointIndex);
    //                 yield return new WaitForEndOfFrame();
    //                 break;
    //             }
    //
    //             Vector3 direction = target.position - transform.position;
    //             Quaternion targetRotation = Quaternion.LookRotation(direction);
    //             transform.rotation =
    //                 Quaternion.Slerp(transform.rotation, targetRotation, lookSmoothSpeed * Time.deltaTime);
    //             yield return new WaitForEndOfFrame();
    //         }
    //     }
    // }

    public void MoveToTarget(WaypointSerialized waypointSerialized, int waypointIndex, UnityAction unityAction = null)
    {
        if (waypointSerialized.waypoints.Count <= waypointIndex)
        {
            LookAtDoctor(waypointSerialized.sequencePhase, unityAction);
            return;
        }

        target = waypointSerialized.waypoints[waypointIndex];

        if (target == null)
            return;

        StartCoroutine(initiateMovement());

        IEnumerator initiateMovement()
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance <= 0.5) // this mean en l waypoint in the same place 
            {
                LookAtDoctor(waypointSerialized.sequencePhase, unityAction);
                yield break;
            }

            print("entered");
            while (distance > minDistance)
            {
                Vector3 targetPosition = target.position;
                transform.position = Vector3.Lerp(transform.position, targetPosition, moveSmoothSpeed * Time.deltaTime);
                distance = Vector3.Distance(transform.position, target.position);
                if (distance < minDistance)
                {
                    Debug.Log("waslt");
                    waypointIndex++;
                    MoveToTarget(waypointSerialized, waypointIndex, unityAction);
                    yield return new WaitForEndOfFrame();
                    break;
                }

                // Vector3 direction = target.position - transform.position;
                // Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, target.rotation, lookSmoothSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void LookAtDoctor(GameSequencePhase phase, UnityAction unityAction = null)
    {
        PostProcessingManager.Instance.ToggleVolume(true, () =>
        {
            QuestionsManager.Instance.HideQuestion();
            PostProcessingManager.Instance.ToggleVolume(false);

            print("look at" + phase.ToString());
            PatientController.Instance.UpdateTransform(phase);
            DoctorController.Instance.UpdateTransform(phase);

            // _patientPosition = SceneManager.Instance.SerializedPatientPositions.FirstOrDefault(p=>p.Phase == phase).patientPostion;
            if (PatientPosition != null)
            {
                transform.LookAt(PatientPosition);
            }
            if (unityAction != null) unityAction();
        });
    }

    //private void Update()
    //{
    //    if (target == null) return;

    //    float distance = Vector3.Distance(transform.position, target.position);

    //    if (distance > minDistance)
    //    {
    //        Vector3 targetPosition = target.position;
    //        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSmoothSpeed * Time.deltaTime);
    //    }
    //    Vector3 direction = target.position - transform.position;
    //    Quaternion targetRotation = Quaternion.LookRotation(direction);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookSmoothSpeed * Time.deltaTime);
    //}
}