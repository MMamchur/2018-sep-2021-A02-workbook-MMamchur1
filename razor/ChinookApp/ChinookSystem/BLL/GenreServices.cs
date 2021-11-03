using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Models;
using ChinookSystem.DAL;
#endregion

namespace ChinookSystem.BLL
{
    public class GenreServices
    {
        #region Constructor andDI variable setup
        private readonly ChinookContext _context;

        internal GenreServices(ChinookContext context)
        {
            _context = context;
        }
        #endregion

        #region Queries
        public List<SelectionList> GenreList()
        {
            List<SelectionList> items = _context.Genres
                    .Select(x => new SelectionList
                    {
                        ValueField = x.GenreId,
                        DisplayField = x.Name
                    })
                    .OrderBy(x => x.DisplayField)
                    .ToList();
            return items;
        }
        #endregion
    }
}
