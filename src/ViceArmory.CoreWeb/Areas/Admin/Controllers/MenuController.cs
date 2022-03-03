using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViceArmory.CoreWeb.Models;
using ViceArmory.DTO.ResponseObject.Account;
using ViceArmory.DTO.ResponseObject.Authenticate;
using ViceArmory.Middleware.Interface;
using ViceArmory.RequestObject.Account;

namespace ViceArmory.CoreWeb.Areas.Admin.Controllers
{
    [TypeFilter(typeof(CustomActionFilter))]
    [Area("Admin")]
    public class MenuController : Controller
    {
        IMenuService _IMenuService;
        public MenuController(IMenuService iMenuService)
        {
            _IMenuService = iMenuService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var res = _IMenuService.GetMenu();
            return View(res.Result);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var req = new MenuRequestDTO();
            req.MenuRequestList = new List<MenuRequest>();
            req.MenuRequestList.Add(new MenuRequest() { Id = "", Name = "" });
            foreach (var item in _IMenuService.GetMenu().Result.Where(l => !l.IsDeleted))
            {
                req.MenuRequestList.Add(new MenuRequest()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return View(req);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MenuRequestDTO req)
        {
            if (!ModelState.IsValid)
                return RedirectToActionPermanent("Create");
            AuthenticateResponse authRes = null;
            if (HttpContext.Session.IsAvailable)
            {
                authRes = JsonConvert.DeserializeObject<AuthenticateResponse>(HttpContext.Session.GetString("UserInfo"));
                _IMenuService.SetSession(authRes);
            }
            req.CreatedBy = authRes.Id;
            req.CreatedDate = DateTime.Now;
            var res = await _IMenuService.CreateMenu(req);
            return RedirectToActionPermanent("index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var res = _IMenuService.GetMenuById(id).Result;
            var req = new MenuRequestDTO()
            {
                Id = res.Id,
                Name = res.Name,
                Description = res.Description,
                IsActive = res.IsActive,
                IsDeleted = res.IsDeleted,
                ParentId = res.ParentId,
            };
            req.MenuRequestList = new List<MenuRequest>();
            req.MenuRequestList.Add(new MenuRequest() { Id = "", Name = "" });
            foreach (var item in _IMenuService.GetMenu().Result.Where(l => !l.IsDeleted))
            {
                if (item.Id != res.Id)
                {
                    req.MenuRequestList.Add(new MenuRequest()
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
            }
            return View(req);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(MenuRequestDTO res)
        {
            if (!ModelState.IsValid)
            {
                res.MenuRequestList = new List<MenuRequest>();
                res.MenuRequestList.Add(new MenuRequest() { Id = "", Name = "" });
                foreach (var item in _IMenuService.GetMenu().Result.Where(l => !l.IsDeleted))
                {
                    if (item.Id != res.Id)
                    {
                        res.MenuRequestList.Add(new MenuRequest()
                        {
                            Id = item.Id,
                            Name = item.Name
                        });
                    }
                }
                return View(res);
            }
            AuthenticateResponse authRes = null;
            if (HttpContext.Session.IsAvailable)
            {
                authRes = JsonConvert.DeserializeObject<AuthenticateResponse>(HttpContext.Session.GetString("UserInfo"));
                _IMenuService.SetSession(authRes);
            }
           
            await _IMenuService.UpdateMenu(new MenuRequestDTO()
            {
                Id = res.Id,
                IsActive = res.IsActive,
                CreatedBy = res.CreatedBy,
                CreatedDate = res.CreatedDate,
                Description = res.Description,
                IsDeleted = res.IsDeleted,
                Name = res.Name,
                ParentId = res.ParentId,
                UpdatedBy = authRes.Id,
                UpdatedDate = DateTime.Now
            });
            return RedirectToActionPermanent("index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteMenu(string id)
        {
            AuthenticateResponse authRes = null;
            if (HttpContext.Session.IsAvailable)
            {
                authRes = JsonConvert.DeserializeObject<AuthenticateResponse>(HttpContext.Session.GetString("UserInfo"));
                _IMenuService.SetSession(authRes);
            }

            await _IMenuService.DeleteMenuById(id);
            return RedirectToActionPermanent("index");
        }

    }
}
