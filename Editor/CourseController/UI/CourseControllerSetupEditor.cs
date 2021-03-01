using System;
using System.Collections.Generic;
using System.Linq;
using Innoactive.Creator.Core;
using Innoactive.Creator.Core.Utils;
using Innoactive.Creator.UX;
using UnityEditor;
using UnityEngine;

namespace Innoactive.CreatorEditor.UX
{
    /// <summary>
    /// Custom editor for <see cref="ICourseController"/>s.
    /// Takes care of adding required components.
    /// </summary>
    [CustomEditor(typeof(CourseControllerSetup))]
    internal class CourseControllerSetupEditor : Editor
    {
        private SerializedProperty courseControllerProperty;
        private SerializedProperty useCustomPrefabProperty;
        private SerializedProperty customPrefabProperty;

        private CourseControllerSetup setupObject;
        
        private ICourseController[] availableCourseControllers;
        private string[] availableCourseControllerNames;
        private GameObject customPrefab = null;
        private int selectedIndex = 0;
        private bool useCustomPrefab;

        private List<Type> currentRequiredComponents = new List<Type>();
        
        private void OnEnable()
        {
            courseControllerProperty = serializedObject.FindProperty("courseControllerQualifiedName");
            useCustomPrefabProperty = serializedObject.FindProperty("useCustomPrefab");
            customPrefabProperty = serializedObject.FindProperty("customPrefab");

            customPrefab = (GameObject) customPrefabProperty.objectReferenceValue;
            setupObject = (CourseControllerSetup) serializedObject.targetObject;

            availableCourseControllers = ReflectionUtils.GetFinalImplementationsOf<ICourseController>()
                .Select(c => (ICourseController) ReflectionUtils.CreateInstanceOfType(c)).OrderByDescending(controller => controller.Priority).ToArray();

            availableCourseControllerNames = availableCourseControllers.Select(controller => controller.Name).ToArray();

            selectedIndex = availableCourseControllers.Select(controller => controller.GetType().AssemblyQualifiedName).ToList().IndexOf(courseControllerProperty.stringValue);
            if (selectedIndex < 0)
            {
                selectedIndex = 0;
            }
            
            currentRequiredComponents = availableCourseControllers[selectedIndex].GetRequiredSetupComponents();
        }

        public override void OnInspectorGUI()
        {
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((CourseControllerSetup)target), typeof(CourseControllerSetup), false);
            GUI.enabled = useCustomPrefab == false && Application.isPlaying == false;
            bool prevUseCustomPrefab = useCustomPrefab;
            int prevIndex = selectedIndex;
            
            selectedIndex = EditorGUILayout.Popup("Course Controller", selectedIndex, availableCourseControllerNames);
            
            GUI.enabled = !Application.isPlaying;

            useCustomPrefab = EditorGUILayout.Toggle("Use custom prefab", useCustomPrefabProperty.boolValue);

            if (Application.isPlaying)
            {
                if (useCustomPrefab)
                {
                    customPrefab = EditorGUILayout.ObjectField("Custom prefab", customPrefab, typeof(GameObject), false) as GameObject;
                }
                serializedObject.ApplyModifiedProperties();
                
                return;
            }
            
            if (useCustomPrefab)
            {
                customPrefab = EditorGUILayout.ObjectField("Custom prefab", customPrefab, typeof(GameObject), false) as GameObject;
                if (useCustomPrefab != prevUseCustomPrefab)
                {
                    RemoveComponents(currentRequiredComponents);
                    currentRequiredComponents = new List<Type>();
                }
                customPrefabProperty.objectReferenceValue = customPrefab;
            }
            else if (prevIndex != selectedIndex || HasComponents(currentRequiredComponents) == false || useCustomPrefab != prevUseCustomPrefab)
            {
                RemoveComponents(currentRequiredComponents);
                currentRequiredComponents = availableCourseControllers[selectedIndex].GetRequiredSetupComponents();
                AddComponents(currentRequiredComponents);
                availableCourseControllers[selectedIndex].HandlePostSetup(setupObject.gameObject);
            }

            useCustomPrefabProperty.boolValue = useCustomPrefab;
            courseControllerProperty.stringValue = availableCourseControllers[selectedIndex].GetType().AssemblyQualifiedName;
            
            serializedObject.ApplyModifiedProperties();
        }

        private void RemoveComponents(List<Type> components)
        {
            foreach (Type component in currentRequiredComponents)
            {
                DestroyImmediate(setupObject.GetComponent(component), true);
            }
        }

        private void AddComponents(List<Type> components)
        {
            if (components != null)
            {
                foreach (Type requiredComponent in components)
                {
                    setupObject.gameObject.AddComponent(requiredComponent);
                }
            }
        }

        private bool HasComponents(List<Type> components)
        {
            return components.Except(setupObject.gameObject.GetComponents<Component>().ToList().Select(c => c.GetType())).Any() == false;
        }
    }
}