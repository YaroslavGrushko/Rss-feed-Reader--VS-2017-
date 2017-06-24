using RssReaderVS2017.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace RssReaderVS2017.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        
            // GET api/values
            //public IEnumerable<string> Get()
            //{
            //    return new string[] { "value1", "value2" };
            //}



            // POST api/values/colPost
            [ActionName("colPost")]
            public void Post([FromBody]Coll value)
            {
                //Db:
                //------------------------------------------------------
                string conStr =
        System.Configuration.ConfigurationManager.
        ConnectionStrings["Model1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    try
                    {
                        //
                        // Open the SqlConnection.
                        //
                        con.Open();
                        //
                        // The following code uses an SqlCommand based on the SqlConnection.
                        //
                        using (SqlCommand command = new SqlCommand("CREATE TABLE " + value.ColName + "(feedID int NOT NULL IDENTITY(1, 1), feedUrl varchar(50));", con))
                            command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                    }

                }
            }
            public class Coll
            {
                public string Col { get; set; }
                public string ColName { get; set; }
            }



            // PUT api/values/5
            public void Put(int id, [FromBody]string value)
            {
            }

            // DELETE api/values/5
            public void Delete(int id)
            {
            }
        }

        public class FeedController : ApiController
        {
            //add url to Collection:
            //POST api/Feed       
            public void Post([FromBody]FeedUrl value)
            {
                //Db:
                //------------------------------------------------------
                string conStr =
        System.Configuration.ConfigurationManager.
        ConnectionStrings["Model1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    try
                    {
                        //
                        // Open the SqlConnection.
                        //
                        con.Open();
                        //
                        // The following code uses an SqlCommand based on the SqlConnection.
                        //


                        using (SqlCommand command = new SqlCommand("INSERT INTO " + value.col + "(feedUrl) VALUES('" + value.urlValue + "');", con))
                            command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                //---------------------------------------------------------
                string a1 = value.col;

                string a2 = value.urlValue;
            }
            public class FeedUrl
            {
                public string col { get; set; }
                public string urlValue { get; set; }
            }
        }
        public class UrlController : ApiController
        {
            //Get Collection Urls: 
            // POST api/Url/
            public List<String> Post([FromBody]Coll value)
            {
                List<String> UrlsList = RssReaderVS2017.Models.RssReader.GetCollectionUrls(value.ColName);
                return UrlsList;
            }

            public class Coll
            {
                public string Col { get; set; }
                public string ColName { get; set; }
            }


        }
        public class DeleteUrlController : ApiController
        {
            //Delete the url from table value.col
            // POST api/DeleteUrl/
            [HttpPost]
            public void PostUrl([FromBody]FeedUrl value)
            {
                RssReaderVS2017.Models.RssReader.DeleteUrl(value);

            }
        }
        public class AddFeedController : ApiController
        {
            //Add Rss :
            // POST: api/AddFeed
            public List<RSS> Post([FromBody]FeedUrl value)
            {



                //System.Threading.Thread.Sleep(1000);

                List<RSS> RSSList = RssReaderVS2017.Models.RssReader.GetRssFeed(value.urlValue);


                // return RSSList.Skip(Math.Max(0, RSSList.Count() - LastN)).ToList();
                //выбераем первые 5 постов:
                return RSSList.Take(5).ToList();


            }
        }
        public class AddAllFeedsController : ApiController
        {
            //Add Rss :
            // POST: api/AddAllFeeds
            public List<RSS> Post([FromBody]Coll value)
            {



                //System.Threading.Thread.Sleep(1000);

                List<RSS> RSSList = RssReaderVS2017.Models.RssReader.GetAllRssFeeds(value.ColName);

                //Thread.Sleep(10000);

                // return RSSList.Skip(Math.Max(0, RSSList.Count() - LastN)).ToList();
                //выбераем первые 5 постов:
                return RSSList.Take(5).ToList();


            }
            public class Coll
            {
                public string Col { get; set; }
                public string ColName { get; set; }
            }

        }
    }

