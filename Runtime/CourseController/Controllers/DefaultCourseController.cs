using System;
using System.Collections.Generic;

namespace Innoactive.Creator.UX
{
    /// <summary>
    /// Default course controller.
    /// </summary>
    public class DefaultCourseController : BaseCourseController
    {
        /// <inheritdoc />
        public override string Name { get; } = "Default";
        
        /// <inheritdoc />
        protected override string PrefabName { get; } = "DefaultCourseController";
        
        /// <inheritdoc />
        public override int Priority { get; } = 50;

        /// <inheritdoc />
        public override List<Type> GetRequiredSetupComponents()
        {
            return new List<Type> { typeof(SpectatorController) };
        }
    }
}