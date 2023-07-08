using DevDynamo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevDynamo.Services
{
    public sealed class App
    {
        internal readonly AppDb db;
        private Lazy<ProjectService> _Projects;

        public App(AppDb db)
        {
            this.db = db;
            //Projects = new ProjectService(this);
            _Projects = new Lazy<ProjectService>(() => new ProjectService(this)) ;
            Tickets = new TicketService(this);
            WorkflowSteps = new WorkflowStepService(this);

        }
        public ProjectService Projects { get => _Projects.Value; }
        public TicketService Tickets { get; set; }
        public WorkflowStepService WorkflowSteps { get; set; }

        public int SaveChanges() => db.SaveChanges();
        public Task<int> SaveChangesAsync() => db.SaveChangesAsync();

        public Func<DateTime> Now { get; private set; } = () => DateTime.Now;
        public void SetNow(DateTime now) => Now = () => now;
        public void ResetNow() => Now = () => DateTime.Now;
        public DateTime Today() => Now().Date;

        //public void SetCurrentUser(Guid id, string username)
        //{
        //    var user = Users.Find(id);
        //    if (user == null)
        //    {
        //        user = new User
        //        {
        //            Id = id,
        //            UserName = username,
        //            CreatedDate = Now(),
        //            Note = null
        //        };
        //        Users.Add(user);
        //        SaveChanges();
        //    }

        //    CurrentUser = user;
        //}

        //public void Throws(AppException ex)
        //{
        //    ex.UserName = CurrentUser?.UserName;

        //    throw ex;
        //}
    }
}
