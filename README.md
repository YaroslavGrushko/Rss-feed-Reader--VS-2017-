# Rss feed Reader (VS 2017)  
ASP.NET WebAPI service for providing Rss feeds from different sources. The project is written in the visual studio 2017.  
# How to run the project:  
1. Download project;
2. Go to the folder Rss-feed-Reader--VS-2017-/RssReaderVS2017/App_Data/
3. Change files extension:  
RSSCollections.mdg->RSSCollections.mdf;  
RSSCollections_log.ldg->RSSCollections_log.ldf
4. Run the solution.
# How to use the program:
You can add collaction of rss, just enter collection name and click on the "Add Collection" button. Then you can add feeds to the collection just type feed's url to the corresponding input and click on the "Add Rss Feed" button (The feed will be added to the selected by the radio button collection).  
To display the feed, click on the appropriate feed link ("Feeds of selected collection" column). To display all feeds from collection click on "show all" link (2 posts from each feed are reflected and so in the loop, 5 posts are  in total [The number of displayed posts can be changed by changing in: "Controllers->AddAllFeedsController->Post->return RSSList.Take(5).ToList();" 5 to 20 for example]).  
Also, you can delete a collection or feed "X" link.





