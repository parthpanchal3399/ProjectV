using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Description;
using ProjectV.Models;

namespace ProjectV.Controllers
{
    [BasicAuthenticationAttribute]
    public class ProjectsController : ApiController
    {
        private ProjectVEntities db = new ProjectVEntities();

        /// GET: api/Projects
        public IQueryable<Project> GetProjects()
        {
            return db.Projects.Where(x => x.Username == User.Identity.Name);
        }

        // GET: api/Projects/5
        [ResponseType(typeof(Project))]
        public IHttpActionResult GetProject(short id)
        {
            Project project = db.Projects.Find(id, User.Identity.Name);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // For Update
        // PUT: api/Projects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProject(short id, Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != project.ProjectID)
            {
                return BadRequest();
            }

            if (project.Username == null)
            {
                project.Username = User.Identity.Name;
            }
            db.Entry(project).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // For Insert
        // POST: api/Projects
        [ResponseType(typeof(Project))]
        public IHttpActionResult PostProject(Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(project.Username == null)
            {
                project.Username = User.Identity.Name;
            }
            db.Projects.Add(project);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = project.ProjectID }, project);
        }

        // For Delete
        // DELETE: api/Projects/5
        [ResponseType(typeof(Project))]
        public IHttpActionResult DeleteProject(short id)
        {
            Project project = db.Projects.Find(id, User.Identity.Name);
            if (project == null)
            {
                return NotFound();
            }

            db.Projects.Remove(project);
            db.SaveChanges();

            return Ok(project);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectExists(short id)
        {
            return db.Projects.Count(e => e.ProjectID == id) > 0;
        }
    }
}