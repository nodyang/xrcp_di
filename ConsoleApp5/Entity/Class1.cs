using System;
using System.Threading.Tasks;
using EventNext;

namespace Entity
{
    public interface IHelloWorld
    {
        Task<string> Hello();
    }
    
    [Service(typeof(IHelloWorld))]
    public class HelloWorldService : IHelloWorld
    {
        private Stu stu;
        public HelloWorldService(Stu stu)
        {
            this.stu = stu;
        }
        
        public Task<string> Hello()
        {
            return $"Hello {stu.Name} {stu.Dt}".ToTask();
        }
    }
    public class Stu
    {
        public String Name { get; set; }
        public DateTime Dt { get; set; }

        public Stu()
        {
            this.Name = "yagn";
            this.Dt = DateTime.Now;
        }
    }
}
