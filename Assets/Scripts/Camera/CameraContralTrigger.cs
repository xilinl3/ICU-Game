using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CameraContralTrigger : MonoBehaviour
{
    public CustomInspectorObject customInspectorObject;

    private Collider2D _coll;
    private float playerEnterXPosition;

    private void Start()
    {
        _coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            if(customInspectorObject.panCameraOnContact)
            {
                //pan the Camera
                CameraManager.Instance.panCameraOnContact(customInspectorObject.panDistance, customInspectorObject.panTime, customInspectorObject.panDirection, false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {

            Vector2 exitDirection = (collider.transform.position - _coll.bounds.center).normalized;

            if(customInspectorObject.swapCamera && 
            customInspectorObject.cameraOnLeft != null && 
            customInspectorObject.cameraOnRight != null)
            {
                Debug.Log($"Exit Direction Detected: {exitDirection}");
                //swap cameras
                CameraManager.Instance.SwapCamera(
                customInspectorObject.cameraOnLeft, 
                customInspectorObject.cameraOnRight, 
                exitDirection);
            }

            if(customInspectorObject.TopBottomCamera && 
            customInspectorObject.cameraOnTop!= null && 
            customInspectorObject.cameraOnBottom!= null)
            {
                //Debug.Log($"Exit Direction Detected: {exitDirection}");
                //top bottom camera
                CameraManager.Instance.TopBottomCamera(
                    customInspectorObject.cameraOnTop, 
                    customInspectorObject.cameraOnBottom, 
                    exitDirection);
            }
            if(customInspectorObject.panCameraOnContact)
            {
                //pan the Camera
                CameraManager.Instance.panCameraOnContact(customInspectorObject.panDistance, customInspectorObject.panTime, customInspectorObject.panDirection, true);
            }
        }
    }
}

[System.Serializable]
public class CustomInspectorObject
{
    public bool swapCamera = false;
    public bool panCameraOnContact = false;
    public bool TopBottomCamera = false;

    [HideInInspector] public CinemachineVirtualCamera cameraOnLeft;
    [HideInInspector] public CinemachineVirtualCamera cameraOnRight;
    [HideInInspector] public CinemachineVirtualCamera cameraOnTop;
    [HideInInspector] public CinemachineVirtualCamera cameraOnBottom;

    [HideInInspector] public PanDirection panDirection;
    [HideInInspector] public float panDistance = 3f;
    [HideInInspector] public float panTime = 0.35f;
}

public enum PanDirection
{
    Up,
    Down,
    Left,
    Right
}

#if UNITY_EDITOR
[CustomEditor(typeof(CameraContralTrigger))]
[CanEditMultipleObjects]  // This attribute allows multi-object editing.
public class MyScriptEditor : Editor
{
    SerializedProperty customInspectorObject;
    SerializedProperty swapCamera;
    SerializedProperty panCameraOnContact;
    SerializedProperty cameraOnLeft;
    SerializedProperty cameraOnRight;
    SerializedProperty cameraOnTop;
    SerializedProperty cameraOnBottom;
    SerializedProperty panDirection;
    SerializedProperty panDistance;
    SerializedProperty panTime;

    private void OnEnable()
    {
        customInspectorObject = serializedObject.FindProperty("customInspectorObject");
        swapCamera = customInspectorObject.FindPropertyRelative("swapCamera");
        panCameraOnContact = customInspectorObject.FindPropertyRelative("panCameraOnContact");
        cameraOnLeft = customInspectorObject.FindPropertyRelative("cameraOnLeft");
        cameraOnRight = customInspectorObject.FindPropertyRelative("cameraOnRight");
        cameraOnTop = customInspectorObject.FindPropertyRelative("cameraOnTop");
        cameraOnBottom = customInspectorObject.FindPropertyRelative("cameraOnBottom");
        panDirection = customInspectorObject.FindPropertyRelative("panDirection");
        panDistance = customInspectorObject.FindPropertyRelative("panDistance");
        panTime = customInspectorObject.FindPropertyRelative("panTime");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();  // Start updating the serialized object

        DrawDefaultInspector();

        if (swapCamera.boolValue)
        {
            EditorGUILayout.PropertyField(cameraOnLeft, new GUIContent("Camera on Left"));
            EditorGUILayout.PropertyField(cameraOnRight, new GUIContent("Camera on Right"));
        }

        if (customInspectorObject.FindPropertyRelative("TopBottomCamera").boolValue)
        {
        EditorGUILayout.PropertyField(cameraOnTop, new GUIContent("Camera on Top"));
        EditorGUILayout.PropertyField(cameraOnBottom, new GUIContent("Camera on Bottom"));
        }

        if (panCameraOnContact.boolValue)
        {
            EditorGUILayout.PropertyField(panDirection, new GUIContent("Camera Pan Direction"));
            EditorGUILayout.PropertyField(panDistance, new GUIContent("Pan Distance"));
            EditorGUILayout.PropertyField(panTime, new GUIContent("Pan Time"));
        }

        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();  // Apply changes to all selected objects
        }
    }
}
#endif