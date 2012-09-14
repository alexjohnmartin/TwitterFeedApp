using System.Web.Mvc;
using PayByPhoneTwitterFeed.DataAccess;

namespace PayByPhoneTwitterFeed.Controllers
{
    public class TwitterFeedsController : Controller
    {
        private readonly ITwitterFeedsProvider _twitterFeedsProvider;

        public TwitterFeedsController(ITwitterFeedsProvider twitterFeedsProvider)
        {
            _twitterFeedsProvider = twitterFeedsProvider;
        }

        public TwitterFeedsController()
        {
            _twitterFeedsProvider = new TwitterFeedsProvider();
        }

        public ActionResult Index()
        {
            return Json(_twitterFeedsProvider.GetTwitterFeeds(), JsonRequestBehavior.AllowGet);
        }
    }
}
