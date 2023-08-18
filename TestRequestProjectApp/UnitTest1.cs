using Business.Abstract;
using Moq;

namespace TestRequestProjectApp
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var mock = new Mock<IProductService>();
            //mock.Setup(i => i(It.IsAny<string>())).Returns(true);

            //var evo = new ApplicationEvaluator(mock.Object);
            //var form = new JobApplication()
            //{
            //    Applicant = new Applicant()
            //    {
            //        Age = 16, // ?
            //        IdentityNumber = "" // OK
            //    },
            //    TechStackList = new() { "C#", ".NET", "RabbitMQ", "Visual Studio" } // OK
            //};



            //var applicationResult = evo.Evaluate(form);



            //Assert.AreEqual(ApplicationResult.AutoRejected, applicationResult);


            Assert.Pass();
        }
    }
}