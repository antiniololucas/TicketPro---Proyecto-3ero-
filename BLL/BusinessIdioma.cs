using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BusinessIdioma
    {
        private readonly DAL_Idioma _dataAccessIdioma;

        public BusinessIdioma()
        {
            _dataAccessIdioma = new DAL_Idioma();
        }

        public BusinessResponse<List<EntityIdioma>> GetAll()
        {
            return new BusinessResponse<List<EntityIdioma>>(true, _dataAccessIdioma.SelectAll());
        }

        public BusinessResponse<string> GetTraduccion(EntityIdioma idioma, string NombreControl)
        {
            return new BusinessResponse<string>(true, _dataAccessIdioma.SelectTraduccion(idioma, NombreControl));
        }
    }
}
