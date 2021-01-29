using System.Threading.Tasks;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BrainstormSessions.Controllers
{
    public class SessionController : Controller
    {
        private readonly IBrainstormSessionRepository _sessionRepository;

        public SessionController(IBrainstormSessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (Logger.inited == false)
                Logger.InitLogger();

            if (!id.HasValue)
            {
                if (Logger.useLogs)
                    Logger.Log.Info("Homepage");
                return RedirectToAction(actionName: nameof(Index),
                    controllerName: "Home");
            }

            var session = await _sessionRepository.GetByIdAsync(id.Value);
            if (session == null)
            {
                if (Logger.useLogs)
                    Logger.Log.Error("Session not found");

                return Content("Session not found.");
            }

            var viewModel = new StormSessionViewModel()
            {
                DateCreated = session.DateCreated,
                Name = session.Name,
                Id = session.Id
            };
            if (Logger.useLogs)
                Logger.Log.Debug($"session.DateCreated: {session.DateCreated} session.Name: {session.Name} session.Id: {session.Id}");

            return View(viewModel);
        }
    }
}
