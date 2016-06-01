using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

using JetRx.Entities;
using JetRx.Api.Controllers;

namespace JetRx.Api.Tests.Controllers
{
    /// <summary>
    /// Summary description for PrescriptionControllerTest
    /// </summary>
    [TestClass]
    public class PrescriptionControllerTest
    {
        public PrescriptionControllerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ShouldUploadPrescription()
        {

            var client = new RestClient("http://jetrx-dev-central.azurewebsites.net/");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("Api/Prescription", Method.POST);
            
            // request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

            // easily add HTTP Headers
            //request.AddHeader("header", "value");

            // add files to upload (works with compatible verbs)
            request.AddFile("image", @"C:\Projects\JetRx\JetRx\JetRx.Api.Tests\Controllers\drivers license 001.jpg");

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            RestResponse<response<prescription>> response2 = (RestResponse<response<prescription>>)client.Execute<response<prescription>>(request);
             var p = response2.Data;
        }
    }
}
