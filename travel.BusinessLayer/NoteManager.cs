using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using travel.DataAccessLayer.EntityFramework;
using travel.Entities;

namespace travel.BusinessLayer
{
    public class NoteManager
    {
        private Repository<Note> repo_note = new Repository<Note>();
        public List<Note> GetAllNote()
        {
            return repo_note.List();
        }
    }
}
