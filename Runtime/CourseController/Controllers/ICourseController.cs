using System;
using System.Collections.Generic;
using UnityEngine;

namespace Innoactive.Creator.UX
{
    /// <summary>
    /// Controller for managing the course.
    /// Can for example instantiate a controller menu and a spectator camera.
    /// </summary>
    public interface ICourseController
    {
        /// <summary>
        /// Prettified name.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Priority of the controller.
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// Gets a course controller game object.
        /// </summary>
        GameObject GetCourseControllerPrefab();

        /// <summary>
        /// List of component types which are required on the setup object.
        /// </summary>
        /// <returns>List of component types.</returns>
        List<Type> GetRequiredSetupComponents();
    }
}