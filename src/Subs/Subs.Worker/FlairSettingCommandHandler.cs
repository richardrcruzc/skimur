using System;
using Infrastructure.Membership;
using Infrastructure.Messaging;
using Infrastructure.Messaging.Handling;
using Subs.Commands;
using Subs.Events;
using Subs.Services;
using Infrastructure.Utils;

namespace Subs.Worker
{

    public class FlairSettingCommandHandler :
         ICommandHandlerResponse<CreateFlairSetting, CreateFlairSettingResponse>,
         ICommandHandlerResponse<EditFlairSetting, EditFlairSettingResponse>,
         ICommandHandlerResponse<DeleteFlairSetting, DeleteFlairSettingResponse>
    {
        private readonly IMembershipService _membershipService;
        private readonly IEventBus _eventBus;
        private readonly IPermissionService _permissionService;
        private readonly IFlairSettingService _flairService;

        public FlairSettingCommandHandler(IMembershipService membershipService,
             IEventBus eventBus,
                     IPermissionService permissionService,
             IFlairSettingService flairService
           )
        {
            _membershipService = membershipService;
            _eventBus = eventBus;
            _permissionService = permissionService;
            _flairService = flairService;
        }

        public CreateFlairSettingResponse Handle(CreateFlairSetting command)
        {
            var response = new CreateFlairSettingResponse();
            try
            {
                
                var user = _membershipService.GetUserByUserName(command.UserName);

                if (user == null)
                {
                    response.Error = "Invalid user.";
                    return response;
                }

                var flair = new FlairSetting
                {
                    Id = GuidUtil.NewSequentialId(),
                    UserName = user.UserName,
                    AllowSubmitterAssignOwnLink=command.AllowSubmitterAssignOwnLink,
                     AllowUserAssignOwn=command.AllowUserAssignOwn,
                      EnableUserSubreddit=command.EnableUserSubreddit,
                      LinkPossition=command.LinkPossition,
                       UserFlairPossition=command.UserFlairPossition,
                    Deleted = false,
                };
                _flairService.InsertFlairSetting(flair);

                response.FlairSettingId = flair.Id;


            }
            catch (Exception ex)
            {
                response.Error = ex.Message;
            }

            return response;

        }

        public EditFlairSettingResponse Handle(EditFlairSetting command)
        {
            var response = new EditFlairSettingResponse();
            try
            {
                
                var user = _membershipService.GetUserByUserName(command.UserName);

                if (user == null)
                {
                    response.Error = "Invalid user.";
                    return response;
                }

                var flair = _flairService.GetFlairSettingById(command.FlairSettingId);
                if (flair == null)
                {
                    response.Error = "Invalid flair Setting.";
                    return response;
                } 
                 
                flair.Deleted = command.Deleted;
                flair.UserName = user.UserName;
                flair.AllowSubmitterAssignOwnLink = command.AllowSubmitterAssignOwnLink;
                flair.AllowUserAssignOwn = command.AllowUserAssignOwn;
                flair.EnableUserSubreddit = command.EnableUserSubreddit;
                flair.LinkPossition = command.LinkPossition;
                flair.UserFlairPossition = command.UserFlairPossition;

                _flairService.UpdateFlairSetting(flair);

                response.FlairSettingId = command.FlairSettingId;


            }
            catch (Exception ex)
            {
                response.Error = ex.Message;
            }

            return response;

        }

        public DeleteFlairSettingResponse Handle(DeleteFlairSetting command)
        {
            var response = new DeleteFlairSettingResponse();
            try
            {
                
                var user = _membershipService.GetUserByUserName(command.UserName);

                if (user == null)
                {
                    response.Error = "Invalid user.";
                    return response;
                }

                var flair = _flairService.GetFlairSettingById(command.FlairSettingId);
                if (flair == null)
                {
                    response.Error = "Invalid flair.";
                    return response;
                } 
               
                flair.Deleted = true;
                

                _flairService.UpdateFlairSetting(flair);

                response.FlairSettingId = command.FlairSettingId;


            }
            catch (Exception ex)
            {
                response.Error = ex.Message;
                response.FlairSettingId = command.FlairSettingId;
            }

            return response;

        }

    }
}