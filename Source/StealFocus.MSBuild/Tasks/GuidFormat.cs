// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GuidFormat.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the GuidFormat type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StealFocus.MSBuild.Tasks
{
    using System;

    /// <summary>
    /// GuidFormat Enumeration.
    /// </summary>
    [Serializable]
    public enum GuidFormat
    {
        /// <summary>
        /// For example 'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx'.
        /// </summary>
        Digits = 0,

        /// <summary>
        /// For example 'xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx'.
        /// </summary>
        DigitsHyphens = 1,

        /// <summary>
        /// For example '{xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}'.
        /// </summary>
        DigitsHyphensBrackets = 2,

        /// <summary>
        /// For example '(xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)'.
        /// </summary>
        DigitsHyphensParentheses = 3
    }
}
