using System;
using System.Linq;
using UnityEngine;
using Innoactive.Creator.Core.Utils;

namespace Innoactive.Creator.UX
{
    /// <summary>
    /// Manages the setup of the course controller and lets the developer choose the <see cref="ICourseController"/>.
    /// </summary>
    public class CourseControllerSetup : MonoBehaviour
    {
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
                Type courseControllerType = RetrieveCourseControllerType();
                
                if (ReflectionUtils.CreateInstanceOfType(courseControllerType) is ICourseController controller)
                {
                    Instantiate(controller.GetCourseControllerPrefab());
                }
            }
        }

        private Type RetrieveCourseControllerType()
        {
            if (string.IsNullOrEmpty(courseControllerQualifiedName))
            {
                return RetrieveDefaultControllerType();
            }
            
            Type courseControllerType = ReflectionUtils.GetTypeFromAssemblyQualifiedName(courseControllerQualifiedName);
            return courseControllerType != null ? courseControllerType : RetrieveDefaultControllerType();
        }

        private Type RetrieveDefaultControllerType()
        {
            return ReflectionUtils.GetFinalImplementationsOf<ICourseController>()
                .Select(c => (ICourseController) ReflectionUtils.CreateInstanceOfType(c)).OrderByDescending(controller => controller.Priority)
                .First()
                .GetType();
        }
    }
}
