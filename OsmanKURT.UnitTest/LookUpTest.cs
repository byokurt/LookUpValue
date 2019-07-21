using NSubstitute;
using NUnit.Framework;
using OsmanKURT.Business;
using OsmanKURT.Cache;
using OsmanKURT.ClientEntites;
using OsmanKURT.Common;
using OsmanKURT.Data.Contracts;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace OsmanKURT.UnitTest
{
    [TestFixture]
    [Category("UnitTests.LookUpValueEngine")]
    public class LookUpTest
    {

        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void GetValue_NameIsEmpty_ThrowException()
        {
            var engine = new LookuUpValueEngine(null, null);

            Assert.That(() => engine.GetValue(new ClientEntites.GetValueRequest()
            {
                Name = "",
                ApplicationName = "ApplicationName",
                RefreshTime = 1500
            }), Throws.TypeOf<ApiException>());

        }

        [Test]
        public void GetValue_ApplicationNameIsEmpty_ThrowException()
        {
            var engine = new LookuUpValueEngine(null, null);

            Assert.That(() => engine.GetValue(new ClientEntites.GetValueRequest()
            {
                Name = "Test",
                ApplicationName = "",
                RefreshTime = 1500
            }), Throws.TypeOf<ApiException>());

        }

        [Test]
        public void GetValue_HappyPath()
        {
            int refreshTime = 1500;
            var request = new GetValueRequest()
            {
                Name = "SiteName",
                ApplicationName = "ServiceA",
                RefreshTime = refreshTime
            };


            var mockLoopUpRepository = Substitute.For<IServiceProvider>();
            var repository = Substitute.For<ILookUpValueRepository>();
            repository.GetValue(request).ReturnsForAnyArgs("Boyner");
            var mockCache = Substitute.For<ICacheManager>();
            mockLoopUpRepository.GetService<ILookUpValueRepository>().Returns(repository);

            var engine = new LookuUpValueEngine(mockLoopUpRepository, mockCache);

            mockCache.ExecuteCached("SiteName-ServiceA", refreshTime, () => repository.GetValue(request)).ReturnsForAnyArgs("Boyner");

            var sut = engine.GetValue(request);

            Assert.AreEqual(sut, "Boyner");

        }

    }

}
