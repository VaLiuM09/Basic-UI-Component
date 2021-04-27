using System;
using System.Collections.Generic;
using Innoactive.Creator.Core.Configuration;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Innoactive.Creator.UX
{
    /// <summary>
    /// Base course controller which also takes care that a course menu is spawned.
    /// </summary>
    public abstract class UIBaseCourseController : BaseCourseController
    {
        /// <summary>
        /// Name of the course controller menu prefab.
        /// </summary>
        public abstract string CourseMenuPrefabName { get; }

        /// <summary>
        /// Control scheme used in the input controller.
        /// </summary>
        protected virtual string ControlScheme { get; } = "Keyboard&Mouse";
        
        /// <summary>
        /// Gets a course controller menu game object.
        /// </summary>
        public virtual GameObject GetCourseMenuPrefab()
        {
            return Resources.Load<GameObject>($"Prefabs/{CourseMenuPrefabName}");
        }

        /// <inheritdoc />
        public override List<Type> GetRequiredSetupComponents()
        {
            return new List<Type> {typeof(CourseMenuSpawner)};
        }

        /// <inheritdoc />
        public override void HandlePostSetup(GameObject courseControllerObject)
        {
            courseControllerObject.GetComponent<CourseMenuSpawner>().SetDefaultPrefab(GetCourseMenuPrefab());
            PlayerInput playerInput = courseControllerObject.GetComponent<PlayerInput>();
            if (playerInput != null) 
            {
                playerInput.actions = RuntimeConfigurator.Configuration.CurrentInputActionAsset;
                playerInput.defaultControlScheme = ControlScheme;
            }
        }
    }
}