using Gym.BusinessLogic.Services;
using Gym.BusinessLogic.ViewModel.Members;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Gym.Presentation.Controllers
{
    public class MembersController(IMemberService members) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var items = await members.GetAllAsync(ct);
            return View(items); // Views/Members/Index.cshtml
        }

        [HttpGet]
        public async Task <IActionResult> Create()
        {


            return View(members);
        }



        [HttpPost]
        public async Task<IActionResult> Create(CreateMemberViewModel model , CancellationToken cancellationToken )
        {
            return View ();
        }
        // problems

        // 1 - test business logic without http request ?
        // 2 - same logic needed in API and MVC ?

        // to solve problem we need use services

        // services => business logic


        // Vaildation  And AntiForegery

        // 1- jquery.Validate.min.js => Clinet side Validation (unobtrusive vaildation )

        // 2- Vaildate.Unbrostive.min.js => Clinet side vaildation 

<<<<<<< Updated upstream
        [HttpGet]
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var memberItem = await members.GetDetailsAsync(id, cancellationToken);
            if (memberItem == null)
            {
                return NotFound();
            }
            return View(memberItem);
        }


        [HttpGet]

        public async Task<IActionResult> HealthRecord(int id, CancellationToken cancellationToken)
        {
            var HealthRecord = await members.GetHealthRecordAsync(id,cancellationToken);

            if (HealthRecord == null)
            {
                return NotFound();
            }

            return View(HealthRecord);
        }
=======


        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var member = await members.GetForUpdateAsync(id, cancellationToken);
            if (member == null)
                return NotFound();

            return View(member);
        }


        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(EditMemberViewModel editMemberViewModel, CancellationToken cancellationToken)
        {

        }

>>>>>>> Stashed changes
    }
}