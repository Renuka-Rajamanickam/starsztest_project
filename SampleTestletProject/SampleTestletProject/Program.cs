using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace SampleTestletProject
{
    
   
        public class Program
        {
            static void Main(string[] args)
            {
                var processTestLet = new ProcessTestLet();

                var result = processTestLet.BuildTestLetQuestionaries();

                result.FinalQuestionariesCollection.ForEach(item =>
                {
                    Console.WriteLine("Printing item details => Value : {0}, Type: {1}", item, item < 10 ? "PreTest" : "Operational");
                });

                Console.ReadLine();
            }


        }

        public interface IProcessTestLet
        {
            TestLet BuildTestLetQuestionaries();
        }

        /// <summary>
        /// Interface way of implementation is required for proper unit testing
        /// </summary>
        public class ProcessTestLet : IProcessTestLet
        {
            public TestLet BuildTestLetQuestionaries()
            {
                var testLet = new TestLet();

                var availablePretests = GetAvailablePretests();
                var availableOperational = GetAvailableOperational();

                //Enumerable.Range(1, 4).ToList().ForEach(item =>
                //{
                //    //this can be re-written in better way to randomly pick two value
                //    testLet.Pretests.Add(availablePretests.IndexOf(new Random().Next(item, availablePretests.Count() - (item + 3)))); 
                //});

                while (testLet.Pretests.Count() != 4) // loops till 4 items added in collection
                {
                    var randomIndexToPick = new Random().Next(0, availablePretests.Count() - 1);
                    if (!testLet.Pretests.Contains(availablePretests[randomIndexToPick]))
                    {
                        testLet.Pretests.Add(availablePretests[randomIndexToPick]);
                    }
                }

                while (testLet.Operational.Count() != 6) // loops till 6 items added in collection
                {
                    var randomIndexToPick = new Random().Next(0, availableOperational.Count() - 1);
                    if (!testLet.Operational.Contains(availableOperational[randomIndexToPick]))
                    {
                        testLet.Operational.Add(availableOperational[randomIndexToPick]);
                    }
                }

                testLet.FinalQuestionariesCollection.AddRange(testLet.Pretests.GetRange(0, 2)); //this force to have first two from pretest collection 
                testLet.FinalQuestionariesCollection.AddRange(testLet.Operational);//this force to have first 6 from operational collection 
                testLet.FinalQuestionariesCollection.AddRange(testLet.Pretests.GetRange(2, 2)); //this force to have last two from pretest collection 

                return testLet;
            }

            private List<int> GetAvailablePretests()
            {
                return Enumerable.Range(1, 10).ToList();
            }

            private List<int> GetAvailableOperational()
            {
                return Enumerable.Range(25, 50).ToList(); // for differentiating range is intentionaly used as 25 through 50.
            }
        }

        public class TestLet
        {
            public TestLet()
            {
                //Initializing to get property instance
                Pretests = new List<int>();
                Operational = new List<int>();
                FinalQuestionariesCollection = new List<int>();
            }
            public List<int> Pretests { get; set; }
            public List<int> Operational { get; set; }

            public List<int> FinalQuestionariesCollection { get; set; }
        }
   
}
