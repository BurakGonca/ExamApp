using AutoMapper;
using BAExamApp.Dtos.Products;
using BAExamApp.MVC.Areas.Student.Models.ProductVMs;

namespace BAExamApp.MVC.Areas.Student.Controllers;

public class ProductController : StudentBaseController
{
    private readonly IMapper _mapper;
    private readonly IStudentClassroomService _studentClassroomService;
    private readonly IStudentService _studentService;
    private readonly IClassroomProductService _classroomProductService;

    public ProductController(IMapper mapper, IStudentService studentService, IStudentClassroomService studentClassroomService, IClassroomProductService classroomProductService)
    {
        _mapper = mapper;
        _studentService = studentService;
        _studentClassroomService = studentClassroomService;
        _classroomProductService = classroomProductService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var studentResult = await _studentService.GetByIdentityIdAsync(UserIdentityId);
        if (studentResult.IsSuccess)
        {
            var studentClassroomResult = await _studentClassroomService.GetAllByStudentIdAsync(studentResult.Data.Id);
            if (studentClassroomResult.IsSuccess)
            {
                var classroomProductResult = await _classroomProductService.GetAllByClassroomListAsync(studentClassroomResult.Data.Select(x => x.ClassroomId).ToList());
                if (classroomProductResult.IsSuccess)
                {
                    return View(_mapper.Map<List<StudentProductListVM>>(classroomProductResult.Data));
                }
                NotifyErrorLocalized(classroomProductResult.Message);
            }
            NotifyErrorLocalized(studentClassroomResult.Message);
        }
        NotifyErrorLocalized(studentResult.Message);

        return RedirectToAction(nameof(Index));
    }
}