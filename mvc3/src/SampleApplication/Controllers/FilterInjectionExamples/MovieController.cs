//-------------------------------------------------------------------------------
// <copyright file="MovieController.cs" company="bbv Software Services AG">
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

namespace SampleApplication.Controllers.FilterInjectionExamples
{
    using System.Linq;
    using System.Web.Mvc;
    using SampleApplication.Models.Movie;

    /// <summary>
    /// Controllre for the movie views.
    /// </summary>
    public class MovieController : Controller
    {
        /// <summary>
        /// The enities for movies.
        /// </summary>
        private readonly MoviesEntities moviesEntities;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieController"/> class.
        /// </summary>
        /// <param name="moviesEntities">The movies entities.</param>
        public MovieController(MoviesEntities moviesEntities)
        {
            this.moviesEntities = moviesEntities;
        }

        /// <summary>
        /// Shows the movie index page.
        /// </summary>
        /// <returns>The movie index view.</returns>
        [Cache(0, 0, 5, 0)]
        public ActionResult Index()
        {
            return this.View(this.moviesEntities.Movies.ToList());
        }

        /// <summary>
        /// The create action for movies.
        /// </summary>
        /// <returns>The create view</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// The create post action.
        /// Creates a movie
        /// </summary>
        /// <param name="newMovie">The new movie.</param>
        /// <returns>The index page if succesful the create view</returns>
        [HttpPost]
        [ClearCacheOnSuccess("Index")]
        public ActionResult Create(Movies newMovie)
        {
            if (ModelState.IsValid)
            {
                this.moviesEntities.AddToMovies(newMovie);
                this.moviesEntities.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(newMovie);
        }
    }
}
