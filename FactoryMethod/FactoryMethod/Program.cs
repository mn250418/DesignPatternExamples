using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    interface IInterviewer
    {
        void AskQuestions();
    }

    class Developer : IInterviewer
    {
        public void AskQuestions()
        {
            Console.WriteLine("Asking about design patterns!");
        }
    }

    class CommunityExecutive : IInterviewer
    {
        public void AskQuestions()
        {
            Console.WriteLine("Asking about community building!");
        }
    }

    abstract class HiringManager
    {
        // Factory method
        abstract protected IInterviewer MakeInterviewer();
        public void TakeInterview()
        {
            var interviewer = this.MakeInterviewer();
            interviewer.AskQuestions();
        }
    }

    class DevelopmentManager : HiringManager
    {
        protected override IInterviewer MakeInterviewer()
        {
            return new Developer();
        }
    }

    class MarketingManager : HiringManager
    {
        protected override IInterviewer MakeInterviewer()
        {
            return new CommunityExecutive();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var devManager = new DevelopmentManager();
            devManager.TakeInterview();

            var marketingManager = new MarketingManager();
            marketingManager.TakeInterview();

            Console.ReadLine();
        }
    }
}
