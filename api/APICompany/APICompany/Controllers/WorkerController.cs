using APICompany.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APICompany.Controllers
{
    public class WorkerController : ApiController
    {
        public IEnumerable<Worker> GetListWorker()
        {
            return Sql.GetWorkers();
        }

        public IHttpActionResult GetListWorker(int id)
        {
            var worker = Sql.GetWorkers().FirstOrDefault((w) => w.Id == id);
            if (worker == null) return NotFound();
            return Ok(worker);
        }

        public HttpResponseMessage PostAddWorker([FromBody]Worker worker)
        {
            if (worker == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
            if (Sql.GetWorkers().FirstOrDefault() != null)
                worker.Id = Sql.GetWorkers().Last().Id + 1;
            else worker.Id = 1;
            Sql.WriteData(worker);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage DeleteWorker(int id)
        {
            if (Sql.GetWorkers().FirstOrDefault((d) => d.Id == id) == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
            Sql.DelData(Sql.GetWorkers().FirstOrDefault((d) => d.Id == id));
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage PutWorker([FromBody]Worker worker)
        {
            var wrk = Sql.GetWorkers().FirstOrDefault((w) => w.Id == worker.Id);
            if (wrk == null) return Request.CreateResponse(HttpStatusCode.NoContent);
            wrk.Name = worker.Name;
            wrk.SecondName = worker.SecondName;
            wrk.SurName = worker.SurName;
            wrk.Department = worker.Department;
            Sql.ChangeData(wrk);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
