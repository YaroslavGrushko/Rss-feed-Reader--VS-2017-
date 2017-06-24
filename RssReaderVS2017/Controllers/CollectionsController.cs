using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RssReaderVS2017.Controllers
{
    public class CollectionsController : ApiController
    {
        // GET: api/Collections
        public IEnumerable<string> Get()
        {
            IEnumerable<String> myColList = RssReaderVS2017.Models.RssReader.GetRssCollections();
            return myColList;            
        }

        // GET: api/Collections/5
        public string Get(int id)
        {
            return "value";
        }

        //// POST: api/Collections
        //public void Post([FromBody]string value)
        //{
        //}
        //удаление коллекции:
        // POST: api/Collections
        public void Post([FromBody]Coll value)
        {
            //Db:
            //drop table:
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
                using (SqlCommand command = new SqlCommand("DROP TABLE " + value.ColName, con))
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

        // PUT: api/Collections/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Collections/5
        public void Delete(int id)
        {
        }
    }
}
