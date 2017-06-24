using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using SimpleFeedReader;
using System.Data.SqlClient;
using System.Data;

namespace RssReaderVS2017.Models
{
    public class RssReader
    {

       
        // public static RSSList GetRssFeed()
        public static List<RSS> GetRssFeed(String _blogURL)
        {
            List<RSS> RSSitems = new List<RSS>();
            RSSList myRSS = new RSSList();
            var reader = new FeedReader();

            

                    var items = reader.RetrieveFeed(_blogURL);//("http://www.nytimes.com/services/xml/rss/nyt/International.xml");

            foreach (var i in items)
            {
                

                RSSitems.Add(new Models.RSS(i.Uri.ToString(), i.Date.ToString(), i.Title, i.Summary));
            }
          
           
            return RSSitems;

        }
        public static List<RSS> GetAllRssFeeds(String CollectionName)
        {
            List<RSS> RSSitems = new List<RSS>();
            RSSList myRSS = new RSSList();

            List<String> Urls = new List<string>();

            var reader = new FeedReader();
            
            Urls = GetCollectionUrls(CollectionName);

            List<List<RSS>> NotSortedFeedList = new List<List<RSS>>();
            
            foreach (var feedurl in Urls)
            {
                List<RSS> Temp = new List<RSS>();
                var items = reader.RetrieveFeed(feedurl);//("http://www.nytimes.com/services/xml/rss/nyt/International.xml");

                foreach (var i in items)
                {
                  

                    Temp.Add(new Models.RSS(i.Uri.ToString(), i.Date.ToString(), i.Title, i.Summary));
                }
                NotSortedFeedList.Add(Temp);
                
            }
            int max = 0;//максимальное количество постов в фиде
            foreach(var feed in NotSortedFeedList)
            {
                int i = 0;
                foreach(var post in feed) { i++; }
                if (i > max) max = i;
            }
            //-----------------
            //начальное значение Pstop (2) - это количество постов от одного фида в группке:
            int Pstop = 2; int Pstart = 0;//индексы первого и последнего постов в группке

            for (Pstop = 2; Pstop < max; Pstop+=2)
            {
                foreach (var feed in NotSortedFeedList)
                {
                    for (int post_i = Pstart; post_i < Pstop; post_i++)
                    {try
                        {
                            if (feed[post_i] != null) RSSitems.Add(feed[post_i]);
                        } catch (Exception) { };
                    }
                }
                Pstart+=2;
            }

            return RSSitems;

        }
        public static List<String> GetRssCollections()
        {
            List<string> TableNames = new List<string>();

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
                // SqlCommand cmd = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = 'dbName'", con);
                DataTable schema = con.GetSchema("Tables");
                foreach (DataRow row in schema.Rows)
                {
                    TableNames.Add(row[2].ToString());
                }

                


                }
                catch (Exception ex)
                {
                }
            }
            //------------------------------------------------------

            return TableNames;
        }
        public static List<String> GetCollectionUrls(string Collection)
        {
            
            List<string> Urls = new List<string>();
            // your data table
            DataTable dataTable = new DataTable();
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
                 SqlCommand cmd = new SqlCommand("SELECT feedUrl FROM " + Collection, con);
                // create data adapter
                using (SqlDataAdapter da = new SqlDataAdapter(cmd)) { 
                    // this will query your database and return the result to your datatable
                    da.Fill(dataTable);
                
                
                //DataTable schema = con.GetSchema("Tables");
                foreach (DataRow row in dataTable.Rows)
                {
                    Urls.Add(row[0].ToString());
                }
                                                                      }



                }
                catch (Exception ex)
                {
                }
            }
            //------------------------------------------------------

            return Urls;
        }
        public static void DeleteUrl(FeedUrl feedUrl)
        {
            //Db:
            //delete url from table:
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

                using (SqlCommand command = new SqlCommand("DELETE FROM " + feedUrl.col.ToString() + " WHERE feedUrl= '" + feedUrl.urlValue.ToString() + "';", con))

                    command.ExecuteNonQuery();

            }
        
                catch (Exception ex)
            {
            }

        }

        }
    }
}