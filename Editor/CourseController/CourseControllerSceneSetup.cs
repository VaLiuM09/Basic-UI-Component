using Innoactive.Creator.Unity;
using Innoactive.Creator.UX;
using UnityEngine;

namespace Innoactive.CreatorEditor.UX
{
    /// <summary>
    /// Will be called on OnSceneSetup to add the course controller menu.
    /// </summary>
    public class CourseControllerSceneSetup : SceneSetup
    {
        /// <inheritdoc />
        public override int Priority { get; } = 20;
        
        /// <inheritdoc />
        public override string Key { get; } = "CourseControllerSetup";
        
        /// <inheritdoc />
        public override void Setup()
        {
            GameObject courseController = FindPrefab("[COURSE_CONTROLLER]");
            courseController.name = courseController.name.Replace("(Clone)", string.Empty);
            courseController.GetOrAddComponent<CourseControllerSetup>().ResetToDefault();
            Object.Instantiate(courseController);
        }
    }
}