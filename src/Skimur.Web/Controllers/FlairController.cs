using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure;
using Infrastructure.Messaging;
using Infrastructure.Utils;
using Skimur.Web.Models;
using Subs;
using Subs.Commands;
using Subs.ReadModel;

namespace Skimur.Web.Controllers
{
    public class FlairController : BaseController
    {
        private readonly IContextService _contextService;
        private readonly IFlairDao _flairDao;
        private readonly IMapper _mapper; 
        private readonly IUserContext _userContext; 
        private readonly IPermissionDao _permissionDao;

        public FlairController(IContextService contextService,
            IFlairDao subDao,
            IMapper mapper,
            ICommandBus commandBus,
            IUserContext userContext,
            IPostDao postDao,
            IVoteDao voteDao,
            ICommentDao commentDao,
            IPermissionDao permissionDao)
        {
            _contextService = contextService;
            _flairDao = subDao;
            _mapper = mapper; 
            _userContext = userContext; 
            _permissionDao = permissionDao;
        }

        public ActionResult Index(string user)
        {

            var allFlair = _flairDao.GetAllFlairsForUser(user)
                .Select(x =>
               {
                   var model = _mapper.Map<Flair, FlairViewModel>(x);
                   return model;
               } 
            ).ToList(); 

            return View(allFlair);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View(new CreateFlairViewModel());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateFlairViewModel model)
        {


            if (ModelState.IsValid)
            {
                var userName = _userContext.CurrentUser.UserName;
                var dateCreated = Common.CurrentTime();
                var flair = new Flair();

                flair.Text = model.CssClass;
                flair.CssClass = model.CssClass;
                flair.Type = model.Type;
                flair.DateCreated = dateCreated;
                flair.Deleted = false;
                flair.IpAddress = Request.UserHostAddress;
                flair.TextEditable = model.TextEditable;
                flair.UserName = userName;


                _flairDao.InsertFlair(flair);
                return Redirect("index");
            }
            
            return View(model);

             
        }

        public ActionResult Edit(string id)
        { 

            if (string.IsNullOrEmpty(id))
                return Redirect("Index");

            var flair = _flairDao.GetFlairById(new Guid(id));

            var model = _mapper.Map<Flair, EditFlairViewModel>(flair); 

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, EditFlairViewModel model)
        { 

            if (ModelState.IsValid)
            {
                var flair = _flairDao.GetFlairById(new Guid(id));

                var userName = _userContext.CurrentUser.UserName;
                var dateCreated = Common.CurrentTime();
         
                flair.Text = model.CssClass;
                flair.CssClass = model.CssClass;
                flair.Type = model.Type;
                flair.DateEdited = dateCreated;
                flair.Deleted = false;
                flair.IpAddress = Request.UserHostAddress;
                flair.TextEditable = model.TextEditable;
                flair.UserName = userName; 

                _flairDao.UpdateFlair(flair);

                return Redirect("Index");
            }

            // todo: success message


            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            var flair = _flairDao.GetFlairById(id);
            var model = _mapper.Map<Flair, EditFlairViewModel>(flair);

            return View(model);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EditFlairViewModel model)
        {  

            try
            {
                var userName = _userContext.CurrentUser.UserName;
                var dateCreated = Common.CurrentTime();
                var flair = _flairDao.GetFlairById(model.Id);
                flair.Deleted = true;
                flair.IpAddress = Request.UserHostAddress; 
                flair.UserName = userName;

                _flairDao.UpdateFlair(flair);
                return Redirect("Index");
            }
            catch (Exception ex)
            {
                // TODO: log
                
            }
            return View();
        }

       
    }
}