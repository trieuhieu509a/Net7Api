using TodoList.Models;

namespace TodoListBlazorWasm.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest login);
        Task Logout();
    }
}
