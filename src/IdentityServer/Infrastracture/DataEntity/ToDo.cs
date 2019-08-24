using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Infrastracture.DataEntity
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public int Priority { get; set; }
        public ToDo(int id, string title, bool isDone, int priority)
        {
            Id = id;
            Title = title;
            IsDone = isDone;
            Priority = priority;
        }
    }
}
