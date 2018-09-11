using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRebuild
{
    /// <summary>
    /// Provides information about the commands entered by the user.
    /// </summary>
    public class PathInfo
    {
        /// <summary>
        /// First Path.
        /// </summary>
        public string FirstPath { get; set; }

        /// <summary>
        /// Second Path.
        /// </summary>
        public string SecondPath { get; set; }

        /// <summary>
        /// Paths entered.
        /// </summary>
        public int PathsNumber { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PathInfo()
        {
        }

        /// <summary>
        /// Constructor with one path.
        /// </summary>
        /// <param name="firstPath">First path introduced.</param>
        public PathInfo(string firstPath)
        {
            this.FirstPath = firstPath;
        }

        /// <summary>
        /// Constructor with two paths.
        /// </summary>
        /// <param name="firstPath">First path introduced.</param>
        /// <param name="secondPath">Second path introduced.</param>
        public PathInfo(string firstPath, string secondPath)
        {
            this.FirstPath = firstPath;
            this.SecondPath = secondPath;
        }
    }
}
