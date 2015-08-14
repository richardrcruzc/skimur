﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Messaging;
using Infrastructure.Messaging.Handling;
using SimpleInjector;
using Skimur;
using Subs.Commands;
using Subs.Events;

namespace Subs.Worker
{
    public class Registrar : IRegistrar
    {
        public void Register(Container container)
        {

            container.Register<ICommandHandlerResponse<CreateFlair, CreateFlairResponse>, FlairCommandHandler>();
            container.Register<ICommandHandlerResponse<EditFlair, EditFlairResponse>, FlairCommandHandler>();

            container.Register<ICommandHandlerResponse<CreateSub, CreateSubResponse>, SubHandler>();
            container.Register<ICommandHandlerResponse<EditSub, EditSubResponse>, SubHandler>();
            container.Register<ICommandHandlerResponse<CreatePost, CreatePostResponse>, SubHandler>();
            container.Register<ICommandHandlerResponse<SubcribeToSub, SubcribeToSubResponse>, SubHandler>();
            container.Register<ICommandHandlerResponse<UnSubcribeToSub, UnSubcribeToSubResponse>, SubHandler>();

            container.Register<IEventHandler<SubScriptionChanged>, SubEventHandler>();

            container.Register<ICommandHandler<CastVoteForPost>, VoteCommandHandler>();
            container.Register<ICommandHandler<CastVoteForComment>, VoteCommandHandler>();

            container.Register<IEventHandler<VoteForPostCasted>, ScoringAndSortingEventHandler>();
            container.Register<IEventHandler<VoteForCommentCasted>, ScoringAndSortingEventHandler>();

            container.Register<ICommandHandlerResponse<CreateComment, CreateCommentResponse>, CommentCommandHandler>();
            container.Register<ICommandHandlerResponse<EditComment, EditCommentResponse>, CommentCommandHandler>();
            container.Register<ICommandHandlerResponse<DeleteComment, DeleteCommentResponse>, CommentCommandHandler>();

            container.Register<ICommandHandlerResponse<BanUserFromSub, BanUserFromSubResponse>, SubBanning>();
        }

        public int Order
        {
            get { return 0; }
        }
    }
}
