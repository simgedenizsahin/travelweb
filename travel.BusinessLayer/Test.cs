using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using travel.DataAccessLayer.EntityFramework;
using travel.Entities;

namespace travel.BusinessLayer
{
    public class Test
    {
        private Repository<travelUser> repo_user = new Repository<travelUser>();
        private Repository<Category> repo_category= new Repository<Category>();
        private Repository<Comment> repo_comment = new Repository<Comment>();
        private Repository<Note> repo_note = new Repository<Note>();

        public Test()
        {
            //DataAccessLayer.DatabaseContext db = new DataAccessLayer.DatabaseContext();
            //db.Categories.ToList();
            List<Category> categories = repo_category.List();
            //List<Category> categories_filtered = repo_category.List(x => x.Id > 5);
        }

        public void InsertTest()
        {
            Repository<travelUser> repo_user = new Repository<travelUser>();
            int result = repo_user.Insert(new travelUser() {
                Name = "aaa",
                Surname = "bbb",
                Email = "simge.deniz.sahin@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "aabb",
                Password = "111",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "aabb"

            });

        }

        public void UpdateTest()
        {
            travelUser user = repo_user.Find(x => x.Username == "aabb");
            if(user != null)
            {
                user.Username = "xxx";
                int result = repo_user.Update(user);
            }
        }

        public void DeleteTest()
        {
            travelUser user = repo_user.Find(x => x.Username == "xxx");

            if(user != null)
            {
               int result = repo_user.Delete(user);
            }
        }

        public void CommentTest()
        {
            travelUser user = repo_user.Find(x => x.Id == 1);
            Note note = repo_note.Find(x => x.Id == 3);

            Comment comment = new Comment()
            {
                Text = "Bu bir testtir.",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUsername = "denizsahin",
                Note = note,
                Owner = user
            };

            repo_comment.Insert(comment);
        }
    }
}
