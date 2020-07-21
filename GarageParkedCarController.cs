using CodeDebate.Samples.SpoAccessWebApi.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;

namespace CodeDebate.Samples.SpoAccessWebApi.Controllers
{
    public class GarageParkedCarController : ApiController
    {
        // GET: api/GarageParkedCar
        public IEnumerable<GarageParkedCar> Get()
        {
            // The collection we will use to store and return 
            // all the records coming back from the SharePoint Online Custom List
            var response = new List<GarageParkedCar>();

            // Get the URL to the SharePoint Online site
            var webUri = 
                new Uri(
                    ConfigurationManager.AppSettings["WebUri"]);

            // Get the access token. The web toolkit will do all the work
            // for you. Remember, you will need ClientId and ClientSecret in the Web.config
            var realm = TokenHelper.GetRealmFromTargetUrl(webUri);
            var accessToken = TokenHelper.GetAppOnlyAccessToken(
                TokenHelper.SharePointPrincipal,
                webUri.Authority, realm).AccessToken;

            // Initialize the SharePoint Online access context 
            var context = TokenHelper.GetClientContextWithAccessToken(
                webUri.ToString(), accessToken);

            // Create an object to access the SharePoint Online Custom List
            var garageParkedCarsList = 
                context.Web.Lists.GetByTitle(
                    ConfigurationManager.AppSettings["ListTitle"]);

            // Create a new query to filter the list items. As we are looking to 
            // retrieve all items, you can leave the query blank
            var query = new CamlQuery();

            // Create an object to store the list items coming back from 
            // SharePoint Online and execute the query request
            var garageParkedCarsCollection = garageParkedCarsList.GetItems(query);
            context.Load(garageParkedCarsCollection);
            context.ExecuteQuery();

            // Loop all list items coming back, and create a new object from our
            // model for each list item, so we can access and process them
            foreach (var item in garageParkedCarsCollection)
            {
                response.Add(
                    new GarageParkedCar(
                        item["Title"].ToString(), 
                        item["Driver"].ToString(), 
                        item["ParkingSpot"].ToString(), 
                        item["Created"].ToString()));
            }

            // Return the collection back to as the response 
            return response;
        }
    }
}
