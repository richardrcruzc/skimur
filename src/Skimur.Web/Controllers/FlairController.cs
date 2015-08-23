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
    public class FlairController : BaseController
    {
        private readonly ICommandBus _commandBus;
        private readonly IContextService _contextService;
       // private readonly IFlairDao _flairDao;
        private readonly IMapper _mapper; 
        private readonly IUserContext _userContext; 
        private readonly IPermissionDao _permissionDao;

        private readonly IFlairService _flairService;


        public FlairController(IContextService contextService,
            IFlairService flairService, 
            IMapper mapper,
            ICommandBus commandBus,
            IUserContext userContext,
            IPostDao postDao,
            IVoteDao voteDao,
            ICommentDao commentDao,
            IPermissionDao permissionDao)
        {
            _contextService = contextService;
            _flairService = flairService;
            _commandBus = commandBus;
            _mapper = mapper; 
            _userContext = userContext; 
            _permissionDao = permissionDao;
        }


        public ActionResult Index(string id= "GrantFlair")
        {
            string user = User.Identity.Name;
            
            var userFlairs = _flairService.GetAllFlairsForUser(user)
                .Select(x =>
                {
                    var query1 = _mapper.Map<Flair, ListFlairViewModel>(x);
                    return query1;
                }
            ).Where(x=>x.Type==1).ToList();

            var linkFlairs = _flairService.GetAllFlairsForUser(user)
               .Select(x =>
               {
                   var query2 = _mapper.Map<Flair, ListFlairViewModel>(x);
                   return query2;
               }
           ).Where(x => x.Type == 2).ToList() ;

            

           var model = new FlairIndexModel();
            model.ActiveTab = id;
            model.UserFlair.AddRange(userFlairs);
            model.LinkFlair.AddRange(linkFlairs);

            var tmp = new ListFlairViewModel();
            tmp.Type = 1;
            model.UserFlair.Add(tmp);
            tmp.Type = 2;
            model.LinkFlair.Add(tmp);

            return View(model);
        }
        [Authorize]
        [HttpPost] 
        public ActionResult InsertFlair(ListFlairViewModel model)
        {
            try
            {
                var userName = _userContext.CurrentUser.UserName;
                var dateCreated = Common.CurrentTime();
                var flair = new Flair();
                flair.Id = Guid.NewGuid();
                flair.Text = model.Text;
                flair.CssClass = model.CssClass;
                flair.Type = model.Type;
                flair.Deleted = false;
                flair.UserName = userName;
                flair.TextEditable = model.TextEditable;

                var response = _commandBus.Send<CreateFlair, CreateFlairResponse>(new CreateFlair
                {
                    Text = model.Text,
                    CssClass = model.CssClass,
                    Type = model.Type,
                    Deleted = false,
                    UserName = userName,
                    TextEditable = model.TextEditable,
            });

                if (!string.IsNullOrEmpty(response.Error))
                { 
                    return Json(new
                    {
                        success = false,
                        error = response.Error
                    });
                }


                    //  _flairService.InsertFlair(flair);

                    return Json(new
                {
                    success = true,
                    error = ""
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    error = ex.Message, 
                    // ModelState.Values.SelectMany(v => v.Errors),
                });
            }

        }
        public ActionResult DeleteFlair(string id)
        {
            try
            {
                var userName = _userContext.CurrentUser.UserName;
                var dateCreated = Common.CurrentTime();
                var flair = _flairService.GetFlairById(new Guid(id));
                flair.Deleted = true;
                
                return Json(new
                {
                    success = true,
                    error = ""
                });
            }
            catch (Exception ex)
            {
                // TODO: log
                return Json(new
                {
                    success = false,
                    error = ex.Message
                });
            }

             

        }

        public ActionResult GetAllFlairByUserAndType(int type)
        {
            string user = User.Identity.Name;
            var allFlairs = _flairService.GetAllFlairsForUser(user)
                .Select(x =>
                {
                    var model = _mapper.Map<Flair, ListFlairViewModel>(x);
                    return model;
                }
            ).Where(x=>x.Type== type).ToList();

            return Json(new
            {
                success = true,
                html = RenderView("~/Views/Flair/_List.cshtml", allFlairs)
            });
        }


        public ActionResult GetFlairById(string id)
        {
            string user = User.Identity.Name;
            var flair = _flairService.GetFlairById(new Guid(id));
            var json = new
            {
                success = true,
                usename =   flair.UserName  ,
                text = flair.Text,
                cssclass = flair.CssClass,
                userid= flair.Id
            };

            return Json(json);
        }
        public ActionResult GetAllFlairByUser()
        {
            string user = User.Identity.Name; 
            var allFlairs = _flairService.GetAllFlairsForUser(user)
                .Select(x =>
                {
                    var model = _mapper.Map<Flair, ListFlairViewModel>(x);
                    return model;
                }
            ).ToList();
             
            return Json(new
            {
                success=true , html = RenderView("~/Views/Flair/_FlairsList.cshtml", allFlairs)
            });
        }

        public ActionResult backIndex()
        {
            string user = User.Identity.Name;
           // var allFlair = _flairService.GetAllFlairsForUser(user).ToList();
            var allFlairs = _flairService.GetAllFlairsForUser(user)
                .Select(x =>
               {
                   var model = _mapper.Map<Flair, ListFlairViewModel>(x);
                   return model;
               }
            ).ToList();

              return View("~/Views/Flair/_FlairsList.cshtml", allFlairs);
            //return Json(new
            //{
            //    html = RenderView("_FlairsList", allFlairs)
            //},JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View(new CreateFlairViewModel());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateByType(ListFlairViewModel model)
        {
            return View(model);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSave(FlairIndexModel model, string SaveUserFlair, string SaveLinkFlair)
        {


            if (ModelState.IsValid)
            {
                var userName = _userContext.CurrentUser.UserName;
                var dateCreated = Common.CurrentTime();
                var create = new CreateFlair();

                var edit = new EditFlair();

                foreach (var query in model.UserFlair)
                {
                    if (query.Id != Guid.Empty)
                    {
                        edit.FlairId = query.Id;
                        edit.TextEditable = query.TextEditable;
                        edit.Text = query.Text;
                        edit.CssClass = query.CssClass;
                        edit.Type = 1;
                        edit.Deleted = false;
                        edit.UserName = userName;

                        var response = _commandBus.Send<EditFlair, EditFlairResponse>(edit);
                    }
                    else
                    {
                        create.TextEditable = query.TextEditable;
                        create.Text = query.Text;
                        create.CssClass = query.CssClass;
                        create.Type = 1;
                        create.Deleted = false;
                        create.UserName = userName;

                        var response = _commandBus.Send<CreateFlair, CreateFlairResponse>(create); 
                    } 
                }
                foreach (var query in model.LinkFlair)
                {
                    if (query.Id != Guid.Empty)
                    {
                        edit.FlairId = query.Id;
                        edit.TextEditable = query.TextEditable;
                        edit.Text = query.Text;
                        edit.CssClass = query.CssClass;
                        edit.Type = 2;
                        edit.Deleted = false;
                        edit.UserName = userName;

                        var response = _commandBus.Send<EditFlair, EditFlairResponse>(edit);
                    }
                    else
                    {
                        create.TextEditable = query.TextEditable;
                        create.Text = query.Text;
                        create.CssClass = query.CssClass;
                        create.Type =2;
                        create.Deleted = false;
                        create.UserName = userName;
                        var response = _commandBus.Send<CreateFlair, CreateFlairResponse>(create);

                    }
                }

               if(SaveUserFlair!=null)
                    return RedirectToAction("index", new  {id = "userflair"} );

                if ( SaveLinkFlair != null)
                    return RedirectToAction("index", new { id = "linkflair" });

                //return Redirect("index");
            }
            
            return View("Index" );

             
        }

        public ActionResult Edit(string id)
        { 

            if (string.IsNullOrEmpty(id))
                return Redirect("Index");

            var flair = _flairService.GetFlairById(new Guid(id));

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
                var flair = _flairService.GetFlairById(new Guid(id));

                var userName = _userContext.CurrentUser.UserName;
                var dateCreated = Common.CurrentTime();
         
                flair.Text = model.CssClass;
                flair.CssClass = model.CssClass;
                flair.Type = model.Type;
              
                flair.Deleted = false;
                
                flair.TextEditable = model.TextEditable;
                flair.UserName = userName;

               
                return Redirect("Index");
            }

            // todo: success message


            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            var flair = _flairService.GetFlairById(id);
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
                var flair = _flairService.GetFlairById(model.Id);
                flair.Deleted = true;
               
                flair.UserName = userName;

               
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