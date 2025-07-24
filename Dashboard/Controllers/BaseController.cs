using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reservation_APIs.Interfaces;

namespace Dashboard.Controllers
{
    public class BaseController : Controller
    {
        private IRepositoryManager? _repositoryManager { get; set; }
        private IMapper? _mapper { get; set; }

        protected IRepositoryManager RepositoryManager => _repositoryManager ??= HttpContext.RequestServices.GetRequiredService<IRepositoryManager>();
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();
    }
}
