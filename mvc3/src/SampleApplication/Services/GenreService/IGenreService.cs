//-------------------------------------------------------------------------------
// <copyright file="IGenreService.cs" company="bbv Software Services AG">
//   Copyright (c) 2010 bbv Software Services AG
//   Author: Remo Gloor (remo.gloor@gmail.com)
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace SampleApplication.Services.GenreService
{
    /// <summary>
    /// A genre service.
    /// </summary>
    public interface IGenreService
    {
        /// <summary>
        /// Checks if given genre is a valid genre.
        /// </summary>
        /// <param name="genre">The genre.</param>
        /// <returns>true if the given genre is valid; otherwise false</returns>
        bool CheckIfValidGenre(string genre);
    }
}