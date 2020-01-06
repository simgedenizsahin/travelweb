using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using travel.Entities;

namespace travel.DataAccessLayer.EntityFramework
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            travelUser admin = new travelUser()
            {
                Name = "Deniz",
                Surname = "Sahin",
                Email = "simge.deniz.sahin@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "denizsahin",
                ProfileImageFilename = "user.jpg",
                Password = "123456",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "denizsahin"

            };

            travelUser standartUser = new travelUser()
            {
                Name = "Sarp",
                Surname = "Erdem",
                Email = "simge.deniz.sahin@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "sarperdem",
                ProfileImageFilename = "user.jpg",
                Password = "654321",
                CreatedOn = DateTime.Now.AddHours(1),
                ModifiedOn = DateTime.Now.AddMinutes(65),
                ModifiedUsername = "sarperdem"
            };
            context.travelUsers.Add(admin);
            context.travelUsers.Add(standartUser);

            for (int i = 0; i < 8; i++)
            {
                travelUser user = new travelUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ProfileImageFilename = "user.jpg",
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    Username = $"user{i}",
                    Password = "123",
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUsername = $"user{i}"
                };

                context.travelUsers.Add(user);
            }
            context.SaveChanges();

            //user list for using...
            List<travelUser> userlist = context.travelUsers.ToList();

            //adding fake categories...
            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUsername = "denizsahin",
                };

                context.Categories.Add(cat);

                //adding fake notes...
                for (int k = 0; k < FakeData.NumberData.GetNumber(5, 9); k++)
                {
                    travelUser owner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];
                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1, 9),
                        Owner = userlist[FakeData.NumberData.GetNumber(0,userlist.Count-1)],
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUsername = owner.Username,
                    };

                    cat.Notes.Add(note);

                    //adding fake comments
                    for (int j = 0; j < FakeData.NumberData.GetNumber(3, 5); j++)
                    {
                        travelUser comment_owner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];

                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                            Owner = comment_owner,
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUsername = comment_owner.Username,
                        };

                        note.Comments.Add(comment);
                    }

                    //adding fake likes...

                    for (int m = 0; m < note.LikeCount; m++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedUser = userlist[m]
                        };
                        note.Likes.Add(liked);
                    }

                }
            }

            context.SaveChanges();
        }
    }
}
