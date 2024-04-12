using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ProjectV.Models;

namespace ProjectV.Controllers
{
    [BasicAuthenticationAttribute]
    public class DevicesController : ApiController
    {
        private ProjectVEntities db = new ProjectVEntities();

        // GET: api/Devices
        public IQueryable<Device> GetDevices()
        {
            return db.Devices.Where(x => x.Username == User.Identity.Name);
        }


        // GET: api/Devices/5
        [ResponseType(typeof(Device))]
        public IHttpActionResult GetDevice(short id)
        {
            Device device = db.Devices.Find(id, User.Identity.Name);
            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        // For Update
        // PUT: api/Devices/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDevice(short id, Device device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != device.DeviceID)
            {
                return BadRequest();
            }

            if (device.Username == null)
            {
                device.Username = User.Identity.Name;
            }

            db.Entry(device).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
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
        // POST: api/Devices
        [ResponseType(typeof(Device))]
        public IHttpActionResult PostDevice(Device device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (device.Username == null)
            {
                device.Username = User.Identity.Name;
            }

            db.Devices.Add(device);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = device.DeviceID }, device);
        }


        // For Inserting Value for a Device
        // POST: api/Devices/AddValue
        [Route("api/Devices/AddValue")]
        [ResponseType(typeof(ValueRec))]
        public IHttpActionResult PostValue(ValueRec valueRec)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (valueRec.Username == null)
            {
                valueRec.Username = User.Identity.Name;
            }

            db.ValueRecs.Add(valueRec);
            try
            {
                db.SaveChanges();
                return Ok(valueRec);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        // For Delete
        // DELETE: api/Devices/5
        [ResponseType(typeof(Device))]
        public IHttpActionResult DeleteDevice(short id)
        {
            Device device = db.Devices.Find(id, User.Identity.Name);
            if (device == null)
            {
                return NotFound();
            }

            db.Devices.Remove(device);
            db.SaveChanges();

            return Ok(device);
        }


        // For Deleting value of a Device
        // DELETE: api/Devices/DeleteValue/{id}
        [Route("api/Devices/DeleteValue/{id}")]
        [ResponseType(typeof(ValueRec))]
        public IHttpActionResult DeleteValue(short id)
        {
            ValueRec value = db.ValueRecs.Find(id, User.Identity.Name);
            if (value == null)
            {
                return NotFound();
            }

            db.ValueRecs.Remove(value);
            db.SaveChanges();

            return Ok(value);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeviceExists(short id)
        {
            return db.Devices.Count(e => e.DeviceID == id) > 0;
        }
    }
}