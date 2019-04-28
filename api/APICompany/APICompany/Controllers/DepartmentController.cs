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
    public class DepartmentController : ApiController
    {
        public IEnumerable<Department> GetListDepartment()
        {
            return Sql.GetDepartments();
        }

        public IHttpActionResult GetListDepartment(int id)
        {
            var department = Sql.GetDepartments().FirstOrDefault((d) => d.Id == id);
            if (department == null) return NotFound();
            return Ok(department);
        }

        public HttpResponseMessage PutChangeDepart([FromBody]Department depart)
        {
            var department = Sql.GetDepartments().FirstOrDefault((d) => d.Id == depart.Id);
            if (department == null) return Request.CreateResponse(HttpStatusCode.NoContent);
            department.DepartName = depart.DepartName;
            Sql.ChangeData(department);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage PostAddDepart([FromBody]Department depart)
        {
            if (depart == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
            if(Sql.GetDepartments().FirstOrDefault() != null)
                depart.Id = Sql.GetDepartments().Last().Id + 1;
            else depart.Id = 1;
            Sql.WriteData(depart);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage DeleteDepart(int id)
        {
            if (Sql.GetDepartments().FirstOrDefault((d) => d.Id == id) == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
            Sql.DelData(Sql.GetDepartments().FirstOrDefault((d) => d.Id == id));
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
