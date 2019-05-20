using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwitterClone;


namespace TwitterClone.Models
{
    public class PersonTweetModel
    {



        //public IEnumerable<TwitterClone.Models.Tweet> TweetsList { get; set; }
        //public TwitterClone.Models.Tweet Tweet
        //{
        //    get; set; }
        
        public List<TWEET> tweet { get; set; }

        public List<FOLLOWING> follow{get;set;}

        public List<PERSON> Person { get; set; }


    }
}