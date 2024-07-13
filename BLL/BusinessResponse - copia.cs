using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BusinessResponse<T>
    {
        public bool Ok { get; set; }
        public string Mensaje { get; set; }
        public T Data { get; set; }

        public BusinessResponse(bool ok, T data, string mensaje = "")
        {
            this.Ok = ok;
            this.Data = data;
            this.Mensaje = mensaje;
        }
    }
}
