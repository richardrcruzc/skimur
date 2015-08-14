using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Infrastructure.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using Skimur;
using Skimur.Tests;
using Subs.ReadModel;
using Subs.Services;

namespace Subs.Tests
{
    [TestFixture]
    public class FlairBuilderTests : DataTestBase
    {
         IFlairService  _flairService;
       
        [Test]
        public void Can_build_Flair()
        {
            var flair = new Flair();
            flair.Id = GuidUtil.NewSequentialId();
            flair.Text = "Class name:";
            flair.CssClass = "Class name:";
            flair.TextEditable = true;
            flair.FlairType = FlairType.Link;
            flair.UserName = "test1";
            flair.Deleted = false;
            flair.DateEdited = Common.CurrentTime();
            flair.DateCreated = Common.CurrentTime();
            flair.IpAddress = "127.0.0.1";

            _flairService.InsertFlair(flair);

            var user1 = _flairService.GetAllFlairsForUser("test1");           


            Assert.That(user1.Count, Has.Count.EqualTo(51));
         
           
        }
         

        protected override void Setup()
        {
            base.Setup();
            _flairService = _container.GetInstance<IFlairService>();
        }
        protected override List<IRegistrar> GetRegistrars()
        {
            var result = base.GetRegistrars();

            // ReSharper disable RedundantNameQualifier
            result.Add(new Subs.Registrar());
            // ReSharper restore RedundantNameQualifier

            return result;
        }


   

        }
}
