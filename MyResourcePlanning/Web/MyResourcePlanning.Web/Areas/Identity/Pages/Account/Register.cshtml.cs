namespace MyResourcePlanning.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using MyResourcePlanning.Common;
    using MyResourcePlanning.Models;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IServiceProvider serviceProvider;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IServiceProvider serviceProvider)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.serviceProvider = serviceProvider;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? this.Url.Content("~/");
            if (this.ModelState.IsValid)
            {
                var user = new User { UserName = this.Input.Email, FirstName = this.Input.FirstName, LastName = this.Input.LastName, Email = this.Input.Email, PhoneNumber = this.Input.PhoneNumber };
                var result = await this.userManager.CreateAsync(user, this.Input.Password);

                if (result.Succeeded)
                {
                    var roleManager = this.serviceProvider.GetRequiredService<RoleManager<UserRole>>();
                    var resourceRoleName = GlobalConstants.ResourceRoleName;

                    var resourceRole = await roleManager.FindByNameAsync(resourceRoleName);

                    if (resourceRole != null)
                    {
                        User currentUser = await this.userManager.FindByEmailAsync(this.Input.Email);
                        var addRoleResult = await this.userManager.AddToRoleAsync(currentUser, resourceRoleName);

                        if (!addRoleResult.Succeeded)
                        {
                            throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                        }
                    }

                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return this.LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return this.Page();
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [RegularExpression("[A-Z][a-z]+", ErrorMessage = "{0} should contain only letters starting with a capital case")]
            [Display(Name = "First Name")]
            [DataType(DataType.Text)]
            public string FirstName { get; set; }

            [Required]
            [RegularExpression("[A-Z][a-z]+", ErrorMessage = "{0} should contain only letters starting with a capital case")]
            [Display(Name = "Last Name")]
            [DataType(DataType.Text)]
            public string LastName { get; set; }

            [Display(Name = "Phone Number")]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }

            [Display(Name = "Image Url")]
            [DataType(DataType.Text)]
            public string ImageUrl { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
    }
}
