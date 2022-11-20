using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern
{
    class JobPost
    {
        public string Title { get; private set; }

        public JobPost(string title)
        {
            Title = title;
        }
    }
    class JobSeeker : IObserver<JobPost>
    {
        public string Name { get; private set; }

        public JobSeeker(string name)
        {
            Name = name;
        }

        public void OnCompleted()
        {
            
        }
        public void OnError(Exception error)
        {
            
        }

        public void OnNext(JobPost value)
        {
            Console.WriteLine("Hi {0} ! New job posted: {1}", Name, value.Title);
        }
    }

    class JobPostings : IObservable<JobPost>
    {
        private List<IObserver<JobPost>> mObservers;
        private List<JobPost> mJobPostings;

        public JobPostings()
        {
            mObservers = new List<IObserver<JobPost>>();
            mJobPostings = new List<JobPost>();
        }

        public IDisposable Subscribe(IObserver<JobPost> observer)
        {
            // Check whether observer is already registered. If not, add it
            if (!mObservers.Contains(observer))
            {
                mObservers.Add(observer);
            }
            return new Unsubscriber<JobPost>(mObservers, observer);
        }

        private void Notify(JobPost jobPost)
        {
            foreach (var observer in mObservers)
            {
                observer.OnNext(jobPost);
            }
        }

        public void AddJob(JobPost jobPost)
        {
            mJobPostings.Add(jobPost);
            Notify(jobPost);
        }

    }

    internal class Unsubscriber<JobPost> : IDisposable
    {
        private List<IObserver<JobPost>> mObservers;
        private IObserver<JobPost> mObserver;

        internal Unsubscriber(List<IObserver<JobPost>> observers, IObserver<JobPost> observer)
        {
            this.mObservers = observers;
            this.mObserver = observer;
        }

        public void Dispose()
        {
            if (mObservers.Contains(mObserver))
                mObservers.Remove(mObserver);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Create Subscribers
            var johnDoe = new JobSeeker("John");
            var janeDoe = new JobSeeker("Rick");

            //Create publisher and attch subscribers
            var jobPostings = new JobPostings();
            jobPostings.Subscribe(johnDoe);
            jobPostings.Subscribe(janeDoe);

            //Add a new job and see if subscribers get notified
            jobPostings.AddJob(new JobPost("Software Engineer"));

            Console.ReadLine();
        }
    }
}
