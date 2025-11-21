namespace BlazorBlog.Services;

public class UIService
{
    public event Action? OnLoginModalRequested;
    public event Action? OnRegisterModalRequested;

    public void ShowLoginModal()
    {
        OnLoginModalRequested?.Invoke();
    }

    public void ShowRegisterModal()
    {
        OnRegisterModalRequested?.Invoke();
    }
}
