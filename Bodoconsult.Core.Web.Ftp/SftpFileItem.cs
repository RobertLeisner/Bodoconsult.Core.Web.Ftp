using System;
using System.Collections.Generic;
using System.Text;

namespace Bodoconsult.Core.Web.Ftp
{

    /// <summary>
    /// Represents a file or directory on the STFP server
    /// </summary>
    public class SftpFileItem
    {

        /// <summary>Gets the full path of the directory or file.</summary>
        public string FullName { get; set; }

        /// <summary>
        /// For files, gets the name of the file. For directories, gets the name of the last directory in the hierarchy if a hierarchy exists.
        /// Otherwise, the Name property gets the name of the directory.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets a value indicating whether file represents a directory.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if file represents a directory; otherwise, <c>false</c>.
        /// </value>
        public bool IsDirectory { get; set; }

        /// <summary>
        /// Gets or sets the time the current file or directory was last accessed.
        /// </summary>
        /// <value>
        /// The time that the current file or directory was last accessed.
        /// </value>
        public DateTime LastAccessTime { get; set; }


        /// <summary>
        /// Gets or sets the time when the current file or directory was last written to.
        /// </summary>
        /// <value>The time the current file was last written.</value>
        public DateTime LastWriteTime { get; set; }


        /// <summary>
        /// Gets or sets the time the current file or directory was last accessed.
        /// </summary>
        /// <value>
        /// The time that the current file or directory was last accessed.
        /// </value>
        public DateTime LastAccessTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the time when the current file or directory was last written to.
        /// </summary>
        /// <value>The time the current file was last written.</value>
        public DateTime LastWriteTimeUtc { get; set; }

        /// <summary>Gets or sets the size, in bytes, of the current file.</summary>
        /// <value>The size of the current file in bytes.</value>
        public long Length { get; set; }
    }
}
