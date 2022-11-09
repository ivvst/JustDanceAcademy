namespace JustDanceAcademy.Web.Areas.Administration.Controllers
{
    using JustDanceAcademy.Common;
    using JustDanceAcademy.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
