﻿using AutoMapper;
using Shrooms.Contracts.DataTransferObjects.Notification;
using Shrooms.Contracts.ViewModels.Notifications;
using Shrooms.Contracts.ViewModels.Wall.Posts;
using Shrooms.DataLayer.EntityModels.Models.Notifications;

namespace Shrooms.Presentation.ModelMappings.Profiles
{
    public class Notifications : Profile
    {
        public Notifications()
        {
            CreateMap<Notification, NotificationDto>()
                .ForMember(dest => dest.SourceIds, opt => opt.MapFrom(u => u.Sources));
            CreateMap<NotificationDto, NotificationViewModel>()
                .ForMember(dest => dest.others, opt => opt.MapFrom(u => 0)); // TODO: Figure out if it is 0 by default

            CreateMap<WallPostViewModel, NotificationDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(u => u.MessageBody))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(u => u.Author.FullName));

            CreateMap<Sources, SourcesDto>();
            CreateMap<SourcesDto, SourcesViewModel>();
        }

    }
}
