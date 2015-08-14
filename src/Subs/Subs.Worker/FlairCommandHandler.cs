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

    public class FlairCommandHandler :
         ICommandHandlerResponse<CreateFlair, CreateFlairResponse>,
         ICommandHandlerResponse<EditFlair, EditFlairResponse>,
         ICommandHandlerResponse<DeleteFlair, DeleteFlairResponse>
    {
        private readonly IMembershipService _membershipService;
        private readonly IEventBus _eventBus;
        private readonly IPermissionService _permissionService;
        private readonly IFlairService _flairService;

        public FlairCommandHandler(IMembershipService membershipService,
             IEventBus eventBus,
                     IPermissionService permissionService,
             IFlairService flairService
           )
        {
            _membershipService = membershipService;
            _eventBus = eventBus;
            _permissionService = permissionService;
            _flairService = flairService;
        }

        public CreateFlairResponse Handle(CreateFlair command)
        {
            var response = new CreateFlairResponse();
            try
            {
                if (string.IsNullOrEmpty(command.Text))
                {
                    response.Error = "A Text is required.";
                    return response;
                }
                if (string.IsNullOrEmpty(command.CssClass))
                {
                    response.Error = "A CssClass is required.";
                    return response;
                }
                var user = _membershipService.GetUserByUserName(command.UserName);

                if (user == null)
                {
                    response.Error = "Invalid user.";
                    return response;
                }

                var flair = new Flair
                {
                    Id = GuidUtil.NewSequentialId(),
                    UserName = user.UserName,
                    CssClass = command.CssClass,
                    Text = command.Text,
                    Type = command.Type,
                    TextEditable = command.TextEditable,
                    Deleted = false,
                    DateCreated = command.DateCreated,
                    IpAddress = command.IpAddress,

                };
                _flairService.InsertFlair(flair);

                response.FlairId = flair.Id;


            }
            catch (Exception ex)
            {
                response.Error = ex.Message;
            }

            return response;

        }

        public EditFlairResponse Handle(EditFlair command)
        {
            var response = new EditFlairResponse();
            try
            {
                if (string.IsNullOrEmpty(command.Text))
                {
                    response.Error = "A Text is required.";
                    return response;
                }
                if (string.IsNullOrEmpty(command.CssClass))
                {
                    response.Error = "A CssClass is required.";
                    return response;
                }
                var user = _membershipService.GetUserByUserName(command.UserName);

                if (user == null)
                {
                    response.Error = "Invalid user.";
                    return response;
                }

                var flair = _flairService.GetFlairById(command.FlairId);
                if (flair == null)
                {
                    response.Error = "Invalid flair.";
                    return response;
                } 

                flair.UserName = user.UserName;
                flair.CssClass = command.CssClass;
                flair.Text = command.Text;
                flair.Type = command.Type;
                flair.TextEditable = command.TextEditable;
                flair.Deleted = command.Deleted;
                flair.DateEdited = command.DateEdited;
                flair.IpAddress = command.IpAddress;
                
                _flairService.UpdateFlair(flair);

                response.FlairId = command.FlairId;


            }
            catch (Exception ex)
            {
                response.Error = ex.Message;
            }

            return response;

        }

        public DeleteFlairResponse Handle(DeleteFlair command)
        {
            var response = new DeleteFlairResponse();
            try
            {
                
                var user = _membershipService.GetUserByUserName(command.UserName);

                if (user == null)
                {
                    response.Error = "Invalid user.";
                    return response;
                }

                var flair = _flairService.GetFlairById(command.FlairId);
                if (flair == null)
                {
                    response.Error = "Invalid flair.";
                    return response;
                } 
               
                flair.Deleted = true;
                flair.DateEdited = command.DateDeleted;                

                _flairService.UpdateFlair(flair);

                response.FlairId = command.FlairId;


            }
            catch (Exception ex)
            {
                response.Error = ex.Message;
                response.FlairId = command.FlairId;
            }

            return response;

        }

    }
}