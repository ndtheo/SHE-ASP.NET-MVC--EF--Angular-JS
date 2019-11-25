using Database.Models.DbContext;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache
{
    public class Cache
    {
        protected static readonly ApplicationDbContext db = new ApplicationDbContext();

        public static SelectList _usersSelectList { get; set; }
        public static SelectList UsersSelectList
        {
            get
            {
                if (null == _usersSelectList)
                {
                    _usersSelectList = new SelectList(db.Users.OrderBy(x => x.Name), "Id", "Name");
                }
                return Cache._usersSelectList;
            }
            set
            {
                _usersSelectList = value;
            }
        }
    }
}
