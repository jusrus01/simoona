﻿//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web.Http;
//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using MoreLinq;
//using Shrooms.Contracts.Constants;
//using Shrooms.Contracts.DAL;
//using Shrooms.DataLayer.EntityModels.Models;
//using Shrooms.Presentation.Api.Filters;
//using Shrooms.Presentation.WebViewModels.Models.Exam;
//using X.PagedList;

//namespace Shrooms.Presentation.Api.Controllers
//{
//    [Authorize]
//    public class ExamController : ApiController
//    {
//        private readonly IMapper _mapper;
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IRepository<Exam> _examRepository;

//        public ExamController(IMapper mapper, IUnitOfWork unitOfWork)
//        {
//            _mapper = mapper;
//            _unitOfWork = unitOfWork;
//            _examRepository = unitOfWork.GetRepository<Exam>();
//        }

//        [PermissionAuthorize(Permission = BasicPermissions.Exam)]
//        public async Task<ExamViewModel> Get(int id, string includeProperties = "")
//        {
//            var examModel = await _examRepository.Get(e => e.Id == id, includeProperties: includeProperties).FirstOrDefaultAsync();
//            var examViewModel = _mapper.Map<Exam, ExamViewModel>(examModel);
//            return examViewModel;
//        }

//        [ValidationFilter]
//        [PermissionAuthorize(Permission = BasicPermissions.Exam)]
//        public async Task<HttpResponseMessage> Post(IList<ExamPostViewModel> models)
//        {
//            foreach (var model in models)
//            {
//                var exam = await _examRepository.Get(e => model.Number == e.Number && model.Title == e.Title).FirstOrDefaultAsync();
//                if (exam == null)
//                {
//                    exam = _mapper.Map<Exam>(model);
//                    _examRepository.Insert(exam);
//                    await _unitOfWork.SaveAsync();
//                }

//                model.Id = exam.Id;
//            }

//            return Request.CreateResponse(HttpStatusCode.Created, models);
//        }

//        [HttpGet]
//        [PermissionAuthorize(Permission = BasicPermissions.Exam)]
//        public async Task<IEnumerable<ExamAutoCompleteViewModel>> GetExamForAutoComplete(string s, int pageSize = WebApiConstants.DefaultAutocompleteListSize)
//        {
//            var start = s.ToLower();
//            var exams = await _examRepository.Get(e => e.Title.Contains(start))
//                   .OrderBy(e => e.Title)
//                   .DistinctBy(d => d.Title)
//                   .ToPagedListAsync(1, pageSize);

//            return _mapper.Map<IEnumerable<ExamAutoCompleteViewModel>>(exams);
//        }

//        [HttpGet]
//        [PermissionAuthorize(Permission = BasicPermissions.Exam)]
//        public async Task<IEnumerable<ExamAutoCompleteViewModel>> GetExamNumbersForAutoComplete(string title, string s, int pageSize = WebApiConstants.DefaultAutocompleteListSize)
//        {
//            var start = s.ToLower();
//            var exams = await _examRepository.Get(e => e.Title == title && e.Number.ToLower().Contains(start))
//                  .OrderBy(e => e.Number)
//                  .DistinctBy(e => e.Number)
//                  .ToPagedListAsync(1, pageSize);

//            return _mapper.Map<IEnumerable<ExamAutoCompleteViewModel>>(exams);
//        }

//        [HttpGet]
//        [PermissionAuthorize(Permission = BasicPermissions.Exam)]
//        public async Task<IEnumerable<ExamAutoCompleteViewModel>> GetExamForAutoCompleteByTitleAndNumber(string title, string number, int pageSize = WebApiConstants.DefaultAutocompleteListSize)
//        {
//            number ??= string.Empty;
//            title ??= string.Empty;

//            var exams = await _examRepository.Get(e => e.Title.Contains(title) && e.Number.Contains(number))
//                  .OrderBy(e => e.Number)
//                  .DistinctBy(e => e.Number)
//                  .ToPagedListAsync(1, pageSize);

//            return _mapper.Map<IEnumerable<ExamAutoCompleteViewModel>>(exams);
//        }
//    }
//}