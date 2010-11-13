//-------------------------------------------------------------------------------
// <copyright file="MoviesMetadata.cs" company="bbv Software Services AG">
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

namespace SampleApplication.Models.Movie
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The movie model
    /// </summary>
    [MetadataType(typeof(MovieMetadata))]
    public partial class Movies
    {
        /// <summary>
        /// Metadata for the movie model.
        /// </summary>
        public class MovieMetadata
        {
            /// <summary>
            /// Gets or sets the title.
            /// </summary>
            /// <value>The title.</value>
            [Required(ErrorMessage = "Titles are required")]
            public string Title { get; set; }

            /// <summary>
            /// Gets or sets the price.
            /// </summary>
            /// <value>The price.</value>
            [Required(ErrorMessage = "The Price is required.")]
            [PriceRange(ErrorMessage = "Movies cost between ${0} and ${1}.")]
            public decimal Price { get; set; }

            /// <summary>
            /// Gets or sets the genre.
            /// </summary>
            /// <value>The genre.</value>
            public string Genre { get; set; }
        }
    }
}