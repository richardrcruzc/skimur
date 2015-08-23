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
using Subs.Services;

namespace Skimur.Web.Controllers
{
    public class FlairSettingController : BaseController
    {
        private readonly ICommandBus _commandBus;
        private readonly IContextService _contextService; 
        private readonly IMapper _mapper; 
        private readonly IUserContext _userContext; 
        private readonly IPermissionDao _permissionDao;

        private readonly IFlairSettingService _flairSettingService;


        public FlairSettingController(IContextService contextService,
            IFlairSettingService flairSettingService, 
            IMapper mapper,
            ICommandBus commandBus,
            IUserContext userContext, 
            IPermissionDao permissionDao)
        {
            _contextService = contextService;
            _flairSettingService = flairSettingService;
            _commandBus = commandBus;
            _mapper = mapper; 
            _userContext = userContext; 
            _permissionDao = permissionDao;
        }


        [Authorize] 
        public ActionResult CreateSave( )
        {
            var userName = _userContext.CurrentUser.UserName;
           
            var model = new FlairSettingsViewModel();

            var setting = _flairSettingService.GetAllFlairSettingForUser(userName).FirstOrDefault(); 
            if(setting!=null)
                model = _mapper.Map<FlairSetting, FlairSettingsViewModel>(setting); 
           
            return PartialView("~/Views/Flair/_FlairsSetting.cshtml", model);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSave(FlairSettingsViewModel model )
        { 
            if (ModelState.IsValid)
            {
                var userName = _userContext.CurrentUser.UserName;
                var dateCreated = Common.CurrentTime();
                var create = new CreateFlairSetting();

                var edit = new EditFlairSetting();

                
                    if (model.Id != Guid.Empty)
                    {
                        edit.FlairSettingId = model.Id;
                        edit.Deleted = model.Deleted;
                        edit.UserName = model.UserName;
                        edit.AllowSubmitterAssignOwnLink = model.AllowSubmitterAssignOwnLink;
                        edit.AllowUserAssignOwn = model.AllowUserAssignOwn;
                        edit.EnableUserSubreddit = model.EnableUserSubreddit;
                        edit.LinkPossition = model.LinkPossition;
                        edit.UserFlairPossition = model.UserFlairPossition;

                    var response = _commandBus.Send<EditFlairSetting, EditFlairSettingResponse>(edit);
                    }
                    else
                    {

                    create.Deleted = model.Deleted;
                    create.UserName = model.UserName;
                    create.AllowSubmitterAssignOwnLink = model.AllowSubmitterAssignOwnLink;
                    create.AllowUserAssignOwn = model.AllowUserAssignOwn;
                    create.EnableUserSubreddit = model.EnableUserSubreddit;
                    create.LinkPossition = model.LinkPossition;
                    create.UserFlairPossition = model.UserFlairPossition;

                    var response = _commandBus.Send<CreateFlairSetting, CreateFlairSettingResponse>(create); 
                    } 
               
            }

            return RedirectPermanent("/flair");


        }
         
       
    }
}