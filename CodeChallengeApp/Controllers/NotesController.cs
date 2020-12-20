using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NotesDataAccess;

namespace CodeChallengeApp.Controllers
{

    /* 
     * Main controller that contains all methods for 
     * note managment.
     */

    public class NotesController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /*
         * Get method returns all notes from databse.
         */

        public IEnumerable<Notes> Get()
        {
            try
            {
                using (NotesDataEntities entities = new NotesDataEntities())
                {
                    return entities.Notes.ToList();
                }
            }
            catch (Exception e)
            {
                log.Debug(e);
                return null;
            }
        }

        /*
         * Get(id) method returns signel note from database based on her id.
         */

        public Notes Get(int id)
        {
            try
            {
                using (NotesDataEntities entites = new NotesDataEntities())
                {
                    return entites.Notes.FirstOrDefault(e => e.id == id);
                }
            }
            catch (Exception e)
            {
                log.Debug(e);
                return null;
            }
        }

        /*
         * Delete method delets note from database based on her id!
         */

        public IHttpActionResult Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest("Note id must be greater than 0!");
            }
            try
            {
                using (var entities = new NotesDataEntities())
                {
                    var entity = entities.Notes
                        .Where(e => e.id == id)
                        .FirstOrDefault();
                    if (entity != null)
                    {
                        entities.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                        entities.SaveChanges();
                    }
                    else
                    {
                        return BadRequest("There is no note with given id!");
                    }
                }
                return Ok();
            }
            catch (Exception e)
            {
                log.Debug(e);
                return BadRequest("Deleting the note failed!");
            }
        }

        /*
         * Post method sends note in body which is than saved in database as new note.
         */
        public IHttpActionResult Post([FromBody] string note)
        {
            try
            {
                using (var entities = new NotesDataEntities())
                {
                    var entity = new Notes()
                    {
                        note = note
                    };

                    entities.Notes.Add(entity);
                    entities.SaveChanges();
                }

                return Ok();
            }
            catch (Exception e)
            {
                log.Debug(e);
                return BadRequest("Adding new note failed!");
            }
        }

        /*
         * Put method is taking id of note from uri and note from body to update existing note in databse
         */

        public IHttpActionResult Put([FromUri] int id, [FromBody] string note)
        {
            try
            {
                using (var entities = new NotesDataEntities())
                {
                    var entity = entities.Notes
                        .Where(e => e.id == id)
                        .FirstOrDefault();
                    if (entity != null)
                    {
                        entity.note = note;
                        entities.SaveChanges();
                    }
                    else
                    {
                        return BadRequest("There is no note with given id!");
                    }
                }
                return Ok();
            }
            catch (Exception e)
            {
                log.Debug(e);
                return BadRequest("Updating note failed!");
            }
        }
    }
}
