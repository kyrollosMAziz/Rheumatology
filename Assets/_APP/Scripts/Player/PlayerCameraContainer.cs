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

    [SerializeField] Transform _patientPosition;

    public void MoveToTarget(WaypointSerialized waypointSerialized, int waypointIndex)
    {
        if (waypointSerialized.waypoints.Count <= waypointIndex)
        {
            LookAtDoctor();
            return;
        }
        target = waypointSerialized.waypoints[waypointIndex];
        if (target == null) return;

        StartCoroutine(initiateMovement());

        IEnumerator initiateMovement()
        {
            float distance = Vector3.Distance(transform.position, target.position);
            while (distance > minDistance)
            {
                Vector3 targetPosition = target.position;
                transform.position = Vector3.Lerp(transform.position, targetPosition, moveSmoothSpeed * Time.deltaTime);
                distance = Vector3.Distance(transform.position, target.position);
                if (distance < minDistance)
                {
                    Debug.Log("waslt");
                    waypointIndex++;
                    MoveToTarget(waypointSerialized, waypointIndex);
                    yield return new WaitForEndOfFrame();
                    break;
                }

                Vector3 direction = target.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookSmoothSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }
    public void MoveToTarget(WaypointSerialized waypointSerialized, int waypointIndex, UnityAction unityAction = null)
    {
        if (waypointSerialized.waypoints.Count <= waypointIndex)
        {
            LookAtDoctor(unityAction);
            return;
        }
        target = waypointSerialized.waypoints[waypointIndex];
        if (target == null) 
            return;

        StartCoroutine(initiateMovement());

        IEnumerator initiateMovement()
        {
            float distance = Vector3.Distance(transform.position, target.position);
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

                Vector3 direction = target.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookSmoothSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }
    
    public void LookAtDoctor(UnityAction unityAction = null)
    {
        PostProcessingManager.Instance.ToggleVolume(true, () =>
        {
            var phase = SceneManager.Instance.currentPhase;
            _patientPosition = SceneManager.Instance.SerializedPatientPositions.FirstOrDefault(p=>p.Phase == phase).patientPostion;
            transform.LookAt(_patientPosition);
            QuestionsManager.Instance.HideQuestion();
            PostProcessingManager.Instance.ToggleVolume(false);
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
