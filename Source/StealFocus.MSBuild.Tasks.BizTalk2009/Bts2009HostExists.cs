// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009HostExists.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009HostExists type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    using Microsoft.Build.Framework;

    /// <summary>
    /// Bts2009HostExists Class.
    /// </summary>
    public class Bts2009HostExists : BizTalkHostTask
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Bts2009HostExists"/> is exists.
        /// </summary>
        [Output]
        public bool Exists
        {
            get;
            set;
        }

        #endregion // Properties

        #region Methods

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>Indicating if the task was successful.</returns>
        public override bool Execute()
        {
            this.Exists = Host.Exists(HostName);
            return true;
        }

        #endregion // Methods
    }
}