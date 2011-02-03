// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009CreateHostInstance.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009CreateHostInstance type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    using Microsoft.Build.Framework;

    /// <summary>
    /// Bts2009CreateHostInstance Class.
    /// </summary>
    public class Bts2009CreateHostInstance : BizTalkHostTask
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the server.
        /// </summary>
        [Required]
        public string ServerName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [Required]
        public string UserName
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
            Log.LogMessage("Creating Host Instance for Server '{0}', Host '{1}' and Username '{2}'.", this.ServerName, this.HostName, this.UserName);
            Host.CreateInstance(this.ServerName, this.HostName, this.UserName, this.Password);
            return true;
        }

        #endregion // Methods
    }
}