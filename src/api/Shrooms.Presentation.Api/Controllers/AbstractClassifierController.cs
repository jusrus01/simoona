﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DAL;
using Shrooms.Contracts.ViewModels;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Presentation.WebViewModels.Models;
using Shrooms.Presentation.WebViewModels.Models.PostModels;

namespace Shrooms.Presentation.Api.Controllers
{
    public class AbstractClassifierController : AbstractWebApiController<AbstractClassifier, AbstractClassifierViewModel, AbstractClassifierPostViewModel>
    {
        private AbstractClassifier _classifierModel;
        private readonly IRepository<AbstractClassifier> _classifierRepository;

        public AbstractClassifierController(IMapper mapper, IUnitOfWork unitOfWork)
            : base(mapper, unitOfWork)
        {
            _classifierRepository = unitOfWork.GetRepository<AbstractClassifier>();
        }

        [AllowAnonymous]
        public override async Task<HttpResponseMessage> Post(AbstractClassifierPostViewModel crudViewModel)
        {
            if (await _repository.GetByIdAsync(crudViewModel.Id) != null)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict);
            }

            var specificType = Type.GetType("DataLayer.Models." + crudViewModel.AbstractClassifierType + ",DataLayer");
            _classifierModel = _mapper.Map(crudViewModel, _classifierModel, crudViewModel.GetType(), specificType) as AbstractClassifier;

            //TODO Need to fix Child saving issue when saving abstract classifier after removing children
            await RemoveAllChildren(_classifierModel);
            UpdateChildren(crudViewModel, _classifierModel, specificType);

            _repository.Insert(_classifierModel);
            await _unitOfWork.SaveAsync();

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        public override async Task<HttpResponseMessage> Put(AbstractClassifierPostViewModel crudViewModel)
        {
            var model = await _repository.GetByIdAsync(crudViewModel.Id);

            if (model == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var specificType = Type.GetType("DataLayer.Models." + crudViewModel.AbstractClassifierType + ",DataLayer");
            model = _mapper.Map(crudViewModel, model, crudViewModel.GetType(), specificType) as AbstractClassifier;

            //TODO Need to fix Child saving issue when saving abstract classifier after removing children
            await RemoveAllChildren(model);
            UpdateChildren(crudViewModel, model, specificType);

            _repository.Update(model);
            await _unitOfWork.SaveAsync();

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        public override async Task<PagedViewModel<AbstractClassifierViewModel>> GetPaged(string includeProperties = null,
            int page = 1,
            int pageSize = WebApiConstants.DefaultPageSize,
            string sort = null,
            string dir = "",
            string s = "")
        {
            sort ??= "Name Asc, OrganizationId Asc";

            return await GetFilteredPaged(includeProperties, page, pageSize, sort, dir, f => f.Name.Contains(s));
        }

        public IEnumerable<AbstractClassifierTypeViewModel> GetAbstractClassifierTypes()
        {
            var abstractClassifierTypes = new List<AbstractClassifierTypeViewModel>();
            var values = WebApiConstants.AbstractClassifierTypes;

            foreach (var value in values)
            {
                var abstractClassifierTypeModel = new AbstractClassifierTypeViewModel();

                abstractClassifierTypeModel.AbstractClassifierType = value;

                abstractClassifierTypes.Add(abstractClassifierTypeModel);
            }

            return abstractClassifierTypes;
        }

        [HttpGet]
        public async Task<IEnumerable<AbstractClassifierViewModel>> GetChildrenForAutoComplete(string search, int id = 0)
        {
            IEnumerable<AbstractClassifierViewModel> childrenViewModel;

            if (!string.IsNullOrEmpty(search))
            {
                var children = await _classifierRepository.Get(o => o.Name.Contains(search) && o.Id != id).ToListAsync();
                childrenViewModel = _mapper.Map<IEnumerable<AbstractClassifier>, IEnumerable<AbstractClassifierViewModel>>(children);
            }
            else
            {
                return null;
            }

            return childrenViewModel;
        }

        public async Task<IEnumerable<AbstractClassifierViewModel>> GetClassifiersWithoutMe()
        {
            var children = await _classifierRepository.Get().ToListAsync();
            var childrenViewModel = _mapper.Map<IEnumerable<AbstractClassifier>, IEnumerable<AbstractClassifierViewModel>>(children);
            return childrenViewModel;
        }

        private void UpdateChildren(AbstractClassifierAbstractViewModel crudViewModel, AbstractClassifier classifierModel, Type specificType)
        {
            _repository.Update(classifierModel);

            if (classifierModel == null || crudViewModel == null)
            {
                return;
            }

            foreach (var childViewModel in crudViewModel.Children)
            {
                var model = _mapper.Map(childViewModel, childViewModel.GetType(), specificType);

                if (model is AbstractClassifier classifier)
                {
                    classifier.ParentId = classifierModel.Id;
                    _repository.Update(classifier);
                }
            }
        }

        private async Task RemoveAllChildren(AbstractClassifier classifierModel)
        {
            var classifierModelId = classifierModel.Id;
            classifierModel = await _repository.Get(f => f.Id == classifierModelId, includeProperties: "Children").FirstOrDefaultAsync();

            if (classifierModel != null)
            {
                classifierModel.Children = null;
                _repository.Update(classifierModel);
            }
        }
    }
}