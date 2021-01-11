using System;
using Innoactive.Creator.Core.Utils;
using UnityEngine;

namespace Innoactive.Creator.UX
{
    /// <summary>
    /// Manages the setup of the course controller and lets the developer choose the <see cref="ICourseController"/>.
    /// </summary>
    public class CourseControllerSetup : MonoBehaviour
    {
        private GameObject courseController;
        
#pragma warning disable 0649
        [SerializeField, SerializeReference]
        private string courseControllerQualifiedName;
        
        [SerializeField, SerializeReference]
        private bool useCustomPrefab;
        
        [SerializeField, SerializeReference]
        private GameObject customPrefab;
#pragma warning restore 0649
        
        private void Awake()
        {
            CreateCourseController();
        }

        private void CreateCourseController()
        {
            if (useCustomPrefab && customPrefab != null)
            {
                Instantiate(customPrefab);
            }
            else
            {
                Type courseControllerType = ReflectionUtils.GetTypeFromAssemblyQualifiedName(courseControllerQualifiedName);
                if (ReflectionUtils.CreateInstanceOfType(courseControllerType) is ICourseController controller)
                {
                    courseController = Instantiate(controller.GetCourseControllerPrefab());
                }
            }
        }
    }
}
