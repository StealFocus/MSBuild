// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009CreateHost.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009CreateHost type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using System;

    using Microsoft.Build.Framework;

    /// <summary>
    /// Bts2009CreateHost Class.
    /// </summary>
    public class Bts2009CreateHost : BizTalkHostTask
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the windows group.
        /// </summary>
        /// <value>The name of the windows group.</value>
        /// <remarks/>
        [Required]
        public string WindowsGroupName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Bts2009CreateHost"/> is trusted.
        /// </summary>
        [Required]
        public bool Trusted
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the host.
        /// </summary>
        [Required]
        public string HostType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [host tracking].
        /// </summary>
        [Required]
        public bool HostTracking
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is default.
        /// </summary>
        [Required]
        public bool IsDefault
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
            Core.BizTalk2009.HostType hostType;
            try
            {
                hostType = (Core.BizTalk2009.HostType)Enum.Parse(typeof(Core.BizTalk2009.HostType), this.HostType, false);
            }
            catch (FormatException)
            {
                Log.LogError("The provided Host Type of '{0}' could not be parsed as a valid Host Type. Possible values are '{}' and '{}'.", this.HostType, Microsoft.BizTalk.ExplorerOM.HostType.InProcess, Microsoft.BizTalk.ExplorerOM.HostType.Isolated);
                return false;
            }

            Log.LogMessage("Creating Host '{0}.", HostName);
            Core.BizTalk2009.Host.Create(this.HostName, this.WindowsGroupName, this.Trusted, hostType, this.HostTracking, this.IsDefault);
            return true;
        }

        #endregion // Methods
    }
}