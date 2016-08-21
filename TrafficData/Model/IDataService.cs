using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficData.Model
{
    public interface IDataService
    {
        //Get dashboard data content
        MainPageData GetMainPageData(KeyValuePair<DateTime,string> datetimeInfo);
        //Get search data content
        SearchPageData GetSearchPageData(List<QueryInfo> queryInfos);
        //Get weight data content
        SearchPageData GetSearchPageDataForWeight(List<QueryInfo> queryInfos);
        //Get speed data content
        SearchPageData GetSearchPageDataForSpeed(List<QueryInfo> queryInfos);
        //Get length data content
        SearchPageData GetSearchPageDataForLenght(List<QueryInfo> queryInfos);
        //Get headway data content
        SearchPageData GetSearchPageDataForHeadway(List<QueryInfo> queryInfos);
        //Get caculated data content
        SearchPageData GetSearchPageDataForCalculatedData(QueryInfo queryInfos);
    }
}
