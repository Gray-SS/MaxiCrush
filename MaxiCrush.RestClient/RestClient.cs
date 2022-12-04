using FluentValidation.Results;
using MaxiCrush.Contracts;
using MaxiCrush.Contracts.Authentication;
using MaxiCrush.Contracts.Authentication.Confirmation;
using MaxiCrush.Contracts.Authentication.Login;
using MaxiCrush.Contracts.Authentication.Register;
using MaxiCrush.Contracts.Authentication.VerifyConfirmation;
using MaxiCrush.Contracts.Dto;
using MaxiCrush.Contracts.Permissions;
using MaxiCrush.Contracts.Roles;
using MaxiCrush.Contracts.Users;
using MaxiCrush.Rest.Exceptions;
using System.Net.Http.Headers;
using System.Security;

namespace MaxiCrush.Rest
{
    public class RestClient : RestClientBase
    {
        public string Token { get; set; }
        public UserDto User { get; private set; }

        public event Action? OnLoggedIn;

        public RestClient(HostType hostType) : base(hostType) { } 
        public RestClient(Uri baseAdress) : base(baseAdress) { }

        public async Task GetConfirmationCodeAsync(string email)
        {
            var requestUri = ApiRoutes.Authentication.Confirmation;

            await DoAsync(RestHttpRequestMethod.Post, requestUri, new ConfirmationRequest(email));
        }

        public async Task<bool> VerifyConfirmationCodeAsync(string email, string code)
        {
            var requestUri = ApiRoutes.Authentication.VerifyConfirmationF(email, code);

            var response = await DoAsync(RestHttpRequestMethod.Get, requestUri);

            var data = await response.GetDataAsync<VerifyConfirmationResponse>();
            return data.IsValid;
        }

        public async Task<UserDto> GetCurrentUserAsync()
        {
            var requestUri = ApiRoutes.Users.AuthorizedUser;

            var response = await DoAsync(RestHttpRequestMethod.Get, requestUri);

            var data = await response.GetDataAsync<UserDto>();
            return data;
        }

        public Task LogoutAsync()
        {
            Token = string.Empty;
            User = null;

            HttpClient.DefaultRequestHeaders.Authorization = null;

            return Task.CompletedTask;
        }

        public async Task LoginAsync(string email, string password)
        {
            var requestUri = ApiRoutes.Authentication.Login;

            var request = new LoginRequest(email, password);
            var response = await DoAsync(RestHttpRequestMethod.Post, requestUri, request);

            var data = await response.GetDataAsync<LoginResponse>();

            Token = data.Token;
            User = data.User;

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            OnLoggedIn?.Invoke();
        }

        public async Task<UserDto> GetUserAsync(Guid id)
        {
            var requestUri = ApiRoutes.Users.GetUserF(id);

            var response = await DoAsync(RestHttpRequestMethod.Get, requestUri);

            var data = await response.GetDataAsync<UserDto>();
            return data;
        }

        public async Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync()
        {
            var requestUri = ApiRoutes.Permissions.GetAllPermissions;

            var response = await DoAsync(RestHttpRequestMethod.Get, requestUri);

            var data = await response.GetDataAsync<IEnumerable<PermissionDto>>();
            return data;
        }
    
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var requestUri = ApiRoutes.Users.GetAllUser;

            var response = await DoAsync(RestHttpRequestMethod.Get, requestUri);

            var data = await response.GetDataAsync<IEnumerable<UserDto>>();
            return data;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            var requestUri = ApiRoutes.Roles.GetAllRoles;

            var response = await DoAsync(RestHttpRequestMethod.Get, requestUri);

            var data = await response.GetDataAsync<IEnumerable<RoleDto>>();
            return data;
        }

        public async Task<RoleDto> UpdateRoleAsync(Guid roleId, string? name = null, int? power = null, Guid[]? permissionIds = null)
        {
            var requestUri = ApiRoutes.Roles.UpdateF(roleId);

            var response = await DoAsync(RestHttpRequestMethod.Patch, requestUri, new UpdateRoleRequest(name, power, permissionIds));

            var data = await response.GetDataAsync<RoleDto>();
            return data;
        }

        public async Task<UserDto> UpdateUserAsync(Guid userId, UpdateUserRequest request)
        {
            var requestUri = ApiRoutes.Users.UpdateUserF(userId);

            var response = await DoAsync(RestHttpRequestMethod.Patch, requestUri, request);

            var data = await response.GetDataAsync<UserDto>();
            return data;
        }

        public async Task<UserDto> SetRoleAsync(Guid userId, string roleName)
        {
            var requestUri = ApiRoutes.Users.SetRoleF(userId);

            var response = await DoAsync(RestHttpRequestMethod.Post, requestUri, roleName);

            var data = await response.GetDataAsync<UserDto>();
            return data;
        }

        public async Task<PermissionDto> CreatePermissionAsync(string name)
        {
            var requestUri = ApiRoutes.Permissions.Create;

            var response = await DoAsync(RestHttpRequestMethod.Post, requestUri, new CreatePermissionRequest(name));

            var data = await response.GetDataAsync<PermissionDto>();
            return data;
        }

        public async Task<RoleDto> CreateRoleAsync(string name, int power)
        {
            var requestUri = ApiRoutes.Roles.Create;

            var response = await DoAsync(RestHttpRequestMethod.Post, requestUri, new CreateRoleRequest(name, power));

            var data = await response.GetDataAsync<RoleDto>();
            return data;

        }

        public async Task DeleteRoleAsync(Guid id)
        {
            var requestUri = ApiRoutes.Roles.DeleteF(id);

            await DoAsync(RestHttpRequestMethod.Delete, requestUri);
        }

        public async Task DeletePermissionAsync(Guid id)
        {
            var requestUri = ApiRoutes.Permissions.DeleteF(id);

            await DoAsync(RestHttpRequestMethod.Delete, requestUri);
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var requestUri = ApiRoutes.Users.DeleteUserF(userId);

            await DoAsync(RestHttpRequestMethod.Delete, requestUri);
        }

        public async Task<UserDto> RegisterAsync(string firstname,
                                                 string lastname,
                                                 string email,
                                                 string password,
                                                 string gender,
                                                 string genderInterest,
                                                 DateTime birthday)
        {
            var requestUri = ApiRoutes.Authentication.Register;

            var response = await DoAsync(RestHttpRequestMethod.Post, requestUri, new RegisterRequest(firstname,
                                                                                                     lastname,
                                                                                                     email,
                                                                                                     password,
                                                                                                     string.Empty,
                                                                                                     gender,
                                                                                                     genderInterest,
                                                                                                     birthday));

            var data = await response.GetDataAsync<RegisterResponse>();
            return data.CreatedUser;
        }
    }
}
