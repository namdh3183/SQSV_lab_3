using SQSV_lab_3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SQSV_lab_3.Controllers
{
    public class BookController : ApiController
    {
        Model1 db;

        public BookController() // Default constructor
        {
            db = new Model1();
        }

        public BookController(Model1 context) // Additional constructor for unit testing
        {
            db = context;
        }

        // GET api/book
        public IEnumerable<BOOK> Get()
        {
            return db.BOOKs;
        }

        // GET api/book/3
        public BOOK Get(int id)
        {
            return db.BOOKs.Find(id);
        }

        // POST api/book
        public void Post([FromBody] BOOK product)
        {
            db.BOOKs.Add(product);
            db.SaveChanges();
        }

        // PUT api/book/3
        public void Put(int id, [FromBody] BOOK book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
        }


        // DELETE api/book/3
        public void Delete(int id)
        {
            var book = db.BOOKs.Find(id);
            db.BOOKs.Remove(book);
            db.SaveChanges();
        }
    }
}
