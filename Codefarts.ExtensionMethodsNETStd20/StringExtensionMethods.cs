// <copyright>
//   Copyright (c) 2012 Codefarts
//   All rights reserved.
//   contact@codefarts.com
//   http://www.codefarts.com
// </copyright>

using System.Collections;

namespace System
{
#if !PORTABLE
    using System.IO;
#endif
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides helper methods for working with strings.
    /// </summary>
    public static class StringExtensionMethods
    {
#if !PORTABLE
        /// <summary>
        /// Checks if a path contains a parent folder with a specific name.
        /// </summary>
        /// <param name="path">
        /// The path to check.
        /// </param>
        /// <param name="folderName">The name of the folder to check for.</param>
        /// <returns>
        /// Returns true if the Path contains the folder; otherwise false.
        /// </returns>
        /// <remarks><see cref="StringComparison.CurrentCulture"/> is used for string comparisons.</remarks>
        public static bool PathContainsFolder(this string path, string folderName)
        {
            return PathContainsFolder(path, folderName, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Checks if a path contains a parent folder with a specific name.
        /// </summary>
        /// <param name="path">
        /// The path to check.
        /// </param>
        /// <param name="folderName">The name of the folder to check for.</param>
        /// <param name="options">The string comparison options to use when comparing folder names.</param>
        /// <returns>
        /// Returns true if the Path contains a the folder; otherwise false.
        /// </returns>
        public static bool PathContainsFolder(this string path, string folderName, StringComparison options)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            var parent = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);
            foreach (var part in parent)
            {
                if (!string.IsNullOrEmpty(part) && part.Equals(folderName, options))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns the index of a folder given a path.
        /// </summary>
        /// <param name="path">
        /// The path to check.
        /// </param>
        /// <param name="folderName">The name of the folder to check for.</param>
        /// <returns>
        /// Returns the index of the folder; otherwise -1 if no folder found.
        /// </returns>
        /// <remarks><see cref="StringComparison.CurrentCulture"/> is used for string comparisons.</remarks>
        public static int IndexOfFolderInPath(this string path, string folderName)
        {
            return IndexOfFolderInPath(path, folderName, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Returns the index of a folder given a path.
        /// </summary>
        /// <param name="path">
        /// The path to check.
        /// </param>
        /// <param name="folderName">The name of the folder to check for.</param>
        /// <param name="options">The string comparison options to use when comparing folder names.</param>
        /// <returns>
        /// Returns the index of the folder; otherwise -1 if no folder found.
        /// </returns>
        /// <remarks>If a given path is missing a folder it will be ignored.</remarks>
        public static int IndexOfFolderInPath(this string path, string folderName, StringComparison options)
        {
            if (string.IsNullOrEmpty(path))
            {
                return -1;
            }

            var separatorChar = Path.DirectorySeparatorChar;
            var modifiedPath = path.Replace(Path.AltDirectorySeparatorChar, separatorChar).RemoveCharactersFromStart(separatorChar);
            if (string.IsNullOrEmpty(modifiedPath))
            {
                return -1;
            }

            var parent = modifiedPath.Split(separatorChar);
            var index = 0;
            for (var i = 0; i < parent.Length; i++)
            {
                var part = parent[i];
                if (string.IsNullOrEmpty(part))
                {
                    throw new IOException("Path is missing a folder name");
                }

                if (part.Equals(folderName, options))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        /// <summary>
        /// Returns an array of parent folders for a given path.
        /// </summary>
        /// <param name="value">The path value to parse.</param>
        /// <returns>An array of folder names representing the folder path.</returns>
        public static string[] GetParentFolders(this string value)
        {
            return value.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);
        }
#endif

        /// <summary>
        /// Determines weather the beginning of this string instance matches any of the specified strings.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="strings">A enumerable collection of strings to check for.</param>
        /// <returns>Returns true if the string starts with any of the specified strings; otherwise false.</returns>
        public static bool StartsWithAny(this string value, IEnumerable<string> strings)
        {
            foreach (var item in strings)
            {
                if (value.StartsWith(item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines weather the beginning of this string instance matches any of the specified strings.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="strings">A enumerable collection of strings to check for.</param>
        /// <param name="compare">Specifies the type of comparison to use.</param>
        /// <returns>Returns true if the string starts with any of the specified strings; otherwise false.</returns>
        public static bool StartsWithAny(this string value, IEnumerable<string> strings, StringComparison compare)
        {
            foreach (var item in strings)
            {
                if (value.StartsWith(item, compare))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes specified characters from the start of a string.
        /// </summary>
        /// <param name="value">The value to modify.</param>
        /// <returns>An new string with the specified characters removed from the start of the string.</returns>
        public static string RemoveCharactersFromStart(this string value, char chars)
        {
            return RemoveCharactersFromStart(value, new[] { chars });
        }

        /// <summary>
        /// Removes specified characters from the start of a string.
        /// </summary>
        /// <param name="value">The value to modify.</param>
        /// <returns>An new string with the specified characters removed from the start of the string.</returns>
        public static string RemoveCharactersFromStart(this string value, char[] chars)
        {
            if (chars == null || chars.Length == 0)
            {
                return value;
            }

            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var index = 0;
            while (index < value.Length && chars.Contains(value[index]))
            {
                index++;
            }

            return value.Substring(index);
        }

        /// <summary>
        /// Determines weather or not all the characters in a string are all the same.
        /// </summary>
        /// <param name="value">The value to check for.</param>
        /// <returns>true is all characters are the same, otherwise false.</returns>
        public static bool AllTheSame(this string value)
        {
#if UNITY_5 || UNITY_2017 || NET35
            if (!StringExtensionMethods.IsNullOrWhiteSpace(value))
#else
            if (!string.IsNullOrWhiteSpace(value))
#endif
            {
                var clone = new string(value[0], value.Length);
                return clone == value;
            }

            return false;
        }

#if UNITY_5 || UNITY_2017 || NET35
        public static bool IsNullOrWhiteSpace(this string value)
        {
            if (value == null)
            {
                return true;
            }

            for (var i = 0; i < value.Length; i++)
            {
                if (!char.IsWhiteSpace(value[i]))
                {
                    return false;
                }
            }

            return true;
        }
#endif

        /// <summary>
        /// Returns file names from given folder that comply to given filters.
        /// </summary>
        /// <param name="path">Folder with files to retrieve.</param>
        /// <param name="filter">Multiple file filters separated by | character.</param>
        /// <param name="searchOption">File.IO.SearchOption, could be AllDirectories or TopDirectoryOnly</param>
        /// <returns>An enumerable type of file path strings that meet the given filter.</returns>
        public static IEnumerable<string> GetFiles(this string path, string filter, SearchOption searchOption)
        {
            // ArrayList will hold all file names
            var allFiles = new List<string>();

            // Create an array of filter string
            var multipleFilters = string.IsNullOrWhiteSpace(filter) ? Array.Empty<string>() : filter.Split('|');

            // for each filter find matching file names
            foreach (var fileFilter in multipleFilters)
            {
                // add found file names to array list
                allFiles.AddRange(Directory.GetFiles(path, fileFilter, searchOption));
            }

            // returns string array of relevant file names
            return allFiles;
        }

        /// <summary>
        /// Returns an "interpolated" string.
        /// </summary>
        /// <param name="start">The starting string.</param>
        /// <param name="end">The end string.</param>
        /// <param name="percentage">The percentage.</param>
        /// <returns></returns>
        public static string Interpolate(string start, string end, double percentage)
        {
            var strStart = start;
            var strEnd = end;

            // We find the length of the interpolated string...
            var iStartLength = strStart.Length;
            var iEndLength = strEnd.Length;

            // interpolate
            var dDifference = iEndLength - iStartLength;
            var dDistance = dDifference * percentage;
            var iLength = (int)(iStartLength + dDistance);

            var result = new char[iLength];

            // Now we assign the letters of the results by interpolating the
            // letters from the start and end strings...
            for (var i = 0; i < iLength; ++i)
            {
                // We get the start and end chars at this position...
                var cStart = 'a';
                if (i < iStartLength)
                {
                    cStart = strStart[i];
                }
                var cEnd = 'a';
                if (i < iEndLength)
                {
                    cEnd = strEnd[i];
                }

                // We interpolate them...
                char cInterpolated;
                if (cEnd == ' ')
                {
                    // If the end character is a space we just show a space
                    // regardless of interpolation. It looks better this way...
                    cInterpolated = ' ';
                }
                else
                {
                    // The end character is not a space, so we interpolate...
                    var iStart = Convert.ToInt32(cStart);
                    var iEnd = Convert.ToInt32(cEnd);

                    // interpolate
                    dDifference = iEnd - iStart;
                    dDistance = dDifference * percentage;
                    var iInterpolated = (int)(iStart + dDistance);

                    cInterpolated = Convert.ToChar(iInterpolated);
                }

                result[i] = cInterpolated;
            }

            return new string(result);
        }
    }
}
