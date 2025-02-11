using FluentValidation;
using FluentValidation.Results;
using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Exceptions;
using HotelBooking.BusinessLogic.Services.Implementation;
using HotelBooking.BusinessLogic.Validators;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace HotelBooking.Tests
{
    public class ValidationServiceTests
    {
        private readonly Mock<IValidator<RegistrationDto>> _validatorMock;
        private readonly ValidationService<RegistrationDto> _validationService;

        public ValidationServiceTests()
        {
            _validatorMock = new Mock<IValidator<RegistrationDto>>();
            _validationService = new ValidationService<RegistrationDto>(_validatorMock.Object);
        }

        [Fact]
        public async Task Validate_WhenValidRegistrationDto()
        {
            // Arrange
            var registrationValidator = new RegistrationValidator();
            var validationService = new ValidationService<RegistrationDto>(registrationValidator);

            var dto = new RegistrationDto
            {
                Firstname = "Anna",
                Lastname = "Abc",
                Email = "john.abc@mail.com",
                PhoneNumber = "123456789",
                DateBirth = new DateOnly(2001, 1, 1),
                Password = "Password123!",
                PasswordConfirmation = "Password123!"
            };

            // Act & Assert
            await validationService.Validate(dto);
        }

        [Theory]
        [InlineData("", "Xyz", "invalid-email", "123", "2015-01-01", "WeakPass", "Firstname is required, Email address is required., Required phone number format., Age of majority required., Password must contain at least one digit., Password must contain at least one special character.")]
        [InlineData("John", "", "", "123", "2015-01-01", "pass", "Lastname is required, Email address is required., Required phone number format., Age of majority required., Password must be at least 8 characters long., Password must contain at least one uppercase letter., Password must contain at least one digit., Password must contain at least one special character.")]
        [InlineData("John", "Xyz", "invalid@", "123456789012345", "2035-01-01", "p@ssw0rd", "Email address is required., Required phone number format., Age of majority required., Password must contain at least one uppercase letter.")]
        [InlineData("John", "Xyz", "valid@email.com", "", "2025-01-01", "weak^pass", "Required phone number format., Age of majority required., Password must contain at least one uppercase letter., Password must contain at least one digit.")]
        [InlineData("John", "Xyz", "valid@email.com", "123456789", "2015-01-01", "WeakPass", "Age of majority required., Password must contain at least one digit., Password must contain at least one special character.")]
        [InlineData("John", "Xyz", "valid@email.com", "123456789", "2000-01-01", "WeakPass", "Password must contain at least one digit., Password must contain at least one special character.")]
        [InlineData("John", "Xyz", "@email.com", "123456789", "2000-01-01", "WeakPass123!", "Email address is required.")]
        [InlineData("John", "Xyz", "valid@email.com", "123456789", "2000-01-01", "WeakPassA!", "Password must contain at least one digit.")]
        [InlineData("", "", "invalid", "12", "2020-01-01", "", "Firstname is required, Lastname is required, Email address is required., Required phone number format., Age of majority required., Password is required., Password must be at least 8 characters long., Password must contain at least one uppercase letter., Password must contain at least one digit., Password must contain at least one special character.")]
        public async Task Validate_WhenInvalidDto_ThrowsBadRequestExceptionWithCorrectMessage(
            string firstname,
            string lastname,
            string email,
            string phoneNumber,
            string dateBirthString,
            string password,
            string expectedErrorMessages)
        {
            // Arrange
            var registrationValidator = new RegistrationValidator();
            var validationService = new ValidationService<RegistrationDto>(registrationValidator);

            var dateBirth = DateOnly.Parse(dateBirthString);
            var dto = new RegistrationDto
            {
                Firstname = firstname,
                Lastname = lastname,
                Email = email,
                PhoneNumber = phoneNumber,
                DateBirth = dateBirth,
                Password = password,
                PasswordConfirmation = password
            };

            var errors = new List<ValidationFailure>();
            string[] expectedErrorMessagesArray = expectedErrorMessages.Split(", ");

            foreach (var errorMessage in expectedErrorMessagesArray)
            {
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    errors.Add(new ValidationFailure("PropertyName", errorMessage)); // "PropertyName" nie ma znaczenia w tym przypadku
                }
            }

            _validatorMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(new ValidationResult(errors));

            // Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => validationService.Validate(dto));

            // Assert
            Assert.Equal("Invalid fields: " + expectedErrorMessages, exception.Message);
        }
    }
}
