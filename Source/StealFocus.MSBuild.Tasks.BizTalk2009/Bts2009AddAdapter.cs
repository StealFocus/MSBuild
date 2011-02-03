// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009AddAdapter.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009AddAdapter type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using System;

    using Core.BizTalk2009;

    using Microsoft.Build.Framework;

    /// <summary>
    /// Bts2009AddAdapter Class.
    /// </summary>
    public class Bts2009AddAdapter : BizTalkAdapterTask
    {
        #region Properties

        /// <summary>
        /// Gets or sets the management class id.
        /// </summary>
        [Required]
        public string ManagementClassId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        public string Comment
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
            Guid mgmtClsId = new Guid(this.ManagementClassId);
            string[] existingAdapterNames = Adapter.GetAdapters();
            bool addAdapter = true;
            foreach (string existingAdapterName in existingAdapterNames)
            {
                if (AdapterName == existingAdapterName)
                {
                    // if we find an existing adapter with the same name...
                    addAdapter = false;

                    // ...check its GUID
                    Guid existingAdapterManagementClassId = Adapter.GetManagementClassId(existingAdapterName);

                    // If the existing adapter's GUID does not match the supplied one then remove the existing adapter
                    if (string.Compare(this.ManagementClassId, existingAdapterManagementClassId.ToString(), StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        Log.LogMessage("Deleting existing Adapter named '{0}'.", AdapterName);
                        Adapter.Delete(AdapterName);
                        addAdapter = true;
                        break;
                    }
                }
            }

            if (addAdapter)
            {
                Log.LogMessage("Adding Adapter named '{0}'.", AdapterName);
                Adapter.Add(AdapterName, mgmtClsId, this.Comment);
            }
            else
            {
                Log.LogMessage("Adapter already found with name '{0}' and Management Class ID '{1}'.", AdapterName, this.ManagementClassId);
            }

            return true;
        }

        #endregion // Methods
    }
}